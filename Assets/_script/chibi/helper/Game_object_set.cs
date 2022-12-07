using System.Collections.Generic;


namespace chibi.tool
{
	//[ CreateAssetMenu( menuName="chibi/helper/set/base" ) ]
	public abstract class Game_object_set<T>: Chibi_object
		where T : Chibi_behaviour
	{
		public List<T> list = new List<T>();

		public void add( T obj )
		{
			if ( !list.Contains( obj ) )
				list.Add( obj );
		}

		public void remove( T obj )
		{
			if ( list.Contains( obj ) )
				list.Remove( obj );
		}
	}
}

