namespace chibi.path.old
{
	public class Path_creator : chibi.Chibi_behaviour
	{
		public Path path;

		public void create_path()
		{
			path = new Path( transform );
		}
	}
}
