using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using helper.test.assert;

namespace tests.controller.trigger
{
	public class Turrent_hold_area : helper.tests.Scene_test
	{
		Assert_colision forward, left, right, back;
		chibi.controller.weapon.gun.turrent.Controller_turrent turrent;
		chibi.controller.npc.Soldier_controller npc;

		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/controller/trigger/turrent_trigger";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			back = helper.game_object.Find._<Assert_colision>(
				scene, "back" );
			forward = helper.game_object.Find._<Assert_colision>(
				scene, "forward" );
			left = helper.game_object.Find._<Assert_colision>(
				scene, "left" );
			right = helper.game_object.Find._<Assert_colision>(
				scene, "right" );

			turrent = helper.game_object.Find._<
				chibi.controller.weapon.gun.turrent.Controller_turrent>(
				scene, "turrent" );

			npc = helper.game_object.Find._<
				chibi.controller.npc.Soldier_controller>( scene, "npc" );
		}

		[UnityTest]
		public IEnumerator when_touch_the_turrent_soldier_should_have_turrent()
		{
			Assert.IsFalse(
				npc.turrent, "el npc no deberia de tener una torreta asignada" );
			Assert.IsFalse(
				npc.hold_turrent_position,
				"el npc no deberia de tener una posisicon de torreta asignada" );
			yield return new WaitForSeconds( 1 );
			Assert.AreEqual( npc.turrent, turrent );
			Assert.IsTrue(
				npc.hold_turrent_position,
				"el npc deberia de tener una posisicon de torreta asignada" );
		}

		[UnityTest]
		public IEnumerator when_leave_the_turrent_soldier_should_no_own_turrent()
		{
			yield return new WaitForSeconds( 1 );
			npc.desire_direction = Vector3.back;
			npc.speed = 10f;
			yield return new WaitForSeconds( 1 );
			back.assert_collision_enter( npc );
			Assert.IsFalse(
				npc.turrent, "el npc no deberia de tener una torreta asignada" );
			Assert.IsFalse(
				npc.hold_turrent_position,
				"el npc no deberia de tener una posisicon de torreta asignada" );
		}
	}
}