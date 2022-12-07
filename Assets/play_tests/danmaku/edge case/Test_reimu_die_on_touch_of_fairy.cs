using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using danmaku.controller.npc;

namespace tests.edge_case.player_receive_damge.danmaku
{
	public class Test_reimu_die_on_touch_of_fairy: helper.tests.Scene_test
	{
		Touha_controller reimu, enemy;

		public override string scene_dir
		{
			get {
				return "danmaku/tests/scene/edge case/reimu_die_on_touch";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			reimu = helper.game_object.Find._<Touha_controller>( scene, "reimu" );
			enemy = helper.game_object.Find._<Touha_controller>( scene, "enemy" );
		}

		[UnityTest]
		public IEnumerator when_reimu_is_touched_should_die()
		{
			enemy.desire_direction =
				reimu.transform.position - enemy.transform.position;
			enemy.speed = 100f;
			yield return new WaitForSeconds( 1 );
			Assert.False( reimu );
		}
	}
}
