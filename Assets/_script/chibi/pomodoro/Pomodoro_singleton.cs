using UnityEngine;
using System.Collections.Generic;
using chibi.pomodoro;

namespace singleton.pomodoro
{
	public class Pomodoro_singleton : Singleton<Pomodoro_singleton>
	{
		protected List<Pomodoro> _list;
		protected Transform container;

		public string container_name
		{
			get {
				return "pomodoros";
			}
		}

		protected virtual void OnEnable()
		{
			_list = new List<Pomodoro>();

			if ( !container )
			{
				container = helper.game_object.prepare.stuff_container(
					container_name ).transform;
				container.gameObject.AddComponent<Pomodoro_ticker>();
			}
		}

		public void add( Pomodoro pomodoro )
		{
			_list.Add( pomodoro );
		}

		public void tick()
		{
			foreach ( var pomodoro in _list )
				pomodoro.tick();
		}
	}
}