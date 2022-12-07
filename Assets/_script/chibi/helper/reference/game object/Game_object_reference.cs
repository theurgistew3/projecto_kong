using System;
using UnityEngine;

namespace chibi.tool.reference
{
	[Serializable]
	public class Game_object_reference
	{
		public bool use_constant = true;
		public GameObject constant_value;
		public Game_object variable;

		public Game_object_reference()
		{ }

		public Game_object_reference( GameObject value )
		{
			use_constant = true;
			constant_value = value;
		}

		public GameObject Value
		{
			get {
				if ( use_constant )
					return constant_value;
				return variable.value;
			}
			set {
				if ( use_constant )
					constant_value = value;
				else
					variable.value = value;
			}
		}
	}
}
