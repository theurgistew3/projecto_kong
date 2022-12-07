using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using chibi.inventory.ui;
using chibi.inventory.item;

namespace tests.inventory.navigation
{
	public class Inventory_test: helper.tests.Scene_test
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
		public IEnumerator when_add_two_diferent_items_should_add_2_properties()
		{
			yield return new WaitForSeconds( 1.0f );
			inventory.add( puerro );
			yield return new WaitForSeconds( 1.0f );
			inventory.add( dango );
			yield return new WaitForSeconds( 1.0f );
			inventory.add( dango );
			yield return new WaitForSeconds( 1.0f );
			Assert.Fail( "es una prueba de navegacion" );
		}

	}
}
