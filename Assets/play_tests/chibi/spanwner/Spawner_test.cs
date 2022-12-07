using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using chibi.spawner;

namespace tests.spawner
{
	public class Spawner_test : helper.tests.Scene_test
	{
		public Spawner spawner;

		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/spawner/spawner";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			spawner = helper.game_object.Find._<Spawner>( scene, "spawner" );
		}

		[UnityTest]
		public IEnumerator work_with_a_simple_spawn()
		{
			yield return new WaitForSeconds( 0.1f );
			var npc = spawner.spawn();
			Assert.IsFalse( helper.game_object.comp.is_null( npc ) );
			yield return new WaitForSeconds( 1.1f );
		}

		[UnityTest]
		public IEnumerator no_have_problem_with_5_times()
		{
			yield return new WaitForSeconds( 0.1f );
			for ( int i = 0; i < 5; ++i )
			{
				var npc = spawner.spawn();
				Assert.IsFalse( helper.game_object.comp.is_null( npc ) );
			}
			yield return new WaitForSeconds( 1.1f );
		}
	}
}