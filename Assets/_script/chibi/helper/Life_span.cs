using chibi.pomodoro;


namespace chibi.tool.life_span
{
	public class Life_span : chibi.Chibi_behaviour
	{
		public float life_time = 60f;

		protected Pomodoro pomodoro;

		protected override void _init_cache()
		{
			base._init_cache();
			pomodoro = Pomodoro.CreateInstance<Pomodoro>();
			pomodoro.frecuency = life_time;
		}

		protected virtual void Update()
		{
			if ( pomodoro.tick() )
				on_dead();
		}

		protected virtual void on_dead()
		{
			this.recycle();
		}
	}
}
