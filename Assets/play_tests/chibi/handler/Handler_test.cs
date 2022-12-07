using System.Collections;
using UnityEngine.TestTools;
using UnityEngine;
using helper.test.assert;
using chibi.controller;

namespace tests.controller.handler.chibi
{
	public class Handler_test : helper.tests.Scene_test
	{
		Assert_colision assert_1, assert_2;
		Controller_motor npc;

		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/controller/handler/handler set";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			( assert_1, assert_2 ) = helper.game_object.Find._<Assert_colision>(
				scene, "assert 1", "assert 2" );
			npc = helper.game_object.Find._<Controller_motor>( scene, "npc" );
		}

		[UnityTest]
		public IEnumerator when_touch_the_handler_should_be_stoped()
		{
			npc.desire_direction = Vector3.forward;
			npc.speed = npc.motor.max_speed;
			yield return new WaitForSeconds( 1f );
			assert_1.assert_not_collision_enter( npc );
			assert_2.assert_not_collision_enter( npc );
		}
	}
}
