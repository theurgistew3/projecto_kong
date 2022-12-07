using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace tests.dialog
{
	public class With_actors : helper.tests.Scene_test
	{
		chibi.dialog.Dialogue_box dialogue_box;
		UnityEngine.UI.Text text_box;
		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/dialog/dialog_manager with actors";
			}
		}

		public override void Instanciate_scenary()
		{
			base.Instanciate_scenary();
			dialogue_box = helper.game_object.Find._<chibi.dialog.Dialogue_box>(
				scene, "dialogue_manager" );
			text_box = dialogue_box.dialogue_box;
		}

		[UnityTest]
		public IEnumerator should_have_setter_the_actors()
		{
			Assert.Greater( dialogue_box.actors.Count, 0 );
			Assert.Greater( dialogue_box.avatars.Count, 0 );
			Assert.Greater( dialogue_box.place_of_actors.Count, 0 );
			yield return new WaitForSeconds( 0.1f );
		}

		[UnityTest]
		public IEnumerator when_start_should_place_the_actors()
		{
			dialogue_box.start_dialogue();
			yield return new WaitForSeconds( 0.1f );
			Transform place_1 = dialogue_box.place_of_actors[0];
			var avatar = place_1.Find( "avatar" );
			Assert.IsNotNull( avatar );
			yield return new WaitForSeconds( 1f );
		}

		[UnityTest]
		public IEnumerator when_set_the_first_actor_should_be_mirrored()
		{
			dialogue_box.start_dialogue();
			yield return new WaitForSeconds( 0.1f );
			Transform place_1 = dialogue_box.place_of_actors[0];
			var avatar = place_1.Find( "avatar" );
			var model = avatar.Find( "model" );
			Assert.AreEqual( model.localScale.x, -1 );
			//var image = model.GetComponent<Image>();
			//Assert.IsNotNull( avatar );
			yield return new WaitForSeconds( 1f );
		}

		[UnityTest]
		public IEnumerator when_2_dialog_should_put_avatar_in_place_2()
		{
			dialogue_box.start_dialogue();
			yield return new WaitForSeconds( 1f );
			dialogue_box.next_dialog();
			yield return new WaitForSeconds( 1f );
			Transform place_1 = dialogue_box.place_of_actors[0];
			var avatar_1 = place_1.Find( "avatar" );
			Assert.IsNull( avatar_1 );

			Transform place_2 = dialogue_box.place_of_actors[1];
			var avatar = place_2.Find( "avatar" );
			Assert.IsNotNull( avatar );
			var model = avatar.Find( "model" );
			Assert.AreEqual( model.localScale.x, 1 );
			yield return new WaitForSeconds( 1f );
		}

		[UnityTest]
		public IEnumerator when_3_dialog_should_no_have_avatars()
		{
			dialogue_box.start_dialogue();
			yield return new WaitForSeconds( 1f );
			dialogue_box.next_dialog();
			yield return new WaitForSeconds( 1f );
			dialogue_box.next_dialog();
			yield return new WaitForSeconds( 1f );

			Transform place_1 = dialogue_box.place_of_actors[0];
			var avatar_1 = place_1.Find( "avatar" );
			Assert.IsNull( avatar_1 );

			Transform place_2 = dialogue_box.place_of_actors[1];
			var avatar = place_2.Find( "avatar" );
			Assert.IsNull( avatar );
			yield return new WaitForSeconds( 1f );
		}
	}
}
