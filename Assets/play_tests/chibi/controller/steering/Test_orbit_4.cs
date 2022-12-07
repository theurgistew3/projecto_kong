using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using helper.test.assert;

namespace tests.controller.steering.behavior.chibi
{
	public class Test_orbit_4 : helper.tests.Scene_test
	{
		Assert_colision forward, back, left, right, a1, a2, a3, a4;
		GameObject npc_1, npc_2, npc_3, npc_4;

		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/controller/steering/orbit 3 3 4";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			( back, forward, left, right ) =
				helper.game_object.Find._<Assert_colision>(
					scene, "back", "forward", "left", "right" );

			( a1, a2, a3, a4 ) = helper.game_object.Find._<Assert_colision>(
				scene, "a1", "a2", "a3", "a4" );

			npc_1 = helper.game_object.Find._( scene, "npc_1" );
			npc_2 = helper.game_object.Find._( scene, "npc_2" );
			npc_3 = helper.game_object.Find._( scene, "npc_3" );
			npc_4 = helper.game_object.Find._( scene, "npc_4" );
		}

		[UnityTest]
		public IEnumerator the_npc_should_touch_all_the_asserts()
		{
			yield return new WaitForSeconds( 10 );
			helper.test.assert.many.assert_colision.assert_collision_enter(
				npc_1, forward, back, left, right, a1, a2, a3, a4 );
			helper.test.assert.many.assert_colision.assert_collision_enter(
				npc_2, forward, back, left, right, a1, a2, a3, a4 );
			helper.test.assert.many.assert_colision.assert_collision_enter(
				npc_3, forward, back, left, right, a1, a2, a3, a4 );
			helper.test.assert.many.assert_colision.assert_collision_enter(
				npc_4, forward, back, left, right, a1, a2, a3, a4 );
		}
	}
}
