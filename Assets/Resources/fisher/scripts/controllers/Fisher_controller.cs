using UnityEngine;
using chibi.inventory;

namespace fisher.controller
{
	public class Fisher_controller : chibi.controller.npc.Controller_npc
	{
		public chibi.weapon.gun.Gun gun;
		public GameObject prefab_target_net;
		public chibi.inventory.Inventory inventory;
		public fisher.game_manager.Game_manager_fisher manager;
		public chibi.inventory.item.Item band_fish;

		public void throw_net( Vector3 position )
		{
			gun.aim_to( position );
			var bullet = (Controller_bullet_net)gun.shot();
			var target_net = helper.instantiate._( prefab_target_net, position );
			bullet.target = target_net.transform;
			bullet.origin = transform;
			bullet.owner = this;
		}

		internal void grab( Item item )
		{
			if ( band_fish == item.item )
			{
				item.recycle();
				manager.add_band_fish();
			}
			else
			{
				inventory.add( item );
				manager.AddPointsScore();
			}
		}

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !gun )
				Debug.Log( string.Format(
					"[Fisher_controller] no tiene asigna un arma en '{0}'",
					helper.game_object.name.full( this ), gameObject ) );

			if ( !inventory )
			{
				inventory = GetComponent<chibi.inventory.Inventory>();
				if ( !inventory )
					Debug.LogError( string.Format(
						"[Fisher_controller] no se encontro el inventario en '{0}'",
						helper.game_object.name.full( this ), gameObject ) );
			}
		}
	}
}