using UnityEngine;

namespace chibi.motor.weapons.gun.bullet
{
	public class Bullet_motor_yoyo : Bullet_motor
	{
		public Transform target;
		public Transform origin;
		public float distant_for_return = 0.1f;

		public float distant_to_target
		{
			get {
				return Vector3.Distance( transform.position, target.position );
			}
		}

		public bool is_in_target
		{
			get {
				return distant_to_target < distant_for_return;
			}
		}

		protected virtual void Update()
		{
			if ( is_in_target )
			{
				on_reach_target();
			}
			desire_direction = target.position - transform.position;
		}

		public virtual void on_reach_target()
		{
			if ( origin )
			{
				target = origin;
				origin = null;
			}
			else
			{
				recycle();
			}
		}
	}
}
