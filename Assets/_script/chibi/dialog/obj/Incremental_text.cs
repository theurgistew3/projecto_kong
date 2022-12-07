using System.Collections.Generic;
using UnityEngine;
using chibi.controller.avatar;

namespace chibi.dialog.obj
{
	[System.Serializable]
	public class Incremental_text
	{
		public string text = "";
		public float letters_by_second = 10;
		public float elapse_time = 0f;

		public void tick( float delta_time )
		{
			if ( is_not_complete )
			{
				elapse_time += delta_time;
			}
		}

		public string current_text
		{
			get {
				return text.Substring( 0, current_amount_of_letters );
			}
		}

		public int current_amount_of_letters
		{
			get {
				int letters = Mathf.RoundToInt( letters_by_second * elapse_time );
				return Mathf.Clamp( letters, 0, text.Length );
			}
		}

		public bool is_complete
		{
			get {
				return current_amount_of_letters >= text.Length;
			}
		}

		public bool is_not_complete
		{
			get {
				return !is_complete;
			}
		}

		public float time_to_put_all_text
		{
			get {
				return text.Length * letters_by_second;
			}
		}

		public void fill_elapse_time()
		{
			elapse_time = time_to_put_all_text;
		}

		public void reset()
		{
			elapse_time = 0f;
		}
	}
}
