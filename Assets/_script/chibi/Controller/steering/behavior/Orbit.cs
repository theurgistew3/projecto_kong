using UnityEngine;

namespace chibi.controller.steering.behavior
{
	[CreateAssetMenu( menuName = "chibi/steering/behavior/Orbit" )]
	public class Orbit : chibi.controller.steering.behavior.Behavior
	{
		public float x_radius = 1;
		public float z_radius = 1;

		public override Vector3 desire_direction(
			Steering controller, Transform target,
			Steering_properties properties )
		{
			float perimeter = helper.shapes.Ellipse.perimeter(
				x_radius, z_radius );

			float delta_period =
				( controller.controller.speed / perimeter );

			float current_period = properties.x;
			current_period += ( delta_period * Time.deltaTime ) % 1f;
			properties.x = current_period;

			var desire = helper.shapes.Ellipse.evaluate(
				x_radius, z_radius, current_period );
			desire += target.position;

			var direction = seek( controller, desire );
			direction.Normalize();
			debug( controller.controller, target, direction );
			return direction;
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

		public override Steering_properties prepare_properties(
			Steering controller, Steering_properties properties,
			Transform target )
		{
			properties = base.prepare_properties( controller, properties, target );
			var current_position = controller.transform.position;

			Vector3 current_direction_to_orbit =
				current_position - target.position;

			properties.x = helper.shapes.Ellipse.get_progrest(
				current_direction_to_orbit );
			return properties;
		}
	}
}
