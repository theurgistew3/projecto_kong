using UnityEngine;
using System.Collections.Generic;
using System;
using chibi.motor;
using chibi.motor.npc;

namespace chibi.manager.collision
{
	public class Chibi_collision_side_scroll : Chibi_collision_manager
	{
		public static string STR_WALL = "wall";
		public static string STR_WALL_left = "wall left";
		public static string STR_WALL_right = "wall right";
		public static string STR_FLOOR = "floor";
		public static string STR_SLOPE = "SLOPE";
		public static string STR_CEIL = "ceil";
		public static string STR_SLOPE_CEIL = "slope ceil";

		public float floor_anlge = 5f;

		public float max_slope_floor_angle = 45f;
		public float max_slope_ceil_angle = 45f;

		public virtual bool is_grounded
		{
			get { return this[ STR_FLOOR ]; }
		}

		public virtual bool is_not_grounded
		{
			get { return !is_grounded; }
		}

		public virtual bool is_in_slope
		{
			get { return this[ STR_SLOPE ]; }
		}

		public virtual bool is_ceiled
		{
			get { return this[ STR_CEIL ]; }
		}

		public virtual bool is_not_ceiled
		{
			get { return !is_ceiled; }
		}

		public virtual bool is_walled
		{
			get { return this[ STR_WALL ]; }
		}

		public virtual bool is_not_walled
		{
			get { return !is_walled; }
		}

		public virtual bool is_walled_left
		{
			get { return this[ STR_WALL_left ]; }
		}

		public virtual bool is_walled_right
		{
			get { return this[ STR_WALL_right ]; }
		}

		public virtual bool no_is_walled_left
		{
			get { return !is_walled_left; }
		}

		public virtual bool no_is_walled_right
		{
			get { return !is_walled_right; }
		}


		protected override void _process_collision_scenary( Collision collision )
		{
			foreach ( ContactPoint contact in collision.contacts )
			{
				// si es piso
				float y = contact.normal.y;
				if ( y > 0.01f )
				{
					if ( _proccess_floor( contact, collision ) )
						break;
				}
				// si es pared
				else if ( y > -0.01 && y < 0.01 )
				{
					if ( _proccess_wall( contact, collision ) )
						break;
				}
				// si es techo
				else if ( y < -0.01 )
				{
					if ( _proccess_ceil( contact, collision ) )
						break;
				}
				// no se que paso
				else
				{
					debug.error(
						"no esta manejando este caso en en el "
						+ "manager de las coliciones" );
					debug.draw.arrow(
						contact.point, contact.normal, Color.green, 5f );
					debug.pause();
				}
			}

		}

		protected virtual bool _proccess_floor(
			ContactPoint contact, Collision collision )
		{
			debug.draw.arrow(
				contact.point, contact.normal, Color.blue, 5f );
			debug.draw.arrow(
				contact.point, Vector3.up, Color.magenta, 5f );

			var slope_angle = Vector3.Angle( contact.normal, Vector3.up );
			if ( -floor_anlge <= slope_angle && slope_angle <= floor_anlge )
			{
				manager_collisions.add( new manager.collision.Collision_info(
					STR_FLOOR, collision, slope_angle ) );
				return true;
			}
			// debug.draw.arrow( contact.point, contact.normal, Color.cyan, duration:1f );
			// Debug.Break();
			manager_collisions.add( new manager.collision.Collision_info(
				STR_SLOPE, collision, slope_angle ) );
			return true;
		}

		protected virtual bool _proccess_wall(
			ContactPoint contact, Collision collision )
		{
			debug.draw.arrow(
				contact.point, contact.normal, Color.red, 5f );
			debug.draw.arrow(
				contact.point, Vector3.up, Color.magenta, 5f );

			var slope_angle = Vector3.Angle( contact.normal, Vector3.up );

			manager_collisions.add( new Collision_info(
				STR_WALL, collision, slope_angle ) );
			if ( contact.normal.z > 0 )
				manager_collisions.add( new Collision_info(
					STR_WALL_left, collision, slope_angle ) );
			else if ( contact.normal.z < 0 )
				manager_collisions.add( new Collision_info(
					STR_WALL_right, collision, slope_angle ) );
			return true;
		}

		protected virtual bool _proccess_ceil(
			ContactPoint contact, Collision collision )
		{
			debug.draw.arrow(
				contact.point, contact.normal, Color.red, 5f );
			debug.draw.arrow(
				contact.point, Vector3.down, Color.magenta, 5f );

			var slope_angle = Vector3.Angle( contact.normal, Vector3.down );
			if ( -floor_anlge <= slope_angle && slope_angle <= floor_anlge )
			{
				manager_collisions.add( new manager.collision.Collision_info(
					STR_SLOPE_CEIL, collision, slope_angle ) );
				return true;
			}
			manager_collisions.add( new manager.collision.Collision_info(
				STR_CEIL, collision, slope_angle ) );
			return true;
		}
	}
}
