using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//When I say spawn, I'm NOT actually spawning these prefabs, but simply setting them active and deactive, and moving them here and there
//This consumes a bit of constant memory during run-time, but makes the job easy for Garbage Collector and Heap as we don't get
//frequent Memory spikes during runtime due to continous instantiation and destroying

public class EndlessScroller : MonoBehaviour {

	//We will switch Patch1 and Patch2 to create dynamic and endless experience
	public GameObject[] Patch = new GameObject[2];

	//These will hold all different level prefabs as an array
	public GameObject[] Patch1Prefabs;
	public GameObject[] Patch2Prefabs;

	//Which one is active at the moment? 1st or 2nd?
	[HideInInspector]
	public int CurrentActivePatch;
	//What is the location of current active prfab in the world on Y Axis?
	[HideInInspector]
	public float CurrentPatchLocation;

	//Creating a singleton
	private static EndlessScroller instance;
	public static EndlessScroller Instance{
		get { return instance; }
	}

	void Awake(){
		instance = this;
		Init ();
	}

	void Init(){
		CurrentPatchLocation = 0;
		for (int i = 0; i < Patch1Prefabs.Length; i++) {
			Patch1Prefabs [i].SetActive (false);
		}
		Patch [0].transform.position = new Vector3 (Patch [0].transform.position.x, CurrentPatchLocation ,Patch [0].transform.position.z);
		Patch1Prefabs [Random.Range(0,Patch1Prefabs.Length)].SetActive (true);
		CurrentActivePatch = 1;
	}

	public void SpawnNextPrefab(){
		CurrentPatchLocation += 50;
		PreparePrefabs ();
	}

	//Prepare Patch for the Next Spawn
	private void PreparePrefabs(){
		//If the Current Patch is 1, then set all child of patch 2 to false and prepare it for the next spawn
		if (CurrentActivePatch == 1) {
			for (int i = 0; i < Patch2Prefabs.Length; i++) {
				Patch2Prefabs [i].SetActive (false);
			}
			Patch [1].transform.position = new Vector3 (Patch [1].transform.position.x, CurrentPatchLocation ,Patch [1].transform.position.z);
			Patch2Prefabs [Random.Range(0,Patch2Prefabs.Length)].SetActive (true);
			CurrentActivePatch = 2;
		}
		//If the Current Patch is 2, then set all child of patch 1 to false and prepare it for the next spawn
		else if (CurrentActivePatch == 2) {
			for (int i = 0; i < Patch1Prefabs.Length; i++) {
				Patch1Prefabs [i].SetActive (false);
			}
			Patch [0].transform.position = new Vector3 (Patch [0].transform.position.x, CurrentPatchLocation ,Patch [0].transform.position.z);
			Patch1Prefabs [Random.Range(0,Patch1Prefabs.Length)].SetActive (true);
			CurrentActivePatch = 1;
		}
	}

}