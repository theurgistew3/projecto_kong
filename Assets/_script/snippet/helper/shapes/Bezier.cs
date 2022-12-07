using UnityEngine;
using System;

namespace helper.shapes
{
	[Serializable]
	public static class Bezier
	{
		public static Vector3 evaluate(
			Vector3 a, Vector3 b, Vector3 c, float t )
		{
			var p1 = Vector3.Lerp( a, b, t );
			var p2 = Vector3.Lerp( b, c, t );
			return Vector3.Lerp( p1, p2, t );
		}

		public static Vector3 evaluate(
			Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t )
		{
			var p1 = evaluate( a, b, c, t );
			var p2 = evaluate( b, c, d, t );
			return Vector3.Lerp( p1, p2, t );
		}
	}
}
