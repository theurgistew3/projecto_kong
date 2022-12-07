using UnityEngine;
using System.Collections.Generic;
using System;
using chibi.motor;
using chibi.motor.npc;
using chibi.manager.collision;

namespace chibi.controller.npc
{
	public class Controller_npc : Controller_motor
	{
		public chibi.damage.motor.HP_engine hp;

		public Chibi_collision_manager manager_collision;
		public chibi.action.Action_handler action_handler;

		public bool is_player = false;

		public virtual Motor_side_scroll motor_side_scroll
		{
			get { return motor as Motor_side_scroll; }
		}

		public virtual Motor_isometric motor_isometric
		{
			get { return motor as Motor_isometric; }
		}

		public override float speed
		{
			get {
				return base.speed;
			}

			set {
				base.speed = value * motor.max_speed;
			}
		}

		protected override void _init_cache()
		{
			base._init_cache();
			hp = GetComponent<chibi.damage.motor.HP_engine>();
			manager_collision = GetComponent<Chibi_collision_manager>();
			action_handler = GetComponent<chibi.action.Action_handler>();
			if ( is_player )
				player_setup();
		}

		#region manejo de salto
		public virtual void jump()
		{
			if ( motor_side_scroll != null )
				motor_side_scroll.start_jump();
			else if ( motor_isometric != null )
				motor_isometric.start_jump();
		}

		public virtual void stop_jump()
		{
			if ( motor_side_scroll != null )
				motor_side_scroll.end_jump();
			else if ( motor_isometric != null )
				motor_isometric.end_jump();
		}
		#endregion

		#region hp
		public virtual void died()
		{
			if ( !hp )
			{
				debug.error( "no tiene un HP_engine" );
			}
			else
				hp.died();
		}
		#endregion

		#region handler action
		public virtual void action()
		{
			call_current_action();
		}

		public virtual void stop_action()
		{
			debug.log( "detener actual accion" );
		}

		public void call_current_action()
		{
			if ( !action_handler )
				return;
			var current_action = action_handler.current_action;
			if ( current_action != null )
				current_action.action( this );
		}

		public void seek_current_action()
		{
			if ( !action_handler )
				return;
			foreach ( var action in action_handler.actions )
				action.unseek();

			var current_action = action_handler.current_action;
			if ( current_action != null )
				current_action.seek();
		}

		public void seek_all_action()
		{
			if ( !action_handler )
				return;
			foreach ( var action in action_handler.actions )
				action.seek();
		}
		#endregion

		#region player helpers
		public void player_setup()
		{
			is_player = true;
			if ( !action_handler )
				return;
			else
			{
				action_handler.seek_actions = true;
			}
		}
		#endregion
	}
}
