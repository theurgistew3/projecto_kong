using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using danmaku.controller.weapon.gun;
using chibi.controller.steering.behavior;
using chibi.controller.steering;

namespace danmaku.boss_behaviour
{
	public class Eternity_larva_1 : Boss_behaviour
	{
		Steering steering;

		protected override IEnumerator do_behaviour()
		{
			yield return null;
			debug.log( this.name + " " + "antes de stegin" );
			// yield return new WaitForEndOfFrame();
			get_sterring();
			set_follow_waypoint();
			//yield return new WaitForEndOfFrame();
			debug.log( this.name + " " + "despues de steering" );
			yield return null;
			debug.log( this.name + " " + "despues de steering" );

			while( touha )
			{
				debug.log( this.name + " " + "shot" );
				touha.shot();
				yield return new WaitForSeconds( 5 );
			}
			end_behaviour();
		}

		protected void get_sterring()
		{
			steering = touha.GetComponent<Steering>();
			if ( !steering )
				steering = touha.gameObject.AddComponent<Steering>();
			steering.target = path.transform;
			steering.controller = touha;
		}

		protected void set_follow_waypoint()
		{
			var behavior = Follow_waypoints.CreateInstance<Follow_waypoints>();
			behavior.loop = true;
			steering.behaviors.Add( behavior );
			steering.reload();
		}
	}
}
