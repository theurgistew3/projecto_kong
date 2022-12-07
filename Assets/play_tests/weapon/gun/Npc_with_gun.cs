using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using helper.test.assert;
using chibi.damage;
using chibi.rol_sheet;

namespace tests.controller.weapon.gun
{
	public class Npc_with_gun : helper.tests.Scene_test
	{
		Assert_colision assert;
		chibi.controller.npc.Controller_npc npc;
		chibi.weapon.gun.Gun gun;

		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/weapon/gun/npc with gun";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			assert = helper.game_object.Find._<Assert_colision>(
				scene, "assert" );
			gun = helper.game_object.Find._<chibi.weapon.gun.Gun>(
				scene, "linear_gun" );

			npc = helper.game_object.Find._<chibi.controller.npc.Controller_npc>(
				scene, "npc" );
		}

		[UnityTest]
		public IEnumerator when_set_the_owner_of_the_gun_should_be_seted_to_each_bullet()
		{
			var rol = npc.GetComponent<Rol_sheet>();
			gun.owner = rol;
			var bullet = gun.shot();
			yield return new WaitForSeconds( 1 );
			var damage = bullet.GetComponent<Damage>();
			Assert.AreEqual( damage.owner, rol );
		}


		[UnityTest]
		public IEnumerator when_remove_the_owner_the_new_damange_should_be_null()
		{
			var rol = npc.GetComponent<Rol_sheet>();
			gun.owner = rol;
			var bullet = gun.shot();
			yield return new WaitForSeconds( 1 );
			var damage = bullet.GetComponent<Damage>();
			Assert.AreEqual( damage.owner, rol );
			yield return new WaitForSeconds( 1 );
			gun.owner = null;
			bullet = gun.shot();
			yield return new WaitForSeconds( 1 );
			damage = bullet.GetComponent<Damage>();
			Assert.IsNull( damage.owner );
		}
	}
}