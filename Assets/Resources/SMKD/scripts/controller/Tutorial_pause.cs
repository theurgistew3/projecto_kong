using UnityEngine;
using chibi.controller.weapon.gun.bullet;

namespace chibi.controller
{
	public class Tutorial_pause : Controller
	{
		bool time_is_pause = false;
		public SMKD.controller.Push_start_controller controller;
		public string action_string;
		public GameObject tuto;

		public string axis_string;

		private void OnTriggerEnter( Collider other )
		{
			var controller = other.transform.GetComponentInParent<
				Controller_bullet>();
			if ( controller )
			{
				Time.timeScale = 0f;
				time_is_pause = true;
				tuto.SetActive( true );
			}
		}

		private void Update()
		{
			if ( time_is_pause )
			{
				if ( axis_string == "any" )
					if ( Input.anyKey )
					{
					}
				switch ( axis_string )
				{
					case "any":
						if ( Input.anyKey )
						{
							time_is_pause = false;
							Time.timeScale = 1f;
							controller.action_tutorial( action_string );
							Destroy( this.gameObject );
						}
						break;
					default:
						if ( Input.GetButton( axis_string ) )
						{
							time_is_pause = false;
							Time.timeScale = 1f;
							controller.action_tutorial( action_string );
							Destroy( this.gameObject );
						}
						break;
				}
			}
		}

		private void OnDrawGizmos()
		{
			BoxCollider cube = GetComponent<BoxCollider>();
			Gizmos.color = Color.black;
			Gizmos.DrawWireCube( transform.position, cube.size );
		}

		protected override void _init_cache()
		{
			// base._init_cache();
		}
	}
}
