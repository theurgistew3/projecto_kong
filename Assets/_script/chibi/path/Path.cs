using System.Collections.Generic;
using UnityEngine;

namespace chibi.path.old
{
	[System.Serializable]
	public class Path
	{
		public List<Transform> points;
		public Transform container;
		public bool _is_close = false;
		protected bool _auto_set_controll_points = false;

		public Path( Transform center )
		{
			container = center;
			points = new List<Transform> {
				new_point( center.position + Vector3.left ),
				new_point( center.position + Vector3.left + Vector3.forward * 0.5f ),
				new_point( center.position + Vector3.right + Vector3.back * 0.5f ),
				new_point( center.position + Vector3.right )
			};
			rename_points();
		}

		protected Transform new_point( Vector3 position )
		{
			var game_object = new GameObject();
			game_object.transform.position = position;
			game_object.transform.parent = container;
			return game_object.transform;
		}

		protected void rename_points()
		{
			for ( int i = 0; i < count; ++i )
				points[ i ].name = string.Format( "p{0}", i );
			List<Transform> to_delete = new List<Transform>();

			for ( int i = container.childCount - 1; i > 0; --i )
			{
				var child = container.GetChild( i );
				if ( points.Find( ( t ) => t == child ) == null )
				{
					GameObject.DestroyImmediate( child.gameObject );
				}
			}
		}

		public bool auto_set_control_points
		{
			get {
				return _auto_set_controll_points;
			}
			set {
				if ( auto_set_control_points != value )
				{
					_auto_set_controll_points = value;
					if ( auto_set_control_points )
						auto_set_all_control_points();
				}
			}
		}

		public bool is_close
		{
			get { return _is_close; }
			set {
				if ( is_close != value )
				{
					_is_close = value;
					if ( is_close )
					{
						var p3 = this[ count - 1 ];
						var p2 = this[ count - 2 ];

						var p4 = p3 * 2 - p2;
						var p5 = this[ 0 ] * 2 - this[ 1 ];

						points.Add( new_point( p4 ) );
						points.Add( new_point( p5 ) );

						if ( auto_set_control_points )
						{
							auto_set_anchor_control_points( 0 );
							auto_set_anchor_control_points( count - 3 );
						}
					}
					else
					{
						points.RemoveRange( points.Count - 2, 2 );
						if ( auto_set_control_points )
							auto_set_start_and_End_controls();
					}
					rename_points();
				}
			}
		}

		public Vector3 this[ int i ]
		{
			get {
				return points[ i ].position;
			}
		}

		public int count
		{
			get {
				return points.Count;
			}
		}

		public int segment_count
		{
			get {
				return count / 3;
			}
		}

		public void add_segment( Vector3 anchor_position )
		{
			// count == 4
			// p3 == 3 el anchor 2
			// p2 == 2 control 2

			var p3 = this[ count - 1 ];
			var p2 = this[ count - 2 ];

			var p4 = p3 * 2 - p2;
			var p5 = ( p4 + anchor_position ) * 0.5f;

			points.Add( new_point( p4 ) );
			points.Add( new_point( p5 ) );
			points.Add( new_point( anchor_position ) );
			if ( auto_set_control_points )
				auto_set_all_affected_control_points( count - 1 );
			rename_points();
		}

		public Vector3[] get_points_in_segment( int i )
		{
			int start = i * 3;
			return new Vector3[] {
				this[ start ], this[ start + 1 ], this[ start + 2 ],
				this[ loop_index( start + 3 ) ] };
		}

		public bool is_anchor( int i )
		{
			return i % 3 == 0;
		}

		public void move_point( int i, Vector3 position )
		{
			Vector3 delta_move = position - this[ i ];
			if ( is_anchor( i ) || !auto_set_control_points )
			{
				points[ i ].position = position;

				if ( auto_set_control_points )
				{
					auto_set_all_affected_control_points( i );
				}
				else
				{
					if ( is_anchor( i ) )
					{
						if ( i + 1 < count || is_close )
							points[ loop_index( i + 1 ) ].position += delta_move;
						if ( i - 1 >= 0 || is_close )
							points[ loop_index( i - 1 ) ].position += delta_move;
					}
					else
					{
						bool next_point_is_a_anchor = ( i + 1 ) % 3 == 0;
						int control_index = loop_index( next_point_is_a_anchor ? i + 2 : i - 2 );
						int anchor_index = loop_index( next_point_is_a_anchor ? i + 1 : i - 1 );

						if ( control_index >= 0 && control_index < count || is_close )
						{
							float distance = (
								this[ anchor_index ] - this[ control_index ] ).magnitude;
							Vector3 direction = ( this[ anchor_index ] - position ).normalized;
							points[ control_index ].position =
								this[ anchor_index ] + direction * distance;
						}
					}
				}
			}
		}


		protected void auto_set_all_affected_control_points( int updated_anchor_index )
		{
			for ( int i = updated_anchor_index - 3; i <= updated_anchor_index; i += 3 )
			{
				if ( i >= 0 && i < count || is_close )
					auto_set_anchor_control_points( loop_index( i ) );
			}
			auto_set_start_and_End_controls();
		}

		protected void auto_set_all_control_points()
		{
			for ( int i = 0; i < count; i += 3 )
			{
				auto_set_anchor_control_points( i );
			}
			auto_set_start_and_End_controls();
		}

		protected void auto_set_anchor_control_points( int anchor_index )
		{
			Vector3 anchor_position = this[ anchor_index ];
			Vector3 direction = Vector3.zero;

			float[] neighbour_distance = new float[ 2 ];

			if ( anchor_index - 3 >= 0 || is_close )
			{
				Vector3 offset = this[ loop_index( anchor_index - 3 ) ] - anchor_position;
				direction += offset.normalized;
				neighbour_distance[ 0 ] = offset.magnitude;
			}
			if ( anchor_index + 3 >= 0 || is_close )
			{
				Vector3 offset = this[ loop_index( anchor_index + 3 ) ] - anchor_position;
				direction -= offset.normalized;
				neighbour_distance[ 1 ] = -offset.magnitude;
			}

			for ( int i = 0; i < 2; i++ )
			{
				int control_index = anchor_index + i * 2 - 1;
				if ( control_index >= 0 && control_index < count || is_close )
					points[ loop_index( control_index ) ].position =
						anchor_position + direction * neighbour_distance[ i ] * 0.5f;
			}
		}

		public void delete_segment( int anchor_index )
		{
			if ( segment_count > 2 || !is_close && segment_count > 1 )
			{
				if ( anchor_index == 0 )
				{
					if ( is_close )
						points[ count - 1 ] = points[ 2 ];
					points.RemoveRange( 0, 3 );
				}
				else if ( anchor_index == count - 1 && !is_close )
					points.RemoveRange( anchor_index - 2, 3 );
				else
					points.RemoveRange( anchor_index - 1, 3 );
			}
		}

		protected void auto_set_start_and_End_controls()
		{
			if ( !is_close )
			{
				points[ 1 ].position = ( this[ 0 ] + this[ 2 ] ) * 0.5f;
				points[ count - 2 ].position =
					( this[ count - 1 ] + this[ count - 3 ] ) * 0.5f;
			}
		}

		protected int loop_index( int i )
		{
			return ( i + count ) % count;
		}
	}
}
