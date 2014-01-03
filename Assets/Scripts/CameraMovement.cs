using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public GameObject player;

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update(){
		transform.position = new Vector3(player.transform.position.x,player.transform.position.y,-1);
	}
}
