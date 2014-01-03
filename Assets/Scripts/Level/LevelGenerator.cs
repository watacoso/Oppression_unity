using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LevelGenerator : MonoBehaviour {

	public int mapWidth=128;
	public int mapHeight=128;
	public int mapSize=10;

	public int nEnemies=1;

	private Room indexRoom;
	private List<Room> roomBuffer=new List<Room>();

	private bool validCompound;

	private Map map;



	void Awake(){
		map = GameObject.FindGameObjectWithTag ("Map").GetComponent<Map> ();
		indexRoom = Room.Create (8, 8);

		for(int i=0;i<mapSize;i++)
			AddCompound ();

		PlaceMapTiles ();
		map.ScanMap ();

		for (int i=0; i<nEnemies; i++) {
			Enemy.Create(map.getRandomRoom(),1, 1);
		}
	}





	private void AddCompound(){
		do{
		InitBuffer ();
		GameObject[] rooms = GameObject.FindGameObjectsWithTag ("Room");
		indexRoom = rooms [(int)Mathf.Floor (Random.value * rooms.Length)].GetComponent<Room> ();
		int size1 =(int) Mathf.Floor (Random.value * 4) + 4;
		int size2 =(int) Mathf.Floor (Random.value * 4) + 4;
		int dir  =(int) Mathf.Floor (Random.value * 3) - 1;
		Append (Room.Create (2, 6), dir, 2);
		Append (Room.Create (size1, size2), 0, -2);

		//Debug.Log (validCompound);
		if(validCompound)
			ConfirmBuffer();
		else
			RollBack();
		}
		while(!validCompound);

	}

	private void InitBuffer(){
		roomBuffer.Clear ();
		validCompound = true;
	}

	private void ConfirmBuffer(){
		//for (int i=0; i<roomBuffer.Count; i++) {
		//	roomBuffer[i].BuildFloor();
		//}
	}

	private void RollBack(){
		for (int i=0; i<roomBuffer.Count; i++) {
			DestroyImmediate(roomBuffer[i].gameObject);
		}
	}
	
	void Append(Room room,int dir,int offset){
		if (AppendNewRoom (indexRoom, room, dir, offset))
			indexRoom = room;
		else {
			DestroyImmediate(room.gameObject);
			validCompound=false;

		}
	}


	bool AppendNewRoom(Room root, Room newRoom, int dir, int offset){
		int roomX, roomY;
		int eraserX, eraserY;
		int eraserW, eraserH;

		Room roomA, roomB;

		bool horizontalConnector;


		Vector2 flowVector = Quaternion.AngleAxis(-90*dir,Vector3.forward)*root.direction;


		if (Mathf.Abs(flowVector.x) > Mathf.Abs(flowVector.y)) {
			if(flowVector.x<0){
				roomX=root.x-newRoom.width;
				eraserX=root.x-1;
				roomA=newRoom;
				roomB=root;
			}
			else{
				roomX=root.x+root.width;
				eraserX=root.x+root.width-1;
				roomA=root;
				roomB=newRoom;
			}

			roomY=root.y+offset;
			eraserY=Mathf.Max(root.y,roomY)-1;
			eraserW=2;


			eraserH=Mathf.Min(root.y+root.height,roomY+newRoom.height)-Mathf.Max(root.y,roomY);
			if(eraserH<=0) return false;
			eraserH+=2;
			horizontalConnector=false;
		}
		else{
			if(flowVector.y<0){
				roomY=root.y-newRoom.height;
				eraserY=root.y-1;
				roomA=newRoom;
				roomB=root;
			}
			else{
				roomY=root.y+root.height;
				eraserY=root.y+root.height-1;
				roomA=root;
				roomB=newRoom;
			}
			roomX=root.x+offset;
			eraserX=Mathf.Max(root.x,roomX)-1;
			eraserH=2;

			eraserW=Mathf.Min(root.x+root.width,roomX+newRoom.width)-Mathf.Max(root.x,roomX);
			if(eraserW<=0) return false;
			eraserW+=2;
			horizontalConnector=true;
		}

		newRoom.setPosition (roomX, roomY);

		for (int i=0; i<roomBuffer.Count; i++) {
			if(roomBuffer[i].GetInstanceID()!= newRoom.GetInstanceID() && roomBuffer[i].GetInstanceID()!= root.GetInstanceID()){
				if(newRoom.IsOverlapping(roomBuffer[i])){
					return false;
				}
			}
		}

		GameObject [] rooms = GameObject.FindGameObjectsWithTag ("Room");
		for (int i=0; i<rooms.Length; i++) {
			if(rooms[i].GetInstanceID()!= newRoom.gameObject.GetInstanceID() && rooms[i].GetInstanceID()!= root.gameObject.GetInstanceID()){
				if(newRoom.IsOverlapping(rooms[i].GetComponent<Room>())){
				    return false;
				}
			}
		}

		newRoom.direction = flowVector;
		RoomConnector w=RoomConnector.Create (roomA, roomB, horizontalConnector, eraserX, eraserY, eraserW, eraserH);
		w.transform.parent = newRoom.transform.GetChild(0).transform;
		roomBuffer.Add (newRoom);
		return true;

	}

	void PlaceMapTiles(){
		GameObject[] go = GameObject.FindGameObjectsWithTag ("Room");

		for (int i=0; i<go.Length; i++) {
			go[i].GetComponent<Room>().BuildFloor();
			go[i].GetComponent<Room>().BuildWalls();
		}

		go = GameObject.FindGameObjectsWithTag ("RoomConnector");
		
		for (int i=0; i<go.Length; i++) {
			go[i].GetComponent<RoomConnector>().AddConnectionWalls();
		}
	}
}
