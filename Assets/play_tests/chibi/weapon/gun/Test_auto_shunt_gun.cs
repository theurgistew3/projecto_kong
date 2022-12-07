using System.Collections;
using UnityEngine.TestTools;
using UnityEngine;
using chibi.controller.weapon.gun.single;
using helper.test.assert;

namespace tests.weapon.linerar.gun
{
	public class Test_auto_shunt_gun: helper.tests.Scene_test
	{
		public Controller_gun_single gun;
		public Assert_colision
			target_1, target_2, target_shunt_1, target_shunt_2;

		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/weapon/gun/gun auto shunt target";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			gun = helper.game_object.Find._<Controller_gun_single>(
					scene, "gun" );
			target_1 = helper.game_object.Find._<Assert_colision>(
					scene, "target_1" );
			( target_1, target_2, target_shunt_1, target_shunt_2 ) =
				helper.game_object.Find._<Assert_colision>(
					scene, "target_1", "target_2", "target_shunt_1", "target_shunt_2" );
		}

		[UnityTest]
		public IEnumerator when_shot_should_kill_the_target()
		{
			gun.gun.auto_aim_target.Value = target_1.gameObject;
			var bullet = gun.shot( true )[0];
			yield return new WaitForSeconds( 1.5f );
			target_shunt_1.assert_collision_enter( bullet );
			target_1.assert_not_collision_enter( bullet );

			gun.gun.auto_aim_target.Value = target_2.gameObject;
			bullet = gun.shot( true )[0];
			yield return new WaitForSeconds( 1.5f );
			target_shunt_2.assert_collision_enter( bullet );
			target_2.assert_not_collision_enter( bullet );
		}
	}
}
