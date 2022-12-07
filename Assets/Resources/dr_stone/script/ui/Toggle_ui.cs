using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dr_stone.ui
{
	public class Toggle_ui : chibi.Chibi_behaviour
	{
		public GameObject object_to_toggle;

		public void toggle()
		{
			if ( object_to_toggle )
				object_to_toggle.SetActive( !object_to_toggle.activeSelf );
			else
				debug.error( "no tiene un objeto para hacer toggle" );
		}
	}
}
