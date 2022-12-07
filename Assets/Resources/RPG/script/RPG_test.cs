using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;
using chibi.rol_sheet.motor;

namespace rpg.manager
{
	public class RPG_test: chibi.Chibi_behaviour
	{
		public GameObject player, enemy;
		public RPG_battle_motor player_motor, enemy_motor;

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !player )
			{
				debug.error( "no se asigno el player" );
			}
			if ( !player_motor )
			{
				debug.info( "buscanod player motor" );
				player_motor = player.GetComponentInChildren<RPG_battle_motor>();
			}
			if ( !player_motor )
			{
				debug.error( "no se encontro el rpg_battle_motor en {0}", player );
			}
			if ( !enemy )
			{
				debug.error( "n ose asigno el enemy" );
			}
			if ( !enemy_motor )
			{
				debug.info( "buscanod enemy motor" );
				enemy_motor = enemy.GetComponentInChildren<RPG_battle_motor>();
			}
			if ( !enemy_motor )
			{
				debug.error( "no se encontro el rpg_battle_motor en {0}", enemy );
			}
		}

		public void on_attack()
		{
			this.debug.info( "ataco" );
			var player_damage = player_motor.get_attack_damage();
			enemy_motor.take_damage( player_damage );
		}
		public void on_defend()
		{
			this.debug.info( "defendio" );
		}

		public void on_run()
		{
			this.debug.info( "corrio" );
		}

	}
}
