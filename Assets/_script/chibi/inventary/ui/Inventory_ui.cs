using UnityEngine;
using System.Collections.Generic;
using System.Linq;


namespace chibi.inventory.ui
{
	[System.Serializable]
	public class items_properties
	{
		[SerializeField]
		public item.Item item;
		public int amount = 0;

		public int add_amount( int amount )
		{
			int max_amount_can_be_add = item.max_stack_amount - this.amount;
			if ( amount > max_amount_can_be_add )
			{
				this.amount = item.max_stack_amount;
				return amount - max_amount_can_be_add;
			}
			else
			{
				this.amount += amount;
				return 0;
			}
		}

		public int remove_amount( int amount )
		{
			int max_amount_can_be_add = this.amount;
			if ( amount > this.amount )
			{
				var result = amount - this.amount;
				this.amount = 0;
				return result;
			}
			else
			{
				this.amount -= amount;
				return 0;
			}
		}
	}

	public class Inventory_ui : chibi.Chibi_ui
	{
		public Dictionary<item.Item, List<items_properties>> items;
		public Item_slot[] slots;

		public void add( item.Item item )
		{
			add( item, 1 );
		}

		public void add( item.Item item, int amount )
		{
			debug.info( "agregando el item: {0} con cantidad {1}", item.name, amount );
			List<items_properties> stacks = new List<items_properties>();
			if ( !items.TryGetValue( item, out stacks ) )
			{
				debug.info( "agregando nuevo stack a {0}", item.name );
				stacks = new List<items_properties>();
				items.Add( item, stacks );
			}
			add_item_to_stack( item, stacks, amount );
			debug.info( "stacks count {0}", stacks.Count );
			redraw_slots();
		}

		public void remove( item.Item item, int amount )
		{
			List<items_properties> stacks = new List<items_properties>();
			if ( items.TryGetValue( item, out stacks ) )
			{
				debug.info( "eliminando el item: {0} con cantidad {1}", item.name, amount );
				remove_item_to_stack( item, stacks, amount );
				redraw_slots();
			}
			else
			{
				debug.warning( "el inventario no tiene los stacks para {0}", item.name );
			}
		}

		protected override void _init_cache()
		{
			base._init_cache();
			items = new Dictionary<item.Item, List<items_properties>>();
			find_all_slots();
		}

		protected items_properties build_item_property( item.Item item )
		{
			var item_prop = new items_properties();
			item_prop.item = item;
			debug.info( "creado un nuevo item property con {0}", item.name );
			return item_prop;
		}

		protected void add_item_to_stack(
			item.Item item, List<items_properties> stacks, int amount )
		{
			if ( item.max_stack_amount < 1 )
			{
				debug.error( "el item {0} no tiene mas stack", item );
				return;
			}
			foreach ( items_properties stack in stacks )
			{
				amount = stack.add_amount( amount );
			}
			int whiles = 0;
			while ( amount > 0 )
			{
				debug.info( "agregando mas stacks amount: {0}", amount );
				var stack = build_item_property( item );
				stacks.Add( stack );
				amount = stack.add_amount( amount );
				whiles += 1;
				if ( whiles > 100 )
				{
					debug.error( "esta vaina parecia entrar a un while infinito {0}", amount );
					break;
				}
			}
		}

		protected void remove_item_to_stack(
			item.Item item, List<items_properties> stacks, int amount )
		{
			foreach ( items_properties stack in stacks )
			{
				amount = stack.remove_amount( amount );
			}
			stacks.RemoveAll( ( stack ) => stack.amount < 1 );
			if ( amount > 0 )
				debug.error( "se elimno items del stack y sobro {0}", amount );
		}

		public IEnumerable<items_properties> stacks
		{
			get {
				foreach ( var stacks in items.Values )
					foreach ( var stack in stacks )
						yield return stack;
			}
		}

		public void redraw_slots()
		{
			if ( slots.Length == 0 )
			{
				return;
			}
			foreach ( var slot in slots )
			{
				slot.item_property = null;
			}

			var slots_with_stacks = slots.Zip( stacks, ( slot, stack ) => ( slot, stack ) );
			foreach ( var ( slot, stack ) in slots_with_stacks )
			{
				debug.warning( slot );
				debug.warning( stack );
				slot.item_property = stack;
			}
		}

		protected void find_all_slots()
		{
			slots = GetComponentsInChildren<Item_slot>();
		}
	}
}
