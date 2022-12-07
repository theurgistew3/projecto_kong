using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using chibi.controller.ai;
using chibi.motor;

namespace tests.controller.motor
{
	public class properties : helper.tests.Scene_test
	{
		Ai_walk ai;
		Motor motor;

		public override string scene_dir
		{
			get {
				return "tests/scene/controller/motor/motor";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();

			ai = helper.game_object.Find._<Ai_walk>( scene, "npc" );
			ai.use_max_speed = true;
			motor = helper.game_object.Find._<Motor>( scene, "npc" );
		}

		[UnityTest]
		public IEnumerator when_is_not_moving_his_speed_should_be_0()
		{
			yield return new WaitForSeconds( 1 );
			Vector3 velocity = new Vector3( motor.velocity.x, 0, motor.velocity.z );
			Assert.AreEqual( 0, velocity.magnitude, 0.001f );
		}

		[UnityTest]
		public IEnumerator when_is_moving_forward_the_z_should_be_1()
		{
			ai.desire_direction = Vector3.forward;
			yield return new WaitForSeconds( 1 );
			Assert.GreaterOrEqual( motor.current_speed.z, 1 );
		}

		[UnityTest]
		public IEnumerator when_is_moving_back_the_z_should_be_minus_1()
		{
			ai.desire_direction = Vector3.back;
			yield return new WaitForSeconds( 1 );
			Assert.LessOrEqual( motor.current_speed.z, -1 );
		}

		[UnityTest]
		public IEnumerator when_is_moving_left_the_x_should_be_1()
		{
			ai.desire_direction = Vector3.left;
			yield return new WaitForSeconds( 1 );
			Assert.LessOrEqual( motor.current_speed.x, -1 );
		}
	}
}
