using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using chibi.inventory.ui;
using chibi.inventory.item;

namespace tests.inventory
{
	public class Item_property_test: helper.tests.Scene_test
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
		public IEnumerator when_add_amount_should_return_the_rest_of_the_amount()
		{
			yield return new WaitForSeconds( 0.1f );
			var stack = new items_properties();
			stack.item = puerro;
			int result = stack.add_amount( 10 );
			Assert.AreEqual( 5, result );
			yield return new WaitForSeconds( 0.1f );
		}

		[UnityTest]
		public IEnumerator when_add_after_is_reached_the_max_amount_shuold_return_the_amount()
		{
			yield return new WaitForSeconds( 0.1f );
			var stack = new items_properties();
			stack.item = dango;
			int result = stack.add_amount( 1 );
			Assert.AreEqual( 0, result );
			result = stack.add_amount( 1 );
			Assert.AreEqual( 1, result );
			yield return new WaitForSeconds( 0.1f );
		}

		[UnityTest]
		public IEnumerator if_the_item_no_reach_the_max_amount_should_return_0()
		{
			yield return new WaitForSeconds( 0.1f );
			var stack = new items_properties();
			stack.item = puerro;
			int result = stack.add_amount( 3 );
			Assert.AreEqual( result, 0 );
			yield return new WaitForSeconds( 0.1f );
		}
	}
}
