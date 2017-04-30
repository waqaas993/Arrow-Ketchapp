using UnityEngine;
using System.Collections;

public class Tail : MonoBehaviour {

	private int order;
	private Transform Head;
	private Vector3 movementVelocity;
	[Range(0.0f,1.0f)]
	public float overTime = 0.2f;
	private Vector3 lookPos;
	private Controller TheHeadController;

	void Start(){
		Head = GameObject.FindGameObjectWithTag ("Player").gameObject.transform;
		TheHeadController = Head.GetComponent<Controller> ();
		for(int i = 0; i < TheHeadController.tailParts.Count ; i++){
			if (gameObject == TheHeadController.tailParts [i].gameObject) {
				order = i;			
			}
		}
	}

	void FixedUpdate(){
		//If it's the first Tail Part
		if (order == 0) {
			lookPos = Head.transform.position - transform.position;
			float rotationZ = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
			transform.position = Vector3.SmoothDamp (transform.position, Head.position, ref movementVelocity, overTime);
//			transform.position = Vector3.Slerp(transform.position, Head.position,Time.deltaTime*6);
		} 
		//If it's any Tail part other the first
		else {
			lookPos = Head.GetComponent<Controller> ().tailParts [order - 1].position - transform.position;
			float rotationZ = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
			transform.position = Vector3.SmoothDamp (transform.position, TheHeadController.tailParts [order - 1].position, ref movementVelocity, overTime);
//			transform.position = Vector3.Slerp(transform.position, TheHeadController.tailParts [order - 1].position,Time.deltaTime*6);
		}
	}
}