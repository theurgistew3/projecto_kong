using UnityEngine;


namespace chibi.inventory
{
	public class Item: chibi.Chibi_behaviour
	{
		public item.Item item;
		public int amount = 1;

		public void send_to_pool()
		{
			throw new System.NotImplementedException();
		}

		public void use( chibi.rol_sheet.Rol_sheet character )
		{
			Debug.Log( item.name );
			item.use( character );
		}

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !item )
				Debug.LogError( string.Format(
					"el item '{0}' no tiene asignado el chibi object de items",
					helper.game_object.name.full( this ) ) );
		}
	}
}

