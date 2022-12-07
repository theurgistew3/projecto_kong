using UnityEngine;
using System.Collections.Generic;
using chibi.rol_sheet.buff;


namespace chibi.rol_sheet
{
	public class Rol_sheet: chibi.Chibi_behaviour
	{
		public Sheet sheet;
		[ SerializeField ]
		public List<Buff_attacher> buffos;
		public List<Buff> start_buffos;
		protected List<Buff_attacher> buffos_are_going_to_remove;
		public chibi.tool.reference.Stat_reference hp;

		public chibi.rol_sheet.rpg.Attributes attributes;


#if UNITY_EDITOR
		[HideInInspector]
		public bool show_attributes_editor = false;
 #endif

		public chibi.damage.motor.HP_engine hp_engine
		{
			get {
				var hp_engine = GetComponent<chibi.damage.motor.HP_engine>();
				if ( !hp_engine )
				{
					debug.error( "no tiene hp_engine" );
				}
				return hp_engine;
			}
		}

		public void attach_buff( Buff buff )
		{
			Buff_attacher buff_attacher;
			buff_attacher = new Buff_attacher( buff, this );
			buffos.Add( buff_attacher );
		}

		public void unattach_buff( Buff_attacher buff )
		{
			buff.buff.unattach( this );
			buffos_are_going_to_remove.Add( buff );
		}

		protected override void _init_cache()
		{
			base._init_cache();
			buffos = new List<Buff_attacher>();
			buffos_are_going_to_remove = new List<Buff_attacher>();
			if ( start_buffos != null )
			{
				foreach ( var buff in start_buffos )
					attach_buff( buff );
			}
		}

		public void clean()
		{
			foreach ( var buff in buffos_are_going_to_remove )
			{
				buffos.Remove( buff );
			}
			buffos_are_going_to_remove.Clear();
		}

		private void Update()
		{
			var delta_time = Time.deltaTime;
			foreach ( var buff_attacher in buffos )
			{
				buff_attacher.total_duration += delta_time;
				buff_attacher.delta_sigma += delta_time;
			}
			clean();
		}
	}
}