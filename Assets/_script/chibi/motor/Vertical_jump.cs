using UnityEngine;
using System;

namespace chibi.motor
{
	[RequireComponent( typeof( Rigidbody ) )]
	public class Vertical_jump : Chibi_behaviour
	{
		public float max_jump_heigh = 4f;
		public float min_jump_heigh = 1f;
		public float jump_time = 0.4f;

		public manager.collision.Manager_collision manager_collisions;

		protected float max_jump_velocity;
		protected float min_jump_velocity;

		protected Rigidbody ridgetbody;
		protected Simple_gravity gravity;

		public Vector3 velocity
		{
			get {
				return ridgetbody.velocity;
			}
		}

		public virtual float desire_velocity
		{
			get {
				return max_jump_velocity;
			}
		}

		public virtual bool want_to_jump
		{
			get; set;
		}

		protected override void Start()
		{
			base.Start();

			gravity = GetComponent<Simple_gravity>();
			if ( !gravity )
				Debug.LogError( string.Format(
					"no se encontro un Simple_gravity en el objeto {0}", name ) );

			float gravity_magniture =
				-( 2 * max_jump_heigh ) / ( jump_time * jump_time );
			max_jump_velocity = Math.Abs( gravity_magniture ) * jump_time;
			min_jump_velocity = ( float )Math.Sqrt(
				2.0 * Math.Abs( gravity_magniture ) * min_jump_heigh );

			gravity.gravity = gravity.gravity.normalized * -gravity_magniture;
		}

		protected override void _init_cache()
		{
			base._init_cache();
			ridgetbody = GetComponent<Rigidbody>();
			if ( !ridgetbody )
				Debug.LogError( string.Format(
					"no se encontro un ridgetbody en el objecto {0}", name ) );

			ridgetbody.useGravity = false;
		}

		protected virtual void FixedUpdate()
		{
			debug.error( "se deberia de elimnar esto" );
			if ( want_to_jump && false )
			{
				ridgetbody.velocity = new Vector3(
					ridgetbody.velocity.x, desire_velocity,
					ridgetbody.velocity.z );
				want_to_jump = false;
			}
		}
	}
}