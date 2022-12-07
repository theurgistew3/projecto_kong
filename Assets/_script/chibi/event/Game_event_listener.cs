using UnityEngine.Events;


namespace chibi.events
{
	public class Game_event_listener : Chibi_behaviour
	{
		public Game_event _event;
		public UnityEvent response;

		protected override void _init_cache()
		{
			base._init_cache();
			_event.register( this );
		}

		protected override void _dispose_cache()
		{
			base._dispose_cache();
			_event.unregister( this );
		}

		public void on_event_raised()
		{
		}
	}
}
