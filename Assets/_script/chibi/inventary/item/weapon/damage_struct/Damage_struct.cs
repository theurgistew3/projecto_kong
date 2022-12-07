using chibi.rol_sheet;
using UnityEngine;


namespace chibi.inventory.item.damage
{
	[System.Serializable]
	public class Damage_struct : System.ICloneable
	{
		public chibi.damage.damage.Damage damage;
		public int amount;
		public chibi.rol_sheet.Rol_sheet owner;

		public object Clone()
		{
			Damage_struct clone = new Damage_struct();
			clone.damage = this.damage;
			clone.amount = this.amount;
			clone.owner = this.owner;
			return clone;
		}
	}
}
