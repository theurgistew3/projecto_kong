using System.Collections;
using System.Collections.Generic;
using chibi.inventory;
using UnityEngine;
using System.Linq;

namespace dr_stone.ui
{
	public class Build_ui:
		chibi.Chibi_ui, IList< Recepie >
	{
		public List<chibi.inventory.Recepie> recepies;
		protected List<Build_slot_ui> slots;
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

		public Recepie this[ int index ]
		{
			get {
				throw new System.NotImplementedException();
			}

			set {
				throw new System.NotImplementedException();
			}
		}

		public int IndexOf( Recepie item )
		{
			throw new System.NotImplementedException();
		}

		public void Insert( int index, Recepie item )
		{
			throw new System.NotImplementedException();
		}

		public void RemoveAt( int index )
		{
			throw new System.NotImplementedException();
		}

		public void Add( Recepie item )
		{
			if ( !recepies.Contains( item ) )
				recepies.Add( item );
			redraw();
		}

		public void Clear()
		{
			throw new System.NotImplementedException();
		}

		public bool Contains( Recepie item )
		{
			throw new System.NotImplementedException();
		}

		public void CopyTo( Recepie[] array, int arrayIndex )
		{
			throw new System.NotImplementedException();
		}

		public bool Remove( Recepie item )
		{
			throw new System.NotImplementedException();
		}

		public IEnumerator<Recepie> GetEnumerator()
		{
			throw new System.NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new System.NotImplementedException();
		}

		protected void redraw()
		{
			debug.log( "redraw del build ui" );
			while ( recepies.Count > slots.Count )
				slots.Add( build_new_slot() );
			var valid_items = recepies.Zip( slots, ( item, slot ) => ( item, slot ) );
			var null_slots = slots.Skip( recepies.Count );
			foreach ( var ( item, slot ) in valid_items )
				slot.recepie = item;
			foreach ( var slot in null_slots )
			{
				slot.recepie = null;
			}
		}

		protected Build_slot_ui build_new_slot()
		{
			throw new System.NotImplementedException();
		}

		public void build( Recepie recepie )
		{
			if ( can_be_builded( recepie ) )
			{
				foreach ( var item in recepie.items )
				{
					inventory.remove( item.item, item.amount );
				}
				inventory.add( recepie.output.item, recepie.output.amount );
			}
			else
				debug.error( "la receta {0} no se puede contruir", recepie.name );
		}

		public bool can_be_builded( Recepie recepie )
		{
			List<chibi.inventory.ui.items_properties> stacks;

			foreach ( var item in recepie.items )
			{
				if ( inventory.items.TryGetValue( item.item, out stacks ) )
				{
					int total_amount = 0;
					foreach ( var stack_item in stacks )
						total_amount += stack_item.amount;
					if ( total_amount < item.amount )
						return false;
				}
				else
					return false;
			}
			return true;
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
				slots = new List<Build_slot_ui>();

			var current_slots = container.GetComponentsInChildren<Build_slot_ui>();
			slots.Clear();
			slots.AddRange( current_slots );
			foreach ( var slot in slots )
				slot.build_ui = this;
			redraw();
		}
	}
}
