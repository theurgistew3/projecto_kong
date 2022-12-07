using System.Collections.Generic;
using UnityEngine;
using chibi.controller.avatar;

namespace chibi.dialog.narrator
{
	public class Narrator_console : chibi.Chibi_behaviour
	{
		public Console_box output;
		public UnityEngine.UI.InputField input;

		public Transform choice_container;
		public GameObject prefab_choice;
		protected List<Choice_option> choices;

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !output )
				debug.error( "no esta asignado el output" );
			if ( !input )
				debug.error( "no esta asignado el input" );

			if ( !choice_container )
				debug.error( "no tiene el contenerdor de los choice" );
			if ( !prefab_choice)
				debug.error( "no tiene el prefab de las choice" );

			choices = new List<Choice_option>( find_choices() );
		}

		public void clear_inout()
		{
			input.text = "";
		}

		public void disable_input()
		{
			input.interactable = false;
		}

		public void enable_input()
		{
			input.interactable = true;
		}

		public void focus_input()
		{
			input.ActivateInputField();
		}

		protected Choice_option build_choice()
		{
			var instance = helper.instantiate.parent( prefab_choice, choice_container );
			return instance.GetComponent<Choice_option>();
		}

		protected Choice_option[] find_choices()
		{
			var choices = choice_container.GetComponentsInChildren<Choice_option>();
			return choices;
		}
	}
}
