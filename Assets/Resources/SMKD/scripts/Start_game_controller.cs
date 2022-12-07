using UnityEngine;
using chibi.controller.weapon.gun.bullet;

namespace chibi.controller
{
	public class Start_game_controller: Chibi_behaviour
	{
		public SMKD.controller.npc.Dodger_controller dodger_start;
		public Controller_bullet bullet;

		public float start_time = 2f;
		public float _delta_time = 0f;


		private void Update()
		{
			_delta_time += Time.deltaTime;

			if ( _delta_time > start_time )
			{
				dodger_start.grab_ball( bullet.transform );
				Destroy( gameObject );
			}
		}

	}
}
