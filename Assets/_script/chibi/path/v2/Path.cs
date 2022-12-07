using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace chibi.path
{
	public enum path_types
	{
		free, aligment
	}

	[System.Serializable]
	public class Path
	{
		public float resolution = 10f, spacing = 1;
		public List<Vector3> bake_points;
		public List<Segment> segments;
		public Transform container;
		protected bool _is_close = false;

		protected path_types _type;

		public bool is_close
		{
			get
			{
				return _is_close;
			}
			set
			{
				if ( is_close == value )
					return;

				_is_close = value;
				if ( is_close )
				{
					var segment_last = segments.Last();
					var segment_first = segments.First();
					Segment new_segment = null;
					Vector3 direction = Vector3.left;

					if ( type == path_types.free )
					{
						Vector3 c1 = - ( segment_last.vc2 - segment_last.vp2 );
						Vector3 c2 = - ( segment_first.vc1 - segment_first.vp1 );
						new_segment = new Segment(
							segment_last.p2, c1, c2, segment_first.p1,
							segment_first.container );
					}
					else if ( type == path_types.aligment )
					{
						Vector3 c1 = - ( segment_last.vc2 - segment_last.vp2 );
						Vector3 c2 = - ( segment_first.vc1 - segment_first.vp1 );
						new_segment = new Segment_aligment(
							segment_last.p2, c1, c2, segment_first.p1,
							segment_first.container );
					}
					segments.Add( new_segment );
					relink();
					rename_points();
				}
				else
				{
					segments.RemoveAt( segments.Count - 1 );
				}
			}
		}

		public path_types type
		{
			get { return _type; }
			set {
				if ( type != value )
				{
					_type = value;
					if ( type == path_types.free )
						change_to_free();
					if ( type == path_types.aligment)
						change_to_aligment();
				}
			}
		}

		public Path( Path path )
		{
			resolution = path.resolution;
			spacing = path.spacing;
			segments = path.segments;
			container = path.container;
			type = path.type;
			bake_points = path.bake_points;
		}

		public Path( Transform center )
		{
			container = center;
			segments = new List<Segment> { new Segment( center, container ) };
		}

		public Path( Transform center, Path path )
		{
			container = center;
			segments = new List<Segment> { new Segment( center, container ) };
			type = path.type;
		}

		public void add_segment_relative( Vector3 direction )
		{
			var segment = segments.Last();
			Segment new_segment = null;
			if ( type == path_types.free )
				new_segment = new Segment(
					segment, segment.p2.position + direction );
			else if ( type == path_types.aligment )
				new_segment = new Segment_aligment(
					segment, segment.p2.position + direction );
			segments.Add( new_segment );
			relink();
			rename_points();
		}

		public Vector3 evaluate( float t )
		{
			if( t == 1 )
				return segments.Last().vp2;
			float persentil_fraction = 1 / segments.Count;
			int segment_index = Mathf.FloorToInt(
				segments.Count * t );
			float t_for_segment = t - persentil_fraction * segment_index;
			Segment segment = segments[ segment_index ];
			
			return segment.evaluate( t_for_segment );
		}

		public List<Vector3> get_eveling_space_points( float spacing )
		{
			Vector3 last_point = segments[ 0 ].vp1;
			float distance_sinse_last_point = 0f;
			var result = new List<Vector3> { last_point };

			foreach ( var segment in segments )
			{
				int divisions = Mathf.CeilToInt(
					segment.estimate_distance * resolution );
				float t = 0;
				float t_step = 1f / divisions;
				while ( t <= 1 )
				{
					t += t_step;
					var new_point = segment.evaluate( t );
					distance_sinse_last_point += Vector3.Distance( last_point, new_point );

					while ( distance_sinse_last_point >= spacing )
					{
						float over_distance = distance_sinse_last_point - spacing;
						Vector3 new_eveling_space_point =
							new_point + ( last_point - new_point ).normalized
							* over_distance;
						result.Add( new_eveling_space_point );
						distance_sinse_last_point = over_distance;
						last_point = new_eveling_space_point;
					}
					last_point = new_point;
				}
			}
			return result;
		}

		public void bake()
		{
			rename_points();
			bake_points = get_eveling_space_points( spacing );
		}

		public IEnumerable<Transform> plain_points()
		{
			foreach ( Segment segment in segments )
			{
				yield return segment.p1;
				yield return segment.c1;
				yield return segment.c2;
			}
			yield return segments.Last().p2;
		}

		protected void change_to_free()
		{
			List<Segment> new_segmnets = new List<Segment>( segments.Count );
			foreach ( var segment in segments )
				new_segmnets.Add( new Segment( segment ) );
			segments = new_segmnets;
			relink();
			rename_points();
		}

		protected void change_to_aligment()
		{
			List<Segment> new_segmnets = new List<Segment>( segments.Count );
			foreach ( var segment in segments )
				new_segmnets.Add( new Segment_aligment( segment ) );
			segments = new_segmnets;
			relink();
			rename_points();
		}

		protected void relink()
		{
			for ( int i = 1; i < segments.Count; ++i )
			{
				segments[ i - 1 ].next = segments[i];
				segments[i].previous = segments[ i - 1 ];
			}
		}

		protected void rename_points()
		{
			List<Transform> points = new List<Transform>();
			for ( int i = 0; i < segments.Count; ++i )
			{
				segments[ i ].p1.name = string.Format( "{0}__p1", i );
				segments[ i ].p2.name = string.Format( "{0}__p2", i );
				segments[ i ].c1.name = string.Format( "{0}__c1", i );
				segments[ i ].c2.name = string.Format( "{0}__c2", i );
				points.Add( segments[ i ].p1 );
				points.Add( segments[ i ].p2 );
				points.Add( segments[ i ].c1 );
				points.Add( segments[ i ].c2 );
			}
			for ( int i = container.childCount - 1; i > 0; --i )
			{
				var child = container.GetChild( i );
				if ( points.Find( ( t ) => t == child ) == null )
				{
					GameObject.DestroyImmediate( child.gameObject );
				}
			}
		}
	}
}
