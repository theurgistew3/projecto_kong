using chibi.rol_sheet;
using UnityEngine;


namespace chibi.inventory.item
{
	[ CreateAssetMenu( menuName="chibi/inventary/item/buff" ) ]
	public class Buff : Item
	{
		public chibi.rol_sheet.buff.Buff buff;

		public override bool use( Rol_sheet rol_sheet )
		{
			rol_sheet.attach_buff( buff );
			return true;
		}
	}
}
