using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine;
using chibi.controller.npc;
using helper.test.assert;
using System.Linq;

namespace tests.controller.motor.side_scroll.jump
{
	public class Vertical_jump : helper.tests.Scene_test
	{
		Assert_colision jump, jump_2, jump_3;
		Controller_npc controller;

		public override string scene_dir
		{
			get {
				return "tests/scene/controller/motor/npc/motor side scroll";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			jump = helper.game_object.Find._<Assert_colision>(
				scene, "assert jump 1" );

			jump_2 = helper.game_object.Find._<Assert_colision>(
				scene, "assert jump 2" );

			jump_3 = helper.game_object.Find._<Assert_colision>(
				scene, "assert jump 3" );

			controller = helper.game_object.Find._<Controller_npc>(
				scene, "npc" );
		}

		[UnityTest]
		public IEnumerator when_move_to_up_should_touch_collider_up()
		{
			yield return new WaitForSeconds( 2 );
			controller.jump();
			yield return new WaitForSeconds( 1 );
			jump.assert_collision_enter( controller.gameObject );
			jump_2.assert_collision_enter( controller.gameObject );
			jump_3.assert_not_collision_enter();
		}

		[UnityTest]
		public IEnumerator should_jump_the_expected_height()
		{
			List< float > diff = new List< float >();
			for ( int i = 0; i < 5; ++i )
			{
				yield return new WaitForSeconds( 1 );
				float lower_point = controller.transform.position.y;
				float expected_height = controller.motor_side_scroll.max_jump_heigh;
				controller.jump();
				float max_point = 0;
				for ( int j = 0; j < 100; ++j )
				{
					yield return null;
					float current_point = controller.transform.position.y;
					if ( current_point > max_point )
						max_point = current_point;
					if ( controller.motor.velocity.y < -0.01 )
						controller.stop_jump();
				}
				diff.Add( max_point - lower_point );
			}
			foreach ( float d in diff )
				Debug.Log( string.Format(
					"la diferencia de salto fue de {0}", d ) );
			Assert.IsFalse( diff.Distinct().Skip(1).Any() );
		}
	}
}
