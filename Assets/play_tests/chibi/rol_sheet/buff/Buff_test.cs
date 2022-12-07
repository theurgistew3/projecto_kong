using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using chibi.rol_sheet;
using chibi.rol_sheet.buff.health;

namespace tests.rol_sheet.buff
{
	public class Buff_test : helper.tests.Scene_test
	{
		public Rol_sheet rol_sheet;

		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/rol_sheet/rol_sheet";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			rol_sheet = helper.game_object.Find._<Rol_sheet>( scene, "npc" );
			Assert.IsTrue( rol_sheet );
		}

		public virtual chibi.rol_sheet.buff.Buff create_buff()
		{
			return Health_restore.CreateInstance<Health_restore>();
		}

		[UnityTest]
		public IEnumerator when_start_the_radar_should_be_empty()
		{
			yield return new WaitForSeconds( 0.1f );
			var buff = create_buff();
			rol_sheet.attach_buff( buff );
			var attacher = rol_sheet.buffos.Find( x => x.buff == buff );
			Assert.IsNotNull( attacher );
			yield return new WaitForSeconds( 0.1f );
		}

		[UnityTest]
		public IEnumerator when_finish_is_period_should_be_remove()
		{
			yield return new WaitForSeconds( 0.1f );
			var buff = create_buff();
			buff.duration = 1f;
			rol_sheet.attach_buff( buff );
			yield return new WaitForSeconds( 2f );
			var attacher = rol_sheet.buffos.Find( x => x.buff == buff );
			Assert.IsNull( attacher );
			yield return new WaitForSeconds( 0.1f );
		}

		[UnityTest]
		public IEnumerator if_no_have_limit_should_no_be_remove()
		{
			yield return new WaitForSeconds( 0.1f );
			var buff = create_buff();
			buff.duration = 1f;
			buff.no_duration_limit = true;
			rol_sheet.attach_buff( buff );
			yield return new WaitForSeconds( 2f );
			var attacher = rol_sheet.buffos.Find( x => x.buff == buff );
			Assert.IsNotNull( attacher );
			yield return new WaitForSeconds( 0.1f );
		}
	}
}