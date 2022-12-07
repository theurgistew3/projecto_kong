using UnityEngine;
using System;
using chibi.manager.collision;
using System.Collections.Generic;

namespace chibi.motor.npc
{
	public class Motor_side_scroll : Motor_physical
	{
		// [Header( "animator" )]
		public chibi.animator.Animator_side_scroll animator;

		public float max_slope_anlge = 45f;

		#region variables de jump
		[SerializeField]
		protected float _max_jump_heigh = 4f;
		[SerializeField]
		protected float _min_jump_heigh = 1f;
		[SerializeField]
		protected float _wall_climp_jump_heigh = 3f;

		[SerializeField]
		protected float _jump_time = 0.4f;
		[SerializeField]
		protected float _falling_time = 0.4f;
		[SerializeField]
		protected float _gravity_when_fall = -10f;
		[SerializeField]
		protected float _jump_time_wall_climp = 0.4f;

		protected bool doing_wall_jump_climp = false;
		public float climp_wall_jump_ignore_input = 1f;
		public float wall_jump_off_ignore_input = 1f;

		public float grabity_after_wall_jump_climp = -10f;
		public float slope_gravity = -10f;

		protected float _max_jump_velocity;
		protected float _min_jump_velocity;
		protected float _climp_wall_jump_velocity;

		public float multiplier_velocity_wall_slice = 0.8f;
		public float multiplier_velocity_climp_jump = 0.8f;

		public Vector3 wall_jump_climp = new Vector3( 0, 14, 14 );
		public Vector3 wall_jump_off = new Vector3( 0, 5, 8 );
		public Vector3 wall_jump_leap = new Vector3( 0, 20, 14 );
		#endregion

		public float time_to_reach_speed_in_ground = 0.1f;
		public float time_to_reach_speed_in_air = 0.2f;

		protected bool try_to_jump_the_next_update = false;
		protected bool want_to_stop_jump = false;

		public int current_direction = 0;

		protected float current_horizontal_time_smooth = 0f;

		#region propiedades publicas
		public override Vector3 desire_direction
		{
			set {
				base.desire_direction = new Vector3( 0, 0, value.z );
			}
		}

		public virtual bool want_to_move
		{
			get {
				return desire_direction.z > 0.01 || desire_direction.z < -0.01;
			}
		}

		public virtual bool want_to_no_move
		{
			get { return !want_to_move; }
		}

		public virtual Chibi_collision_side_scroll collision_manager_side_scroll
		{
			get { return manager_collision as Chibi_collision_side_scroll; }
		}

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

		public virtual float wall_climp_jump_heigh
		{
			get { return _wall_climp_jump_heigh; }
			set {
				_wall_climp_jump_heigh = value;
				update_jump_properties();
			}
		}

		public virtual float jump_time
		{
			get { return _jump_time; }
			set {
				_jump_time = value;
				update_jump_properties();
			}
		}

		public virtual float jump_time_wall_climp
		{
			get { return _jump_time_wall_climp; }
			set {
				_jump_time_wall_climp = value;
				update_jump_properties();
			}
		}

		public virtual float falling_time
		{
			get { return _falling_time; }
			set {
				_falling_time = value;
				update_jump_properties();
			}
		}

		public virtual float gravity_when_fall
		{
			get { return _gravity_when_fall; }
			set {
				_gravity_when_fall = value;
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

		public virtual float climp_wall_jump_velocity
		{
			get { return _climp_wall_jump_velocity; }
		}
		#endregion

		#region propiedades conocer el estado de las coliciones
		public virtual bool is_grounded
		{
			get { return collision_manager_side_scroll.is_grounded; }
		}

		public virtual bool is_not_grounded
		{
			get { return !is_grounded; }
		}

		public virtual bool is_in_slope
		{
			get { return collision_manager_side_scroll.is_in_slope; }
		}

		public virtual bool is_not_in_slope
		{
			get { return !is_in_slope; }
		}

		public virtual bool is_walled
		{
			get { return collision_manager_side_scroll.is_walled; }
		}

		public virtual bool is_not_walled
		{
			get { return !is_walled; }
		}

		public virtual bool is_walled_left
		{
			get { return collision_manager_side_scroll.is_walled_left; }
		}

		public virtual bool is_walled_right
		{
			get { return collision_manager_side_scroll.is_walled_right; }
		}

		public virtual bool no_is_walled_left
		{
			get { return !is_walled_left; }
		}

		public virtual bool no_is_walled_right
		{
			get { return !is_walled_right; }
		}

		public virtual Vector3 wall_direction
		{
			get {
				if ( is_walled_left )
					return Vector3.back;
				else if ( is_walled_right )
					return Vector3.forward;
				else
					return Vector3.zero;
			}
		}

		public virtual bool can_do_wall_jump
		{
			get {
				return is_walled && is_not_grounded && is_not_in_slope;
			}
		}

		public virtual bool can_do_jump
		{
			get { return is_grounded || is_in_slope; }
		}

		public virtual bool should_do_slope_motion
		{
			get {
				float slope_angle = this.slope_angle;
				return is_in_slope && slope_angle > 0f && slope_angle < max_slope_anlge;
			}
		}

		public virtual float slope_angle
		{
			get {
				float slope_angle = collision_manager_side_scroll.slope(
					Chibi_collision_side_scroll.STR_SLOPE );
				return slope_angle;
			}
		}

		#endregion

		#endregion

		protected override void update_motion()
		{
			current_speed = velocity;
			Vector3 velocity_vector = velocity;

			update_change_direction( ref velocity_vector );

			process_motion( ref velocity_vector );

			if ( should_finish_jump( velocity_vector ) )
				end_jump();

			ridgetbody.velocity = velocity_vector;
			// debug.draw.arrow( velocity_vector, Color.magenta, duration:1f );

			update_animator();
		}

		public List<Vector3> calculate_motion(
			float delta_time, int steps, ref List<Vector3> result,
			Vector3 desire_direction, ref float smooth_time )
		{
			//List<Vector3> result = new List<Vector3>();
			Vector3 velocity_vector = velocity;
			return calculate_motion(
				delta_time, steps, ref result, current_direction, velocity_vector,
				desire_direction, ref smooth_time );
		}

		public List<Vector3> calculate_motion(
			float delta_time, int steps, ref List<Vector3> result, int direction,
			Vector3 desire_direction, ref float smooth_time )
		{
			//List<Vector3> result = new List<Vector3>();
			Vector3 velocity_vector = velocity;
			return calculate_motion(
				delta_time, steps, ref result, direction, velocity_vector,
				desire_direction, ref smooth_time );
		}

		public List<Vector3> calculate_motion(
			float delta_time, int steps, ref List<Vector3> result,
			int direction, Vector3 initial_velocity, Vector3 desire_direction,
			ref float smooth_time )
		{
			result.Clear();
			Vector3 current_position = transform.position;
			result.Add( current_position );

			Vector3 velocity_vector = initial_velocity;
			for ( int i = 0; i < steps; ++i )
			{
				calculate_motion(
					ref velocity_vector, delta_time, direction, desire_direction,
					ref smooth_time );
				current_position = current_position + ( velocity_vector * delta_time );
				result.Add( current_position );
			}
			return result;
		}

		public List<Vector3> calculate_motion(
			float delta_time, int steps, ref List<Vector3> result,
			int direction, Vector3 initial_velocity, float desire_speed,
			Vector3 desire_direction, bool is_walled, ref float smooth_time )
		{
			result.Clear();
			Vector3 current_position = transform.position;
			result.Add( current_position );

			Vector3 velocity_vector = initial_velocity;
			for ( int i = 0; i < steps; ++i )
			{
				calculate_motion(
					ref velocity_vector, delta_time, direction, desire_speed,
					desire_direction, is_walled, ref smooth_time );
				current_position += velocity_vector * delta_time;
				result.Add( current_position );
			}
			return result;
		}

		public override Vector3 process_motion( ref Vector3 velocity_vector )
		{
			if ( is_grounded )
				process_motion_ground( ref velocity_vector );
			else if ( should_do_slope_motion )
				process_motion_slope( ref velocity_vector, desire_direction );
			else
				process_motion_air( ref velocity_vector );

			return velocity_vector;
		}
		public virtual Vector3 calculate_motion(
			ref Vector3 velocity_vector, float delta_time, int direction,
			Vector3 desire_direction, ref float smooth_time )
		{
			if ( is_grounded )
				calculate_motion_ground(
					ref velocity_vector, desire_direction, delta_time );
			else if ( should_do_slope_motion )
				calculate_motion_slope(
					ref velocity_vector, desire_direction, delta_time );
			else
				calculate_motion_air(
					ref velocity_vector, delta_time, direction, desire_direction,
					ref smooth_time );

			return velocity_vector;
		}

		public virtual Vector3 calculate_motion(
			ref Vector3 velocity_vector, float delta_time, int direction,
			float desire_speed, Vector3 desire_direction, bool is_walled,
			ref float smooth_time )
		{
			if ( is_grounded )
				calculate_motion_ground(
					ref velocity_vector, desire_direction, delta_time );
			else if ( should_do_slope_motion )
				calculate_motion_slope(
					ref velocity_vector, desire_direction, delta_time );
			else
			{
				calculate_motion_air(
					ref velocity_vector, delta_time, direction, desire_speed,
					desire_direction, is_walled, ref smooth_time );
			}

			return velocity_vector;
		}

		public Vector3 process_motion_ground( ref Vector3 velocity_vector )
		{
			calculate_ground_horizontal_velocity(
				ref velocity_vector, desire_direction );
			_process_jump( ref velocity_vector );
			_proccess_gravity( ref velocity_vector );
			return velocity_vector;
		}

		public Vector3 calculate_motion_ground(
			ref Vector3 velocity_vector, Vector3 desire_direction,
			float delta_time )
		{
			calculate_ground_horizontal_velocity(
				ref velocity_vector, desire_direction );
			calculate_gravity( ref velocity_vector, delta_time );
			return velocity_vector;
		}

		public Vector3 process_motion_slope(
			ref Vector3 velocity_vector, Vector3 desire_direction )
		{
			calculate_ground_horizontal_velocity(
				ref velocity_vector, desire_direction );
			calculate_slope_velocity( ref velocity_vector );
			_process_jump( ref velocity_vector );
			// _proccess_slope_gravity_gravity( ref velocity_vector );
			if ( should_calcutate_gravity_in_slope( velocity_vector ) )
				_proccess_gravity( ref velocity_vector );
			return velocity_vector;
		}

		public Vector3 calculate_motion_slope(
			ref Vector3 velocity_vector, Vector3 desire_direction, float delta_time )
		{
			calculate_ground_horizontal_velocity( ref velocity_vector, desire_direction );
			calculate_slope_velocity( ref velocity_vector );
			if ( should_calcutate_gravity_in_slope( velocity_vector ) )
				calculate_gravity( ref velocity_vector, delta_time );
			return velocity_vector;
		}

		public Vector3 process_motion_air( ref Vector3 velocity_vector )
		{
			if ( want_to_move )
				calculate_air_horizontal_velocity(
					ref velocity_vector, current_direction,
					desire_direction, ref current_horizontal_time_smooth );
			_process_jump( ref velocity_vector );
			_proccess_gravity( ref velocity_vector );
			return velocity_vector;
		}

		public Vector3 calculate_motion_air(
			ref Vector3 velocity_vector, float delta_time, int direction,
			Vector3 desire_direction, ref float smooth_time )
		{
			if ( calcualte_want_to_move( desire_direction ) )
			{
				calculate_air_horizontal_velocity(
					ref velocity_vector, direction, desire_direction, ref smooth_time );
			}
			calculate_gravity( ref velocity_vector, delta_time );
			return velocity_vector;
		}

		public Vector3 calculate_motion_air(
			ref Vector3 velocity_vector, float delta_time, int direction,
			float desire_speed, Vector3 desire_direction, bool is_walled,
			ref float smooth_time )
		{
			if ( calcualte_want_to_move( desire_direction ) )
			{
				if ( is_walled )
					calculate_air_horizontal_velocity_on_wall(
						ref velocity_vector, direction, desire_direction );
				else
					calculate_air_horizontal_velocity_not_on_wall(
						ref velocity_vector, direction, desire_speed,
						desire_direction, ref smooth_time );
			}
			calculate_gravity( ref velocity_vector, delta_time );
			return velocity_vector;
		}

		protected virtual void update_animator()
		{
			animator.speed = ridgetbody.velocity.z;
			animator.vertical_speed = ridgetbody.velocity.y;
			animator.is_grounded = is_grounded || is_in_slope;
			if ( is_not_grounded )
			{
				Vector3 vector_current_direction = new Vector3( 0, 0, current_direction );
				if ( vector_current_direction == wall_direction )
					animator.is_walled = true;
				else
					animator.is_walled = false;
			}
			else
				animator.is_walled = is_walled;
			animator.direction = new Vector3( current_direction, 0, 0 );
		}

		protected virtual void calculate_ground_horizontal_velocity(
			ref Vector3 velocity_vector, Vector3 desire_direction )
		{
			calculate_horizontal_velocity(
				ref velocity_vector, time_to_reach_speed_in_ground, desire_direction );
		}

		protected virtual void calculate_slope_velocity( ref Vector3 velocity_vector )
		{
			Vector3 slope_normal = collision_manager_side_scroll.normal(
				Chibi_collision_side_scroll.STR_SLOPE );

			if ( should_wall_in_slope( velocity_vector, slope_normal ) )
			{
				calculate_walk_in_slope( ref velocity_vector, slope_normal );
				debug.draw.arrow( slope_normal, Color.yellow, duration:1f );
				debug.draw.arrow( velocity_vector, Color.black, duration:1f );
			}
		}

		public virtual Vector3 calculate_walk_in_slope(
			ref Vector3 velocity_vector, Vector3 slope_normal )
		{
			velocity_vector = Vector3.ProjectOnPlane( velocity_vector, slope_normal );
			return velocity_vector;
		}

		protected virtual void calculate_air_horizontal_velocity( 
			ref Vector3 velocity_vector, int direction, Vector3 desire_direction,
			ref float smooth_time )
		{
			calculate_air_horizontal_velocity(
				ref velocity_vector, direction, desire_speed, desire_direction,
				ref smooth_time );
		}

		protected virtual void calculate_air_horizontal_velocity( 
			ref Vector3 velocity_vector, int direction, float desire_speed,
			Vector3 desire_direction, ref float time_smooth )
		{
			if ( is_walled )
				calculate_air_horizontal_velocity_on_wall(
					ref velocity_vector, direction, desire_direction );
			else
				calculate_air_horizontal_velocity_not_on_wall(
					ref velocity_vector, direction, desire_speed,
					desire_direction, ref time_smooth );
		}

		protected virtual void calculate_air_horizontal_velocity_not_on_wall( 
			ref Vector3 velocity_vector, int direction, float desire_speed,
			Vector3 desire_direction, ref float time_smooth )
		{
			Vector3 vector_current_direction = new Vector3( 0, 0, direction );
			if ( doing_wall_jump_climp )
				calculate_horizontal_velocity(
					ref velocity_vector,
					desire_speed * multiplier_velocity_climp_jump,
					this.max_speed, time_to_reach_speed_in_air, desire_direction,
					ref time_smooth );
			else
				calculate_horizontal_velocity(
					ref velocity_vector, desire_speed, this.max_speed,
					time_to_reach_speed_in_air, desire_direction, ref time_smooth );
		}

		protected virtual void calculate_air_horizontal_velocity_on_wall( 
			ref Vector3 velocity_vector, int direction, Vector3 desire_direction )
		{
			Vector3 vector_current_direction = new Vector3( 0, 0, direction );
			if ( vector_current_direction == wall_direction )
				velocity_vector.z = 0;
			else
				calculate_horizontal_velocity(
					ref velocity_vector, time_to_reach_speed_in_air,
					desire_direction );
		}

		protected virtual void calculate_horizontal_velocity(
			ref Vector3 velocity_vector, float time_to_reach_target,
			Vector3 desire_direction )
		{
			calculate_horizontal_velocity(
				ref velocity_vector, this.desire_speed, this.max_speed,
				desire_direction, time_to_reach_target );
		}

		protected virtual void calculate_horizontal_velocity(
			ref Vector3 velocity_vector, float desire_speed,
			Vector3 desire_direction, float time_to_reach_target )
		{
			calculate_horizontal_velocity(
				ref velocity_vector, desire_speed, this.max_speed,
				desire_direction, time_to_reach_target );
		}


		protected virtual void calculate_horizontal_velocity(
			ref Vector3 velocity_vector, float desire_speed,
			float max_speed, Vector3 desire_direction, float time_to_reach_target )
		{
			calculate_horizontal_velocity(
				ref velocity_vector, desire_speed, max_speed,
				time_to_reach_target, desire_direction,
				ref current_horizontal_time_smooth );
		}

		protected virtual void calculate_horizontal_velocity(
			ref Vector3 velocity_vector, float desire_speed,
			float max_speed, float time_to_reach_target,
			Vector3 desire_direction, ref float time_smooth )
		{
			float desire_horizontal_velocity =
				desire_direction.z * Mathf.Clamp( desire_speed, 0, max_speed );
			float current_horizontal_velocity = velocity_vector.z;

			float final_horizontal_velocity = Mathf.SmoothDamp(
				current_horizontal_velocity, desire_horizontal_velocity,
				ref time_smooth, time_to_reach_target );

			velocity_vector.z = final_horizontal_velocity;
		}

		protected virtual void update_change_direction(
			ref Vector3 velocity_vector )
		{
			if ( want_to_move )
			{
				int new_dir = Math.Sign( desire_direction.z );
				if ( new_dir != 0 && new_dir != current_direction )
					current_direction = new_dir;
			}
		}

		public int calcualte_jump_direction()
		{
			int wall_direction = is_walled_left ? -1 : 1;
			var jump_direction = -wall_direction;
			return jump_direction;
		}

		public Vector3 calculate_wall_jump( ref Vector3 velocity_vector )
		{
			int jump_direction = calcualte_jump_direction();
			if ( should_do_wall_jump_climp( jump_direction ) )
			{
				calculate_wall_jump_climp( ref velocity_vector, -jump_direction );
			}
			else if ( should_do_wall_jump_off() )
			{
				calculate_wall_jump_off( ref velocity_vector, -jump_direction );
			}
			else
			{
				calculate_wall_jump_leap( ref velocity_vector, -jump_direction );
			}
			return velocity_vector;
		}

		protected virtual void _process_jump( ref Vector3 speed_vector )
		{
			if ( try_to_jump_the_next_update )
			{
				if ( can_do_wall_jump )
				{
					int jump_direction = is_walled_left ? -1 : 1;
					current_direction = -jump_direction;
					if ( should_do_wall_jump_climp( jump_direction ) )
					{
						doing_wall_jump_climp = true;
						desire_direction = -desire_direction;
						start_to_ignore_input( climp_wall_jump_ignore_input );
						calculate_wall_jump_climp( ref speed_vector, jump_direction );
					}
					else if ( should_do_wall_jump_off() )
					{
						// se pone en cero para no sea alterado por el input
						// del player
						desire_direction = Vector3.zero;
						desire_speed = 0f;
						start_to_ignore_input( wall_jump_off_ignore_input );
						calculate_wall_jump_off( ref speed_vector, jump_direction );
					}
					else
					{
						desire_direction = Vector3.zero;
						desire_speed = 0f;
						start_to_ignore_input( wall_jump_off_ignore_input );
						calculate_wall_jump_leap( ref speed_vector, jump_direction );
					}
				}
				else if ( can_do_jump )
				{
					calculate_regular_jump( ref speed_vector );
					//speed_vector.y = max_jump_velocity;
				}
				try_to_jump_the_next_update = false;
			}
			else if ( should_stop_jump_with_minimal( speed_vector ) )
			{
				speed_vector.y = _min_jump_velocity;
				want_to_stop_jump = false;
			}
			else
			{
				want_to_stop_jump = false;
			}
		}

		public virtual Vector3 calculate_regular_jump( ref Vector3 velocity_vector )
		{
			velocity_vector.y = max_jump_velocity;
			return velocity_vector;
		}

		public virtual Vector3 calculate_wall_jump_climp(
			ref Vector3 velocity_vector, int jump_direction )
		{
			velocity_vector.z = -jump_direction * wall_jump_climp.z;
			velocity_vector.y = climp_wall_jump_velocity;
			return velocity_vector;
		}

		public virtual Vector3 calculate_wall_jump_climp(
			ref Vector3 velocity_vector )
		{
			int jump_direction = calcualte_jump_direction();
			velocity_vector.z = jump_direction * wall_jump_climp.z;
			velocity_vector.y = climp_wall_jump_velocity;
			return velocity_vector;
		}

		public virtual Vector3 calculate_wall_jump_off(
			ref Vector3 velocity_vector, int jump_direction )
		{
			velocity_vector.z = -jump_direction * wall_jump_off.z;
			velocity_vector.y = wall_jump_off.y;
			return velocity_vector;
		}

		public virtual Vector3 calculate_wall_jump_off(
			ref Vector3 velocity_vector )
		{
			int jump_direction = calcualte_jump_direction();
			velocity_vector.z = jump_direction * wall_jump_off.z;
			velocity_vector.y = wall_jump_off.y;
			return velocity_vector;
		}

		public virtual Vector3 calculate_wall_jump_leap(
			ref Vector3 velocity_vector, int jump_direction )
		{
			velocity_vector.z = -jump_direction * wall_jump_leap.z;
			velocity_vector.y = wall_jump_leap.y;
			return velocity_vector;
		}
		public virtual Vector3 calculate_wall_jump_leap(
			ref Vector3 velocity_vector )
		{
			int jump_direction = calcualte_jump_direction();
			velocity_vector.z = jump_direction * wall_jump_leap.z;
			velocity_vector.y = wall_jump_leap.y;
			return velocity_vector;
		}

		protected override void _proccess_gravity( ref Vector3 velocity_vector )
		{

			if ( is_not_grounded && is_walled )
				calculate_gravity_when_wall_slice( ref velocity_vector, Time.deltaTime );
			else
			{
				if ( velocity_vector.y > 0 )
					if ( doing_wall_jump_climp )
						calculate_gravity_after_wall_jump_climp( ref velocity_vector, Time.deltaTime );
					else
						calculate_normal_gravity( ref velocity_vector, Time.deltaTime );
				else
				{
					calculate_falling_gravity( ref velocity_vector, Time.deltaTime );
					doing_wall_jump_climp = false;
					//stop_to_ignore_input();
				}
			}
		}

		public virtual void calculate_gravity(
			ref Vector3 velocity_vector, float delta_time )
		{

			if ( is_not_grounded && is_walled )
				calculate_gravity_when_wall_slice( ref velocity_vector, delta_time );
			else
			{
				if ( velocity_vector.y > 0 )
					if ( doing_wall_jump_climp )
						calculate_gravity_after_wall_jump_climp( ref velocity_vector, delta_time );
					else
						calculate_normal_gravity( ref velocity_vector, delta_time );
				else
				{
					calculate_falling_gravity( ref velocity_vector, delta_time );
				}
			}
		}

		/// <summary>
		/// gravedad normal se calcula cuando esta saltando
		/// </summary>
		/// <param name="velocity_vector"></param>
		/// <returns></returns>
		public virtual Vector3 calculate_normal_gravity(
			ref Vector3 velocity_vector, float delta_time )
		{
			velocity_vector.y += gravity * delta_time;
			return velocity_vector;
		}

		public virtual Vector3 calculate_gravity_after_wall_jump_climp(
			ref Vector3 velocity_vector, float delta_time )
		{
			velocity_vector.y += grabity_after_wall_jump_climp * delta_time;
			return velocity_vector;
		}

		public virtual Vector3 calculate_gravity_when_wall_slice(
			ref Vector3 velocity_vector, float delta_time )
		{
			velocity_vector.y += gravity * multiplier_velocity_wall_slice * delta_time;
			return velocity_vector;
		}

		public virtual Vector3 calculate_falling_gravity(
			ref Vector3 velocity_vector, float delta_time )
		{
			velocity_vector.y += gravity_when_fall * delta_time;
			return velocity_vector;
		}

		protected override void _init_cache()
		{
			base._init_cache();
			update_jump_properties();
			if ( !animator )
				debug.error( "no esta asignado el animator" );
		}

		protected virtual void update_jump_properties()
		{
			gravity = -( 2 * max_jump_heigh ) / ( jump_time * jump_time );
			gravity_when_fall = -( 2 * max_jump_heigh ) / ( falling_time * falling_time );
			grabity_after_wall_jump_climp  =
				-( 2 * wall_climp_jump_heigh )
				/ ( jump_time_wall_climp * jump_time_wall_climp );

			_max_jump_velocity = Math.Abs( _gravity ) * jump_time;
			_min_jump_velocity = ( float )Math.Sqrt(
				2.0 * Math.Abs( _gravity ) * min_jump_heigh );
			_climp_wall_jump_velocity =
				Math.Abs( grabity_after_wall_jump_climp ) * jump_time_wall_climp;
		}

		public void start_jump()
		{
			try_to_jump_the_next_update = true;
		}

		public void end_jump()
		{
			try_to_jump_the_next_update = false;
			want_to_stop_jump = true;
		}

		public virtual bool should_finish_jump( Vector3 velocity_vector )
		{
			return try_to_jump_the_next_update && velocity_vector.y < 0.01;
		}

		public virtual bool should_do_wall_jump_climp( int jump_direction )
		{
			return Math.Sign( desire_direction.z ) == jump_direction;
		}

		public virtual bool should_do_wall_jump_off()
		{
			return desire_direction.z == 0;
		}

		public virtual bool should_stop_jump_with_minimal( Vector3 velocity_vector )
		{
			return want_to_stop_jump && velocity_vector.y > _min_jump_velocity;
		}

		public virtual bool should_wall_in_slope( Vector3 velocity_vector, Vector3 slope_normal )
		{
			return slope_normal != Vector3.zero && ( -0.1 > velocity_vector.z || velocity_vector.z < 0.1 );
		}

		public virtual bool should_calcutate_gravity_in_slope( Vector3 velocity_vector )
		{
			return -0.001 > velocity_vector.z && velocity_vector.z > 0.01f;
		}

		public virtual bool calcualte_want_to_move( Vector3 desire_direction )
		{
			return desire_direction.z > 0.01 || desire_direction.z < -0.01;
		}
	}
}
