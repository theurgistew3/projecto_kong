using UnityEngine;

namespace chibi
{
	public class Chibi_ui : Chibi_behaviour
	{
		public virtual void hide()
		{
			gameObject.SetActive( false );
		}

		public virtual void show()
		{
			gameObject.SetActive( true );
		}

		public virtual void toggle()
		{
			gameObject.SetActive( !gameObject.activeSelf );
		}
	}
}
