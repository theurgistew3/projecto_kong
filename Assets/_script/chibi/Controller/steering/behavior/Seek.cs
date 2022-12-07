using UnityEngine;

namespace chibi.controller.steering.behavior
{
	[CreateAssetMenu( menuName = "chibi/steering/behavior/seek" )]
	public class Seek : chibi.controller.steering.behavior.Behavior
	{
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
			return 1f;
		}

		public virtual void debug(
			Controller controller, Transform target, Vector3 direction )
		{
			controller.debug.draw.arrow( direction, seek_color );
		}
	}
}
