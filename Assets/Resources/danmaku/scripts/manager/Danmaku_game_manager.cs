using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using danmaku.controller.player;
using danmaku.controller.npc;
using chibi.damage.motor;

namespace danmaku.game_manager
{
	public class Danmaku_game_manager : chibi.Chibi_behaviour
	{
		[ Header( "joystick" ) ]
		public chibi.joystick.Joystick joystick;
		public Danmaku_player_controller player_controller;

		[ Header( "player" ) ]
		public int lives;
		public chibi.tool.reference.Game_object_reference player_reference;
		public GameObject player_prefab;
		public Transform start_point;
		public float respawn_time = 3f;

		protected IEnumerator __respawn_player;

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !joystick )
				debug.error( "no esta asignado el joytick en el manager" );

			if ( !start_point )
				debug.error( "no esta asignado el punto de inicio del player" );

			if ( !player_controller )
				debug.error( "no esta asignado el player controller" );

			instance_player();
		}

		protected void instance_player()
		{
			var new_player = helper.instantiate._(
				player_prefab, start_point.position );

			player_controller.touha_controller = new_player.GetComponent<
				Touha_controller >();
			player_reference.Value = new_player;
			var hp = new_player.GetComponent<HP_engine>();
			hp.on_died += on_player_died;
			joystick.enabled = true;
		}

		protected void on_player_died()
		{
			debug.log( "player murio" );
			joystick.enabled = false;
			__respawn_player = respawn_player();
			StartCoroutine( __respawn_player );
		}

		protected virtual IEnumerator respawn_player()
		{
			yield return new WaitForSeconds( respawn_time );
			instance_player();
		}
	}
}
