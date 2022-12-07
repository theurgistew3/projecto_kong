using UnityEngine;
using NUnit.Framework;
using weapon.ammo;
using chibi.motor.weapons.gun.bullet;
using singleton.object_pool;

namespace unit_tests.singleton.object_pool
{
	public class Test_ammo_pool : helper.tests.basic_test
	{
		[Test]
		public void should_create_the_container()
		{
			var ins = Ammo_pool.instance;
			var stuff = GameObject.Find(
				helper.consts.game_object_names.stuff );
			Assert.IsNotNull( stuff );
			var container = stuff.transform.Find( ins.container_name );
			Assert.IsNotNull( container );
		}

		[Test]
		public void should_instance_when_no_have_objects()
		{
			var bullet_1 = load_bullet_slow();
			var bullet_2 = load_bullet_fast();

			Ammo ammo_1 = Ammo.CreateInstance<Ammo>();
			ammo_1.prefab_bullet = bullet_1.GetComponent<
				Bullet_motor>();
			Ammo ammo_2 = Ammo.CreateInstance<Ammo>();
			ammo_2.prefab_bullet = bullet_2.GetComponent<
				Bullet_motor>();

			var ins = Ammo_pool.instance;
			Assert.IsNull( ins[ ammo_1 ] );
			Assert.IsNull( ins[ ammo_2 ] );

			var inst_bullet_1 = ins.pop( ammo_1 );
			Assert.IsNotNull( inst_bullet_1 );
		}

		[Test]
		public void should_add_the_same_object_in_the_same_stack()
		{
			var bullet_1 = load_bullet_slow();

			Ammo ammo_1 = Ammo.CreateInstance<Ammo>();
			ammo_1.prefab_bullet = bullet_1.GetComponent<
				Bullet_motor>();

			var ins = Ammo_pool.instance;
			Assert.IsNull( ins[ ammo_1 ] );

			var inst_bullet_1 = ins.pop( ammo_1 );
			var inst_bullet_2 = ins.pop( ammo_1 );
			ins.push( inst_bullet_1 );
			ins.push( inst_bullet_2 );
			Assert.IsNotNull( ins[ ammo_1 ] );
			Assert.AreEqual( 2, ins[ ammo_1 ].Count );
		}

		[Test]
		public void should_retrive_the_object_from_the_container()
		{
			var bullet_1 = load_bullet_slow();

			Ammo ammo_1 = Ammo.CreateInstance<Ammo>();
			ammo_1.prefab_bullet = bullet_1.GetComponent<
				Bullet_motor>();

			var ins = Ammo_pool.instance;
			ins.clean_container_immediate();
			Assert.IsNull( ins[ ammo_1 ] );

			var inst_bullet_1 = ins.pop( ammo_1 );
			var inst_bullet_2 = ins.pop( ammo_1 );
			ins.push( inst_bullet_1 );
			ins.push( inst_bullet_2 );
			Assert.IsNotNull( ins[ ammo_1 ] );
			Assert.AreEqual( 2, ins[ ammo_1 ].Count );
			Assert.AreEqual( 2, ins.container.childCount );
			ins.pop( ammo_1 );
			Assert.AreEqual( 1, ins[ ammo_1 ].Count );
			Assert.AreEqual( 1, ins.container.childCount );
			ins.pop( ammo_1 );
			Assert.AreEqual( 0, ins[ ammo_1 ].Count );
			Assert.AreEqual( 0, ins.container.childCount );
		}

		protected GameObject load_bullet_slow()
		{
			string bullet_1_path = "prefab/base/weapon/gun/bullet/slow";
			var bullet_1 = Resources.Load( bullet_1_path ) as GameObject;
			Assert.IsNotNull(bullet_1, "la bala slow no existe" );
			return bullet_1;
		}

		protected GameObject load_bullet_fast()
		{
			string bullet_1_path = "prefab/base/weapon/gun/bullet/fast";
			var bullet_1 = Resources.Load( bullet_1_path ) as GameObject;
			Assert.IsNotNull(bullet_1, "la bala fast no existe" );
			return bullet_1;
		}
	}
}
