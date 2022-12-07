using System.Collections;
using System.Collections.Generic;
using chibi.inventory.item;
using UnityEngine;
using System.Linq;

namespace dr_stone.ui
{
	public class Gather_ui :
		chibi.Chibi_ui, IList< chibi.inventory.item.Item >
	{
		public List<chibi.inventory.item.Item> items;
		protected List<Gather_slot_ui> slots;
		public GameObject slot_prefab;
		public GameObject container;
		public chibi.inventory.ui.Inventory_ui inventory;

		public int Count
		{
			get {
				throw new System.NotImplementedException();
			}
		}

		public bool IsReadOnly
		{
			get {
				throw new System.NotImplementedException();
			}
		}

		public Item this[ int index ]
		{
			get {
				throw new System.NotImplementedException();
			}

			set {
				throw new System.NotImplementedException();
			}
		}

		public int IndexOf( Item item )
		{
			throw new System.NotImplementedException();
		}

		public void Insert( int index, Item item )
		{
			throw new System.NotImplementedException();
		}

		public void RemoveAt( int index )
		{
			throw new System.NotImplementedException();
		}

		public void Add( Item item )
		{
			if ( !items.Contains( item ) )
				items.Add( item );
			redraw();
		}

		public void Clear()
		{
			throw new System.NotImplementedException();
		}

		public bool Contains( Item item )
		{
			throw new System.NotImplementedException();
		}

		public void CopyTo( Item[] array, int arrayIndex )
		{
			throw new System.NotImplementedException();
		}

		public bool Remove( Item item )
		{
			throw new System.NotImplementedException();
		}

		public IEnumerator<Item> GetEnumerator()
		{
			throw new System.NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new System.NotImplementedException();
		}

		protected void redraw()
		{
			debug.log( "redraw del gather ui" );
			while ( items.Count > slots.Count )
				slots.Add( build_new_slot() );
			var valid_items = items.Zip( slots, ( item, slot ) => ( item, slot ) );
			var null_slots = slots.Skip( items.Count );
			foreach ( var ( item, slot ) in valid_items )
				slot.item = item;
			foreach ( var slot in null_slots )
			{
				slot.item = null;
			}
		}

		protected Gather_slot_ui build_new_slot()
		{
			throw new System.NotImplementedException();
		}
		public void gather( Item item )
		{
			inventory.add( item );
		}

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !container )
				debug.error( "no esta asignado el container de los slots" );
			if ( !slot_prefab )
				debug.error( "no esta asignado el prefab de los slots" );
			if ( !inventory )
				debug.error( "no esta asignado el inventario" );
			if ( slots == null )
				slots = new List<Gather_slot_ui>();

			var current_gathers_slots = container.GetComponentsInChildren<Gather_slot_ui>();
			slots.Clear();
			slots.AddRange( current_gathers_slots );
			foreach ( var slot in slots )
				slot.gather_ui = this;
			redraw();
		}
	}
}
