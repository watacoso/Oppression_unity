using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

	private static Object resource= Resources.Load ("room_generic");

	public GameObject floorPrefab;
	public GameObject wallPrefab;

	public int x;
		public int y;
	public int width;
	public int height;

	public Vector2 direction;

	private bool built = false;
	public Wall wall;
	private Floor floor;



	public static Room Create(int width, int height){
		GameObject r=(GameObject)Instantiate(resource);
		Room rs= r.GetComponent<Room> ();
		rs.transform.parent = GameObject.FindGameObjectWithTag ("Map").transform;
		rs.width = width;
		rs.height = height;
		return rs;
	}

	void Awake(){
		wall = wallPrefab.GetComponent<Wall> ();
		floor = floorPrefab.GetComponent<Floor> ();
		direction = new Vector2 (0, 1);
	}

	public void setPosition(int x, int y){
		this.x = x;
		this.y = y;
		transform.position = new Vector3 (Grid.Get (x), Grid.Get (y), 0);
	}


	public void BuildFloor(){
		if(built || width*height==0) return;
		built = true;


		for(int i=0;i < width;i++){
			for(int j=0;j<height;j++){
				floor.PlaceTile(i,j,0,transform.GetChild (1));
			}
		}

	}


	public void BuildWalls(){

		if(!isTaleBlocked(-1,-1))
			wall.PlaceTile(-1,-1,WallType.C,0,transform.GetChild(2));
		if(!isTaleBlocked(width,-1))
			wall.PlaceTile(width, -1,WallType.C,1,transform.GetChild(2));

		for (int i=0; i<height+1; i++) {
			if(!isTaleBlocked(-1,i))
				wall.PlaceTile(-1,i,WallType.B,-1,transform.GetChild(2));
			if(!isTaleBlocked(width,i))
				wall.PlaceTile(width, i,WallType.B,1,transform.GetChild(2));
		}

		for(int i=0;i< width;i++){
			if(!isTaleBlocked(i,-1))
				wall.PlaceTile(i,-1,WallType.B,0,transform.GetChild(2));
			if(!isTaleBlocked(i,height))
				wall.PlaceTile(i, height,WallType.A,0,transform.GetChild(2));
		}
	}

	private bool isTaleBlocked(int x, int y){
		Vector2 v = new Vector2 (Grid.Get (this.x+x), Grid.Get (this.y+y));
		RaycastHit2D hit = Physics2D.Linecast (v, v);
		if (hit.collider != null ) {
			if(hit.collider.tag=="RoomConnector")
				return true;
		}
		return false;
	}

	public bool IsOverlapping(Room r){
		if(x-2>=r.x+r.width || x + width+2<=r.x)
			return false;
		if(y-2>=r.y+r.height || y + height+2<=r.y)
			return false;

		return true;
	}

}
