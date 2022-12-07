using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;

namespace tactic.board
{
	public class Board : chibi.Chibi_behaviour
	{
		public bool lock_bake = false;
		public BoxCollider box;
		public float cell_size = 0.5f;
		public Cell proto_cell;

		[SerializeField]
		public List<Column> matriz;

		public float select_row = 0f;
		public float select_column = 0f;

		public Vector3 min_corner_1
		{
			get {
				return box.bounds.min;
			}
		}

		public Vector3 min_corner_2
		{
			get {
				var min = min_corner_1;
				return new Vector3( min.x, min.y, max_corner_3.z );
			}
		}

		public Vector3 min_corner_3
		{
			get {
				var min = min_corner_1;
				var max = max_corner_3;
				return new Vector3( max.x, min.y, max.z );
			}
		}

		public Vector3 min_corner_4
		{
			get {
				var min = min_corner_1;
				var max = max_corner_3;
				return new Vector3( max.x, min.y, min.z );
			}
		}

		public Vector3 max_corner_1
		{
			get {
				var min = min_corner_1;
				var max = max_corner_3;
				return new Vector3( min.x, max.y, min.z );
			}
		}
		public Vector3 max_corner_2
		{
			get {
				var min = min_corner_1;
				var max = max_corner_3;
				return new Vector3( min.x, max.y, max.z );
			}
		}

		public Vector3 max_corner_3
		{
			get {
				return box.bounds.max;
			}
		}

		public Vector3 max_corner_4
		{
			get {
				var min = min_corner_1;
				var max = max_corner_3;
				return new Vector3( max.x, max.y, min.z );
			}
		}

		public float width
		{
			get {
				return max_corner_3.x - min_corner_1.x;
			}
		}

		public float height
		{
			get {
				return max_corner_3.z - min_corner_1.z;
			}
		}

		public int count_columns
		{
			get {
				return Mathf.FloorToInt( width / cell_size );
			}
		}

		public int count_rows
		{
			get {
				return Mathf.FloorToInt( height / cell_size );
			}
		}

		public IEnumerable<Cell> cells
		{
			get {
				foreach ( var column in matriz )
				{
					foreach ( var cell in column )
						yield return cell;
				}
			}
		}

		public IEnumerable<Vector3> cells_centers
		{
			get {
				var center = this.init_vector;
				center.z += cell_size / 2;
				center.x += cell_size / 2;
				float init_x = center.x;
				//yield return center;
				for ( int i = 0; i < count_columns; ++i )
				{
					for ( int j = 0; j < count_rows; ++j )
					{
						yield return center;
						center.x += cell_size;
					}
					center.x = init_x;
					center.z += cell_size;
				}
			}
		}

		public Vector3 calculate_center_cell( int column, int row )
		{
			var center = this.init_vector;
			center.z += ( cell_size / 2 ) + ( cell_size * row );
			center.x += ( cell_size / 2 ) + ( cell_size * column );
			return center;
		}

		public Vector3 this[ int column, int row ]
		{
			get {
				return calculate_center_cell( column, row );
			}
		}

		public Vector3 this[ Vector3 select_vector ]
		{
			get {
				var init_vector = this.init_vector;
				helper.draw.sphere.gizmo( select_vector, 0.1f );
				select_vector = select_vector - init_vector;
				int column = Mathf.FloorToInt( select_vector.x / cell_size );
				int row = Mathf.FloorToInt( select_vector.z / cell_size );
				return this[ column, row ];
			}
		}

		public Vector3 init_vector
		{
			get {
				return max_corner_1;
			}
		}

		public Vector3 first_cell_center
		{
			get {
				var init_vector = this.init_vector;
				return new Vector3(
					init_vector.x + cell_size / 2,
					init_vector.y,
					init_vector.z + cell_size / 2 );
			}
		}

		public void bake()
		{
			if ( lock_bake )
				return;
			if ( !proto_cell )
			{
				debug.error( "no tiene una proto cell" );
				return;
			}
			debug.log( "--- iniciando bake ---" );
			clear_cells();
			preprare_matrix();
			for ( int i = 0; i < count_columns; ++i )
			{
				for ( int j = 0; j < count_rows; ++j )
				{
					string cell_name = string.Format( "cell {0}, {1}", i, j );
					Cell new_cell = helper.instantiate.parent<Cell>(
						proto_cell, this.gameObject, cell_name );
					new_cell.size = cell_size;
					new_cell.center = calculate_center_cell( i, j );
					matriz[ i ][ j ] = new_cell;
				}
			}
			debug.log( "--- terminando bake ---" );
		}

		public void recovery()
		{
			if ( lock_bake )
				return;
			debug.log( "--- iniciando recuperacion ---" );
			preprare_matrix();
			var find_refex = new Regex( @"cell (?<column>\d+), (?<row>\d+)" );
			for ( int i = 0; i < transform.childCount; ++i )
			{
				var child = transform.GetChild( i );
				var match = find_refex.Match( child.name );
				if ( !match.Success )
					continue;

				int column = int.Parse( match.Groups[ "column" ].Value );
				int row = int.Parse( match.Groups[ "row" ].Value );
				debug.log( "se encontro la celda {0}, {1}", column, row );
				var cell = child.GetComponent<Cell>();
				matriz[ column ][ row ] = cell;
			}
			debug.log( "--- terminando la recuperacion ---" );
		}

		public void clear_cells()
		{
			debug.log( "--- iniciando limpiesa de childs ---" );
			while ( transform.childCount > 0 )
			{
				DestroyImmediate( transform.GetChild( 0 ).gameObject );
			}
			debug.log( "--- terminando limpesa de childs ---" );
		}

		protected void preprare_matrix()
		{
			matriz = new List<Column>();
			for ( int i = 0; i < this.count_columns; ++i )
			{
				var column = new Column();
				column.cells = new List<Cell>( count_rows );
				column.cells.AddRange( Enumerable.Repeat<Cell>( null, count_rows ) );
				matriz.Add( column );
			}
		}

		protected virtual void OnDrawGizmos()
		{
			if ( !box )
				return;

			foreach ( var cell in cells )
			{
				cell.gizmo();
			}


			/*
			gizmo_corners();

			var old_color = Gizmos.color;
			Gizmos.color = Color.black;
			gizmo_center_cells();
			Gizmos.color = old_color;


			old_color = Gizmos.color;
			Gizmos.color = Color.green;
			gizmo_grid();
			Gizmos.color = old_color;

			old_color = Gizmos.color;
			Gizmos.color = Color.yellow;
			var vector = new Vector3(
					init_vector.x + select_column,
					init_vector.y,
					init_vector.z + select_row );
			helper.draw.sphere.gizmo( this[ vector ], Color.magenta, 0.1f );
			Gizmos.color = old_color;
			*/
		}

		public void gizmo_corners()
		{
			helper.draw.arrow.gizmo( box.bounds.center, Vector3.up, Color.black );
			helper.draw.arrow.gizmo( min_corner_1, Vector3.down, Color.red );
			helper.draw.arrow.gizmo( min_corner_2, Vector3.down, Color.magenta );
			helper.draw.arrow.gizmo( min_corner_3, Vector3.down, Color.cyan );
			helper.draw.arrow.gizmo( min_corner_4, Vector3.down, Color.blue );

			helper.draw.arrow.gizmo( max_corner_1, Vector3.up, Color.red );
			helper.draw.arrow.gizmo( max_corner_2, Vector3.up, Color.magenta );
			helper.draw.arrow.gizmo( max_corner_3, Vector3.up, Color.cyan );
			helper.draw.arrow.gizmo( max_corner_4, Vector3.up, Color.blue );
		}

		public void gizmo_center_cells()
		{
			foreach ( var center in cells_centers )
			{
				helper.draw.sphere.gizmo( center, 0.1f );
			}
		}

		public void gizmo_grid()
		{
			Vector3 init = init_vector;
			Vector3 to;
			float init_x = init_vector.x;
			for ( int i = 0; i < count_columns; ++i )
			{
				for ( int j = 0; j < count_rows; ++j )
				{
					to = new Vector3( init.x, init.y, init.z + cell_size );
					Gizmos.DrawLine( init, to );
					to = new Vector3( init.x + cell_size, init.y, init.z );
					Gizmos.DrawLine( init, to );

					init.x += cell_size;
				}
				init.x = init_x;
				init.z += cell_size;
			}

			init = max_corner_4;
			to = new Vector3( init.x, init.y, init.z + height );
			Gizmos.DrawLine( init, to );
			init = max_corner_2;
			to = new Vector3( init.x + width, init.y, init.z );
			Gizmos.DrawLine( init, to );
		}

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !box )
				box = GetComponent<BoxCollider>();
			if ( !box )
				debug.error( "no se encontro un box collider" );
		}

	}
}
