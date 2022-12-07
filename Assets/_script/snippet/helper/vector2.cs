using UnityEngine;
using System;

namespace helper {
	class vector2 {
		/// <summary>
		/// obtiene el angulo del vector basoda en el angulo de origen
		/// </summary>
		/// <param name="v">vector del que se evaluera el angulo</param>
		/// <param name="origin">origen del angulo</param>
		/// <returns>angulo positivo</returns>
		public static float angle( Vector2 v, Vector2 origin ) {
			float angle = Vector2.Angle( v, origin );
			float cross = Vector3.Cross( v, origin ).z;
			return cross > 0 ? 360f - angle : angle;
		}

		public static Boolean if_need_normalize( ref Vector2 vector ) {
			if ( vector.magnitude > 1 ) {
				vector.Normalize();
				return true;
			}
			else
				return false;
		}
	}
}