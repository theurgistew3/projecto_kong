using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using helper.test.assert;

namespace tests.controller.weapon.gun.turrent
{
	public class Turrent_side_scroll_reverse : helper.tests.Scene_test
	{
		Assert_colision up, left, right, down;
		chibi.motor.weapons.gun.turrent.Turrent turrent;
		chibi.controller.weapon.gun.turrent.Controller_turrent controller;

		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/weapon/gun/turrent side scroll reverse";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			( up, down, left, right ) = 
				helper.game_object.Find._<Assert_colision>(
					scene, "up", "down", "left", "right" );

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

			controller.desire_direction = Vector3.down;
			Debug.Log( "down" );
			yield return new WaitForSeconds( 1 );
			var bullet = controller.shot();
			yield return new WaitForSeconds( 1 );
			down.assert_collision_enter( bullet[0] );

			controller.desire_direction = Vector3.right;
			Debug.Log( "right" );
			yield return new WaitForSeconds( 1 );
			bullet = controller.shot();
			yield return new WaitForSeconds( 1 );
			right.assert_not_collision_enter();

			controller.desire_direction = Vector3.left;
			Debug.Log( "left" );
			yield return new WaitForSeconds( 1 );
			bullet = controller.shot();
			yield return new WaitForSeconds( 1 );
			left.assert_collision_enter( bullet[0] );

			controller.desire_direction = Vector3.up;
			Debug.Log( "up" );
			yield return new WaitForSeconds( 1 );
			bullet = controller.shot();
			yield return new WaitForSeconds( 1 );
			up.assert_collision_enter( bullet[0] );
		}
	}
}