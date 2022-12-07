using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using danmaku.controller.npc;
using chibi.damage.motor;

namespace tests.edge_case.self_shot.danmaku
{
	public class Test_self_shot_reimu : helper.tests.Scene_test
	{
		Touha_controller reimu;

		public override string scene_dir
		{
			get {
				return "danmaku/tests/scene/edge case/reimu self shot";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			reimu = helper.game_object.Find._<Touha_controller>( scene, "reimu" );
		}

		[UnityTest]
		public IEnumerator when_shot_should_reach_the_assert_collider()
		{
			var bullets = reimu.shot();
			yield return new WaitForSeconds( 1 );
			var hp = reimu.GetComponent<HP_engine>();
			Assert.False( hp.is_dead );
		}
	}
}