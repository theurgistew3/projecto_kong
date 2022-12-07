using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using danmaku.controller.npc;
using TMPro;

namespace tests.navigation.touhas.danmaku
{
	public class Touhas : helper.tests.Scene_test
	{
		TextMeshProUGUI name, action;
		Transform spawn_pos;

		public override string scene_dir
		{
			get {
				return "danmaku/tests/scene/navigation/touhas";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			name = helper.game_object.Find._<TextMeshProUGUI>( scene, "name" );
			action = helper.game_object.Find._<TextMeshProUGUI>( scene, "action" );
			spawn_pos = helper.game_object.Find._( scene.transform, "touha" );
		}

		[UnityTest]
		[ Timeout( 120000 ) ]
		public IEnumerator see_all_touhas()
		{
			yield return new WaitForSeconds( 1 );
			var a = Resources.LoadAll<GameObject>( "" );
			for( int i = 0; i < a.Length; ++i )
			{
				GameObject obj = a[i] as GameObject;
				var touha_controller = obj.GetComponent<Touha_controller>();
				if( obj && touha_controller )
				{
					touha_controller = instanciate( obj );
					idle( touha_controller );
					yield return new WaitForSeconds( 1 );

					shot( touha_controller );
					yield return new WaitForSeconds( 3 );

					die( touha_controller );
					yield return new WaitForSeconds( 1 );
				}
			}
			Assert.Fail( "es una prueba de navegacion" );
		}

		protected Touha_controller instanciate( GameObject obj )
		{
			name.text = obj.name;
			var touha = helper.instantiate.parent( obj, spawn_pos );
			var touha_controller = touha.GetComponent<Touha_controller>();
			return touha_controller;
		}

		protected void idle( Touha_controller touha )
		{
			action.text = "idle";
		}

		protected void die( Touha_controller touha )
		{
			touha.died();
			action.text = "died";
		}

		protected void shot( Touha_controller touha )
		{
			touha.shot();
			action.text = "shot";
		}
	}
}
