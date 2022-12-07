using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using helper.test.assert;
using chibi.controller.npc;
using chibi.controller.ai;
using chibi.rol_sheet;

namespace tests.controller.npc.soldier.side_scroll
{
	public class movement : helper.tests.Scene_test
	{
		Assert_colision up, down, left, right;
		Ai_walk ai;

		public override string scene_dir
		{
			get {
				return "tests/scene/controller/motor/npc/motor side scroll";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			( up, down, left, right ) = 
				helper.game_object.Find._<Assert_colision>(
					scene, "assert up", "assert down", "assert left",
					"assert right" );

			ai = helper.game_object.Find._<Ai_walk>( scene, "npc" );
			ai.gameObject.AddComponent<Rol_sheet>();
			var soldier = ai.gameObject.AddComponent<Soldier_controller>();
			soldier.npc = ai.controller as Controller_npc;
			ai.controller = soldier;
			ai.use_max_speed = true;
		}

		[UnityTest]
		public IEnumerator when_move_to_up_should_touch_collider_up()
		{
			ai.desire_direction = Vector3.forward;
			yield return new WaitForSeconds( 1 );
			up.assert_collision_enter( ai.gameObject );
			helper.test.assert.many.assert_colision.assert_not_collision_enter(
				down, left, right );
		}

		[UnityTest]
		public IEnumerator when_move_to_down_should_touch_collider_down()
		{
			ai.desire_direction = Vector3.back;
			yield return new WaitForSeconds( 1 );
			down.assert_collision_enter( ai.gameObject );
			helper.test.assert.many.assert_colision.assert_not_collision_enter(
				up, left, right );
		}

		[UnityTest]
		public IEnumerator when_move_to_left_should_touch_nothing()
		{
			ai.desire_direction = Vector3.left;
			yield return new WaitForSeconds( 1 );
			helper.test.assert.many.assert_colision.assert_not_collision_enter(
				up, down, right, left );
		}

		[UnityTest]
		public IEnumerator when_move_to_right_should_touch_nothing()
		{
			ai.desire_direction = Vector3.right;
			yield return new WaitForSeconds( 1 );
			helper.test.assert.many.assert_colision.assert_not_collision_enter(
				up, down, left, right );
		}
	}
}
