using UnityEngine;

namespace chibi.path
{
	[System.Serializable]
	public class Segment_aligment : Segment
	{
		public Segment_aligment( Transform center, Transform container )
			: base( center, container )
		{
		}

		public Segment_aligment( Segment segment, Transform p3 )
			: base( segment, p3 )
		{
		}

		public Segment_aligment( Segment segment, Vector3 vp3 )
			: base( segment, vp3 )
		{
		}

		public Segment_aligment( Segment segment )
			: base( segment )
		{
		}

		public Segment_aligment(
			Transform p1, Vector3 c1, Vector3 c2, Transform p2, Transform container )
			: base( p1, c1, c2, p2, container )
		{
		}

		public Segment_aligment(
			Transform p1, Vector3 c1, Vector3 c2, Vector3 p2, Transform container )
			: base( p1, c1, c2, p2, container )
		{
		}

		public Segment_aligment(
			Transform p1, Transform c1, Transform c2, Transform p2, Transform container )
			: base( p1, c1, c2, p2, container )
		{
		}

		public override Vector3 vp1
		{
			get {
				return base.vp1;
			}

			set {
				Vector3 delta_move = value - vp1;
				if ( previous != null )
				{
					previous.c2.position += delta_move;
				}
				c1.position += delta_move;
				base.vp1 = value;
			}
		}

		public override Vector3 vp2
		{
			get {
				return base.vp2;
			}

			set {
				Vector3 delta_move = value - vp2;
				if ( next != null )
				{
					next.c1.position += delta_move;
				}
				c2.position += delta_move;
				base.vp2 = value;
			}
		}

		public override Vector3 vc1
		{
			get {
				return base.vc1;
			}

			set {
				base.vc1 = value;
				if ( previous != null )
				{
					Vector3 direction = ( vp1 - value ).normalized;
					float distance = Vector3.Distance( previous.vc2, vp1 );
					previous.c2.position = vp1 + direction * distance;
				}
			}
		}

		public override Vector3 vc2
		{
			get {
				return base.vc2;
			}

			set {
				base.vc2 = value;
				if ( next != null )
				{
					//float distance = Vector3.Distance( next.vc1, vp2 );
					//next.c1.position = direction * distance;

					Vector3 direction = ( vp2 - value ).normalized;
					float distance = Vector3.Distance( next.vc1, vp2 );
					next.c1.position = vp2 + direction * distance;
				}
			}
		}
	}
}
