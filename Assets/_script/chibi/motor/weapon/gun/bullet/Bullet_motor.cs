using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon.ammo;
using chibi.pomodoro;

namespace chibi.motor.weapons.gun.bullet
{
	public class Bullet_motor : Motor_physical
	{
		public Ammo ammo;
		public float life_span;
		public bool alway_rotate_to_velocity_direction = true;
		public float time_to_disable = 1f;
		protected IEnumerator __life_span;

		[Header( "step motor" )]
		public int index_step = 0;
		public List< Bullet_step > steps;

		protected bool stop_steps = false;

		protected override void _init_cache()
		{
			base._init_cache();
			prepare_life_span();
		}

		protected virtual void prepare_life_span()
		{
			__life_span = recicle_when_life_span_end();
			StartCoroutine( __life_span );
			StartCoroutine( "_disable_in" );
		}

		protected virtual IEnumerator recicle_when_life_span_end()
		{
			yield return new WaitForSeconds( life_span );
			recycle();
		}

		protected virtual IEnumerator _disable_in()
		{
			yield return new WaitForSeconds( time_to_disable );
			set_static_next_update();
		}

		public override void recycle()
		{
			ammo.push( this );
		}

		public override Vector3 desire_direction
		{
			get
			{
				return base.desire_direction;
			}
			set
			{
				if ( alway_rotate_to_velocity_direction && value != Vector3.zero )
					transform.rotation = Quaternion.LookRotation(
						value, transform.up );
				base.desire_direction = value;
			}
		}

		protected override void update_motion()
		{
			if ( steps.Count != 0 )
			{
				fixed_update_life_span();
				if ( !stop_steps )
					step();
			}
			base.update_motion();
		}

		protected virtual void step()
		{
			var step = steps[ index_step ];
			step.update( this );
		}

		protected virtual void fixed_update_life_span()
		{
			if ( stop_steps )
				return;
			var step = steps[ index_step ];
			if ( step.tick( Time.fixedDeltaTime ) )
			{
				if ( ++index_step < steps.Count )
				{
					step = steps[ index_step ];
					step.start( this );
				}
				else
				{
					stop_steps = true;
					set_static_next_update();
				}
			}
		}

		public override void reset()
		{
			index_step = 0;
			stop_steps = false;
			foreach ( var step in steps )
			{
				step.reset();
				step.prepare( this );
			}
			if ( steps.Count > 0 )
				steps[ 0 ].start( this );
			base.reset();
		}
	}
}
