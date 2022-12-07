using UnityEngine;
using System.Collections.Generic;

namespace chibi.game_manager
{
	public class Enable_over_time : Manager
	{
		public List< Game_object_to_enable > objs;

		public int index = 0;
		public float _current_time = 0f;

		public void Update()
		{
			_current_time += Time.deltaTime;
			if ( _current_time > objs[ index ].target_time )
			{
				_current_time -= objs[ index ].target_time;
				objs[ index ].obj.SetActive( true );
				++index;
				if ( index >= objs.Count )
					gameObject.SetActive( false );
			}
		}

		protected override void _init_cache()
		{
			base._init_cache();
			foreach ( var obj in objs )
				obj.obj.SetActive( false );
			if ( objs.Count == 0 )
				debug.warning( "no tiene objetos a habilitar" );
		}
	}
}
