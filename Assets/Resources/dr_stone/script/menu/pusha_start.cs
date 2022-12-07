using UnityEngine;
using UnityEngine.SceneManagement;

namespace dr_stone.menu
{
	public class pusha_start : chibi.Chibi_behaviour
	{
		public string next_scene = "Resources/dr_stone/scene/menu";

		protected void Update()
		{
			if ( Input.anyKey )
			{
				SceneManager.LoadScene( next_scene );
			}
		}
	}
}
