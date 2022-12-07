using UnityEngine;
using chibi.controller.handler;

namespace chibi.controller.steering.behavior
{
	[CreateAssetMenu( menuName = "chibi/steering/behavior/arrive" )]
	public class Arrive : Behavior
	{
		public float deacceleration_distant = 0.5f;

		public override Vector3 desire_direction(
			Steering controller, Transform target,
			Steering_properties properties )
		{
			var direction = seek( controller, target.position );
			direction.Normalize();
			debug( controller.controller, target, direction );
			return direction.normalized;
		}

		public override float desire_speed(
			Steering controller, Transform target,
			Steering_properties properties )
		{
			var distance = target.position - controller.transform.position;
			if ( distance.magnitude < deacceleration_distant )
			{
				float speed = (float)System.Math.Round(
					distance.magnitude / deacceleration_distant );
				if ( speed < 0.1f )
					return 0f;
				return speed;
			}
			else
				return 1f;
		}

		public virtual void debug(
			Controller controller, Transform target, Vector3 direction )
		{
			controller.debug.draw.arrow( direction, seek_color );
		}
	}
}
