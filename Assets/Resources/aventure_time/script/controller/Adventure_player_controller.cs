using UnityEngine;
using chibi.controller.npc;
using System.Collections.Generic;
using platformer.controller.platform;

namespace platformer.controller.player
{
	public class Adventure_player_controller : chibi.controller.Controller
	{
		public Controller_npc player;

		public override Vector3 desire_direction
		{
			get {
				return player.desire_direction;
			}
			set {
				player.desire_direction = value;
			}
		}

		public override float speed
		{
			get {
				return player.speed;
			}
			set {
				player.speed = player.max_speed * value;
			}
		}

		protected void Update()
		{
		}

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !player )
				debug.error( "no esta asignado el player controller" );
			else
			{
				player.player_setup();
			}
		}

		public virtual void action( string name, string e, bool buffer )
		{
			switch ( name )
			{
				case "fire1":
					switch ( e )
					{
						case chibi.joystick.events.down:
							break;
						case chibi.joystick.events.up:
							break;
					}
					break;
				case "jump":
					switch ( e )
					{
						case chibi.joystick.events.down:
							player.jump();
							break;
						case chibi.joystick.events.up:
							player.stop_jump();
							break;
					}
					break;
			}
		}

		public override void action( string name, string e )
		{
			base.action( name, e );
			switch ( name )
			{
				case "fire1":
					action( name, e, true );
					break;
				case "jump":
					action( name, e, true );
					break;
				case "p1__bumper__left":
				case "p1__bumper__right":
				case "p1__trigger__left":
				case "p1__trigger__right":
					break;
				case "action":
					switch ( e )
					{
						case chibi.joystick.events.down:
							player.action();
							break;
						case chibi.joystick.events.up:
							player.stop_action();
							break;
					}
					break;
			}
		}

		public override void direction( string name, Vector3 direction )
		{
			base.direction( name, direction );
			switch ( name )
			{
				case "p1__aim":
					break;
			}
		}
	}
}
