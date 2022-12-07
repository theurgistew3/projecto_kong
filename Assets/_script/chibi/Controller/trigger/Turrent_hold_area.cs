using UnityEngine;

namespace chibi.controller.trigger
{
	public class Turrent_hold_area : chibi.Chibi_behaviour
	{
		public weapon.gun.turrent.Controller_turrent turrent;
		public Transform hold_position;

		protected virtual void OnTriggerEnter( Collider other )
		{
			Debug.Log(
				string.Format(
					"[Turrent_hold_area] entro en el area {0}",
					helper.game_object.name.full( other.gameObject ) ) );
			var soldier = other.GetComponent<npc.Soldier_controller>();
			if ( soldier )
			{
				soldier.turrent = turrent;
				soldier.hold_turrent_position = hold_position;
			}
		}

		protected virtual void OnTriggerExit( Collider other )
		{
			Debug.Log(
				string.Format(
					"[Turrent_hold_area] salio del area {0}",
					helper.game_object.name.full( other.gameObject ) ) );
			var soldier = other.GetComponent<npc.Soldier_controller>();
			if ( soldier )
			{
				if ( soldier.turrent == turrent )
				{
					soldier.turrent = null;
					soldier.hold_turrent_position = null;
				}
			}
		}
	}
}