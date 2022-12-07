using UnityEngine;
using System;


namespace chibi
{
	[Serializable]
	public class unsigned_vector3
	{
		[SerializeField]
		protected float _x, _y, _z;

		public float x
		{
			get {
				return _x;
			}
			set {
				_x = value > 0 ? value : 0f;
			}
		}

		public float y
		{
			get {
				return _y;
			}
			set {
				_y = value > 0 ? value : 0f;
			}
		}

		public float z
		{
			get {
				return _z;
			}
			set {
				_z = value > 0 ? value : 0f;
			}
		}

		public static Vector3 operator *( unsigned_vector3 t, Vector3 other )
		{
			return other * t;
		}

		public static Vector3 operator *( Vector3 other, unsigned_vector3 t )
		{
			return new Vector3( other.x * t.x, other.y * t.y, other.z * t.z );
		}
	}
}