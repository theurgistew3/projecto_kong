using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using chibi.inventory.ui;
using chibi.inventory.item;

namespace tests.inventory
{
	public class Inventory_ui_test : helper.tests.Scene_test
	{
		Item puerro, dango;
		chibi.inventory.ui.Inventory_ui inventory;

		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/inventory/inventory_ui";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			inventory =  helper.game_object.Find._<Inventory_ui>(
				scene, "inventory" );
			puerro = Resources.Load<Item>( "object/inventary/item/test puerro" );
			dango = Resources.Load<Item>( "object/inventary/item/test dango" );
		}

		[UnityTest]
		public IEnumerator when_start_the_dict_should_be_empty()
		{
			yield return new WaitForSeconds( 0.1f );
			Assert.AreEqual( inventory.items.Count, 0 );
			yield return new WaitForSeconds( 0.1f );
		}

		[UnityTest]
		public IEnumerator when_add_a_single_should_be_apppear_in_the_items()
		{
			yield return new WaitForSeconds( 0.1f );
			inventory.add( puerro );
			Assert.AreEqual( 1, inventory.items.Count );
			yield return new WaitForSeconds( 0.1f );
		}

		[UnityTest]
		public IEnumerator when_add_two_diferent_items_should_add_2_properties()
		{
			yield return new WaitForSeconds( 0.1f );
			inventory.add( puerro );
			inventory.add( dango );
			Assert.AreEqual( 2, inventory.items.Count );
			yield return new WaitForSeconds( 0.1f );
		}

		[UnityTest]
		public IEnumerator add_twice_the_item_should_increase_the_stack()
		{
			yield return new WaitForSeconds( 0.1f );
			inventory.add( puerro );
			inventory.add( puerro );
			Assert.AreEqual( 1, inventory.items.Count );
			Assert.AreEqual( 1, inventory.items[ puerro ].Count );
			Assert.AreEqual( 2, inventory.items[ puerro ][0].amount );
			yield return new WaitForSeconds( 0.1f );
		}

		[UnityTest]
		public IEnumerator add_with_amount_should_add_the_amount()
		{
			yield return new WaitForSeconds( 0.1f );
			inventory.add( puerro, 3 );
			Assert.AreEqual( 1, inventory.items.Count );
			Assert.AreEqual( 1, inventory.items[ puerro ].Count );
			Assert.AreEqual( 3, inventory.items[ puerro ][0].amount );
			yield return new WaitForSeconds( 0.1f );
		}

		[UnityTest]
		public IEnumerator if_the_max_stack_is_reach_should_add_more_stacks()
		{
			yield return new WaitForSeconds( 0.1f );
			inventory.add( puerro, 8 );
			Assert.AreEqual( 1, inventory.items.Count );
			Assert.AreEqual( 2, inventory.items[ puerro ].Count );
			Assert.AreEqual( 5, inventory.items[ puerro ][0].amount );
			Assert.AreEqual( 3, inventory.items[ puerro ][1].amount );
			yield return new WaitForSeconds( 0.1f );
		}

		[UnityTest]
		public IEnumerator should_work_with_big_amounts()
		{
			yield return new WaitForSeconds( 0.1f );
			inventory.add( puerro, 100 );
			Assert.AreEqual( 1, inventory.items.Count );
			Assert.AreEqual( 20, inventory.items[ puerro ].Count );
			foreach ( var p in inventory.items[ puerro ] )
			{
				Assert.AreEqual( 5, p.amount );
			}
			yield return new WaitForSeconds( 0.1f );
		}
	}
}
