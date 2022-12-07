namespace chibi.pomodoro
{
	public class Pomodoro_ticker: chibi.Chibi_behaviour
	{
		private void Update()
		{
			singleton.pomodoro.Pomodoro_singleton.instance.tick();
		}
	}
}