using System.Collections.Generic;
using UnityEngine;
using chibi.controller.avatar;

namespace chibi.dialog
{
	public class Console_box : chibi.Chibi_behaviour
	{
		public obj.Incremental_text text;
		public TMPro.TextMeshProUGUI text_box;

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !text_box )
				debug.error( "la consola de texto no tiene un textbox asignado" );
		}

		private void Update()
		{
			text.tick( Time.deltaTime );
			text_box.text = text.current_text;
		}
	}
}
