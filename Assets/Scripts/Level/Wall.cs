using UnityEngine;
using System.Collections;

public enum WallType{
	A,B,C,D
}

public class Wall : MonoBehaviour {


	public GameObject A,B,C,D;
	

	private GameObject getWallTile(WallType type){
		GameObject g=null;
		switch (type) {
		case WallType.A:
			g=A;
			break;
		case WallType.B:
			g=B;
			break;
		case WallType.C:
			g=C;
			break;
		case WallType.D:
			g=D;
			break;
		}

		//g.transform.Rotate(new Vector3(0,0,r*90));

		return g;
	}

	public GameObject PlaceTile(int x, int y, WallType type, int rotation, Transform parent){
		GameObject gc=(GameObject)Instantiate(getWallTile(type));
		gc.transform.parent = parent;
		gc.transform.localPosition = new Vector3 (Grid.Get (x), Grid.Get(y), 0);
		gc.transform.Rotate(0, 0, 90*rotation);


		BoxCollider2D bc=gc.AddComponent<BoxCollider2D> ();
		if (type == WallType.B && rotation == 0) {
			bc.size=new Vector2(Grid.Get(1),Grid.Get (0.5f));
			bc.center=new Vector2(0,-Grid.Get (0.25f));
		}
		else{
			bc.size=new Vector2(Grid.Get (1), Grid.Get(1));
		}

		return gc;
	}

	public GameObject PlaceTile(int x, int y, WallType type, int rotation){
		GameObject gc=(GameObject)Instantiate(getWallTile(type));
		gc.transform.parent = GameObject.FindGameObjectWithTag ("ConnectionWallContainer").transform;
		gc.transform.position = new Vector3 (Grid.Get (x), Grid.Get(y), 0);
		gc.transform.Rotate(0, 0, 90*rotation);

		
		BoxCollider2D bc=gc.AddComponent<BoxCollider2D> ();
		if (type == WallType.B && rotation==0 || type == WallType.D && rotation == 0 ) {
			bc.size=new Vector2(Grid.Get(1),Grid.Get (0.5f));
			bc.center=new Vector2(0,-Grid.Get (0.25f));
		}
		else if(type == WallType.D &&  rotation ==1){
			bc.size=new Vector2(Grid.Get(0.5f),Grid.Get (1));
			bc.center=new Vector2(-Grid.Get (0.25f),0);
		}
		else{
			bc.size=new Vector2(Grid.Get (1), Grid.Get(1));
		}
		
		return gc;
	}
}
