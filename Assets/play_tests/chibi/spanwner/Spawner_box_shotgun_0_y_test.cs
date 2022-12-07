using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.TestTools;
using UnityEngine;
using chibi.spawner;

namespace tests.spawner
{
	public class Spawner_box_shotgun_0_y_test : helper.tests.Scene_test
	{
		public Spawner spawner;

		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/spawner/spawner shorgun box 0 y";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			spawner = helper.game_object.Find._<Spawner_in_box_shotgun>( scene, "spawner" );
		}

		[UnityTest]
		public IEnumerator the_five_spawn_should_be_in_diferent_place()
		{
			List<Transform> npcs = new List<Transform>();
			yield return new WaitForSeconds( 0.1f );
			for ( int i = 0; i < 5; ++i )
			{
				var npc = spawner.spawn();
				Assert.IsFalse( helper.game_object.comp.is_null( npc ) );
				npcs.Add( npc.transform );
			}
			yield return new WaitForSeconds( 1.1f );
			for ( int i = 0; i < npcs.Count; ++i )
				for ( int j = i + 1; j < npcs.Count; ++j )
				{
					Assert.AreNotEqual( npcs[i].position.x, npcs[j].position.x );
					Assert.AreNotEqual( npcs[i].position.z, npcs[j].position.z );
				}
		}
	}
}