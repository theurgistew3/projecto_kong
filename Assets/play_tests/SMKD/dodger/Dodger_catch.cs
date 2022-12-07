using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using helper.test.assert;
using SMKD.controller.npc;
using SMKD.motor;
using NUnit.Framework;

namespace tests.controller.motor.SMKD
{
	public class Dodger_catch : helper.tests.Scene_test
	{
		Assert_colision up, up_45, down;
		chibi.weapon.gun.Gun gun;
		Dodger_controller dodger;

		public override string scene_dir
		{
			get {
				return "SMKD/tests/scene/dodger/dodget catch";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			( up, up_45, down ) = helper.game_object.Find._<Assert_colision>(
				scene, "assert up", "assert 45", "assert down" );
			gun = helper.game_object.Find._<chibi.weapon.gun.Gun>(
				scene, "gun" );
			dodger = helper.game_object.Find._<Dodger_controller>(
				scene, "dodger mecha" );
		}

		[UnityTest]
		public IEnumerator when_is_touched_by_the_ball_should_died()
		{
			var bullet = gun.shot();
			yield return new WaitForSeconds( 2 );
			Assert.IsTrue( dodger.dodger_motor.hp_motor.is_dead );
		}

		[UnityTest]
		public IEnumerator after_is_dead_should_disable_his_coliders()
		{
			var bullet = gun.shot();
			yield return new WaitForSeconds( 2 );
			var motor = dodger.dodger_motor;
			Assert.IsTrue( motor.is_dead );
			Assert.IsFalse( motor.damage_reciver.activeSelf );
			foreach ( var collider in motor.GetComponents<Collider>() )
			{
				Assert.IsFalse( collider.enabled );
			}
		}

		[UnityTest]
		public IEnumerator after_kill_the_dodger_the_ball_should_bounce()
		{
			var bullet = gun.shot();
			yield return new WaitForSeconds( 7 );
			up.assert_collision_enter( bullet );
			down.assert_not_collision_enter( bullet );
		}

		[UnityTest]
		public IEnumerator when_catch_the_ball_should_be_load_the_gun()
		{
			var bullet = gun.shot();
			yield return new WaitForSeconds( 1.4f );
			dodger.dodge();
			yield return new WaitForSeconds( 0.1f );
			var motor = dodger.motor as Dodger_motor;
			Assert.IsTrue( motor.has_the_ball );
		}

		[UnityTest]
		public IEnumerator when_dodge_should_no_have_ball()
		{
			var bullet = gun.shot();
			yield return new WaitForSeconds( 1.2f );
			dodger.dodge();
			yield return new WaitForSeconds( 0.1f );
			var motor = dodger.motor as Dodger_motor;
			Assert.IsFalse( motor.has_the_ball );
			Assert.IsTrue( motor.is_dodging );
			yield return new WaitForSeconds( 1f );
			Assert.IsFalse( dodger.dodger_motor.hp_motor.is_dead );
		}

		[UnityTest]
		public IEnumerator after_catch_should_can_shot()
		{
			var bullet = gun.shot();
			yield return new WaitForSeconds( 1.4f );
			dodger.dodge();
			yield return new WaitForSeconds( 0.1f );
			var motor = dodger.motor as Dodger_motor;
			Assert.IsTrue( motor.has_the_ball );
			dodger.desire_direction = Vector3.right + Vector3.forward;
			var list_of_bullets = dodger.shot();
			Assert.IsNotNull( list_of_bullets );
			Assert.GreaterOrEqual( 1, list_of_bullets.Count );
			bullet = list_of_bullets[0];
			yield return new WaitForSeconds( 7 );
			tests_tool.assert.game_object.is_not_null( bullet );
			up_45.assert_collision_enter( bullet );
		}

		[UnityTest]
		public IEnumerator after_catch_shot_when_the_counter_end()
		{
			var bullet = gun.shot();
			yield return new WaitForSeconds( 1.4f );
			dodger.dodge();
			yield return new WaitForSeconds( 0.1f );
			var motor = dodger.motor as Dodger_motor;
			Assert.IsTrue( motor.has_the_ball );
			dodger.desire_direction = Vector3.right + Vector3.forward;
			yield return new WaitForSeconds( 1.1f );
			yield return new WaitForSeconds( 7 );
			up_45.assert_collision_enter();
		}

		[UnityTest]
		public IEnumerator if_give_the_ball_should_no_enable_the_counter()
		{
			yield return new WaitForSeconds( 0.1f );
			var motor = dodger.motor as Dodger_motor;
			motor.give_ball();
			yield return new WaitForSeconds( 0.1f );
			Assert.IsTrue( motor.has_the_ball );
			dodger.desire_direction = Vector3.right + Vector3.forward;
			yield return new WaitForSeconds( 1.1f );
			yield return new WaitForSeconds( 7 );
			up_45.assert_not_collision_enter();
		}
	}
}