using UnityEngine;
using System.Collections.Generic;


namespace chibi.events
{
	[ CreateAssetMenu( menuName="chibi/event/event" ) ]
	public class Game_event: Chibi_object
	{
		protected List<Game_event_listener> listeners =
			new List<Game_event_listener>();

		public void raises()
		{
			for ( int i = listeners.Count - 1; i >= 0; --i )
				listeners[ i ].on_event_raised();
		}

		public void register( Game_event_listener listener )
		{
			if ( !listeners.Contains( listener ) )
				listeners.Add( listener );
		}

		public void unregister( Game_event_listener listener )
		{
			listeners.Remove( listener );
		}
	}
}
