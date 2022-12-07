using UnityEngine;


namespace helper
{
	namespace draw
	{
		public class cube
		{
			public static void debug(
				Vector3 position, Vector3 size, Color color, float duration = 0f )
			{
				var extents = size * 0.5f;
				float x = extents.x;
				float y = extents.y;
				float z = extents.z;

				Vector3 ruf = position + new Vector3( x, y, z );
				Vector3 rub = position + new Vector3( x, y, -z );
				Vector3 luf = position + new Vector3( -x, y, z );
				Vector3 lub = position + new Vector3( -x, y, -z );

				Vector3 rdf = position + new Vector3( x, -y, z );
				Vector3 rdb = position + new Vector3( x, -y, -z );
				Vector3 lfd = position + new Vector3( -x, -y, z );
				Vector3 lbd = position + new Vector3( -x, -y, -z );

				Debug.DrawLine( ruf, luf, color, duration );
				Debug.DrawLine( ruf, rub, color, duration );
				Debug.DrawLine( luf, lub, color, duration );
				Debug.DrawLine( rub, lub, color, duration );

				Debug.DrawLine( ruf, rdf, color, duration );
				Debug.DrawLine( rub, rdb, color, duration );
				Debug.DrawLine( luf, lfd, color, duration );
				Debug.DrawLine( lub, lbd, color, duration );

				Debug.DrawLine( rdf, lfd, color, duration );
				Debug.DrawLine( rdf, rdb, color, duration );
				Debug.DrawLine( lfd, lbd, color, duration );
				Debug.DrawLine( lbd, rdb, color, duration );
			}
		}
	}
}