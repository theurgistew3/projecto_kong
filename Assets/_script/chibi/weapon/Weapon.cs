using chibi.rol_sheet;

namespace chibi.weapon
{
	public abstract class Weapon : Chibi_behaviour
	{
		public Rol_sheet owner;

		public abstract void attack();
	}
}
