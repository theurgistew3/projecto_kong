using System;
using UnityEngine;
using chibi.manager.collision;

namespace chibi.motor.npc
{
	public class Motor_isometric : Motor_physical
	{
		#region variables de jump
		protected float _max_jump_heigh = 4f;
		protected float _min_jump_heigh = 1f;
		protected float _jump_time = 0.4f;

		protected float _max_jump_velocity;
		protected float _min_jump_velocity;

		public override Vector3 desire_direction
		{
			set {
				base.desire_direction = new Vector3( value.x, 0, value.z );
			}
		}

		protected bool try_to_jump_the_next_update = false;
		#endregion

		#region propiedades de salto
		public virtual float max_jump_heigh
		{
			get { return _max_jump_heigh; }
			set {
				_max_jump_heigh = value;
				update_jump_properties();
			}
		}

		public virtual float min_jump_heigh
		{
			get { return _min_jump_heigh; }
			set {
				_min_jump_heigh = value;
				update_jump_properties();
			}
		}

		public virtual float jump_time
		{
			get { return _jump_time; }
			set {
				jump_time = value;
				update_jump_properties();
			}
		}

		public virtual float max_jump_velocity
		{
			get { return _max_jump_velocity; }
		}

		public virtual float min_jump_velocity
		{
			get { return _min_jump_velocity; }
		}
		#endregion

		#region propiedades publicas
		public virtual Chibi_collision_isometric collision_manager_isometric
		{
			get { return manager_collision as Chibi_collision_isometric; }
		}
		#region propiedades conocer el estado de las coliciones
		public virtual bool is_grounded
		{
			get { return collision_manager_isometric.is_grounded; }
		}

		public virtual bool is_not_grounded
		{
			get { return !is_grounded; }
		}

		public virtual bool is_walled
		{
			get { return collision_manager_isometric.is_walled; }
		}

		public virtual bool is_not_walled
		{
			get { return !is_walled; }
		}

		public virtual bool is_walled_left
		{
			get { return collision_manager_isometric.is_walled_left; }
		}

		public virtual bool is_walled_right
		{
			get { return collision_manager_isometric.is_walled_right; }
		}

		public virtual bool no_is_walled_left
		{
			get { return !is_walled_left; }
		}

		public virtual bool no_is_walled_right
		{
			get { return !is_walled_right; }
		}
		#endregion
		#endregion

		protected override void update_motion()
		{
			current_speed = desire_velocity;
			Vector3 velocity_vector = new Vector3(
				current_speed.x, ridgetbody.velocity.y,
				current_speed.z );

			process_motion( ref velocity_vector );
			// update_change_direction( ref velocity_vector );

			//_process_jump( ref velocity_vector );
			//_proccess_gravity( ref velocity_vector );

			ridgetbody.velocity = velocity_vector;
		}

		public override Vector3 process_motion( ref Vector3 velocity_vector )
		{
			_process_jump( ref velocity_vector );
			_proccess_gravity( ref velocity_vector );
			return velocity_vector;
		}

		protected override void _proccess_gravity(
				ref Vector3 velocity_vector )
		{
			velocity_vector.y += ( gravity * Time.deltaTime );
		}

		protected virtual void _process_jump(ref Vector3 speed_vector)
		{
			if ( try_to_jump_the_next_update )
			{
				if ( is_grounded )
				{
					speed_vector.y = _max_jump_velocity;
				}
			}
			else if ( speed_vector.y > _min_jump_velocity )
				speed_vector.y = _min_jump_velocity;
		}

		protected override void _init_cache()
		{
			base._init_cache();
			update_jump_properties();
		}

		protected virtual void update_jump_properties()
		{
			gravity = -( 2 * max_jump_heigh ) / ( jump_time * jump_time );
			_max_jump_velocity = Math.Abs( _gravity ) * jump_time;
			_min_jump_velocity = ( float )Math.Sqrt(
				2.0 * Math.Abs( _gravity ) * min_jump_heigh );
		}

		public virtual void on_died()
		{
			debug.info( "murio" );
		}

		public virtual void on_end_died()
		{
			debug.info( "termino de morir" );
		}

		public void start_jump()
		{
		}

		public void end_jump()
		{
		}
	}
}
