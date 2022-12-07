using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using helper.test.assert;

namespace tests.controller.weapon.gun.turrent
{
	public class Turrent_desire_direction_reversed : helper.tests.Scene_test
	{
		Assert_colision forward, left, right, back;
		chibi.motor.weapons.gun.turrent.Turrent turrent;
		chibi.controller.weapon.gun.turrent.Controller_turrent controller;

		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/weapon/gun/turrent reverse";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			( back, forward, left, right ) = 
				helper.game_object.Find._<Assert_colision>(
					scene, "back", "forward", "left", "right" );

			turrent = helper.game_object.Find._<
				chibi.motor.weapons.gun.turrent.Turrent>( scene, "turrent" );
			controller = helper.game_object.Find._<
				chibi.controller.weapon.gun.turrent.Controller_turrent>(
				scene, "turrent" );
		}

		[UnityTest]
		public IEnumerator multiple_shots()
		{
			yield return new WaitForSeconds( 1 );
			controller.desire_direction = Vector3.right;
			yield return new WaitForSeconds( 1 );
			var bullet = controller.shot();
			yield return new WaitForSeconds( 1 );
			right.assert_collision_enter( bullet[0] );

			controller.desire_direction = Vector3.left;
			yield return new WaitForSeconds( 1 );
			bullet = controller.shot();
			yield return new WaitForSeconds( 1 );
			left.assert_collision_enter( bullet[0] );

			controller.desire_direction = Vector3.back;
			yield return new WaitForSeconds( 1 );
			bullet = controller.shot();
			yield return new WaitForSeconds( 1 );
			back.assert_collision_enter( bullet[0] );

			controller.desire_direction = Vector3.forward;
			yield return new WaitForSeconds( 1 );
			bullet = controller.shot();
			yield return new WaitForSeconds( 1 );
			forward.assert_not_collision_enter();
		}
	}
}