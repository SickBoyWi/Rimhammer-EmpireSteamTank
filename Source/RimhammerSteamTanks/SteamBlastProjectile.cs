using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using VEF.Weapons;

namespace RimhammerSteamTanks
{
    class SteamBlastProjectile : ExpandableProjectile
    {
		public override void DoDamage(IntVec3 pos)
		{
			base.DoDamage(pos);
			try
			{
				if (pos != this.launcher.Position && this.launcher.Map != null && GenGrid.InBounds(pos, this.launcher.Map))
				{
					var list = this.launcher.Map.thingGrid.ThingsListAt(pos);
					for (int num = list.Count - 1; num >= 0; num--)
					{
						if (IsDamagable(list[num]))
						{
							this.customImpact = true;
							base.Impact(list[num]);
							this.customImpact = false;
						}
					}
				}
			}
			catch { };
		}

		public override bool IsDamagable(Thing t)
		{
			return base.IsDamagable(t) && t.def != ThingDefOf.Fire;
		}
	}
}
