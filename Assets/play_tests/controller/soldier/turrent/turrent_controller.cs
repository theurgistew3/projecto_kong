using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using helper.test.assert;

namespace tests.controller.trigger
{
	public class turrent_controller : helper.tests.Scene_test
	{
		Assert_colision forward, left, right, back;
		chibi.controller.weapon.gun.turrent.Controller_turrent turrent;
		chibi.controller.npc.Soldier_controller npc;

		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/controller/soldier/turrent controller";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			( back, forward, left, right ) = 
				helper.game_object.Find._<Assert_colision>(
					scene, "back", "forward", "left", "right" );

			turrent = helper.game_object.Find._<
				chibi.controller.weapon.gun.turrent.Controller_turrent>(
				scene, "turrent" );

			npc = helper.game_object.Find._<
				chibi.controller.npc.Soldier_controller>( scene, "npc" );
		}

		[UnityTest]
		public IEnumerator when_grab_the_turrent_the_npc_should_no_move()
		{
			yield return new WaitForSeconds( 0.5f );
			npc.grab_turrent();
			yield return new WaitForSeconds( 0.5f );
			npc.desire_direction = Vector3.left;
			yield return new WaitForSeconds( 1f );
			left.assert_not_collision_enter( npc );
		}

		[UnityTest]
		public IEnumerator when_grab_and_release_the_npc_should_move()
		{
			yield return new WaitForSeconds( 0.5f );
			npc.grab_turrent();
			yield return new WaitForSeconds( 0.5f );
			npc.desire_direction = Vector3.left;
			yield return new WaitForSeconds( 0.5f );
			npc.release_turrent();
			yield return new WaitForSeconds( 1f );
			left.assert_not_collision_enter( npc );
		}

		[UnityTest]
		public IEnumerator when_shot_left_the_bullet_should_hit_left()
		{
			yield return new WaitForSeconds( 0.5f );
			npc.grab_turrent();
			yield return new WaitForSeconds( 0.5f );
			npc.desire_direction = Vector3.left;
			yield return new WaitForSeconds( 0.5f );
			var bullets = npc.shot();
			yield return new WaitForSeconds( 1f );
			Assert.IsNotNull( bullets );
			foreach( var bullet in bullets )
				left.assert_collision_enter( bullet );
		}

		[UnityTest]
		public IEnumerator should_be_in_the_position_of_the_turtrent()
		{
			npc.desire_direction = Vector3.left;
			npc.speed = 1f;
			yield return new WaitForSeconds( 0.5f );
			npc.grab_turrent();
			yield return new WaitForSeconds( 0.5f );
			Assert.AreEqual(
				npc.transform.position.x, npc.hold_turrent_position.position.x,
				0.1f );
			Assert.AreEqual(
				npc.transform.position.y, npc.hold_turrent_position.position.y,
				0.1f );
		}
	}
}