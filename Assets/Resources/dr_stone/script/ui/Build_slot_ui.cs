using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace dr_stone.ui
{
	public class Build_slot_ui : chibi.Chibi_ui
	{
		public chibi.inventory.Recepie recepie;
		public UnityEngine.UI.Image sprite;
		public TMPro.TextMeshProUGUI text;

		public Build_ui build_ui;

		protected void Update()
		{
			if ( recepie )
			{
				sprite.sprite = recepie.output.item.image;
				bool can = build_ui.can_be_builded( recepie );
				text.text = recepie.output.item.name;
				if ( can )
					text.text += " si";
				else
					text.text += " no";
				sprite.enabled = true;
			}
			else
			{
				sprite.enabled = false;
				text.text = "";
			}
		}

		public void activate()
		{
			if ( recepie )
				build_ui.build( recepie );
			else
				debug.warning( "fue activado cuando estaba desabilitado" );
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

			if ( !text )
			{
				var obj_text = transform.Find( "text" );
				if ( obj_text )
					text = obj_text.GetComponent<TMPro.TextMeshProUGUI>();
				else
					debug.error( "no se encontro el gameobject text mesh pro" );
			}
			if ( !text )
				debug.error( "no esta asignado el text mesh pro para el item" );
		}
	}
}
