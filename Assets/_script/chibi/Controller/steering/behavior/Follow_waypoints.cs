using UnityEngine;
using chibi.controller.handler;

namespace chibi.controller.steering.behavior
{
	[CreateAssetMenu( menuName = "chibi/steering/behavior/follow_waypoints" )]
	public class Follow_waypoints : Behavior
	{
		public bool loop = false;

		public override Vector3 desire_direction(
			Steering controller, Transform target,
			Steering_properties properties )
		{
			var current_target = properties.waypoints[
				properties.current_waypoint ];
			if ( loop
				&& properties.current_waypoint >= properties.waypoints.Count - 1 )
			{
				properties.current_waypoint = 0;
			}
			if ( properties.current_waypoint < properties.waypoints.Count - 1
				&& Vector3.Distance(
					current_target, controller.transform.position ) < 0.2 )
			{
				properties.current_waypoint += 1;
			}

			var direction = seek( controller, current_target );
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

		public override Steering_properties prepare_properties(
			Steering controller, Steering_properties properties,
			Transform target )
		{
			properties = base.prepare_properties(
				controller, properties, target );
			chibi.path.Path_behaviour path = target.GetComponent<
				chibi.path.Path_behaviour>();
			properties.current_waypoint = 0;
			properties.waypoints = path.path.bake_points;

			var handlers = target.GetComponentsInChildren<Handler_behaviour>();
			foreach( var handler in handlers )
				handler.add( controller.controller );
			return properties;
		}
	}
}
