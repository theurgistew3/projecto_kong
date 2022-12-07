using UnityEngine;
using System.Collections.Generic;

namespace chibi.joystick
{
	public class Joystick_fisher : chibi.Chibi_behaviour
	{
		#region public vars
		public string key_map = "player 1";
		public fisher.controller.Fisher_controller controller;
		public Camera main_camara;

		public Vector3 axis_mouse = Vector2.zero;
		public Vector3 axis_esdf = Vector2.zero;
		public Vector3 mouse_position = Vector2.zero;
		public float mouse_wheel = 0f;

		public bool run_key = false;
		public bool jump_key = false;
		public bool jump_key_release = false;

		public float dead_zone_esdf_axis = 0.01f;
		public float dead_zone_mouse_axis = 0.01f;
		public float dead_zone_mouse_wheel = 0.01f;

		public List<string> actions = new List<string>() {
			"fire1",
			"fire2",
			"fire3",
		};
		#endregion

		#region public properties
		public bool is_pass_deadzone_esdf_axis
		{
			get;
			protected set;
		}
		public bool is_pass_deadzone_mouse_axis
		{
			get;
			protected set;
		}
		public bool is_pass_deadzone_mouse_wheel
		{
			get;
			protected set;
		}
		#endregion

		#region public functions
		public void update_all_axis()
		{
			_get_axis_esdf();
			_get_axis_mouse();
			_get_mouse_pos();
		}

		public void update_all_buttons()
		{
			_get_keys_running();
			_get_keys_jump();
			_get_key_jump_is_release();
		}
		#endregion

		#region funciones protegdas
		protected void Update()
		{
			update_all_axis();
			update_all_buttons();
			// si pasa la zona muerta el stick entonces se mueve
			// y cambia la direcion
			if ( is_pass_deadzone_esdf_axis )
			{
				controller.desire_direction = axis_esdf;
				controller.speed = 1f;
			}
			else
			{
				controller.desire_direction = Vector3.zero;
				controller.speed = 0f;
			}
			foreach ( string action in actions )
			{
				if ( check_action_down( action ) )
					controller.action( action, "down" );
				if ( check_action_up( action ) )
					controller.action( action, "up" );
			}

			if ( Input.GetButtonDown( "fire1" ) )
			{
				RaycastHit hit;
				var ray = main_camara.ScreenPointToRay( mouse_position );
				bool has_a_hit = Physics.Raycast( ray, out hit );
				if ( has_a_hit )
				{
					controller.throw_net( hit.point );
				}
			}
		}

		/// <summary>
		/// actualiza el eje de movimiento
		/// </summary>
		protected void _get_axis_esdf()
		{
			axis_esdf = helper.joystick.axis_left;
			is_pass_deadzone_esdf_axis = helper.joystick.pass_dead_zone(
				axis_esdf.magnitude, dead_zone_esdf_axis );
		}

		/// <summary>
		/// revisa si se preciono el boton para correr
		/// </summary>
		protected void _get_keys_running()
		{
			run_key = Input.GetButton( "run" );
		}

		/// <summary>
		/// revisa si se preciono el boton para saltar
		/// </summary>
		protected void _get_keys_jump()
		{
			jump_key = Input.GetButton( "jump" );
		}

		/// <summary>
		/// revisa si se dejo de precionar el boton de salto
		/// </summary>
		protected void _get_key_jump_is_release()
		{
			jump_key_release = Input.GetButtonUp( "jump" );
		}

		/// <summary>
		/// revisa si el boton de fire se preciono, no es un auto fire
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		protected bool _fire_key_down( string action )
		{
			return Input.GetButtonDown( action );
		}

		/// <summary>
		/// revisa si el boton de fire se libero
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		protected bool _fire_key_up( string action )
		{
			return Input.GetButtonUp( action );
		}

		protected bool _left_bumper_key_down()
		{
			return Input.GetButtonDown( "left_bumper" );
		}

		protected bool _right_bumper_key_down()
		{
			return Input.GetButtonDown( "right_bumper" );
		}

		/// <summary>
		/// actualiza el eje de movimiento del mouse
		/// </summary>
		protected void _get_axis_mouse()
		{
			axis_mouse.x = helper.mouse.axis_x;
			axis_mouse.y = helper.mouse.axis_y;
			mouse_wheel = helper.mouse.wheel;
			is_pass_deadzone_mouse_axis = helper.joystick.pass_dead_zone( axis_mouse.magnitude, dead_zone_mouse_axis );
			is_pass_deadzone_mouse_wheel = helper.joystick.pass_dead_zone( mouse_wheel, dead_zone_mouse_wheel );
		}

		/// <summary>
		/// actualiza la posicion del mouse
		/// </summary>
		protected void _get_mouse_pos()
		{
			mouse_position = Input.mousePosition;
		}

		/// <summary>
		/// inicializa el chache del script
		/// </summary>
		protected override void _init_cache()
		{
			_init_cache_controller();
		}

		/// <summary>
		/// inicia el cache del controller
		/// </summary>
		protected virtual void _init_cache_controller()
		{
		}

		/// <summary>
		/// dibuga el debug
		/// </summary>
		protected virtual void _draw_debug()
		{
			debug.draw.arrow( axis_esdf );
		}

		protected virtual bool check_action_down( string action )
		{
			return Input.GetButtonDown( action );
		}

		protected virtual bool check_action_up( string action )
		{
			return Input.GetButtonUp( action );
		}
		#endregion
	}
}
