using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;

namespace tactic.grid.obj
{
	[System.Serializable]
	public class Grid<T>
	{
		public int width;
		public int height;

		public float size;

		public T[,] grid;
		public Vector3 origin;
		public TextMesh[,] debug_text;

		public Grid( int width, int height, float size, Vector3 origin )
		{
			this.width = width;
			this.height = height;
			this.size = size;
			this.origin = origin;

			grid = new T[ width, height ];
			debug_text = new TextMesh[ width, height ];

			var rotation = Quaternion.Euler( 90f, 0, 0 );
			var position_w = get_world_position( width, 0 );
			var position_h = get_world_position( 0, height );
			var position_x_1 = get_world_position( width, height );
			var position_y_1 = get_world_position( width, height );
			// lineas vertical final
			Debug.DrawLine( position_w, position_x_1, Color.white, 100f );
			// linea horizontal final
			Debug.DrawLine( position_h, position_y_1, Color.white, 100f );
			for ( int x = 0; x < width; ++x )
				for ( int y = 0; y < width; ++y )
				{
					debug_text[ x, y ] = helper.text._(
						build_debug_text( x, y ), null,
						get_world_position( x, y ) + new Vector3( size, 0, size ) * 0.5f,
						rotation );
					var position = get_world_position( x, y );
					position_x_1 = get_world_position( x + 1, y );
					position_y_1 = get_world_position( x, y + 1 );
					Debug.DrawLine( position, position_x_1, Color.white, 100f );
					Debug.DrawLine( position, position_y_1, Color.white, 100f );
					//Debug.Log( string.Format( "x: {0}, y: {1}", x, y ) );
				}
		}

		public string build_debug_text( int x, int y )
		{
			return string.Format( "{0}, {1} = {2}", x, y, grid[ x, y ] );
		}

		public Vector3 get_world_position( int x, int y )
		{
			return new Vector3( x, 0, y ) * size + origin;
		}

		public void get_x_y_from_world( Vector3 vector, out int x, out int y )
		{
			x = Mathf.FloorToInt( ( vector.x - origin.x ) / size );
			y = Mathf.FloorToInt( ( vector.z - origin.z ) / size );
		}

		public T this[ int x, int y ]
		{
			get {
				return grid[ x, y ];
			}
			set {
				grid[ x, y ] = value;
				debug_text[ x, y ].text = value.ToString();
			}
		}

		public T this[ Vector3 vector ]
		{
			get {
				int x, y;
				get_x_y_from_world( vector, out x, out y );
				return this[ x, y ];
			}
			set {
				int x, y;
				get_x_y_from_world( vector, out x, out y );
				this[ x, y ] = value;
			}
		}
	}
}
