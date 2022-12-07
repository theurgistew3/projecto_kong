using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;

namespace tactic.grid
{
	public class Grid : chibi.Chibi_behaviour
	{
		public obj.Grid<bool> grid;
		public Camera camera;

		protected override void _init_cache()
		{
			base._init_cache();
			grid = new obj.Grid<bool>( 3, 3, 10, transform.position );

			//grid[ 1, 1 ] = 33;
		}

		protected void Update()
		{
			if ( UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame )
			{
				Vector3 position_mouse = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
				var position = helper.camera.get_mouse_to_world_position(
					position_mouse, camera );
				var camera_ray = camera.ScreenPointToRay( position_mouse );
				var difference_camera_object = transform.position - camera_ray.origin;
				var total_direction = (
					camera_ray.direction * difference_camera_object.magnitude );

				debug.draw.arrow(
					camera_ray.origin, total_direction, Color.green, 10f );

				position = camera_ray.origin + total_direction;
				int x, y;
				grid.get_x_y_from_world( position, out x, out y );
				if ( x >= 0 && y >= 0 && x < grid.width && y < grid.height )
				{
					grid[ x, y ] = !grid[ x, y ];
				}
			}
		}

	}
}
