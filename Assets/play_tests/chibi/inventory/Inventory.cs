using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using chibi.inventory;

namespace tests.inventory
{
	public class Inventory_test : helper.tests.Scene_test
	{
		List<Item> puerros, dangos;
		chibi.inventory.Inventory inventory;

		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/inventory/inventory";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			puerros =  helper.game_object.Find.regex<Item>(
				scene, @"test puerro.*" );
			dangos =  helper.game_object.Find.regex<Item>(
				scene, @"test dango.*" );
			inventory =  helper.game_object.Find._<chibi.inventory.Inventory>(
				scene, "inventory" );
			Assert.Greater( puerros.Count, 0 );
			Assert.Greater( dangos.Count, 0 );
		}

		[UnityTest]
		public IEnumerator when_send_a_item_should_be_disable()
		{
			yield return new WaitForSeconds( 0.1f );
			Item item = puerros[ 0 ];
			inventory.add( item );
			Assert.IsFalse( item.gameObject.activeSelf );
			yield return new WaitForSeconds( 1f );
		}

		[UnityTest]
		public IEnumerator when_add_item_his_parent_should_be_the_container()
		{
			yield return new WaitForSeconds( 0.1f );
			Item item = puerros[ 0 ];
			inventory.add( item );
			Assert.AreEqual( item.transform.parent, inventory.container );
			yield return new WaitForSeconds( 1f );
		}

		[UnityTest]
		public IEnumerator when_add_item_should_create_a_list()
		{
			yield return new WaitForSeconds( 0.1f );
			Item item = puerros[ 0 ];
			Assert.AreEqual( inventory.items.Count, 0 );
			inventory.add( item );
			Assert.AreEqual( inventory.items.Count, 1 );
			var list = inventory.items[ item.item ];
			Assert.AreEqual( list.Count, 1 );
			Assert.Contains( item, list );
			yield return new WaitForSeconds( 1f );
		}

		[UnityTest]
		public IEnumerator when_add_the_same_twice_should_be_added_in_the_list()
		{
			yield return new WaitForSeconds( 0.1f );
			Item item = puerros[ 0 ];
			Assert.AreEqual( inventory.items.Count, 0 );
			inventory.add( puerros[0] );
			inventory.add( puerros[1] );
			Assert.AreEqual( inventory.items.Count, 1 );
			var list = inventory.items[ item.item ];
			Assert.AreEqual( list.Count, 2 );
			Assert.Contains( puerros[0], list );
			Assert.Contains( puerros[1], list );
			yield return new WaitForSeconds( 1f );
		}

		[UnityTest]
		public IEnumerator when_add_different_items_should_create_diferent_list()
		{
			yield return new WaitForSeconds( 0.1f );
			Item item = puerros[ 0 ];
			Assert.AreEqual( inventory.items.Count, 0 );
			inventory.add( puerros[0] );
			inventory.add( dangos[1] );
			Assert.AreEqual( inventory.items.Count, 2 );

			var list = inventory.items[ item.item ];
			Assert.AreEqual( list.Count, 1 );
			Assert.Contains( puerros[0], list );
			Assert.IsFalse( list.Contains( dangos[ 1 ] ) );

			list = inventory.items[ dangos[1].item ];
			Assert.AreEqual( list.Count, 1 );
			Assert.Contains( dangos[1], list );
			Assert.IsFalse( list.Contains( puerros[ 0 ] ) );
			yield return new WaitForSeconds( 1f );
		}
	}
}
