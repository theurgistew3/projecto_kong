using UnityEngine;
using chibi.rol_sheet.buff;
using chibi.rol_sheet;


namespace fisher.rol_sheet.buff.reproduction
{
	[ CreateAssetMenu( menuName="fisher/rol sheet/buff/fish/reprodution" ) ]
	public class Reproduction_cicle : Buff
	{
		public float amount = 0.05f;

		public override void effect_in_rol_sheet( Rol_sheet rol_sheet )
		{
			var rol = rol_sheet as Fish_sheet;
			float delta_amount = amount * delta;
			if ( rol.want_to_reproducing )
			{
				rol.reproduction.current -= delta_amount;
				if ( rol.reproduction.is_empty )
					rol.want_to_reproducing = false;
			}
			else
			{
				rol.reproduction.current += delta_amount;
				if ( rol.reproduction.is_full )
					rol.want_to_reproducing = true;
			}
		}
	}
}