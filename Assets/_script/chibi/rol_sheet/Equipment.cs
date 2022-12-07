using UnityEngine;
using System.Collections.Generic;
using chibi.rol_sheet.buff;
using chibi.inventory.item;


namespace chibi.rol_sheet
{
	public class Equipment : chibi.Chibi_behaviour
	{
		public chibi.inventory.item.Weapon left_arm = null;
		public chibi.inventory.item.Weapon right_arm = null;

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !left_arm )
			{
				debug.warning(
					"no se asigno el default de left arm, usando el default" );
				left_arm = Weapon.CreateInstance<Weapon>().find_default<Weapon>();
			}
			if ( !right_arm)
			{
				debug.warning(
					"no se asigno el default de right arm, usando el default" );
				right_arm = Weapon.CreateInstance<Weapon>().find_default<Weapon>();
			}
		}
	}
}