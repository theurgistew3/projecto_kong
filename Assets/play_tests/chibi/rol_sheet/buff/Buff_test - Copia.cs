using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using chibi.rol_sheet;

namespace tests.rol_sheet.buff
{
	public class Start_with_buffos : helper.tests.Scene_test
	{
		public Rol_sheet rol_sheet;

		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/rol_sheet/rol_sheet start_with_buffos";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			rol_sheet = helper.game_object.Find._<Rol_sheet>( scene, "npc" );
			Assert.IsTrue( rol_sheet );
		}

		[UnityTest]
		public IEnumerator should_start_with_buffos()
		{
			yield return new WaitForSeconds( 0.1f );
			var expected_buff = Resources.Load<chibi.rol_sheet.buff.Buff>(
				"tests/scene/chibi/rol_sheet/TEST_Health_restore" );

			var attacher = rol_sheet.buffos.Find( x => x.buff == expected_buff );
			Assert.IsNotNull( attacher, "no agrego el buffo esperado en el rol sheet" );
			yield return new WaitForSeconds( 0.1f );
		}
	}
}