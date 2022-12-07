using UnityEngine;
using chibi.controller.npc;
using System.Collections.Generic;
using platformer.motor.platform;

namespace platformer.controller.platform
{
	public class Platform_bender_controller : chibi.controller.Controller
	{
		public Transform default_vertical_position;
		public Transform default_horizontal_position;

		public Platform_motor vertical_platform;
		public Platform_motor horizontal_platform;

		public float size_of_the_delta_time = 0.1f;
		public int steps = 3;

		public chibi.motor.npc.Motor_side_scroll motor;

		public Vector3 position 
		{
			get {
				return new Vector3();
			}
		}

		public Vector3 postion_when_falling()
		{
			var air_postions_prediction = new List<Vector3>();
			float smooth_time = 0f;
			Vector3 desire_direction = new Vector3( 0, 0, motor.current_direction );
			motor.calculate_motion(
				size_of_the_delta_time, steps, ref air_postions_prediction,
				desire_direction, ref smooth_time );
			foreach ( Vector3 step in air_postions_prediction )
			{
				debug.draw.sphere( step, Color.black, 0.1f );
			}
			return air_postions_prediction[ air_postions_prediction.Count - 1 ];
		}

		public Vector3 postion_when_upper_jump()
		{
			var air_postions_prediction = new List<Vector3>();
			float smooth_time = 0f;
			Vector3 desire_direction = new Vector3( 0, 0, motor.current_direction );
			motor.calculate_motion(
				size_of_the_delta_time, steps, ref air_postions_prediction,
				desire_direction, ref smooth_time );
			foreach ( Vector3 step in air_postions_prediction )
			{
				debug.draw.sphere( step, Color.black, 0.1f );
			}
			return air_postions_prediction[ air_postions_prediction.Count - 1 ];
		}
		public Vector3 postion_when_is_in_air()
		{
			var air_postions_prediction = new List<Vector3>();
			float smooth_time = 0f;
			Vector3 desire_direction = new Vector3( 0, 0, motor.current_direction );
			motor.calculate_motion(
				size_of_the_delta_time, steps, ref air_postions_prediction,
				desire_direction, ref smooth_time );
			foreach ( Vector3 step in air_postions_prediction )
			{
				debug.draw.sphere( step, Color.black, 0.1f );
			}
			return air_postions_prediction[ air_postions_prediction.Count - 1 ];
		}

		public Vector3 position_when_wall_jump()
		{
			var air_postions_prediction = new List<Vector3>();
			Vector3 velocity_vector = Vector3.zero;
			motor.calculate_wall_jump_off( ref velocity_vector );
			float smooth_time = 0f;
			int jump_direction = motor.calcualte_jump_direction();
			Vector3 desire_direction = new Vector3( 0, 0, jump_direction );
			float desire_speed = motor.max_speed;
			bool is_walled = false;
			motor.calculate_motion( 
				size_of_the_delta_time, steps, ref air_postions_prediction,
				jump_direction, velocity_vector, desire_speed, desire_direction,
				is_walled, ref smooth_time );
			foreach ( Vector3 step in air_postions_prediction )
			{
				debug.draw.sphere( step, Color.black, 0.1f );
			}
			return air_postions_prediction[ air_postions_prediction.Count - 1 ];
		}

		public void spawn_vertical()
		{
			debug.info( "spawn vertical" );
			if ( motor.is_not_grounded )
			{
				if ( motor.is_not_walled )
				{
					var position = postion_when_is_in_air();
					instanciate_vertical_platform( position );
				}
				else
				{
					var position = position_when_wall_jump();
					instanciate_vertical_platform( position );
				}
			}
			else
			{
				debug.info( "no vertical spwan" );
			}
		}

		public void spawn_horizontal()
		{
			debug.info( "spawn horizontal" );
			if ( motor.is_not_grounded )
			{
				if ( motor.is_not_walled )
				{
					var position = postion_when_is_in_air();
					instanciate_horizontal_platform( position );
				}
				else
				{
					var position = position_when_wall_jump();
					instanciate_horizontal_platform( position );
				}
			}
			else
			{
				debug.info( "no horizontal spwan" );
			}
		}

		public void instanciate_vertical_platform( Vector3 position )
		{
			var platform = helper.instantiate._<Platform_motor>(
				vertical_platform, position );
			platform.owner = motor.gameObject;
		}

		public void instanciate_horizontal_platform( Vector3 position )
		{
			var platform = helper.instantiate._<Platform_motor>(
				horizontal_platform, position );
			platform.owner = motor.gameObject;
		}

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !motor )
				debug.error( "no tiene un motor para calcular los spawn de las plataformas" );
		}

		protected void Update()
		{
			if ( motor.is_not_grounded )
			{
				if ( motor.is_not_walled )
				{
					var position = postion_when_is_in_air();
				}
				else
				{
					var position = position_when_wall_jump();
				}
			}
		}
	}
}
