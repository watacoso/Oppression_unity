using UnityEngine;
using System.Collections;

public class RoomConnector : MonoBehaviour {

	public Wall wall;

	private static Object resource=Resources.Load ("room_connector");

	private Room roomA;
	private Room roomB;
	private bool horizontal;
	private int x, y, width, height;

	public static RoomConnector Create(Room roomA, Room roomB, bool horizontal, int x, int y, int width, int height){
		GameObject r=(GameObject)Instantiate(resource);
		RoomConnector rs= r.GetComponent<RoomConnector> ();
		BoxCollider2D bc=r.AddComponent<BoxCollider2D> ();
		r.transform.position = new Vector3 (Grid.Get (x-0.5f), Grid.Get (y-0.5f),-3);
		bc.size=new Vector2(Grid.Get (width),Grid.Get (height));
		bc.center=new Vector2(Grid.Get (width*0.5f),Grid.Get (height*0.5f));		
		bc.isTrigger = true;
		rs.roomA=roomA;
		rs.horizontal = horizontal;
		rs.roomB=roomB;

		rs.x = x;
		rs.y = y;
		rs.width = width;
		rs.height = height;

		return rs;
	}

	public void AddConnectionWalls(){
		int v1, v2;

		if (horizontal) {
			v1=roomA.x-roomB.x;
			v2=roomA.x+roomA.width-roomB.x-roomB.width;

			if(v1>0){
				wall.PlaceTile(x	, y	  ,	WallType.D,0);
			}
			else if(v1==0){
				wall.PlaceTile(x	, y	  ,	WallType.B,-1);
				wall.PlaceTile(x	, y+1 ,	WallType.B,-1);
			}
			else{
				wall.PlaceTile(x	, y+1 ,	WallType.A,0);
			}

			if(v2>0){
				wall.PlaceTile(x+width-1, y+1 ,	WallType.A,0);
			}
			else if(v2==0){
				wall.PlaceTile(x+width-1, y+1 ,	WallType.B,1);
				wall.PlaceTile(x+width-1, y	  ,	WallType.B,1);
			}
			else{
				wall.PlaceTile(x+width-1, y	  ,	WallType.D,1);
			}

			//wall.PlaceTile(x	, y	  ,	WallType.D,0);
			//wall.PlaceTile(width, y	  ,	WallType.D,1);
			//wall.PlaceTile(x	, y+1 ,	WallType.A,0);
			//wall.PlaceTile(width, y+1 ,	WallType.A,0);
		}
		else{
			v1=roomA.y-roomB.y;
			v2=roomA.y+roomA.height-roomB.y-roomB.height;
			
			if(v1>0){
				wall.PlaceTile(x	, y	,	WallType.D,0);
			}
			else if(v1==0){
				wall.PlaceTile(x	, y	,	WallType.B,0);
				wall.PlaceTile(x+1	, y ,	WallType.B,0);
			}
			else{
				wall.PlaceTile(x+1	, y ,	WallType.D,1);
			}
			
			if(v2>0){
				wall.PlaceTile(x+width-1, y+height-1 ,	WallType.A,0);
			}
			else if(v2==0){
				wall.PlaceTile(x+1, y+height-1 ,	WallType.A,0);
				wall.PlaceTile(x  , y+height-1 ,	WallType.A,0);
			}
			else{
				wall.PlaceTile(x  , y+height-1 ,	WallType.A,0);
			}
		}
	}
}
