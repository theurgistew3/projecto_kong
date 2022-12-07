using System.Collections.Generic;
using UnityEngine;
using chibi.controller;

namespace aventure_time.action
{
	public class Action_talk: chibi.action.Action
	{
		public GameObject seek_gameobject;
		public aventure_time.dialog.Dialogue dialogue;

		public Controller current_emiter;
		public Controller current_receptor;


		public dialog.Bubble_dialog current_dialog;
		public dialog.Bubble_dialog current_emiter_dialog;
		public dialog.Bubble_dialog current_receptor_dialog;

		protected bool on_dialog = false;
		public chibi.pomodoro.Pomodoro_obj pomodore_next_message =
			new chibi.pomodoro.Pomodoro_obj( 1f );

		protected IEnumerator<dialog.Message> messages;


		public Controller my_controller 
		{
			get {
				var controller = GetComponentInParent<Controller>();
				debug.log( "se encontro este controller {0} en {1}", controller, transform.parent );
				return controller;
			}
		}

		public override void action( Controller controller )
		{
			debug.log( "activo {0} la accion", controller );
			start_dialoge( controller, my_controller );
		}

		public override void seek()
		{
			seek_gameobject.SetActive( true );
			//throw new System.NotImplementedException();
		}

		public override void unseek()
		{
			seek_gameobject.SetActive( false );
			//throw new System.NotImplementedException();
		}

		protected override void _init_cache()
		{
			base._init_cache();
			if ( dialogue )
			{
				debug.warning( "no tiene un dialogo asignado" );
			}
		}

		protected virtual void Update()
		{
			if ( !on_dialog )
				return;
			if ( current_dialog )
			{
				if ( current_dialog.is_complete )
					if ( pomodore_next_message.tick( Time.deltaTime ) )
						move_next_message();
			}
			else
			{
				if ( pomodore_next_message.tick( Time.deltaTime ) )
					end_dialog();
			}
		}

		protected void start_dialoge( Controller emiter, Controller receptor )
		{
			debug.log( "iniciando dialogo de {0} con {1}", emiter, receptor );
			current_emiter = emiter;
			current_receptor = receptor;
			current_emiter_dialog = get_bubble( emiter );
			current_receptor_dialog = get_bubble( receptor );
			emiter.block();
			receptor.block();
			on_dialog = true;
			pomodore_next_message.reset();

			if ( !current_emiter_dialog )
			{
				debug.error( "no se encontro el dialogo del emisor" );
				end_dialog();
			}

			if ( !current_receptor_dialog )
			{
				debug.error( "no se encontro el dialogo del receptor" );
				end_dialog();
			}

			current_receptor_dialog.hide();
			current_emiter_dialog.hide();

			messages = dialogue.messages.GetEnumerator();
			if ( !messages.MoveNext() )
			{
				end_dialog();
				debug.error( "el dialogo no tiene mensajes {0}", dialogue );
				return;
			}

			unseek();

			current_dialog = who_talk( messages.Current );
			current_dialog.show();
			current_dialog.start_dialog( messages.Current );
		}

		protected void move_next_message()
		{
			if ( messages.MoveNext() )
			{
				current_dialog.hide();
				pomodore_next_message.reset();
				current_dialog = who_talk( messages.Current );
				current_dialog.show();
				current_dialog.start_dialog( messages.Current );
			}
			else
			{
				current_dialog = null;
			}
		}

		protected void end_dialog()
		{
			current_emiter_dialog.hide();
			current_receptor_dialog.hide();

			current_emiter.unblock();
			current_receptor.unblock();
			current_emiter = null;
			current_receptor = null;
			current_emiter_dialog = null;
			current_receptor_dialog = null;
			current_dialog = null;
			on_dialog = false;
		}

		protected dialog.Bubble_dialog who_talk( dialog.Message message )
		{
			if ( message.owner == dialog.Owner.emiter )
				return current_emiter_dialog;
			else
				return current_receptor_dialog;
		}

		protected dialog.Bubble_dialog get_bubble( Controller controller )
		{
			var canvas = controller.transform.Find( "Canvas" );
			if ( !canvas )
				debug.error( "no se encontro canvas en {0}", controller );

			var dialog = canvas.GetComponentInChildren<dialog.Bubble_dialog>();
			if ( !dialog )
				debug.error( "no se encontro bubble dialog en {0}", canvas );
			return dialog;
		}
	}
}