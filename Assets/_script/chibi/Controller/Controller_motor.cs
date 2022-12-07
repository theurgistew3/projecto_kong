using UnityEngine;

namespace chibi.controller
{
	public class Controller_motor : Controller
	{
		[Header( "motor properties" )]
		public motor.Motor motor;
		public bool pasive_motor;

		public override Vector3 desire_direction
		{
			get {
				return base.desire_direction;
			}

			set {
				base.desire_direction = value;
				motor.desire_direction = desire_direction;
			}
		}

		public override float speed {
			get {
				return base.speed;
			}

			set {
				base.speed = value;
				motor.desire_speed = speed;
			}
		}

		public virtual float max_speed {
			get {
				return motor.max_speed;
			}
		}

		protected override void _init_cache()
		{
			base._init_cache();
			load_motors();
		}

		protected virtual void load_motors()
		{
			motor = GetComponent<motor.Motor>();
			if ( !motor )
			{
				Debug.LogError( string.Format(
					"no se encontro un motor en el object {0}" +
					"se agrega un motor",
					helper.game_object.name.full( this ) ),
					gameObject );
				motor = gameObject.AddComponent<motor.Motor>();
			}
			if ( pasive_motor )
			{
				var rigidbody = GetComponent<Rigidbody>();
				rigidbody.useGravity = false;
			}
		}

	}
}
