using UnityEngine;
using System;

namespace helper {
	public static class math {
		/// <summary>
		/// restringe un angulo a un minimo y maximo
		/// </summary>
		/// <param name="angle">angulo a evaluar</param>
		/// <param name="min">valor minimo del angulo</param>
		/// <param name="max">valor maximo del angulo</param>
		/// <returns>angulo con la restricion</returns>
		public static float clamp_angle( float angle, float min, float max ) {
			do {
				if ( angle > 360f ) {
					angle -= 360;
				}
				else if ( angle < -360 )
					angle += 360;
			} while ( angle > 360f || angle < -360f );
			return Mathf.Clamp(angle, min, max);
		}

		/// <summary>
		/// region al que pertenece un angulo dado 0 region inicial
		/// </summary>
		/// <param name="angle">angulo que se evaluara</param>
		/// <param name="size_region">tamanaño de las regiones en angulos</param>
		/// <returns>region del angulo ( la region inicial es 0 )</returns>
		public static int angle_region( float angle, float size_region ) {
			return ( int )Math.Floor( angle / size_region );
		}

		public static bool between( float number, float min, float max )
		{
			return number > min && number < max;
		}
	}
}
