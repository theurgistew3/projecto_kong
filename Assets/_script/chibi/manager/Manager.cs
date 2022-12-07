using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace chibi.game_manager
{
	public class Manager : chibi.Chibi_behaviour
	{
		public UnityEvent on_start;
		public UnityEvent on_end;

		protected override void OnEnable()
		{
			base.OnEnable();
			if ( on_start != null )
				on_start.Invoke();
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			if ( on_end != null )
				on_end.Invoke();
		}
	}
}
