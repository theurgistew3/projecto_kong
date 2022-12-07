using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using helper.test.assert;

namespace tests.controller.motor
{
	public class Gun : helper.tests.Scene_test
	{
		Assert_colision assert;
		chibi.weapon.gun.Gun gun;

		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/weapon/gun/gun";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			assert = helper.game_object.Find._<Assert_colision>(
				scene, "assert" );
			gun = helper.game_object.Find._<chibi.weapon.gun.Gun>(
				scene, "linear_gun" );
		}

		[UnityTest]
		public IEnumerator when_shot_should_create_a_bullet()
		{
			var bullet = gun.shot();
			yield return new WaitForSeconds( 1 );
			tests_tool.assert.game_object.is_not_null( bullet );
		}

		[UnityTest]
		public IEnumerator on_burst_should_shot_the_same_amount_in_the_stat()
		{
			float wait_time =
				( 1 / gun.stat.rate_fire ) * gun.stat.burst_amount + 1;
			gun.burst();
			yield return new WaitForSeconds( wait_time );
			assert.assert_collision_enter( gun.stat.burst_amount );
		}
	}
}
