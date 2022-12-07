
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace school.plane
{
	public class Cartesian_plane : chibi.Chibi_behaviour
	{
		public int scale = 10;
		public float size_line = 1f;

		public void Update()
		{
			debug.draw.line( Vector3.down * scale, Vector3.up * scale );
			debug.draw.line( Vector3.left * scale, Vector3.right * scale );

			var vector_scala = new Vector3( 0, -size_line, 0 );
			var vector_linea = new Vector3( 0, size_line, 0 );
			for ( int i = 0; i < scale; ++i )
			{
				vector_scala.x += 1;
				vector_linea.x = vector_scala.x;
				debug.draw.line( vector_scala, vector_linea );
			}

			vector_scala.x = 0;
			for ( int i = 0; i < scale; ++i )
			{
				vector_scala.x -= 1;
				vector_linea.x = vector_scala.x;
				debug.draw.line( vector_scala, vector_linea );
			}

			vector_scala = new Vector3( -size_line, 0, 0 );
			vector_linea = new Vector3( size_line, 0, 0 );
			for ( int i = 0; i < scale; ++i )
			{
				vector_scala.y -= 1;
				vector_linea.y = vector_scala.y;
				debug.draw.line( vector_scala, vector_linea );
			}

			vector_scala.y = 0;
			for ( int i = 0; i < scale; ++i )
			{
				vector_scala.y += 1;
				vector_linea.y = vector_scala.y;
				debug.draw.line( vector_scala, vector_linea );
			}
		}
	}
}