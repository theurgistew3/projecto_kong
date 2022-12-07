using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using chibi.controller.npc;
using chibi.actuator;

namespace tests.controller.npc.soldier.side_scroll
{
	public class Actutor : helper.tests.Scene_test
	{
		Actuator up, down, left, right;
		chibi.controller.npc.Soldier_controller npc;

		public override string scene_dir
		{
			get {
				return
					"tests/scene/chibi/controller/soldier/" +
					"soldier side scroll actuator";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			( up, down, left, right ) = 
				helper.game_object.Find._<Actuator>(
					scene, "up", "down", "left", "right" );

			npc = helper.game_object.Find._<Soldier_controller>( scene, "npc" );
		}

		[UnityTest]
		public IEnumerator should_can_active_the_actuator()
		{
			npc.transform.position = up.transform.position;
			yield return new WaitForSeconds( 1 );
			npc.activate();
		}
	}
}
