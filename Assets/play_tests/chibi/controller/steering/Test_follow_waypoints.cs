using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using helper.test.assert;

namespace tests.controller.steering.behavior.chibi
{
	public class Test_follow_waypoints : helper.tests.Scene_test
	{
		Assert_colision assert_1, assert_2, assert_3, assert_4;
		GameObject npc;

		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/controller/steering/follow waypoint";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			( assert_1, assert_2, assert_3, assert_4 ) =
				helper.game_object.Find._<Assert_colision>(
					scene, "assert 1", "assert 2", "assert 3", "assert 4" );

			npc = helper.game_object.Find._( scene, "npc" );
		}

		[UnityTest]
		public IEnumerator the_npc_should_touch_all_the_asserts()
		{
			yield return new WaitForSeconds( 10 );
			helper.test.assert.many.assert_colision.assert_collision_enter(
				npc, assert_1, assert_2, assert_3, assert_4  );
		}
	}
}
