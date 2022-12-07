using UnityEngine;

namespace helper
{
	class vector3 {
		public static Vector3 clamp( Vector3 vector, Vector3 min, Vector3 max ){
			return new Vector3(
					Mathf.Clamp( vector.x, min.x, max.x ),
					Mathf.Clamp( vector.y, min.y, max.y ),
					Mathf.Clamp( vector.z, min.z, max.z )
				);
		}

		public static Vector3 normalize_speed( Vector3 v )
		{
			if ( v.magnitude > 1 )
				v = v.normalized;
			return v;
		}

		public static Vector3 normalize_speed( Vector3 v, float mult_speed )
		{
			if ( v.magnitude > 1 )
				v = v.normalized;
			return v * mult_speed;
		}

		public static void set( ref Vector3 o, Vector3 i )
		{
			o.x = i.x;
			o.y = i.y;
			o.z = i.z;
		}

		public static Vector3 smooth_damp(
			Vector3 v, Vector3 d, float time, ref float smooth_x,
			ref float smooth_y, ref float smooth_z )
		{
			float x = Mathf.SmoothDamp( v.x, d.x, ref smooth_x, time );
			float y = Mathf.SmoothDamp( v.y, d.y, ref smooth_y, time );
			float z = Mathf.SmoothDamp( v.z, d.z, ref smooth_z, time );

			return new Vector3( x, y, z );
		}
	}
}
