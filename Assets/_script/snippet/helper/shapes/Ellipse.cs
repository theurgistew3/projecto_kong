using UnityEngine;
using System;

namespace helper.shapes
{
	[Serializable]
	public static class Ellipse
	{
		public static Vector3 evaluate(
			float x_axis, float y_axis, float t )
		{
			float angle = Mathf.Deg2Rad * 360 * t;
			float x = Mathf.Sin( angle ) * x_axis;
			float y = Mathf.Cos( angle ) * y_axis;
			return new Vector3( x, 0, y );
		}

		public static float get_progrest( Vector3 direction )
		{
			var angle =
				Vector3.SignedAngle(
					Vector3.forward, direction.normalized, Vector3.up );
			if ( angle < 0 )
				angle += 360;
			return angle / 360;
		}

		public static float perimeter( float r1, float r2 )
		{
			if ( r1 == r2 )
				return r1 * 2 * Mathf.PI;
			else if( r1 > r2 )
				return 3 * ( r1 + r2 )
					- Mathf.Sqrt( ( 3*r1 + r2 ) * ( r1 + 3*r2 ) );
			else
				return perimeter( r2, r1 );
		}
	}
}
