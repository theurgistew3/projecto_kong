namespace chibi.spawner.invoker
{
	public class Timer : Invoker
	{
		public float frequency = 1f;

		protected float _sigma_time = 0f;

		public float time
		{
			get {
				return _sigma_time;
			}
			set {
				_sigma_time = value;
				if ( _sigma_time >= frequency )
				{
					target.spawn();
					_sigma_time -= frequency;
				}
			}
		}

		private void Update()
		{
			time += UnityEngine.Time.deltaTime;
		}
	}
}