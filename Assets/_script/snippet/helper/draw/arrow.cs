using UnityEngine;


namespace helper
{
	namespace draw
	{
		public class arrow
		{
			/// <summary>
			/// dibuja un gizmo en forma de flecha
			/// </summary>
			/// <param name="position">position inicial de la fecha</param>
			/// <param name="direction">
			///	direcion a la que apunta la flecha</param>
			/// <param name="arrow_head_length">
			///	lingitud de la cabeza de la fecha</param>
			/// <param name="arrow_head_angle">
			///	angulo de la cabeza de la fecha</param>
			public static void gizmo(
				Vector3 position, Vector3 direction,
				float arrow_head_length = 0.25f, float arrow_head_angle = 20.0f )
			{
				Gizmos.DrawRay( position, direction );

				Vector3 right =
					Quaternion.LookRotation( direction )
					* Quaternion.Euler( 0, 180 + arrow_head_angle, 0 )
					* new Vector3( 0, 0, 1 );
				Vector3 left =
					Quaternion.LookRotation( direction )
					* Quaternion.Euler( 0, 180 - arrow_head_angle, 0 )
					* new Vector3( 0, 0, 1 );
				Vector3 up =
					Quaternion.LookRotation( direction )
					* Quaternion.Euler( 180 +  arrow_head_angle, 0 , 0 )
					* new Vector3( 0, 0, 1 );
				Vector3 down =
					Quaternion.LookRotation( direction )
					* Quaternion.Euler( 180 -  arrow_head_angle, 0 , 0 )
					* new Vector3( 0, 0, 1 );

				Gizmos.DrawRay( position + direction, right * arrow_head_length );
				Gizmos.DrawRay( position + direction, left * arrow_head_length );
				Gizmos.DrawRay( position + direction, up * arrow_head_length );
				Gizmos.DrawRay( position + direction, down * arrow_head_length );
			}

			/// <summary>
			/// dibuja una flecha con un color en especifico
			/// </summary>
			/// <param name="position">position inicial de la fecha</param>
			/// <param name="direction">
			///	direcion a la que apunta la flecha</param>
			/// <param name="color">color del gizmodo</param>
			/// <param name="arrow_head_length">
			///	lingitud de la cabeza de la fecha</param>
			/// <param name="arrow_head_angle">
			///	angulo de la cabeza de la fecha</param>
			public static void gizmo(
				Vector3 position, Vector3 direction, Color color,
				float arrow_head_length = 0.25f, float arrow_head_angle = 20.0f )
			{
				Color current_color = Gizmos.color;
				Gizmos.color = color;
				gizmo( position, direction, arrow_head_length, arrow_head_angle );
				Gizmos.color = current_color;
			}

			public static void debug(
				Vector3 position, Vector3 direction, Color color = new Color(),
				float arrow_head_length = 0.25f, float arrow_head_angle = 20.0f,
				float duration = 0f )
			{
				if ( direction.magnitude == 0 )
					return;

				Debug.DrawRay( position, direction, color, duration );

				Vector3 right =
					Quaternion.LookRotation( direction )
					* Quaternion.Euler( 0, 180 + arrow_head_angle, 0 )
					* new Vector3( 0, 0, 1 );
				Vector3 left =
					Quaternion.LookRotation( direction )
					* Quaternion.Euler( 0, 180 - arrow_head_angle, 0 )
					* new Vector3( 0, 0, 1 );
				Vector3 up =
					Quaternion.LookRotation( direction )
					* Quaternion.Euler( 180 +  arrow_head_angle, 0 , 0 )
					* new Vector3( 0, 0, 1 );
				Vector3 down =
					Quaternion.LookRotation( direction )
					* Quaternion.Euler( 180 -  arrow_head_angle, 0 , 0 )
					* new Vector3( 0, 0, 1 );

				Debug.DrawRay(
					position + direction, right * arrow_head_length, color,
					duration );
				Debug.DrawRay(
					position + direction, left * arrow_head_length, color,
					duration );
				Debug.DrawRay(
					position + direction, up * arrow_head_length, color, duration );
				Debug.DrawRay(
					position + direction, down * arrow_head_length, color,
					duration );
			}
		}
	}
}