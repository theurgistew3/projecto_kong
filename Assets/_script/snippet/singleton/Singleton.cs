using UnityEngine;

namespace singleton
{
	public class Singleton<T> : chibi.Chibi_object
		where T : chibi.Chibi_object
	{
		private static T _instance;
		private static bool _quitting = false;

		public static T instance {
			get {
				if ( _quitting )
				{
					Debug.LogWarning( string.Format(
						"[Singleton< {0} >] la instancia ya fue destruida " +
						"en la salida de la aplicacion, no se creara de nuevo " +
						"regresa NULL" ) );
					return null;
				}
				if ( !_instance )
				{
					var a = Resources.FindObjectsOfTypeAll<T>();
					if ( a.Length > 0 )
						_instance = a[0];
				}
				if ( !_instance )
					_instance = CreateInstance<T>();
				return _instance;
			}
		}

		public void OnDestroy () {
			_quitting = true;
		}
	}
}