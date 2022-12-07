using UnityEngine;
using chibi.controller.npc;
using System.Collections.Generic;
using platformer.controller.platform;
using platformer.motor.platform;

namespace platformer.controller.platform
{
	public class Platform_controller : chibi.controller.Controller
	{
		public motor.platform.Platform_motor motor;
		public chibi.controller.Controller_motor controller;

		public chibi.controller.steering.Steering steering
		{
			get {
				var steering = controller.GetComponent<
					chibi.controller.steering.Steering>();
				if ( !steering )
				{
					steering = controller.gameObject.AddComponent<
						chibi.controller.steering.Steering>();
					steering.controller = controller;
				}
				return steering;
			}
		}

		protected override void _init_cache()
		{
			base._init_cache();
		}

		public virtual void prepare_motor()
		{
			if ( !motor )
				motor = find_child_platforms();
			if ( !motor )
				debug.warning( "no tiene un platform motor" );
			else
				controller = motor.GetComponent<
					chibi.controller.Controller_motor>();
		}

		public virtual Platform_motor find_child_platforms()
		{
			Platform_motor motor = null;

			if ( transform.childCount < 1 )
			{
				debug.warning( "la plataforma no tiene hijos" );
			}
			else
			{
				Transform platform = transform.GetChild( 0 );
				motor = platform.GetComponent< Platform_motor >();
			}
			return motor;
		}

		public void seek( Transform target )
		{
			var seek = chibi.controller.steering.behavior.Arrive.CreateInstance<
				chibi.controller.steering.behavior.Arrive>();
			steering.target = target;
			steering.behaviors.Clear();
			steering.behaviors.Add( seek );
			steering.reload();
		}
	}
}
