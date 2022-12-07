using UnityEngine;

namespace chibi.path
{
	[System.Serializable]
	public class Segment
	{
		public Transform p1, p2, c1, c2;
		public Transform container;

		public Segment previous, next;

		public virtual Vector3 vp1
		{
			get {
				return p1.position;
			}
			set {
				p1.position = value;
			}
		}

		public virtual Vector3 vp2
		{
			get {
				return p2.position;
			}
			set {
				p2.position = value;
			}
		}

		public virtual Vector3 vc1
		{
			get {
				return c1.position;
			}
			set {
				c1.position = value;
			}
		}

		public virtual Vector3 vc2
		{
			get {
				return c2.position;
			}
			set {
				c2.position = value;
			}
		}

		public float net_distance
		{
			get {
				return
					Vector3.Distance( vp1, vc1 ) + Vector3.Distance( vc2, vc2 )
					+ Vector3.Distance( vc2, vp2 );
			}
		}

		public float estimate_distance
		{
			get {
				return
					Vector3.Distance( vp1, vp2 ) + ( net_distance / 2 );
			}
		}

		public Segment( Transform center, Transform container )
		{
			this.container = container;
			p1 = create_point( center.position + Vector3.left );
			c1 = create_point( center.position + Vector3.forward * 0.5f );
			c2 = create_point(
				center.position + Vector3.right + Vector3.back * 0.5f );
			p2 = create_point( center.position + Vector3.right );
		}

		public Segment(
			Transform p1, Vector3 c1, Vector3 c2, Transform p2,
			Transform container )
		{
			this.container = container;
			this.p1 = p1;
			this.p2 = p2;
			this.c1 = create_point( c1 );
			this.c2 = create_point( c2 );
		}

		public Segment(
			Transform p1, Vector3 c1, Vector3 c2, Vector3 p2,
			Transform container )
		{
			this.container = container;
			this.p1 = p1;
			this.p2 = create_point( p2 );
			this.c1 = create_point( c1 );
			this.c2 = create_point( c2 );
		}

		public Segment(
			Transform p1, Transform c1, Transform c2, Transform p2,
			Transform container )
		{
			this.container = container;
			this.p1 = p1;
			this.p2 = p2;
			this.c1 = c1;
			this.c2 = c2;
		}

		public Segment( Segment segment ) : this(
			segment.p1, segment.c1, segment.c2, segment.p2,
			segment.container )
		{
		}

		public Segment( Segment segment, Transform p3 ) : this(
			  segment.p2,
			  segment.vp2 * 2 - segment.vc2,
			  ( segment.vc1 + p3.position ) * 0.5f,
			  p3, segment.container )
		{
		}

		public Segment( Segment segment, Vector3 vp3 ) : this(
			  segment.p2,
			  segment.vp2 * 2 - segment.vc2,
			  ( segment.vc1 + vp3 ) * 0.5f,
			  vp3, segment.container )
		{
		}

		protected Transform create_point( Vector3 position )
		{
			var game_object = new GameObject();
			game_object.transform.position = position;
			game_object.transform.parent = container;
			return game_object.transform;
		}

		public Vector3 evaluate( float t )
		{
			return helper.shapes.Bezier.evaluate( vp1, vc1, vc2, vp2, t );
		}

		public void draw_gizmo()
		{
			// UnityEditor.Handles.color = Color.black;
			// UnityEditor.Handles.DrawLine( vc1, vp1 );
			// UnityEditor.Handles.DrawLine( vc2, vp2 );
			// UnityEditor.Handles.DrawBezier( vp1, vp2, vc1, vc2, Color.green, null, 2 );

			Gizmos.color = Color.red;
			Gizmos.DrawSphere( vp1, 0.1f );
			Gizmos.DrawSphere( vp2, 0.1f );
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere( vc1, 0.1f );
			Gizmos.DrawSphere( vc2, 0.1f );
		}
	}
}
