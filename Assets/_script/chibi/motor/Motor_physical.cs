using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using chibi.manager.collision;

namespace chibi.motor
{
	[ RequireComponent( typeof( Rigidbody ) ) ]
	public class Motor_physical : Motor
	{
		protected Rigidbody ridgetbody;


		public override Vector3 velocity
		{
			get {
				return ridgetbody.velocity;
			}
		}

		protected override void _init_cache()
		{
			base._init_cache();
			ridgetbody = GetComponent<Rigidbody>();
			if ( !ridgetbody )
				debug.log( "no se encontro el ridgetbody" );
		}

		protected override void update_motion()
		{
			Vector3 velocity_vector = desire_velocity;
			process_motion( ref velocity_vector );

			ridgetbody.velocity = velocity_vector;
			current_speed = velocity_vector;
		}

		public override Vector3 process_motion( ref Vector3 velocity_vector )
		{
			_proccess_gravity( ref velocity_vector );
			return velocity_vector;
		}
	}
}
