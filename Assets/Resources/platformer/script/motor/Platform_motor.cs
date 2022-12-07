using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace platformer.motor.platform
{
	public class Platform_motor : chibi.motor.Motor
	{
		public chibi.pomodoro.Pomodoro_obj pomodoro;
		public int life_span = 5;
		public GameObject owner;

		protected override void _proccess_gravity( ref Vector3 velocity_vector )
		{
		}

		private void OnCollisionExit( Collision collision )
		{
			if ( !owner )
			{
				debug.error( "no se agigno owner a la plataforma" );
			}
			else if ( collision.gameObject == owner )
			{
				pomodoro.is_enable = true;
				pomodoro.frecuency = life_span;
			}
		}

		private void OnCollisionEnter( Collision collision )
		{
			if ( !owner )
			{
				debug.error( "no se agigno owner a la plataforma" );
			}
			else if ( collision.gameObject == owner )
			{
				pomodoro.is_enable = false;
				pomodoro.reset();
			}
		}

		protected void Update()
		{
			if ( pomodoro.tick( Time.deltaTime ) )
				Destroy( this.gameObject );
		}

		protected override void _init_cache()
		{
			base._init_cache();
			pomodoro = new chibi.pomodoro.Pomodoro_obj( life_span );
			pomodoro.is_enable = false;
		}
	}
}
