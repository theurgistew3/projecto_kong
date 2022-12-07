using UnityEngine;
using System.Collections.Generic;
using chibi.controller;

namespace chibi.joystick
{
	public class Joystick_SMKD : chibi.Chibi_behaviour
	{
		#region public vars
		public string key_map = "p1";
		public Controller controller;

		public Vector3 axis_esdf = Vector2.zero;

		public float dead_zone_esdf_axis = 0.01f;

		public List<string> actions = new List<string>() {
			"shot", "catch",
		};
		#endregion

		#region public properties
		public bool is_pass_deadzone_esdf_axis
		{
			get;
			protected set;
		}
		#endregion

		#region public functions
		public void update_all_axis()
		{
			_get_axis_esdf();
		}
		#endregion

		#region funciones protegdas
		protected void Update()
		{
			update_all_axis();
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
				string action_input = build_input_string( action );
				if ( check_action_down( action_input ) )
					controller.action( action, "down" );
				if ( check_action_up( action_input ) )
					controller.action( action, "up" );
			}
			_draw_debug();
		}

		/// <summary>
		/// actualiza el eje de movimiento
		/// </summary>
		protected void _get_axis_esdf()
		{

			string vertical_string = build_input_string( "vertical" );
			string horizontal_string = build_input_string( "horizontal" );
			var vertial = Input.GetAxisRaw( vertical_string );
			var horizontal = Input.GetAxisRaw( horizontal_string );
			axis_esdf = new Vector3( horizontal, 0, vertial );
			is_pass_deadzone_esdf_axis = helper.joystick.pass_dead_zone(
				axis_esdf.magnitude, dead_zone_esdf_axis );
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
			if ( controller == null )
				controller = GetComponent<Controller>();
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

		protected virtual string build_input_string( string input )
		{
			return string.Format( "{0}__{1}", key_map, input );
		}
		#endregion
	}
}
