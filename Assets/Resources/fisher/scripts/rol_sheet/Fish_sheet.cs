namespace fisher.rol_sheet
{
	public class Fish_sheet : chibi.rol_sheet.Rol_sheet
	{
		public chibi.rol_sheet.gender.Gender gender;
		public chibi.tool.reference.Need_reference reproduction;
		public bool want_to_reproducing = false;

		public fisher.tool.fish_set all_fish;
		public fisher.tool.fish_specie_set specie;

		protected override void _init_cache()
		{
			base._init_cache();
			all_fish.add( this );
			specie.add( this );
		}

		protected override void _dispose_cache()
		{
			base._dispose_cache();
			all_fish.remove( this );
			specie.add( this );
		}
	}
}