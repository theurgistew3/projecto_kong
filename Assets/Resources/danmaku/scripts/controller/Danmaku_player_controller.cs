using UnityEngine;

namespace danmaku.controller.player
{
	public class Danmaku_player_controller : chibi.controller.Controller
	{
		public npc.Touha_controller touha_controller;

		public override Vector3 desire_direction
		{
			get
			{
				return touha_controller.desire_direction;
			}
			set
			{
				touha_controller.desire_direction = value;
			}
		}

		public override float speed
		{
			get
			{
				return touha_controller.speed;
			}
			set
			{
				if ( value > 0 )
					touha_controller.speed = touha_controller.max_speed;
				else
					touha_controller.speed = 0f;
			}
		}

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !touha_controller )
				debug.error( "no esta asignado el touha controller" );
		}

		public override void action( string name, string e )
		{
			switch ( name )
			{
				case "fire1":
					switch ( e )
					{
						case chibi.joystick.events.down:
							touha_controller.automatic_shot = true;
							break;
						case chibi.joystick.events.up:
							touha_controller.automatic_shot = false;
							break;
					}
					break;
			}
		}
	}
}
