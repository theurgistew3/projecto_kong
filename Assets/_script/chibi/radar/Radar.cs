using UnityEngine;
using System;
using System.Collections.Generic;

namespace chibi.radar
{
	[System.Serializable]
	public class Radar
	{
		public Vector3 size;
		public Quaternion rotation;

		public List< LayerMask > masks;

		public Transform origin;

		public Dictionary< LayerMask, List< Radar_hit > > masks_hits;
		public List< Radar_hit > hits;

		public Predicate<Transform> filter;

		public Radar( Radar radar )
			: this( radar.origin, radar.size, radar.rotation, radar.masks )
		{
			masks_hits = new Dictionary<LayerMask, List<Radar_hit>>();
			hits = new List< Radar_hit >();
			this.filter = radar.filter;
			if ( this.filter == null )
				this.filter = x => true;
		}

		public Radar(
			Transform origin, Vector3 size, Quaternion rotation,
			List<LayerMask> masks, Predicate< Transform > filter = null )
		{
			this.origin = origin;
			this.size = size;
			this.rotation = rotation;
			this.masks = masks;

			masks_hits = new Dictionary<LayerMask, List<Radar_hit>>();
			hits = new List< Radar_hit >();
			if ( filter == null )
				this.filter = x => true;
			else
				this.filter = filter;
		}

		public virtual void ping()
		{
			throw new NotImplementedException();
		}
	}
}
