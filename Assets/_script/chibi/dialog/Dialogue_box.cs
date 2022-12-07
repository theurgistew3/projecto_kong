using System.Collections.Generic;
using UnityEngine;
using chibi.controller.avatar;

namespace chibi.dialog
{
	public class Dialogue_box : chibi.Chibi_behaviour
	{
		public Dialogue dialogues;
		public float letters_by_second = 10;
		public int current_dialogue = 0;
		public bool put_texy = false;

		public float total_delta_time = 0f;

		public UnityEngine.UI.Text dialogue_box;

		public List<Transform> place_of_actors;
		protected List<Controller_avatar> _instanciate_actors;

		public obj.Incremental_text text;

		public string current_text
		{
			get {
				return dialogues.texts[ current_dialogue ];
			}
		}

		public List<Controller_avatar> avatars
		{
			get {
				return dialogues.avatars;
			}
		}

		public List<Controller_avatar> actors
		{
			get {
				try
				{
					return dialogues.actors[ current_dialogue ].actors;
				}
				catch ( System.IndexOutOfRangeException )
				{
					return null;
				}
				catch ( System.NullReferenceException )
				{
					return null;
				}
				catch ( System.ArgumentOutOfRangeException )
				{
					return null;
				}
			}
		}

		public List<Actor_propeties> actors_propeties
		{
			get {
				try
				{
					return dialogues.actors[ current_dialogue ].propierties;
				}
				catch ( System.IndexOutOfRangeException )
				{
					return null;
				}
				catch ( System.NullReferenceException )
				{
					return null;
				}
				catch ( System.ArgumentOutOfRangeException )
				{
					return null;
				}
			}
		}

		public void start_dialogue()
		{
			put_texy = true;
			dialogue_box.text = "";
			text.reset();
			set_actors_in_place();
		}

		public void pull_all_text()
		{
			dialogue_box.text = current_text;
			text.fill_elapse_time();
			put_texy = false;
		}

		public void next_dialog()
		{
			++current_dialogue;
			if ( current_dialogue >= dialogues.texts.Count )
				current_dialogue = dialogues.texts.Count - 1;
			else
			{
				total_delta_time = 0f;
				put_texy = true;
				set_actors_in_place();
			}
		}

		public void previous_dialog()
		{
			--current_dialogue;
			if ( current_dialogue < 0 )
				current_dialogue = 0;
			else
			{
				total_delta_time = 0f;
				put_texy = true;
				set_actors_in_place();
			}
		}

		public void set_actors_in_place()
		{
			var actors = this.actors;
			for ( int i = 0; i < _instanciate_actors.Count; ++i )
			{
				if ( !_instanciate_actors[ i ] )
					continue;
				Destroy( _instanciate_actors[ i ].gameObject );
				_instanciate_actors[ i ] = null;
			}
			if ( actors == null )
				return;
			for ( int i = 0; i < actors.Count; ++i )
			{
				var actor = actors[ i ];
				if ( !actor )
				{
					continue;
				}
				var place = place_of_actors[ i ];
				var avatar = helper.instantiate.parent<Controller_avatar>(
					actor, place, true, "avatar" );
				_instanciate_actors.Add( avatar );
				var properties = actors_propeties;
				if ( properties == null )
					continue;
				try
				{
					var propertie = properties[ i ];
					if ( propertie != null )
						avatar.set_properties( propertie );
				}
				catch ( System.IndexOutOfRangeException )
				{
					continue;
				}
				catch ( System.NullReferenceException )
				{
					continue;
				}
				catch ( System.ArgumentOutOfRangeException )
				{
					continue;
				}
			}
		}

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !dialogue_box )
				Debug.LogError(
					string.Format(
						"the dialog box {0} no have assigned the TextBox",
						helper.game_object.name.full( this ) ) );
			if ( !dialogues )
				Debug.LogError(
					string.Format(
						"the dialog box {0} no have dialogues",
						helper.game_object.name.full( this ) ) );
			_instanciate_actors = new List<Controller_avatar>();
		}

		private void Update()
		{
			if ( put_texy )
			{
				text.text = current_text;
				text.tick( Time.deltaTime );
				dialogue_box.text = text.current_text;
			}

			/*
			if ( put_texy )
			{
				total_delta_time += Time.deltaTime;
				float total_of_letters = ( letters_by_second * total_delta_time );

				// detener el calculo cuando escriba todas las letras
				if ( total_of_letters >= current_text.Length )
				{
					put_texy = false;
					pull_all_text();
				}
				else
					dialogue_box.text = current_text.Substring(
						0, Mathf.RoundToInt( total_of_letters ) );
			}
			*/
		}
	}
}
