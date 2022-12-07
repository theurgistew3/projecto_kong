using UnityEngine;

namespace chibi.controller.steering.behavior
{
	public abstract class Behavior : Chibi_object
	{
		public float weight = 1;
		public Color debug_color;
		public Color seek_color;

		public abstract Vector3 desire_direction(
			Steering controller, Transform target,
			Steering_properties properties );

		public abstract float desire_speed(
			Steering controller, Transform target,
			Steering_properties properties );

		public virtual void debug(
			Steering controller, Transform target,
			Steering_properties properties )
		{
			Debug.Log( string.Format(
				"[sterring behavior] actuando en '{0}' hacia '{1}'",
				helper.game_object.name.full( controller ), target
			) );
		}

		public Vector3 seek( Steering controller, Vector3 target )
		{
			return target - controller.transform.position;
		}

		public Vector3 flee( Steering controller, Vector3 target )
		{
			return controller.transform.position - target;
		}

		public virtual Steering_properties prepare_properties(
			Steering controller, Steering_properties properties, Transform target )
		{
			return properties;
		}
	}

}
