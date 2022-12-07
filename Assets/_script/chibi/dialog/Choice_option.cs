using System.Collections.Generic;
using UnityEngine;
using chibi.controller.avatar;

namespace chibi.dialog
{
	public class Choice_option : chibi.Chibi_ui
	{
		public TMPro.TextMeshProUGUI text_ui;

		public string text
		{
			get {
				return text_ui.text;
			}
			set {
				text_ui.text = value;
			}
		}

		protected override void _init_cache()
		{
			base._init_cache();
			if ( text_ui )
			{
				debug.error( "el text_ui no esta asignado" );
			}
		}
	}
}
