using System;
using UnityEngine;


namespace chibi.pomodoro
{
	[Serializable]
	public class Pomodoro_obj
	{
		public float frecuency = 1f;
		public bool is_enable = true;

		[HideInInspector]
		public float _sigma_frecuency = 0f;

		public Pomodoro_obj( float frecuency )
		{
			this.frecuency = frecuency;
		}

		public bool is_time
		{
			get {
				return _sigma_frecuency > frecuency;
			}
		}

		public void reset()
		{
			_sigma_frecuency = 0f;
		}

		public bool tick()
		{
			return tick( Time.deltaTime );
		}

		public bool tick( float delta_time )
		{
			if ( is_enable )
			{
				_sigma_frecuency += delta_time;
				return is_time;
			}
			return false;
		}
	}
}

