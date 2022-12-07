using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using helper.test.assert;

namespace tests.controller.steering.behavior.chibi
{
	public class Test_orbit : helper.tests.Scene_test
	{
		Assert_colision center, forward, back, left, right, a1, a2, a3, a4;
		GameObject npc;

		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/controller/steering/orbit 3 3";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			( center, back, forward, left, right ) =
				helper.game_object.Find._<Assert_colision>(
					scene, "center", "back", "forward", "left", "right" );

			( a1, a2, a3, a4 ) = helper.game_object.Find._<Assert_colision>(
				scene, "a1", "a2", "a3", "a4" );

			npc = helper.game_object.Find._( scene, "npc" );
		}

		[UnityTest]
		public IEnumerator the_npc_should_touch_all_the_asserts()
		{
			yield return new WaitForSeconds( 10 );
			helper.test.assert.many.assert_colision.assert_collision_enter(
				npc, center, forward, back, left, right, a1, a2, a3, a4 );
		}
	}
}
