using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {
	

	public GameObject[] tiles;
	
	//public GameObject PlaceTile(int x, int y){
		
	//}

	public void PlaceTile(int x, int y, int type, Transform parent){
		GameObject tile=(GameObject)Instantiate(tiles[type]);
		tile.transform.parent=parent;
		tile.transform.localPosition = new Vector3 ( Grid.Get (x), Grid.Get(y), 0);

	}
}
