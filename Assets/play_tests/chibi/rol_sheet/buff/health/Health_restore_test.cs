using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using chibi.rol_sheet.buff.health;
using chibi.rol_sheet.buff;

namespace tests.rol_sheet.buff
{
	public class Health_restore_test : Buff_test
	{
		public override Buff create_buff()
		{
			return Health_restore.CreateInstance<Health_restore>();
		}

		[UnityTest]
		public IEnumerator should_add_more_hp()
		{
			yield return new WaitForSeconds( 0.1f );
			var buff = (Health_restore)create_buff();
			rol_sheet.hp.max = 10f;
			rol_sheet.hp.current = 1f;
			buff.duration = 3f;
			buff.amount = 1;
			rol_sheet.attach_buff( buff );
			yield return new WaitForSeconds( 3f );
			Assert.AreEqual( 4, rol_sheet.hp.current, 0.05 );
		}
	}
}