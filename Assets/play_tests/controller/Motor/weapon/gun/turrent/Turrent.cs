using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using helper.test.assert;

namespace tests.motor.weapons.gun.turrent
{
	public class Turrent : helper.tests.Scene_test
	{
		Assert_colision forward, left, right, back;
		chibi.motor.weapons.gun.turrent.Turrent turrent;
		chibi.controller.weapon.gun.turrent.Controller_turrent controller;

		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/weapon/gun/turrent";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			back = helper.game_object.Find._<Assert_colision>(
				scene, "back" );
			forward = helper.game_object.Find._<Assert_colision>(
				scene, "forward" );
			left = helper.game_object.Find._<Assert_colision>(
				scene, "left" );
			right = helper.game_object.Find._<Assert_colision>(
				scene, "right" );
			turrent = helper.game_object.Find._<
				chibi.motor.weapons.gun.turrent.Turrent>( scene, "turrent" );
			controller = helper.game_object.Find._<
				chibi.controller.weapon.gun.turrent.Controller_turrent>(
				scene, "turrent" );
		}

		[UnityTest]
		public IEnumerator move_left_should_can_hit_left_assert()
		{
			turrent.desire_direction = Vector3.left;
			yield return new WaitForSeconds( 1 );
			controller.shot();
			yield return new WaitForSeconds( 1 );
			left.assert_collision_enter();

			right.assert_not_collision_enter();
			forward.assert_not_collision_enter();
			back.assert_not_collision_enter();
		}

		[UnityTest]
		public IEnumerator move_right_should_can_hit_right_assert()
		{
			turrent.desire_direction = Vector3.right;
			yield return new WaitForSeconds( 1 );
			controller.shot();
			yield return new WaitForSeconds( 1 );
			right.assert_collision_enter();

			left.assert_not_collision_enter();
			forward.assert_not_collision_enter();
			back.assert_not_collision_enter();
		}

		[UnityTest]
		public IEnumerator move_back_should_can_hit_left_or_right()
		{
			turrent.desire_direction = Vector3.back;
			yield return new WaitForSeconds( 1 );
			controller.shot();
			yield return new WaitForSeconds( 1 );
			forward.assert_not_collision_enter();
			try
			{
				left.assert_collision_enter();
			}
			catch ( System.Exception )
			{
				right.assert_collision_enter();
			}
			back.assert_not_collision_enter();
		}

		[UnityTest]
		public IEnumerator move_forward_should_can_hit_forward_assert()
		{
			turrent.desire_direction = Vector3.forward;
			yield return new WaitForSeconds( 1 );
			controller.shot();
			yield return new WaitForSeconds( 1 );
			forward.assert_collision_enter();

			left.assert_not_collision_enter();
			right.assert_not_collision_enter();
			back.assert_not_collision_enter();
		}
	}
}