using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using helper.test.assert;
using chibi.controller.npc;
using chibi.actuator;

namespace tests.controller.npc.soldier.side_scroll.actuator
{
	public class Teleporter : helper.tests.Scene_test
	{
		Actuator up, left, right;
		Assert_colision down;
		Soldier_controller npc;

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
			( up, left, right ) = 
				helper.game_object.Find._<Actuator>(
					scene, "up", "left", "right" );
			down = helper.game_object.Find._<Assert_colision>( scene, "down" );
			npc = helper.game_object.Find._<Soldier_controller>( scene, "npc" );
		}

		[UnityTest]
		public IEnumerator should_can_active_the_actuator()
		{
			npc.transform.position = up.transform.position;
			yield return new WaitForSeconds( 1 );
			npc.activate();
			yield return new WaitForSeconds( 1 );
			down.assert_collision_enter( npc );
		}
	}
}
