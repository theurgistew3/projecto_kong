using UnityEngine;
using System.Collections.Generic;
using System.Linq;


namespace chibi.inventory.ui
{
	public class Item_slot : chibi.Chibi_behaviour
	{
		protected chibi.inventory.ui.items_properties _item_property;
		public UnityEngine.UI.Image sprite;
		public TMPro.TextMeshProUGUI amount;

		public items_properties item_property
		{
			get {
				return _item_property;
			}

			set {
				_item_property = value;
			}
		}

		protected void Update()
		{
			if ( item_property != null )
			{
				sprite.sprite = item_property.item.image;
				sprite.enabled = true;
				amount.text = item_property.amount.ToString();
			}
			else
			{
				sprite.enabled = false;
				amount.text = "";
			}
		}

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !sprite )
			{
				var obj_sprite = transform.Find( "sprite" );
				if ( obj_sprite )
					sprite = obj_sprite.GetComponent<UnityEngine.UI.Image>();
				else
					debug.error( "no se encontro el gameobject sprite" );
			}
			if ( !sprite )
				debug.error( "no esta asignado el sprite para el item" );

			if ( !amount )
			{
				var obj_amount = transform.Find( "amount" );
				if ( obj_amount )
					amount = obj_amount.GetComponent<TMPro.TextMeshProUGUI>();
				else
					debug.error( "no se encontro el gameobject amount" );
			}
			if ( !amount )
				debug.error( "no se encontro el text para el amount" );
		}
	}
}
