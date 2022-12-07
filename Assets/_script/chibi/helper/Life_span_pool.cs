namespace chibi.tool.life_span
{
	public class Life_span_pool : Life_span
	{
		//public chibi.pool.Pool_behaviour pool;

		protected override void on_dead()
		{
			if ( pool )
			{
				pomodoro.reset();
				pool.push( this );
			}
			else
			{
				debug.error( "no tiene una 'pool' definida" );
				base.on_dead();
			}
		}
	}
}
