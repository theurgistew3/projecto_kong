using chibi.motor.weapons.gun.bullet;

namespace fisher.motor.weapons.gun.bullet
{

	public class Bullet_motor_net : Bullet_motor_yoyo
	{
		public chibi.radar.Radar_box catch_radar;
		public fisher.controller.Fisher_controller owner;

		public override void on_reach_target()
		{
			if ( origin )
			{
				catch_radar.ping();
				foreach ( var hit in catch_radar.hits )
				{
					var item = hit.transform.GetComponent< chibi.inventory.Item >();
					owner.grab( item );
				}
			}
			base.on_reach_target();
		}

		protected override void _init_cache()
		{
			base._init_cache();
			catch_radar = new chibi.radar.Radar_box( catch_radar );
		}
	}
}
