using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace tests.dialog
{
	public class Dialogue_box : helper.tests.Scene_test
	{
		chibi.dialog.Dialogue_box dialogue_box;
		UnityEngine.UI.Text text_box;
		public override string scene_dir
		{
			get {
				return "tests/scene/chibi/dialog/dialog_manager";
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
		public IEnumerator when_the_dialog_start_should_show_the_text()
		{
			string original_text = text_box.text;
			dialogue_box.start_dialogue();
			yield return new WaitForSeconds( 1f );
			Assert.AreNotEqual( original_text, dialogue_box.dialogue_box.text );
			yield return new WaitForSeconds( 1f );
			Assert.Greater( dialogue_box.dialogue_box.text.Length, 1 );
			Assert.IsTrue(
				dialogue_box.current_text.Contains(
					dialogue_box.dialogue_box.text ) );
		}

		[UnityTest]
		public IEnumerator if_start_is_no_executre_should_mantenint_the_text()
		{
			string original_text = text_box.text;
			yield return new WaitForSeconds( 1f );
			Assert.AreEqual( original_text, dialogue_box.dialogue_box.text );
			yield return new WaitForSeconds( 1f );
		}

		[UnityTest]
		public IEnumerator when_execute_start_should_clean_the_text()
		{
			dialogue_box.start_dialogue();
			Assert.AreEqual( "", dialogue_box.dialogue_box.text );
			yield return new WaitForSeconds( 1f );
		}

		[UnityTest]
		public IEnumerator pull_text_before_start_should_put_new_text()
		{
			Assert.AreNotEqual( dialogue_box.dialogue_box.text[ 0 ], "" );
			dialogue_box.pull_all_text();
			Assert.AreEqual(
				dialogue_box.dialogues.texts[0], dialogue_box.dialogue_box.text );
			yield return new WaitForSeconds( 1f );
		}

		[UnityTest]
		public IEnumerator pull_text_after_start_no_add_more_dialog_of_nesesary()
		{
			dialogue_box.start_dialogue();
			yield return new WaitForSeconds( 0.1f );
			dialogue_box.pull_all_text();
			Assert.AreEqual(
				dialogue_box.dialogues.texts[0], dialogue_box.dialogue_box.text );
			yield return new WaitForSeconds( 1f );
		}

		[UnityTest]
		public IEnumerator next_dialog_should_move_the_next_one()
		{
			dialogue_box.start_dialogue();
			yield return new WaitForSeconds( 0.1f );
			dialogue_box.next_dialog();
			dialogue_box.pull_all_text();
			Assert.AreEqual(
				dialogue_box.dialogues.texts[1], dialogue_box.dialogue_box.text );
			yield return new WaitForSeconds( 1f );
		}

		[UnityTest]
		public IEnumerator next_dialog_should_reset_the_delta_time()
		{
			dialogue_box.start_dialogue();
			yield return new WaitForSeconds( 0.1f );
			dialogue_box.next_dialog();
			Assert.AreEqual( dialogue_box.total_delta_time, 0 );
			yield return new WaitForSeconds( 1f );
		}

		[UnityTest]
		public IEnumerator prev_dialog_should_reset_the_delta_time()
		{
			dialogue_box.start_dialogue();
			yield return new WaitForSeconds( 0.1f );
			dialogue_box.next_dialog();
			dialogue_box.pull_all_text();
			yield return new WaitForSeconds( 1f );
			dialogue_box.previous_dialog();
			Assert.AreEqual( dialogue_box.total_delta_time, 0 );
			yield return new WaitForSeconds( 1f );
		}

		[UnityTest]
		public IEnumerator prev_dialog_should_move_the_next_one()
		{
			dialogue_box.start_dialogue();
			yield return new WaitForSeconds( 0.1f );
			dialogue_box.next_dialog();
			dialogue_box.pull_all_text();
			Assert.AreEqual(
				dialogue_box.dialogues.texts[1], dialogue_box.dialogue_box.text );

			yield return new WaitForSeconds( 1f );
			dialogue_box.previous_dialog();
			dialogue_box.pull_all_text();
			Assert.AreEqual(
				dialogue_box.dialogues.texts[0], dialogue_box.dialogue_box.text );
			yield return new WaitForSeconds( 1f );
		}
	}
}
