using UnityEngine;
using System.Collections.Generic;
using System;
using chibi.motor;
using chibi.motor.npc;

namespace chibi.manager.platform
{
	public class Chibi_platform : Chibi_behaviour
	{
		protected virtual void OnCollisionEnter( Collision collision )
		{
			if ( chibi.tag.consts.is_scenary( collision ) )
				return;
			collision.transform.parent = transform;
		}

		protected virtual void OnCollisionExit( Collision collision )
		{
			collision.transform.parent = null;
		}
	}
}
