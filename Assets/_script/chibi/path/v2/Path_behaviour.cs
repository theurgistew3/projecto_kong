using System.Collections.Generic;
using UnityEngine;

namespace chibi.path
{
	public class Path_behaviour : chibi.Chibi_behaviour
	{
		public Path path;
		public List< Path_handler > path_handlers;

		public void create_path()
		{
			if ( path != null )
				path = new Path( transform, path );
			else
				path = new Path( transform );
		}

		protected override void _init_cache()
		{
			base._init_cache();
			path = new Path( path );
		}

		private void OnDrawGizmos()
		{
			foreach ( var segment in path.segments )
			{
				segment.draw_gizmo();
			}

			if ( path.bake_points != null
				&& path.bake_points.Count > 0 )
			{
				Gizmos.color = Color.green;
				foreach ( var vector in path.bake_points )
					Gizmos.DrawWireSphere( vector, 0.05f );
			}
		}
	}
}
