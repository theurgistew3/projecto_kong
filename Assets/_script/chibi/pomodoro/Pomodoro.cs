using UnityEngine;


namespace chibi.pomodoro
{
	[ CreateAssetMenu( menuName="chibi/pomodoro/base" ) ]
	public class Pomodoro : chibi.Chibi_object
	{
		public float frecuency = 1f;
		public float _sigma_frecuency = 0f;

		public bool is_enable = true;

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

		public void add_to_global()
		{
			singleton.pomodoro.Pomodoro_singleton.instance.add( this );
		}
	}
}

