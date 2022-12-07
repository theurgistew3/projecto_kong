using System.Collections.Generic;
using UnityEngine;
using chibi.controller.avatar;

namespace chibi.dialog
{
	[System.Serializable]
	public class Actor_propeties
	{
		public bool mirrored = false;
		public chibi.animator.avatar.Emotions emotion;
	}

	[System.Serializable]
	public class Actors
	{
		public List<Controller_avatar> actors;
		public List<Actor_propeties> propierties;
	}

	[ CreateAssetMenu( menuName="dialogue/base" ) ]
	public class Dialogue : chibi.Chibi_object
	{
		[TextArea( 3, 10 )]
		public List<string> texts;
		public List< Actors > actors;

		public List<Controller_avatar> avatars;
	}
}
