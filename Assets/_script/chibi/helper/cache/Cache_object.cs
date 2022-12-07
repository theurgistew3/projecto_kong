using UnityEngine;
using System.Collections.Generic;
using System;


namespace chibi.tool
{
	public class Cache_object<T>
	{
		public List<Tuple<T, float, float>> cache_objects =
			new List<Tuple<T, float, float>>();

		public bool clear( T obj )
		{
			var finded = cache_objects.Find( t => t.Item1.Equals( obj ) );
			if ( finded != null )
			{
				if ( finded.Item2 + finded.Item3 < Time.time )
				{
					cache_objects.Remove( finded );
					return true;
				}
				else
					return false;
			}
			return true;
		}

		public bool add( T obj, float born, float die_in )
		{
			if ( clear( obj ) )
			{
				cache_objects.Add( new Tuple<T, float, float>( obj, born, die_in ) );
				return true;
			}
			return false;
		}
	}
}

