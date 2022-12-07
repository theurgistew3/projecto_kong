using UnityEngine;
using System.Collections.Generic;
using chibi.controller;
using UnityEngine.InputSystem;


namespace chibi.joystick
{
	public class Joystick : chibi.Chibi_behaviour
	{
		#region public vars
		public string key_map = "player 1";
		public platformer.input.Platformer control;
		public Controller controller;

		public Axis desire_direction;

		public List<string> actions = new List<string>() {
			"fire1", "fire2", "fire3", };
		public List<Axis> axis_actions = new List<Axis>() {};
		private List<bool> axis_is_up;

		public List<Axis> axis = new List<Axis>();
		#endregion

		#region protected vars
		protected Vector3 _desire_direction = Vector3.zero;
		protected float _desire_speed = 0f;
		#endregion

		#region public properties
		#endregion

		#region public functions
		public void update_all_axis()
		{
			desire_direction.update();
		}
		#endregion

		public void on_move( InputAction.CallbackContext context )
		{
			var vector = context.ReadValue<Vector2>();
			_desire_direction = new Vector3( vector.y, 0, vector.x );
			if ( 0.1f < _desire_direction.magnitude )
				_desire_speed = 1f;
			else
				_desire_speed = 0f;
		}
		public void on_horizontal_spawn( InputAction.CallbackContext context )
		{
			debug.log( "horizontal" );
			if ( context.started )
				controller.action( "p1__bumper__left", chibi.joystick.events.down );
		}

		public void on_vertical_spawn( InputAction.CallbackContext context )
		{
			debug.log( "vertical" );
			if ( context.started )
				controller.action( "p1__trigger__right", chibi.joystick.events.down );
		}

		public void on_jump( InputAction.CallbackContext context )
		{
			if ( context.started )
			{
				debug.log( "jump" );
				controller.action( "jump", chibi.joystick.events.down );
			}
			else if ( context.canceled )
			{
				debug.log( "cancel jump" );
				controller.action( "jump", chibi.joystick.events.up );
			}
		}

		#region funciones protegdas
		protected void Update()
		{
			controller.desire_direction = _desire_direction;
			controller.speed = _desire_speed;
			/*
			update_all_axis();
			if ( desire_direction.pass_dead_zone )
			{
				controller.desire_direction = desire_direction.vector;
				controller.speed = 1f;
			}
			else
			{
				controller.desire_direction = Vector3.zero;
				controller.speed = 0f;
			}

			foreach ( Axis axi in axis )
			{
				axi.update();
				if ( axi.pass_dead_zone )
					controller.direction( axi );
			}

			foreach ( string action in actions )
			{
				if ( check_action_down( action ) )
					controller.action( action, "down" );
				if ( check_action_up( action ) )
					controller.action( action, "up" );
			}

			for ( int i = 0; i < axis_actions.Count; ++i )
			{
				var axis = axis_actions[ i ];
				axis.update();
				if ( axis_is_up[ i ] )
				{
					if ( !axis.pass_dead_zone )
					{
						controller.action( axis.name, "up" );
						axis_is_up[ i ] = false;
					}
				}
				else
				{
					if ( axis.pass_dead_zone )
					{
						controller.action( axis.name, "down" );
						axis_is_up[ i ] = true;
					}
				}
			}
			*/
			/*
			debug.log(
				"{0} {1}",
				Input.GetAxis( "p1__trigger__left" ),
				Input.GetAxis( "p1__trigger__right" ) );

			debug.log(
				"{0} {1}",
				Input.GetButton( "p1__bumper__left" ),
				Input.GetButton( "p1__bumper__right" ) );
			*/
		}

		/// <summary>
		/// inicializa el chache del script
		/// </summary>
		protected override void _init_cache()
		{
			if ( controller == null )
				controller = GetComponent<Controller>();
			if ( !desire_direction )
			{
				debug.error( "no hay un axis de desire_direction" );
			}
			axis_is_up = new List<bool>( axis_actions.Count );
			for ( int i = 0; i < axis_actions.Count; ++i )
				axis_is_up.Add( false );

			control.Enable();
			control.Player.Move.performed += on_move;
			control.Player.Move.canceled += on_move;
			control.Player.Move.started += on_move;

			control.Player.jump.performed += on_jump;
			control.Player.jump.canceled += on_jump;
			control.Player.jump.started += on_jump;

			control.Player.spawn_horizontal_platformer.performed += on_horizontal_spawn;
			control.Player.spawn_horizontal_platformer.canceled += on_horizontal_spawn;
			control.Player.spawn_horizontal_platformer.started += on_horizontal_spawn;

			control.Player.spawn_vertical_platformer.performed += on_vertical_spawn;
			control.Player.spawn_vertical_platformer.canceled += on_vertical_spawn;
			control.Player.spawn_vertical_platformer.started += on_vertical_spawn;
		}

		protected override void _dispose_cache()
		{
			base._dispose_cache();
			control.Disable();
			control.Player.Move.performed -= on_move;
			control.Player.Move.canceled -= on_move;
			control.Player.Move.started -= on_move;

			control.Player.jump.performed -= on_jump;
			control.Player.jump.canceled -= on_jump;
			control.Player.jump.started -= on_jump;

			control.Player.spawn_horizontal_platformer.performed -= on_horizontal_spawn;
			control.Player.spawn_horizontal_platformer.canceled -= on_horizontal_spawn;
			control.Player.spawn_horizontal_platformer.started -= on_horizontal_spawn;

			control.Player.spawn_vertical_platformer.performed -= on_vertical_spawn;
			control.Player.spawn_vertical_platformer.canceled -= on_vertical_spawn;
			control.Player.spawn_vertical_platformer.started -= on_vertical_spawn;
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

		protected override void Awake()
		{
			base.Awake();
			control = new platformer.input.Platformer();
		}

	}
}
