using UnityEngine;

namespace chibi.controller.ai
{
	public class Ai_walk : Chibi_behaviour
	{
		public Vector3 desire_direction = Vector3.zero;
		public float speed = 1f;
		public bool use_max_speed = true;
		public Controller controller;

		protected virtual float max_speed
		{
			get {
				var motor = GetComponent<motor.Motor>();
				if ( motor )
					return motor.max_speed;
				return speed;
			}
		}

		protected virtual float desire_speed
		{
			get {
				if ( use_max_speed )
					return max_speed;
				return speed;
			}
		}

		protected virtual void Update()
		{
			if ( desire_direction != Vector3.zero )
			{
				controller.desire_direction = desire_direction;
				controller.speed = desire_speed;
			}
		}

		protected virtual void OnDrawGizmos()
		{
			if ( desire_direction != Vector3.zero )
				helper.draw.arrow.gizmo(
					transform.position, desire_direction, Color.magenta );
		}

		protected override void _init_cache()
		{
			base._init_cache();
			if ( controller == null )
				controller = GetComponent< Controller >();
		}
	}
}