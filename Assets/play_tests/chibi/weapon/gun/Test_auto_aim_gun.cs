using System.Collections;
using UnityEngine.TestTools;
using UnityEngine;
using helper.test.assert;
using chibi.controller.weapon.gun.single;

namespace tests.weapon.linerar.gun
{
	public class Test_auto_aim_gun: helper.tests.Scene_test
	{
		public Controller_gun_single gun;
		public Assert_colision assert_1, assert_2;

		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/weapon/gun/gun auto aim target";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			gun = helper.game_object.Find._<Controller_gun_single>(
					scene, "gun" );
			assert_1 = helper.game_object.Find._<Assert_colision>(
					scene, "assert 1" );
			assert_2 = helper.game_object.Find._<Assert_colision>(
					scene, "assert 2" );
		}

		[UnityTest]
		public IEnumerator when_shot_should_kill_the_target()
		{
			gun.gun.auto_aim_target.Value = assert_1.gameObject;
			var bullet = gun.shot( true )[0];
			yield return new WaitForSeconds( 1.5f );
			assert_1.assert_collision_enter( bullet );

			gun.gun.auto_aim_target.Value = assert_2.gameObject;
			bullet = gun.shot( true )[0];
			yield return new WaitForSeconds( 1.5f );
			assert_2.assert_collision_enter( bullet );
		}
	}
}
