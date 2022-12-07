using UnityEngine;


namespace chibi.rol_sheet.buff.health
{
	[ CreateAssetMenu( menuName= "chibi/rol sheet/buff/health/poison" ) ]
	public class Poison : Buff
	{
		public float amount = 1f;

		public override void effect_in_rol_sheet( Rol_sheet rol_sheet )
		{
			float delta_amount = -amount * delta;
			rol_sheet.hp.current += delta_amount;
		}
	}
}