using UnityEngine;

namespace chibi.animator
{
	public abstract class Animator_base : chibi.Chibi_behaviour
	{
		#region Var public
		public Animator animator;
		#endregion

		#region funciones protegidas
		protected override void _init_cache()
		{
			if ( !animator )
				animator = GetComponent<Animator>();
			if ( !animator )
			{
				debug.error( "no se encontro el componente animator" );
			}
		}
		#endregion
	}
}
