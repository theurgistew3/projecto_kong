using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using danmaku.controller.npc;

namespace tests.edge_case.player_shot_enemy.danmaku
{
	public class Test_dead_fairy_go_to_pool: helper.tests.Scene_test
	{
		Touha_controller reimu, enemy;

		public override string scene_dir
		{
			get {
				return "danmaku/tests/scene/edge case/reimu can kill faries";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			reimu = helper.game_object.Find._<Touha_controller>( scene, "reimu" );
			enemy = helper.game_object.Find._<Touha_controller>( scene, "enemy" );
		}

		[UnityTest]
		public IEnumerator when_shot_should_kill_the_fairy()
		{
			var bullets = reimu.shot();
			yield return new WaitForSeconds( 1 );
			Assert.IsFalse( enemy );
		}
	}
}
