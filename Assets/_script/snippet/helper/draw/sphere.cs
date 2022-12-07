using UnityEngine;


namespace helper.draw
{
	public class sphere
	{
		public static void debug(
			Vector3 position, Color color, float radius = 1.0f,
			float duration = 0, bool depth_test = true )
		{
			float angle = 10.0f;

			Vector3 x = new Vector3(
				position.x, position.y + radius * Mathf.Sin( 0 ),
				position.z + radius * Mathf.Cos( 0 ) );
			Vector3 y = new Vector3(
				position.x + radius * Mathf.Cos( 0 ), position.y,
				position.z + radius * Mathf.Sin( 0 ) );
			Vector3 z = new Vector3(
				position.x + radius * Mathf.Cos( 0 ),
				position.y + radius * Mathf.Sin( 0 ), position.z );

			Vector3 new_x;
			Vector3 new_y;
			Vector3 new_z;

			for ( int i = 1; i < 37; i++ )
			{

				new_x = new Vector3(
					position.x,
					position.y + radius * Mathf.Sin( angle * i * Mathf.Deg2Rad ),
					position.z + radius * Mathf.Cos( angle * i * Mathf.Deg2Rad )
				);
				new_y = new Vector3(
					position.x + radius * Mathf.Cos( angle * i * Mathf.Deg2Rad ),
					position.y,
					position.z + radius * Mathf.Sin( angle * i * Mathf.Deg2Rad )
				);
				new_z = new Vector3(
					position.x + radius * Mathf.Cos( angle * i * Mathf.Deg2Rad ),
					position.y + radius * Mathf.Sin( angle * i * Mathf.Deg2Rad ),
					position.z );

				Debug.DrawLine( x, new_x, color, duration, depth_test );
				Debug.DrawLine( y, new_y, color, duration, depth_test );
				Debug.DrawLine( z, new_z, color, duration, depth_test );

				x = new_x;
				y = new_y;
				z = new_z;
			}
		}

		public static void gizmo( Vector3 position, float radius = 1.0f )
		{
			float angle = 10.0f;

			Vector3 x = new Vector3(
				position.x, position.y + radius * Mathf.Sin( 0 ),
				position.z + radius * Mathf.Cos( 0 ) );
			Vector3 y = new Vector3(
				position.x + radius * Mathf.Cos( 0 ), position.y,
				position.z + radius * Mathf.Sin( 0 ) );
			Vector3 z = new Vector3(
				position.x + radius * Mathf.Cos( 0 ),
				position.y + radius * Mathf.Sin( 0 ), position.z );

			Vector3 new_x;
			Vector3 new_y;
			Vector3 new_z;

			for ( int i = 1; i < 37; i++ )
			{

				new_x = new Vector3(
					position.x,
					position.y + radius * Mathf.Sin( angle * i * Mathf.Deg2Rad ),
					position.z + radius * Mathf.Cos( angle * i * Mathf.Deg2Rad )
				);
				new_y = new Vector3(
					position.x + radius * Mathf.Cos( angle * i * Mathf.Deg2Rad ),
					position.y,
					position.z + radius * Mathf.Sin( angle * i * Mathf.Deg2Rad )
				);
				new_z = new Vector3(
					position.x + radius * Mathf.Cos( angle * i * Mathf.Deg2Rad ),
					position.y + radius * Mathf.Sin( angle * i * Mathf.Deg2Rad ),
					position.z );

				Gizmos.DrawLine( x, new_x );
				Gizmos.DrawLine( y, new_y );
				Gizmos.DrawLine( z, new_z );

				x = new_x;
				y = new_y;
				z = new_z;
			}
		}
		public static void gizmo(
			Vector3 position, Color color , float radius = 1.0f )
		{
			Color current_color = Gizmos.color;
			Gizmos.color = color;
			gizmo( position, radius );
			Gizmos.color = current_color;
		}
	}
}