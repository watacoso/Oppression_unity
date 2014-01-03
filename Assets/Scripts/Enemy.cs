using UnityEngine;
using System.Collections;
using Pathfinding;

public class Enemy : MonoBehaviour {

	private static Object resource= Resources.Load ("Enemy");

	public int speed = 10;

	private Seeker seeker ;
	private GameObject player;
	private Path path;

	private float nextWaypointDistance = 3;
	

	private int currentWaypoint = 0;

	public static void Create(Room room, int x, int y){
		GameObject r=(GameObject)Instantiate(resource);
		Enemy rs= r.GetComponent<Enemy> ();
		rs.transform.position = new Vector2 (Grid.Get (room.x + x), Grid.Get (room.y + y));
	}

	void Awake () {
		seeker = GetComponent<Seeker>();
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	//void Update(){
	//	seeker.StartPath (transform.position,player.transform.position,OnPathComplete);
	//}

	public void OnPathComplete (Path p) {
		Debug.Log ("Yey, we got a path back. Did it have an error? "+p.error);
		if (!p.error) {
			path = p;

			currentWaypoint = 0;
		}
	}

	public void FixedUpdate () {

		seeker.StartPath (transform.position,player.transform.position,OnPathComplete);

		if (path == null) {
			//We have no path to move after yet
			return;
		}
		
		if (currentWaypoint >= path.vectorPath.Count) {
			Debug.Log ("End Of Path Reached");
			return;
		}
		
		//Direction to the next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;


		rigidbody2D.AddForce (dir*speed);
		
		//Check if we are close enough to the next waypoint
		//If we are, proceed to follow the next waypoint
		if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
	}
	

}
