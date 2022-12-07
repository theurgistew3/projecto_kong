using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using chibi.manager.collision;

namespace chibi.motor
{
	public class Motor : chibi.Chibi_behaviour
	{
		public Vector3 current_speed = Vector3.zero;
		protected float _desire_speed;
		public float max_speed = 4f;

		public bool is_steering = false;

		public float steering_mass = 1f;
		protected Vector3 smooth_velocity = Vector3.zero;
		public Vector3 velocity_acceleration = Vector3.zero;

		private Vector3 _desire_direction;
		public Chibi_collision_manager manager_collision;

		// protected Rigidbody ridgetbody;
		[SerializeField]
		protected float _gravity = -9.8f;
		protected Vector3 _velocity;

		protected IEnumerator courutine_ignore_input;
		protected bool ignore_input;

		public virtual float gravity
		{
			get { return _gravity; }
			set { _gravity = value; }
		}

		public virtual Vector3 velocity
		{
			get {
				return _velocity;
			}
		}

		public virtual Vector3 desire_velocity
		{
			get {
				var desire_speed_vector = desire_direction.normalized
					* Mathf.Clamp( desire_speed, 0, max_speed );

				debug.draw.arrow( desire_speed_vector, Color.magenta );
				float final_x = Mathf.SmoothDamp(
					velocity.x, desire_speed_vector.x,
					ref smooth_velocity.x, velocity_acceleration.x );

				float final_y = Mathf.SmoothDamp(
					velocity.y, desire_speed_vector.y,
					ref smooth_velocity.y, velocity_acceleration.y );

				float final_z = Mathf.SmoothDamp(
					velocity.z, desire_speed_vector.z,
					ref smooth_velocity.z, velocity_acceleration.z );

				var final_speed = new Vector3( final_x, final_y, final_z );
				debug.draw.arrow( final_speed, Color.blue );
				if ( is_steering )
				{
					var steering = final_speed - velocity;
					steering /= steering_mass;
					debug.draw.arrow( velocity + steering, Color.yellow );
					return ( velocity + steering );
				}
				else
					return final_speed;
			}
		}

		public virtual Vector3 desire_direction
		{
			get {
				return _desire_direction;
			}

			set {
				if ( !ignore_input )
					_desire_direction = value;
			}
		}

		public float desire_speed
		{
			get {
				return _desire_speed;
			}

			set {
				if ( !ignore_input )
					_desire_speed = value;
			}
		}

		public void set_static_next_update()
		{
			StartCoroutine( "_set_static_next_update" );
		}

		protected IEnumerator _set_static_next_update()
		{
			yield return new WaitForFixedUpdate();
			enabled = false;
		}

		protected override void _init_cache()
		{
			base._init_cache();
			manager_collision = GetComponent<Chibi_collision_manager>();
			if ( !manager_collision )
				debug.error( "no se encontro el manager de las collisiones" );
		}

		protected virtual void update_motion()
		{
			Vector3 velocity_vector = desire_velocity;
			process_motion( ref velocity_vector );

			//_proccess_gravity( ref velocity_vector );
			//transform.Translate( velocity_vector * Time.deltaTime );
			// ridgetbody.velocity = velocity_vector;
			transform.position += velocity_vector * Time.deltaTime;
			debug.draw.arrow( velocity_vector * Time.deltaTime, Color.magenta );
			_velocity = velocity_vector;
			current_speed = velocity_vector;
		}

		public virtual Vector3 process_motion( ref Vector3 velocity_vector )
		{
			_proccess_gravity( ref velocity_vector );
			//transform.Translate( velocity_vector * Time.deltaTime );
			//transform.position += velocity_vector * Time.deltaTime;
			//debug.draw.arrow( velocity_vector * Time.deltaTime, Color.magenta );
			//_velocity = velocity_vector;
			// ridgetbody.velocity = velocity_vector;
			//current_speed = velocity_vector;
			return velocity_vector;
		}

		protected virtual void _proccess_gravity(
				ref Vector3 velocity_vector )
		{
			velocity_vector.y += ( gravity * Time.deltaTime );
		}

		protected virtual void FixedUpdate()
		{
			update_motion();
		}

		protected virtual void start_to_ignore_input( float time )
		{
			stop_to_ignore_input();
			debug.log( "start ignore input por {0}", time );
			courutine_ignore_input = wait_for_ignore_input( time );
			StartCoroutine( courutine_ignore_input );
			ignore_input = true;
		}
		protected virtual void stop_to_ignore_input()
		{
			if ( courutine_ignore_input != null )
			{
				debug.log( "stop ignore input por" );
				StopCoroutine( courutine_ignore_input );
			}
			ignore_input = false;
			courutine_ignore_input = null;
		}

		protected virtual IEnumerator wait_for_ignore_input( float time )
		{
			yield return new WaitForSeconds( time );
			ignore_input = false;
		}
	}
}
