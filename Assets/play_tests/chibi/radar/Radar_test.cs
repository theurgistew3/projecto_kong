using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using chibi.radar;

namespace tests.radar
{
	public class Radar_test : helper.tests.Scene_test
	{
		Transform def, item, trigger;

		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/radar/radar box";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			( def, item, trigger ) = helper.game_object.Find._(
				scene.transform, "default", "item", "trigger" );
			Assert.IsTrue( def );
			Assert.IsTrue( item );
			Assert.IsTrue( trigger );
		}

		[UnityTest]
		public IEnumerator when_start_the_radar_should_be_empty()
		{
			yield return new WaitForSeconds( 0.1f );
			List<LayerMask> masks = new List<LayerMask>();
			masks.Add( helper.layer_mask.def );
			var radar = new Radar_box(
				def, Vector3.one, Quaternion.identity, masks );
			Assert.AreEqual( radar.hits.Count, 0 );
			Assert.AreEqual( radar.masks_hits.Count, 0 );
			yield return new WaitForSeconds( 0.1f );
		}

		[UnityTest]
		public IEnumerator should_no_find_himself()
		{
			yield return new WaitForSeconds( 0.1f );
			List<LayerMask> masks = new List<LayerMask>();
			masks.Add( helper.layer_mask.def );
			var radar = new Radar_box(
				def, Vector3.one, Quaternion.identity, masks );
			radar.ping();
			Assert.AreEqual( radar.hits.Count, 0 );
			Assert.AreEqual( radar.masks_hits.Count, 0 );
			yield return new WaitForSeconds( 0.1f );
		}

		[UnityTest]
		public IEnumerator should_find_all_in_the_layer_default()
		{
			yield return new WaitForSeconds( 0.1f );
			List<LayerMask> masks = new List<LayerMask>();
			masks.Add( helper.layer_mask.def );
			var radar = new Radar_box(
				def, Vector3.one * 3, Quaternion.identity, masks );
			radar.ping();
			Assert.AreEqual( radar.hits.Count, 4 );
			Assert.AreEqual( radar.masks_hits.Count, 1 );
			Assert.AreEqual(
				radar.masks_hits[ helper.layer_mask.def ].Count, 4 );

			string[] expected = {
				"default (1)", "default (3)", "trigger", "floor" };

			foreach ( var hit in radar.masks_hits[ helper.layer_mask.def ] )
			{
				bool finded = false;
				foreach ( var s in expected )
					if ( hit.transform.name == s )
						finded = true;

				if ( !finded )
					Assert.Fail( string.Format(
						"no se encontro {0}", hit.transform.name ) );

			}
			yield return new WaitForSeconds( 0.1f );
		}

		[UnityTest]
		public IEnumerator should_find_all_the_objects()
		{
			yield return new WaitForSeconds( 0.1f );
			List<LayerMask> masks = new List<LayerMask>();
			masks.Add( helper.layer_mask.def );
			masks.Add( helper.layer_mask.item );
			var radar = new Radar_box(
				def, Vector3.one * 3, Quaternion.identity, masks );
			radar.ping();
			Assert.AreEqual( radar.hits.Count, 5 );
			Assert.AreEqual( radar.masks_hits.Count, 2 );
			Assert.AreEqual(
				radar.masks_hits[ helper.layer_mask.def ].Count, 4 );
			Assert.AreEqual(
				radar.masks_hits[ helper.layer_mask.item].Count, 1 );

			string[] expected = {
				"default (1)", "default (3)", "trigger", "floor", "item" };

			foreach ( var hit in radar.masks_hits[ helper.layer_mask.def ] )
			{
				bool finded = false;
				foreach ( var s in expected )
					if ( hit.transform.name == s )
						finded = true;

				if ( !finded )
					Assert.Fail( string.Format(
						"no se encontro {0}", hit.transform.name ) );

			}
			yield return new WaitForSeconds( 0.1f );
		}

		[UnityTest]
		public IEnumerator if_no_find_something_should_no_create_layers()
		{
			yield return new WaitForSeconds( 0.1f );
			List<LayerMask> masks = new List<LayerMask>();
			masks.Add( helper.layer_mask.def );
			masks.Add( helper.layer_mask.item );
			var radar = new Radar_box(
				def, Vector3.one, Quaternion.identity, masks );
			radar.ping();
			Assert.AreEqual( radar.hits.Count, 0 );
			Assert.AreEqual( radar.masks_hits.Count, 0 );
			yield return new WaitForSeconds( 0.1f );
		}

		[UnityTest]
		public IEnumerator filter_should_skip_when_no_have_rigetbody()
		{
			yield return new WaitForSeconds( 0.1f );
			List<LayerMask> masks = new List<LayerMask>();
			masks.Add( helper.layer_mask.def );
			masks.Add( helper.layer_mask.item );
			var radar = new Radar_box(
				def, Vector3.one * 3, Quaternion.identity, masks,
				x => x.GetComponent<Rigidbody>() );
			radar.ping();
			Assert.AreEqual( 1, radar.hits.Count );
			yield return new WaitForSeconds( 0.1f );
		}
	}
}
