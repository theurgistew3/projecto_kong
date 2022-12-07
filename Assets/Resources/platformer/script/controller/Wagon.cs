using UnityEngine;
using chibi.controller.npc;
using System.Collections.Generic;
using platformer.controller.platform;

namespace platformer.controller.wagon
{
	public class Wagon : chibi.controller.Controller
	{
		public chibi.path.Path_behaviour path;
		public controller.platform.Platform_controller controller;

		public int index = 0;

		public motor.platform.Platform_motor motor
		{
			get {
				return controller.motor;
			}
		}

		public Transform next_target
		{
			get {
				index = ++index % path.path.segments.Count;
				var current_segment = path.path.segments[ index ];
				return current_segment.p2;
			}
		}

		public Transform prev_target
		{
			get {
				index = --index % path.path.segments.Count;
				var current_segment = path.path.segments[ index ];
				return current_segment.p1;
			}
		}

		public void move_next_instant_step()
		{
			var target = next_target;
			motor.transform.position = target.position;
		}

		public void move_prev_instant_step()
		{
			var target = prev_target;
			motor.transform.position = target.position;
		}

		public void move_next_step()
		{
			var target = next_target;
			debug.log( target );
			controller.seek( target );
		}

		public void move_prev_step()
		{
			var target = prev_target;
			debug.log( target );
			controller.seek( target );
		}

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !path )
				debug.warning( "no tiene path" );

			if ( !controller )
				debug.warning( "no tiene un controller" );
		}
	}
}
