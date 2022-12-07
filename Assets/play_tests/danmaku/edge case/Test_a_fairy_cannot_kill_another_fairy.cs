using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using danmaku.controller.npc;
using chibi.damage.motor;

namespace tests.edge_case.friend_fire.danmaku
{
	public class Test_a_fairy_cannot_kill_another_fairy : helper.tests.Scene_test
	{
		Touha_controller enemy1, enemy2;

		public override string scene_dir
		{
			get {
				return "danmaku/tests/scene/edge case/a_fairy_cannot_kill_another_fairy";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			enemy1 = helper.game_object.Find._<Touha_controller>(
					scene, "enemy1" );
			enemy2 = helper.game_object.Find._<Touha_controller>(
					scene, "enemy2" );
		}

		[UnityTest]
		public IEnumerator when_fairy_one_shot_should_no_kill_the_other_fairy()
		{
			var bullets = enemy1.shot();
			yield return new WaitForSeconds( 1 );
			var hp = enemy2.GetComponent<HP_engine>();
			Assert.False( hp.is_dead );
		}

		[UnityTest]
		public IEnumerator when_fairy_two_shot_should_no_kill_the_other_fairy()
		{
			var bullets = enemy2.shot();
			yield return new WaitForSeconds( 1 );
			var hp = enemy1.GetComponent<HP_engine>();
			Assert.False( hp.is_dead );
		}

		[UnityTest]
		public IEnumerator when_the_enemies_touch_another_should_no_be_fine()
		{
			enemy1.desire_direction = Vector3.back;
			enemy1.speed = enemy1.max_speed;
			enemy2.desire_direction = Vector3.forward;
			enemy2.speed = enemy2.max_speed;
			yield return new WaitForSeconds( 1 );
			var hp = enemy1.GetComponent<HP_engine>();
			Assert.False( hp.is_dead );
			hp = enemy2.GetComponent<HP_engine>();
			Assert.False( hp.is_dead );
		}
	}
}
