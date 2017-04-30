using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
	[Tooltip("Turning Angle for the Character")]
	[Range(1,90)]
	public int AngleInDegrees;
	[Tooltip("Desired Forward speed of the Character")]
	public float ForwardSpeed;
	//Is the Player pressing tap?
	private bool bool_tap = false;

	//This holds the information on Tail Parts of the Arrow
	[HideInInspector]
	public List<Transform> tailParts = new List<Transform> ();

	//Useful data members for adding Tail objects to Character's Head (Arrow)
	private Vector3 currentPos;
	private GameObject theTailPart;
	public GameObject tailPrefab;
	public Transform tailHolder;

	void Awake(){
		AddTail ();
		AddTail ();
		AddTail ();
		AddTail ();
		AddTail ();
	}

	//Physics based computations goes in here
	void FixedUpdate(){
		MoveForward ();
		ChangeDirection ();
	}

	//Character always moves forward
	private void MoveForward(){
		//TODO: Put this inside if gameplay condition
		transform.position += transform.up * ForwardSpeed * Time.deltaTime;
	}

	//Character always moves forward
	private void ChangeDirection(){
		//TODO: Put this inside if gameplay condition
		if (bool_tap) {
			transform.eulerAngles = new Vector3 (0, 0, Mathf.Lerp (FetchAngle(transform.rotation.eulerAngles.z),AngleInDegrees,Time.deltaTime*6));
		}
		else if(!bool_tap){
			transform.eulerAngles =  new Vector3 (0, 0, Mathf.Lerp (FetchAngle(transform.rotation.eulerAngles.z),-AngleInDegrees,Time.deltaTime*6));
		}
	}


	public void OnClickTapDown(){
		bool_tap = true;
	}

	public void OnClickTapUp(){
		bool_tap = false;
	}

	//Converting the second quadrant angle values into negative e.g 330 = -30
	public float FetchAngle(float Angle){
		//Don't really have to perform conversion for the first quadrant

		//Perform conversion if the Angle is in second quadrant
		if (Angle >= 270 && Angle < 360) {
			Angle -= 360;
		}
		//3rd and 4th Quadrant, this should NOT happen and this block of code is not supposed to execute since we're 
		//only dealing with first and second quadrant in this game
		else if (Angle > 90 && Angle < 270) {
			//Just for house-keeping
			Mathf.Clamp (Angle,-AngleInDegrees,AngleInDegrees);
		}
		return Angle;
	}


	void OnTriggerEnter(Collider Other){
		if (Other.CompareTag("Collectible")) {
			//TODO: Increment Score
			//TODO: Add Tail for visual representation if neccessary
			//TODO: Destroy Collectibe
		}
	}

	//Adds tail object to the Character's Head (Arrow)
	void AddTail(){
		if (tailParts.Count == 0)
			currentPos = transform.position;		
		else
			currentPos = tailParts [tailParts.Count - 1].position;
		theTailPart = Instantiate (tailPrefab, currentPos + (new Vector3(5,0,0)), Quaternion.Euler(0,0,180)) as GameObject;
		theTailPart.transform.SetParent (tailHolder);
		tailParts.Add (theTailPart.transform);
	}

}