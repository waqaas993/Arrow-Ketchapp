using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public Transform Player;
	public float Offset;
	void FixedUpdate(){
		transform.position = new Vector3 (transform.position.x, Player.transform.position.y+Offset, transform.position.z);
	}
}