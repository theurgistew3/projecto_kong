using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using helper.test.assert;

namespace tests.weapon.gun.bullet
{
	public class Bullet : helper.tests.Scene_test
	{
		Assert_colision left, center, right, forward, back;
		chibi.controller.weapon.gun.bullet.Controller_bullet bullet;
		//chibi.controller.bullet.Bullet_controller bullet;

		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/weapon/gun/bullet/bullet";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			center = helper.game_object.Find._<Assert_colision>(
				scene, "center" );
			forward = helper.game_object.Find._<Assert_colision>(
				scene, "forward" );
			right = helper.game_object.Find._<Assert_colision>( scene, "right" );
			back = helper.game_object.Find._<Assert_colision>( scene, "back" );
			left = helper.game_object.Find._<Assert_colision>( scene, "left" );
			right = helper.game_object.Find._<Assert_colision>( scene, "right" );

			bullet = helper.game_object.Find._<
				chibi.controller.weapon.gun.bullet.Controller_bullet>(
				scene, "bullet" );
		}

		[UnityTest]
		public IEnumerator if_the_desire_directin_is_zero_should_no_move()
		{
			bullet.desire_direction = Vector3.zero;
			yield return new WaitForSeconds( 1 );
			tests_tool.assert.game_object.is_not_null( bullet );
			center.assert_not_collision_exit( bullet );

			left.assert_not_collision_enter();
			right.assert_not_collision_enter();
			back.assert_not_collision_enter();
			forward.assert_not_collision_enter();
		}

		[UnityTest]
		public IEnumerator if_desire_direction_is_up_should_only_touch_forward()
		{
			bullet.desire_direction = Vector3.forward;
			yield return new WaitForSeconds( 1 );
			tests_tool.assert.game_object.is_not_null( bullet );

			left.assert_not_collision_enter();
			right.assert_not_collision_enter();
			back.assert_not_collision_enter();
			forward.assert_collision_enter( bullet );
		}

		[UnityTest]
		public IEnumerator if_desire_direction_is_back_should_only_touch_back()
		{
			bullet.desire_direction = Vector3.back;
			yield return new WaitForSeconds( 1 );
			tests_tool.assert.game_object.is_not_null( bullet );

			left.assert_not_collision_enter();
			right.assert_not_collision_enter();
			forward.assert_not_collision_enter();
			back.assert_collision_enter( bullet );
		}

		[UnityTest]
		public IEnumerator if_desire_direction_is_left_should_only_touch_left()
		{
			bullet.desire_direction = Vector3.left;
			yield return new WaitForSeconds( 1 );
			tests_tool.assert.game_object.is_not_null( bullet );

			back.assert_not_collision_enter();
			right.assert_not_collision_enter();
			forward.assert_not_collision_enter();
			left.assert_collision_enter( bullet );
		}

		[UnityTest]
		public IEnumerator if_desire_direction_is_right_should_only_touch_right()
		{
			bullet.desire_direction = Vector3.right;
			yield return new WaitForSeconds( 1 );
			tests_tool.assert.game_object.is_not_null( bullet );

			back.assert_not_collision_enter();
			left.assert_not_collision_enter();
			forward.assert_not_collision_enter();
			right.assert_collision_enter( bullet );
		}
	}
}
