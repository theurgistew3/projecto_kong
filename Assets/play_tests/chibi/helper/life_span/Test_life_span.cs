using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace tests.life_span.chibi
{
	public class Test_life_span : helper.tests.Scene_test
	{
		Transform obj;

		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/helper/life_span/life_span";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			obj = helper.game_object.Find._( scene.transform, "life_span" );
		}

		[UnityTest]
		public IEnumerator should_be_destroy_in_1_second()
		{
			GameObject game_object = obj.gameObject;
			tests_tool.assert.game_object.is_not_null( game_object );
			yield return new WaitForSeconds( 1.1f );
			tests_tool.assert.game_object.is_null( game_object );
		}
	}
}
