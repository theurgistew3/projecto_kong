using UnityEngine;
using System.Collections.Generic;


namespace chibi.inventory
{
	public class Inventory : chibi.Chibi_behaviour
	{
		public Transform container;
		public Dictionary<item.Item, List<Item>> items;

		public void add( item.Item item )
		{
			throw new System.NotImplementedException();
		}

		public void add( Item item, int amount=-1 )
		{
			List<Item> list_items = new List<Item>();
			if ( !items.TryGetValue( item.item, out list_items ) )
			{
				list_items = new List<Item>();
				items.Add( item.item, list_items );
			}
			if ( item.item.is_stackable )
			{
				throw new System.NotImplementedException();
			}
			if ( amount < 0 )
			{
				list_items.Add( item );
				add_single_item( item );
			}
			else
				throw new System.NotImplementedException();
		}

		protected virtual Item clone_item( Item item )
		{
			Item result = helper.instantiate.parent<Item>( item, container );
			return result;
		}

		protected virtual void add_single_item( Item item )
		{
			item.gameObject.SetActive( false );
			item.transform.parent = container;
		}

		protected override void _init_cache()
		{
			base._init_cache();
			items = new Dictionary<item.Item, List<Item>>();
			if ( !container )
				Debug.LogError( string.Format(
					"no esta asignado el contenedor de los items en '{0}'",
					helper.game_object.name.full( this ) ) );
		}
	}
}
