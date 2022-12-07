using UnityEngine;

namespace chibi
{
	public class Chibi_object : ScriptableObject
	{
		public virtual string path_of_the_default
		{
			get { throw new System.NotImplementedException(); }
		}

		public virtual T find_default<T>() where T : Chibi_object
		{
			var obj = Resources.Load<T>( path_of_the_default );
			if ( obj == null )
				throw new System.Exception(
					string.Format(
						"no se encontro el default en '{0}' of '{1}'", 
						path_of_the_default, typeof( T ) ) );
			return obj;
		}
	}
}
