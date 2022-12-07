using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace tests.spawner.invoker
{
	public class Timer_test : helper.tests.Scene_test
	{
		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/spawner/spawner timer";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
		}

		[UnityTest]
		public IEnumerator should_find_a_npc_after_a_second()
		{
			yield return new WaitForSeconds( 1.5f );
			var npcs = helper.game_object.Find.all_regex( @"npc.*" );
			Assert.AreEqual( 1, npcs.Length );
			yield return new WaitForSeconds( 1f );
			npcs = helper.game_object.Find.all_regex( @"npc.*" );
			Assert.AreEqual( 2, npcs.Length );
			yield return new WaitForSeconds( 1f );
		}
	}
}