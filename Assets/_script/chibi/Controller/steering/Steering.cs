using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace chibi.controller.steering
{
	public class Steering : Chibi_behaviour
	{
		public Transform target;
		public Controller_motor controller;
		public List<behavior.Behavior> behaviors;
		public List<Steering_properties> behaviors_properties;

		public float start_speed = -1f;

		protected override void _init_cache()
		{
			base._init_cache();
			if ( behaviors == null )
				behaviors = new List<behavior.Behavior>();
			if ( !behaviors.Any() )
				debug.warning( "el steering controller no tiene behaviors" );
		}

		public virtual void reload_behaviors()
		{
			if ( behaviors == null )
				behaviors = new List<behavior.Behavior>();
			behaviors_properties = new List<Steering_properties>(
				behaviors.Count );

			for ( int i = 0; i < behaviors.Count; ++i )
			{
				Steering_properties propertie = new Steering_properties();
				behaviors[ i ].prepare_properties( this, propertie, target );
				behaviors_properties.Add( propertie );
			}
		}

		public virtual void reload()
		{
			reload_behaviors();
			if ( start_speed == -1f )
				controller.speed = controller.max_speed;
			else
				controller.speed = start_speed;
		}

		protected override void Start()
		{
			base.Start();
			reload();
		}

		private void Update()
		{
			Vector3 desire_direction = Vector3.zero;
			float desire_speed = 1f;
			behavior.Behavior behavior;
			Steering_properties properties;

			for ( int i = 0; i < behaviors_properties.Count; ++i )
			{
				properties = behaviors_properties[i];
				behavior = behaviors[i];
				properties.time += Time.deltaTime;
				var behavior_direction = behavior.desire_direction(
					this, target, properties );

				if ( behavior_direction == Vector3.zero )
					continue;
				behavior_direction *= behavior.weight;
				desire_speed = behavior.desire_speed(
					this, target, properties );
				//entity.sterring.debug.draw.arrow( behavior_direction, Color.black );
				desire_direction += behavior_direction;
			}
			debug.draw.arrow( desire_direction, Color.black );
			controller.desire_direction = desire_direction;
			controller.speed = controller.max_speed * desire_speed;
		}
	}
}
