using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public Transform Player;
	//How much far should the camera be from Play on Y-Axis?
	public float Offset;
	private static CameraFollow instance;
	public static CameraFollow Instance{
		get { return instance; }
	}
	void Awake(){
		instance = this;
	}
	void FixedUpdate(){
		if (EventsManager.Instance.CurrentScreen == (int)GameState.Gameplay)
			transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,Player.transform.position.y+Offset,transform.position.z), Time.deltaTime*5);
	}
	public void Init(){
		transform.position = new Vector3 (transform.position.x, Player.transform.position.y + Offset, transform.position.z);
	}
}