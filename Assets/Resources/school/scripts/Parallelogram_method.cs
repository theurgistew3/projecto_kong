
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace school.plane
{
	public class Parallelogram_method: chibi.Chibi_behaviour
	{

		public float magnitude_scale = 50f;
		public float angle_vector_a = 30f;
		public float magnitude_vector_a = 250f;
		public float angle_vector_b = 90;
		public float magnitude_vector_b = 300f;

		public float angle_result = 0f;
		public float magnitude_result = 0f;

		public Vector3 a_direction = Vector3.zero;

		public void Update()
		{
			a_direction = Quaternion.Euler( 0, 0, angle_vector_a ) * Vector3.right;
			a_direction = a_direction.normalized * ( magnitude_vector_a / magnitude_scale );
			debug.draw.arrow( a_direction, Color.red );

			Vector3 b_direction = Quaternion.Euler( 0, 0, angle_vector_b ) * Vector3.right;
			b_direction = b_direction.normalized * ( magnitude_vector_b / magnitude_scale );
			debug.draw.arrow( b_direction, Color.blue );

			var color_a = new Color( 0.5f, 0, 0, 1 );
			var color_b = new Color( 0, 0, 0.5f, 1 );

			debug.draw.arrow( b_direction, a_direction, color_a );
			debug.draw.arrow( a_direction, b_direction, color_b );

			var result = a_direction + b_direction;
			debug.draw.arrow( result, Color.magenta );

			angle_result = Vector3.Angle( result, Vector3.right );
			magnitude_result = result.magnitude * magnitude_scale;
		}
	}
}