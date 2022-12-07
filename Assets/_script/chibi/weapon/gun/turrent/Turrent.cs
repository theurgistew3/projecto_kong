using UnityEngine;

namespace chibi.motor.weapons.gun.turrent
{
	public class Turrent : Motor_physical
	{
		public Vector3 rotation_vector = Vector3.up;
		public float max_rotation_angle = 180f;

		[HideInInspector] public float current_rotation_angle = 0f;

		public Vector3 original_direction;
		public Quaternion original_rotation;
		public float rotation_times;
		public float current_angle = 0f;

		protected override void _init_cache()
		{
			base._init_cache();
			original_direction = transform.forward;
			original_rotation = transform.rotation;
		}

		private void Update()
		{
			float angle = Vector3.SignedAngle(
				original_direction, desire_direction,
				rotation_vector );
			float max_angle = max_rotation_angle / 2;
			angle = Mathf.Clamp( angle, -max_angle, max_angle );
			Quaternion desire_rotation;
			if ( angle == 0 )
			{
				desire_rotation = original_rotation;
			}
			else
			{
				desire_rotation = Quaternion.AngleAxis( angle, rotation_vector );
				desire_rotation = desire_rotation * original_rotation;
			}
			/*
			Debug.Log( string.Format(
				"[system.turrent]{0} :: {1} :: {2} :: {3}",
				angle, original_direction, desire_direction,
				desire_rotation.eulerAngles ) );
			*/
			//entity.rigidbody.rotation = desire_rotation;
			ridgetbody.MoveRotation( desire_rotation );
		}

		protected override void _proccess_gravity( ref Vector3 velocity_vector )
		{
		}
	}
}