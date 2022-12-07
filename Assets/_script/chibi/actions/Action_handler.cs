using UnityEngine;
using System.Collections.Generic;

namespace chibi.action
{
	public class Action_handler: chibi.Chibi_behaviour
	{
		public List<Action> actions;

		public bool seek_actions = false;

		public Action current_action
		{
			get {
				if ( actions.Count > 0 )
					return actions[ 0 ];
				return null;
			}
		}

		protected void OnTriggerEnter( Collider other )
		{
			if ( is_not_my_action( other ) )
			{
				var action = other.GetComponents<Action>();
				foreach ( var a in action )
				{
					if ( actions.Contains( a ) )
						continue;
					actions.Add( a );
					if ( seek_actions )
						a.seek();
				}
			}
		}

		protected void OnTriggerExit( Collider other )
		{
			if ( is_not_my_action( other ) )
			{
				var action = other.GetComponents<Action>();
				foreach ( var a in action )
				{
					if ( !actions.Contains( a ) )
						continue;
					actions.Remove( a );
					if ( seek_actions )
						a.unseek();
				}
			}
		}

		protected bool is_my_action( Collider other )
		{
			return other.gameObject == gameObject;
		}

		protected bool is_not_my_action( Collider other )
		{
			return !is_my_action( other );
		}

		protected override void _init_cache()
		{
			base._init_cache();
			actions = new List<Action>();
		}
	}
}