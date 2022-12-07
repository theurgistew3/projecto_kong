using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using danmaku.controller.weapon.gun;
using danmaku.controller.npc;

namespace danmaku.boss_behaviour
{
	public class Boss_behaviour : chibi.Chibi_behaviour
	{
		[ Header( "boss settings" ) ]
		public Touha_controller touha;
		public chibi.damage.motor.HP_engine hp;
		public chibi.path.Path_behaviour path;
		public List<Controller_gun_pattern> gun_patterns;

		public delegate void on_start_delegate();
		public event on_start_delegate on_start;

		public delegate void on_end_delegate();
		public event on_end_delegate on_end;

		protected IEnumerator __do_behaviour;

		public virtual void start_behaviour()
		{
			__do_behaviour = do_behaviour();
			if ( on_start != null )
				on_start();

			StartCoroutine( __do_behaviour );
		}

		public virtual void end_behaviour()
		{
			StopCoroutine( __do_behaviour );
			if ( on_end != null )
				on_end();
		}

		protected virtual IEnumerator do_behaviour()
		{
			//yield return new WaitForSeconds( 1f );
			yield return null;
			end_behaviour();
		}
	}
}
