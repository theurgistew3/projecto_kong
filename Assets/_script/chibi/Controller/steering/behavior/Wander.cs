using UnityEngine;

namespace chibi.controller.steering.behavior
{
	[CreateAssetMenu( menuName = "chibi/steering/behavior/wander" )]
	public class Wander : chibi.controller.steering.behavior.Behavior
	{
		public float circle_distance = 1f;
		public float circle_radius = 1f;
		public float frequency = 1f;

		public override Vector3 desire_direction(
			Steering controller, Transform target,
			Steering_properties properties )
		{
			if ( properties.time > frequency )
			{
				properties.last_target = find_a_new_target(
					controller, target );
				properties.time -= frequency;
			}
			var result = seek( controller, properties.last_target );
			debug_seek( controller, result );
			return result;
		}

		public override float desire_speed(
			Steering controller, Transform target,
			Steering_properties properties )
		{
			return 1f;
		}

		public virtual void debug_seek(
			Steering controller, Vector3 seek_direction )
		{
			controller.debug.draw.arrow( seek_direction, seek_color );
		}

		public virtual void debug_find_new_target(
			Steering controller, Transform target, Vector3 circle_position,
			Vector3 direction )
		{
			controller.debug.draw.line(
				circle_position, debug_color, duration:0.1f );
			controller.debug.draw.sphere(
				circle_position, debug_color, circle_radius, 0.1f );
			controller.debug.draw.arrow(
				circle_position, direction, debug_color, duration:0.1f );
		}

		protected virtual Vector3 find_a_new_target(
			Steering controller, Transform target )
		{
			var circle_position = 
				controller.controller.motor.velocity.normalized * circle_distance;
			circle_position += controller.transform.position;

			Quaternion rotation = helper.random.quaternion._();
			Vector3 direction = rotation * Vector3.one;
			debug_find_new_target(
				controller, target, circle_position, direction );
			return circle_position + ( direction * circle_radius );
		}
	}
}
