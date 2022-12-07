using UnityEngine;
using UnityEngine.UI;
using chibi.controller;

namespace aventure_time.dialog
{
	public class Bubble_dialog: chibi.Chibi_ui
	{
		public GameObject bubble;
		public TMPro.TextMeshProUGUI text_ui;

		public chibi.dialog.obj.Incremental_text text;

		public bool is_complete
		{
			get {
				return text.is_complete;
			}
		}

		public void start_dialog( Message message )
		{
			if ( message is Text text )
				start_dialog( text );
			else
				debug.error( "el mensaje {0} no se pudo transformar en text", message );
		}

		public void start_dialog( aventure_time.dialog.Text message )
		{
			text.reset();
			text.text = message.text;
		}

		private void Update()
		{
			text.tick( Time.deltaTime );
			text_ui.text = text.current_text;
		}

		public override void hide()
		{
			bubble.gameObject.SetActive( false );
			text_ui.gameObject.SetActive( false );
		}

		public override void show()
		{
			bubble.gameObject.SetActive( true );
			text_ui.gameObject.SetActive( true );
		}
	}
}