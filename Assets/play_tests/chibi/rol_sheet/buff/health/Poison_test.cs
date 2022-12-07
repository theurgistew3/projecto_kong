using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using chibi.rol_sheet.buff.health;
using chibi.rol_sheet.buff;

namespace tests.rol_sheet.buff.health
{
	public class Poison_test : Buff_test
	{
		public override Buff create_buff()
		{
			return Poison.CreateInstance<Poison>();
		}

		[UnityTest]
		public IEnumerator Should_reduce_the_hp()
		{
			yield return new WaitForSeconds( 0.1f );
			var buff = (Poison)create_buff();
			rol_sheet.hp.max = 10f;
			rol_sheet.hp.current = 10f;
			buff.duration = 3f;
			buff.amount = 1;
			rol_sheet.attach_buff( buff );
			yield return new WaitForSeconds( 3f );
			Assert.AreEqual( 7, rol_sheet.hp.current, 0.05 );
		}
	}
}