using UnityEngine;

namespace helper
{
	public class joystick {

		/// <summary>
		/// Obtiene el valor del eje X del stick izquierdo
		/// </summary>
		public static float axis_left_x {
			get {
				return Input.GetAxisRaw( "horizontal" );
			}
		}

		/// <summary>
		/// Obtiene el valor del eje Y del stick izquierdo
		/// </summary>
		public static float axis_left_y {
			get {
				return Input.GetAxisRaw( "vertical" );
			}
		}

		/// <summary>
		/// Obtiene el vector del stick izquierdo
		/// </summary>
		public static Vector3 axis_left {
			get {
				return new Vector3( axis_left_x, 0, axis_left_y );
			}
		}

		/// <summary>
		/// decide si el eje paso la zona muerta
		/// </summary>
		/// <param name="value">valor a evaluar si pasa la zona muerta</param>
		/// <param name="dead_zone">tamaño de la sona muerta</param>
		/// <returns>si paso la zona muerta</returns>
		public static bool pass_dead_zone( float value, float dead_zone ) {
			return value < -dead_zone || dead_zone < value;
		}
	}
}