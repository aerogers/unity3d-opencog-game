using UnityEngine;
using System.Collections;

namespace OpenCog.Map
{
	public class OCColumnMap
	{

		private List2D<OCColumnChunk> columns = new List2D<OCColumnChunk>();
	
		public void SetBuilt(int x, int z) {
			GetColumnChunk(x, z).built = true;
		}
	
		public bool IsBuilt(int x, int z) {
			return GetColumnChunk(x, z).built;
		}

		public Vector3i? GetClosestEmptyColumn(int cx, int cz, int rad) {
			Vector3i center = new Vector3i(cx, 0, cz);
			Vector3i? closest = null;
			for(int z=cz-rad; z<=cz+rad; z++) {
				for(int x=cx-rad; x<=cx+rad; x++) {
					Vector3i current = new Vector3i(x, 0, z);
					int dis = center.DistanceSquared( current );
					if(dis > rad*rad) continue;
					if( IsBuilt(x, z) ) continue;
					if(!closest.HasValue) {
						closest = current;
					} else {
						int oldDis = center.DistanceSquared(closest.Value);
						if(dis < oldDis) closest = current;
					}
				}
			}
			return closest;
		}
	
	
		private OCColumnChunk GetColumnChunk(int x, int z) {
			return columns.GetInstance(x, z);
		}
	
		class OCColumnChunk {
			public bool built = false;
		}
	
	}

}


