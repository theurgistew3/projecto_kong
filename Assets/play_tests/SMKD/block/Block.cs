using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using SMKD.motor;
using NUnit.Framework;

namespace tests.controller.block.SMKD
{
	public class Block: helper.tests.Scene_test
	{
		Block_motor block_up, block_down;
		chibi.weapon.gun.Gun gun;

		public override string scene_dir
		{
			get {
				return "SMKD/tests/scene/arkanoid/block";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			( block_up, block_down ) = helper.game_object.Find._<Block_motor>(
				scene, "block up", "block down" );
			gun = helper.game_object.Find._<chibi.weapon.gun.Gun>(
				scene, "dodger_gun" );
		}

		[UnityTest]
		public IEnumerator when_is_touched_by_the_ball_should_died()
		{
			var bullet = gun.shot();
			yield return new WaitForSeconds( 1 );
			tests_tool.assert.game_object.is_not_null( bullet );
			Assert.IsTrue( helper.game_object.comp.is_null( block_up ) );
			Assert.IsFalse( helper.game_object.comp.is_null( block_down ) );
		}
	}
}
