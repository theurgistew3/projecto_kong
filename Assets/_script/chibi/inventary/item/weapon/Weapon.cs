using System.Collections.Generic;
using UnityEngine;


namespace chibi.inventory.item
{
	[ CreateAssetMenu( menuName="chibi/inventary/item/weapon" ) ]
	public class Weapon : Item
	{
		public List< damage.Damage_struct > damages;

		public override string path_of_the_default
		{
			get { return "RPG/prefab/obj/weapon/punch"; }
		}
	}
}
