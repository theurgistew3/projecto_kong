using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using chibi.damage.motor;

namespace chibi.controller
{
	public class Check_score : Chibi_behaviour
	{
		public GameObject win_1, win_2;
		public bool end_game;

		public List<HP_engine> dodgers_player_1;
		public List<HP_engine> dodgers_player_2;

		private void Update()
		{
			if ( end_game && Input.anyKey )
			{
				SceneManager.LoadScene( "Resources/SMKD/scene/game_mode_mvp" );
			}

			if ( dodgers_player_1.All( x => x.is_dead ) )
			{
				win_2.SetActive( true );
				end_game = true;
			}

			if ( dodgers_player_2.All( x => x.is_dead ) )
			{
				win_1.SetActive( true );
				end_game = true;
			}
		}
	}
}
