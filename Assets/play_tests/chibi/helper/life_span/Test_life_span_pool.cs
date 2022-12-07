using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using chibi.tool.life_span;
using chibi.pool;


namespace tests.life_span.chibi
{
	public class Test_life_span_pool : helper.tests.Scene_test
	{
		Transform obj;
		Life_span_pool life_span_pool;
		Pool_behaviour pool;

		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/helper/life_span/life_span_pool";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			obj = helper.game_object.Find._( scene.transform, "life_span" );
			life_span_pool = obj.GetComponent<Life_span_pool>();
			pool = life_span_pool.pool;
		}

		[UnityTest]
		public IEnumerator should_be_in_the_pool()
		{
			GameObject game_object = obj.gameObject;
			Assert.AreEqual( 0, pool.count );
			yield return new WaitForSeconds( 1.1f );
			tests_tool.assert.game_object.is_not_null( game_object );
			Assert.AreEqual( 1, pool.count );
		}
	}
}
