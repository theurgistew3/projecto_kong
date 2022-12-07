using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using danmaku.controller.npc;
using chibi.damage.motor;

namespace tests.controller.weapon.gun.danmaku
{
	public class Test_pattern_gun : helper.tests.Scene_test
	{
		HP_engine hp;
		Touha_controller reimu;

		public override string scene_dir
		{
			get {
				return "danmaku/tests/scene/controller/weapon/gun/gun pattern";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			hp = helper.game_object.Find._<HP_engine>( scene, "hp engine" );
			reimu = helper.game_object.Find._<Touha_controller>( scene, "reimu" );
		}

		[UnityTest]
		public IEnumerator when_shot_should_reach_the_assert_collider()
		{
			var bullets = reimu.shot();
			yield return new WaitForSeconds( 1 );
			// Assert.Greater( bullets.Count, 0 );
			foreach ( var bullet in bullets )
			{
				tests_tool.assert.game_object.is_not_null( bullet );
				Assert.True( hp.is_dead );
			}
			yield return new WaitForSeconds( 1 );
		}
	}
}
