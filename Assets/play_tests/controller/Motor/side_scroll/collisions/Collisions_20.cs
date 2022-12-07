using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using chibi.controller.npc;
using chibi.controller.ai;

namespace tests.controller.npc.side_scroll
{
	public class Collisions_20 : helper.tests.Scene_test
	{
		Controller_npc motor;
		Ai_walk ai;

		public override string scene_dir
		{
			get {
				return
					"tests/scene/controller/motor/npc/" +
					"motor side scroll collisions 20";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();

			motor = helper.game_object.Find._< Controller_npc >( scene, "npc" );
			ai = helper.game_object.Find._< Ai_walk >( scene, "npc" );
		}

		[UnityTest]
		public IEnumerator should_be_grounded()
		{
			ai.desire_direction = Vector3.zero;
			yield return new WaitForSeconds( 1 );
			Assert.IsTrue( motor.motor_side_scroll.is_grounded );
		}

		[UnityTest]
		public IEnumerator should_no_be_grounded_if_in_air()
		{
			ai.desire_direction = Vector3.zero;
			Assert.IsFalse( motor.motor_side_scroll.is_grounded );
			yield return new WaitForSeconds( 0.05f );
		}

		[UnityTest]
		public IEnumerator should_no_be_walled_if_is_ony_grounded()
		{
			ai.desire_direction = Vector3.zero;
			yield return new WaitForSeconds( 1 );
			Assert.IsTrue( motor.motor_side_scroll.is_grounded );
			Assert.IsFalse( motor.motor_side_scroll.is_walled );
		}

		[UnityTest]
		public IEnumerator should_be_walled_if_hit_in_front()
		{
			ai.desire_direction = Vector3.forward;
			yield return new WaitForSeconds( 2.5f );
			Assert.IsTrue( motor.motor_side_scroll.is_walled );
		}

		[UnityTest]
		public IEnumerator should_be_walled_if_hit_in_back()
		{
			ai.desire_direction = Vector3.back;
			yield return new WaitForSeconds( 2.5f );
			Assert.IsTrue( motor.motor_side_scroll.is_walled );
		}
	}
}
