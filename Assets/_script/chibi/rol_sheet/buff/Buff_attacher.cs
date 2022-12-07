using UnityEngine;


namespace chibi.rol_sheet.buff
{
	[ SerializeField ]
	public class Buff_attacher
	{
		[ SerializeField ]
		public Buff buff;
		public float _total_duration = 0f;
		protected float _delta_sigma = 0f;

		public chibi.rol_sheet.Rol_sheet rol_sheet;

		public Buff_attacher( Buff buff, chibi.rol_sheet.Rol_sheet rol_sheet )
		{
			this.buff = buff;
			this.rol_sheet = rol_sheet;
			attach();
		}

		public float total_duration {
			get {
				return _total_duration;
			}

			set {
				_total_duration = value;
				if ( !buff.no_duration_limit && total_duration > buff.duration )
					unattach();
			}
		}

		public float delta_sigma
		{
			get {
				return _delta_sigma;
			}
			set {
				_delta_sigma = value;
				if ( delta_sigma > buff.delta )
				{
					effect_in_rol_sheet();
					_delta_sigma -= buff.delta;
				}
			}
		}

		public void effect_in_rol_sheet()
		{
			buff.effect_in_rol_sheet( rol_sheet );
		}

		public void attach()
		{
			buff.attach( rol_sheet );
		}

		public void unattach()
		{
			rol_sheet.unattach_buff( this );
		}
	}
}
