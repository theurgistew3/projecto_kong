
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace school.plane
{
	public class Polygon_method: chibi.Chibi_behaviour
	{

		public float magnitude_scale = 50f;
		public float angle_vector_a = 30f;
		public float magnitude_vector_a = 300f;
		public float angle_vector_b = 80f;
		public float magnitude_vector_b = 500f;
		public float angle_vector_c = 270f;
		public float magnitude_vector_c = 400f;

		public float angle_result = 0f;
		public float magnitude_result = 0f;

		public Vector3 a_direction = Vector3.zero;

		public void Update()
		{
			a_direction = Quaternion.Euler( 0, 0, angle_vector_a ) * Vector3.right;
			a_direction = a_direction.normalized * ( magnitude_vector_a / magnitude_scale );

			Vector3 b_direction = Quaternion.Euler( 0, 0, angle_vector_b ) * Vector3.right;
			b_direction = b_direction.normalized * ( magnitude_vector_b / magnitude_scale );

			Vector3 c_direction = Quaternion.Euler( 0, 0, angle_vector_c ) * Vector3.right;
			c_direction = c_direction.normalized * ( magnitude_vector_c / magnitude_scale );

			debug.draw.arrow( a_direction, Color.red );
			debug.draw.arrow( a_direction, b_direction, Color.blue );
			debug.draw.arrow( a_direction + b_direction, c_direction, Color.green );

			debug.draw.arrow( a_direction + b_direction + c_direction, Color.magenta );

			var result = a_direction + b_direction + c_direction;
			angle_result = Vector3.Angle( result, Vector3.left);
			magnitude_result = result.magnitude * magnitude_scale;
		}
	}
}