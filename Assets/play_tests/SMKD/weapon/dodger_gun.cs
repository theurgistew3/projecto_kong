using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using helper.test.assert;

namespace tests.controller.motor.SMKD
{
	public class Dodger_gun : helper.tests.Scene_test
	{
		Assert_colision up, up_45;
		chibi.weapon.gun.Gun gun;

		public override string scene_dir
		{
			get {
				return "SMKD/tests/scene/weapon/dodger_gun";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			( up, up_45 ) = helper.game_object.Find._<Assert_colision>(
				scene, "assert up", "assert 45" );
			gun = helper.game_object.Find._<chibi.weapon.gun.Gun>(
				scene, "dodger_gun" );
		}

		[UnityTest]
		public IEnumerator should_bounce_with_the_wall_and_hit_the_assert()
		{
			gun.aim_direction = new Vector3( 1, 0, 1 );
			var bullet = gun.shot();
			yield return new WaitForSeconds( 7 );
			tests_tool.assert.game_object.is_not_null( bullet );
			up_45.assert_collision_enter( bullet );
			up.assert_collision_enter( bullet );
		}
	}
}