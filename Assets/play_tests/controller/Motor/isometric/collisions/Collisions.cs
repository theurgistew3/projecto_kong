using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using chibi.controller.npc;
using chibi.controller.ai;
using chibi.motor.npc;

namespace tests.controller.npc.isometric
{
	public class Collisions : helper.tests.Scene_test
	{
		Motor_isometric controller;
		Ai_walk ai;

		public override string scene_dir
		{
			get {
				return
					"tests/scene/controller/motor/npc/" +
					"motor isometric collisions";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();

			controller = helper.game_object.Find._< Motor_isometric >( scene, "npc" );
			ai = helper.game_object.Find._< Ai_walk >( scene, "npc" );
		}

		[UnityTest]
		public IEnumerator should_be_grounded()
		{
			ai.desire_direction = Vector3.zero;
			yield return new WaitForSeconds( 1 );
			Assert.IsTrue( controller.is_grounded );
		}

		[UnityTest]
		public IEnumerator should_no_be_walled_if_is_ony_grounded()
		{
			ai.desire_direction = Vector3.zero;
			yield return new WaitForSeconds( 1 );
			Assert.IsTrue( controller.is_grounded );
			Assert.IsFalse( controller.is_walled );
		}

		[UnityTest]
		public IEnumerator should_no_be_grounded_if_in_air()
		{
			ai.desire_direction = Vector3.zero;
			Assert.IsFalse( controller.is_grounded );
			yield return new WaitForSeconds( 0.05f );
		}

		[UnityTest]
		public IEnumerator should_be_walled_if_hit_in_front()
		{
			ai.desire_direction = Vector3.forward;
			yield return new WaitForSeconds( 2 );
			Assert.IsTrue( controller.is_walled );
		}

		[UnityTest]
		public IEnumerator should_be_walled_if_hit_in_back()
		{
			ai.desire_direction = Vector3.back;
			yield return new WaitForSeconds( 2 );
			Assert.IsTrue( controller.is_walled );
		}

		[UnityTest]
		public IEnumerator should_be_walled_if_hit_in_left()
		{
			ai.desire_direction = Vector3.left;
			yield return new WaitForSeconds( 2 );
			Assert.IsTrue( controller.is_walled );
		}

		[UnityTest]
		public IEnumerator should_be_walled_if_hit_in_right()
		{
			ai.desire_direction = Vector3.right;
			yield return new WaitForSeconds( 2 );
			Assert.IsTrue( controller.is_walled );
		}

		[UnityTest]
		public IEnumerator should_be_walled_if_hit_in_corner_up_right()
		{
			ai.desire_direction = Vector3.right + Vector3.forward;
			yield return new WaitForSeconds( 3 );
			Assert.IsTrue( controller.is_walled );
		}

		[UnityTest]
		public IEnumerator should_be_walled_if_hit_in_corner_up_left()
		{
			ai.desire_direction = Vector3.left + Vector3.forward;
			yield return new WaitForSeconds( 3 );
			Assert.IsTrue( controller.is_walled );
		}

		[UnityTest]
		public IEnumerator should_be_walled_if_hit_in_corner_back_right()
		{
			ai.desire_direction = Vector3.right + Vector3.back;
			yield return new WaitForSeconds( 3 );
			Assert.IsTrue( controller.is_walled );
		}

		[UnityTest]
		public IEnumerator should_be_walled_if_hit_in_corner_back_left()
		{
			ai.desire_direction = Vector3.left + Vector3.back;
			yield return new WaitForSeconds( 3 );
			Assert.IsTrue( controller.is_walled );
		}
	}
}
