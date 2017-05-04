using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public Transform Player;
	//How much far should the camera be from Play on Y-Axis?
	public float Offset;
	void FixedUpdate(){
		if (EventsManager.Instance.CurrentScreen == (int)GameState.Gameplay)
			transform.position = new Vector3 (transform.position.x, Player.transform.position.y+Offset, transform.position.z);
	}
}