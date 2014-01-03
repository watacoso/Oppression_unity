using UnityEngine;
using System.Collections;
using Pathfinding;

public class Map : MonoBehaviour {

	Bounds bounds;

	void Awake(){
		gameObject.AddComponent<Renderer> ();
	}

	public void ScanMap(){
		for (int i=0; i<transform.childCount; i++) {
			Room r=transform.GetChild(i).gameObject.GetComponent<Room>();
			bounds.Encapsulate(new Vector3(r.x,r.y,0));
			bounds.Encapsulate(new Vector3(r.x+r.width,r.y+r.height,0));
		}

		AstarData data = AstarPath.active.astarData;
		// This creates a Grid Graph
		GridGraph gg = data.graphs [0] as GridGraph;
		//GridGraph gg = data.AddGraph(typeof(GridGraph)) as GridGraph;
		// Setup a grid graph with some values
		gg.width =(int) bounds.size.x;
		gg.depth =(int) bounds.size.y;
		gg.nodeSize = Grid.Get(1);
		gg.bounds = bounds;

		gg.center = new Vector3(Grid.Get(bounds.center.x-0.5f),Grid.Get(bounds.center.y-0.5f),0);
		gg.rotation = new Vector3 (90, 0, 0);
		// Updates internal size from the above values
		gg.UpdateSizeFromWidthDepth();
		//gg.maxClimbAxis = 2;

		// Scans all graphs, do not call gg.Scan(), that is an internal method
		AstarPath.active.Scan();
	}

	public Room getRandomRoom(){
		int r = Mathf.FloorToInt(Random.value*transform.childCount);
		return transform.GetChild (r).gameObject.GetComponent<Room> ();
	}
}
