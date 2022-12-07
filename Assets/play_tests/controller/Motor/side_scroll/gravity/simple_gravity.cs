using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using helper.test.assert;
using chibi.controller.ai;

namespace tests.controller.motor.side_scroll.gravity
{
	public class simple_gravity : helper.tests.Scene_test
	{
		Assert_colision up, down, left, right, jump, jump_2;
		Ai_walk ai;

		public override string scene_dir
		{
			get {
				return "tests/scene/controller/motor/npc/"
					+ "motor side scroll gravity";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			up = helper.game_object.Find._<Assert_colision>( scene, "assert up" );
			down = helper.game_object.Find._<Assert_colision>(
				scene, "assert down" );
			left = helper.game_object.Find._<Assert_colision>(
				scene, "assert left" );
			right = helper.game_object.Find._<Assert_colision>(
				scene, "assert right" );
			jump = helper.game_object.Find._<Assert_colision>(
				scene, "assert jump 1" );
			jump_2 = helper.game_object.Find._<Assert_colision>(
				scene, "assert jump 2" );

			ai = helper.game_object.Find._<Ai_walk>( scene, "npc" );
			ai.use_max_speed = true;
		}

		[UnityTest]
		public IEnumerator when_move_to_up_should_touch_collider_up()
		{
			ai.desire_direction = Vector3.forward;
			yield return new WaitForSeconds( 2 );
			up.assert_collision_enter( ai.gameObject );
			down.assert_not_collision_enter();
			left.assert_not_collision_enter();
			right.assert_not_collision_enter();
			// jump.assert_not_collision_enter();
		}

		[UnityTest]
		public IEnumerator when_move_to_down_should_touch_collider_down()
		{
			ai.desire_direction = Vector3.back;
			yield return new WaitForSeconds( 2 );
			down.assert_collision_enter( ai.gameObject );
			up.assert_not_collision_enter();
			left.assert_not_collision_enter();
			right.assert_not_collision_enter();
			// jump.assert_not_collision_enter();
		}

		[UnityTest]
		public IEnumerator when_no_have_move_should_fall_in_all_the_jumps()
		{
			yield return new WaitForSeconds( 2 );
			jump.assert_collision_enter( ai.gameObject );
			jump_2.assert_collision_enter( ai.gameObject );
			up.assert_not_collision_enter();
			left.assert_not_collision_enter();
			down.assert_not_collision_enter();
			right.assert_not_collision_enter();
		}
	}
}
