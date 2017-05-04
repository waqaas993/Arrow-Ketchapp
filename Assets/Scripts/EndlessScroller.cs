using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//When I say spawn, I'm NOT actually spawning these prefabs, but simply setting them active and deactive, and moving them here and there
//This consumes a bit of constant memory during run-time, but makes the job easy for Garbage Collector and Heap as we don't get
//frequent Memory spikes during runtime due to continous instantiation and destroying

public class EndlessScroller : MonoBehaviour {

	//Reference to Wall material for Gradient Effect
	public Material WallMaterial;
	private bool isColorTransitioning=false;
	public Color[] GradientColor;
	private int DecideColor;
	public float ColorTransitioningInterval;

	//We will switch Patch1 and Patch2 to create dynamic and endless experience
	public GameObject[] Patch = new GameObject[2];

	//These will hold all different level prefabs as an array
	public GameObject[] Patch1Prefabs;
	public GameObject[] Patch2Prefabs;

	//Real-time random decision for which prefab to choose
	private int Decision = 0;
	//Number of children in 'Collectible' game object of patch
	private int childCount;
	//Child e.g Colectbiles or Gems
	private GameObject TheChild;

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
		Invoke ("TransitionColor",ColorTransitioningInterval);
	}

	public void Init(){
		CurrentPatchLocation = 0;
		CurrentActivePatch = 1;
		DecideColor = Random.Range (0,GradientColor.Length);
		WallMaterial.color = GradientColor [DecideColor];
		PreparePrefabs ();
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
			//Decide which patch to put next
			Decision = Random.Range (0, Patch2Prefabs.Length);
			//Find Child is not recommended since Unity has to go through each and every object to find it
			//It's better to pass references on inspectpr
			//But since this game doesn't make use of computations very much and it will be a lot of references if we pass it on inspector
			TheChild = Patch2Prefabs [Decision].transform.FindChild ("Collectibles").transform.gameObject;
			childCount = TheChild.transform.childCount;
			for (int i = 0; i < childCount; ++i)
				TheChild.transform.GetChild (i).transform.gameObject.SetActive (true);
			//Re-activate all the gems of this Patch
			TheChild = Patch2Prefabs [Decision].transform.FindChild ("Gems").transform.gameObject;
			childCount = TheChild.transform.childCount;
			for (int i = 0; i < childCount; ++i)
				TheChild.transform.GetChild (i).transform.gameObject.SetActive (true);
			Patch2Prefabs [Decision].SetActive (true);
			CurrentActivePatch = 2;
		}
		//If the Current Patch is 2, then set all child of patch 1 to false and prepare it for the next spawn
		else if (CurrentActivePatch == 2) {
			for (int i = 0; i < Patch1Prefabs.Length; i++) {
				Patch1Prefabs [i].SetActive (false);
			}
			Patch [0].transform.position = new Vector3 (Patch [0].transform.position.x, CurrentPatchLocation ,Patch [0].transform.position.z);
			//Decide which patch to put next
			Decision = Random.Range (0, Patch1Prefabs.Length);
			TheChild = Patch1Prefabs [Decision].transform.FindChild ("Collectibles").transform.gameObject;
			childCount = TheChild.transform.childCount;
			for (int i = 0; i < childCount; ++i)
				TheChild.transform.GetChild (i).transform.gameObject.SetActive (true);
			//Re-activate all the gems of this Patch
			TheChild = Patch1Prefabs [Decision].transform.FindChild ("Gems").transform.gameObject;
			childCount = TheChild.transform.childCount;
			for (int i = 0; i < childCount; ++i)
				TheChild.transform.GetChild (i).transform.gameObject.SetActive (true);
			Patch1Prefabs [Decision].SetActive (true);
			CurrentActivePatch = 1;
		}
	}

	void FixedUpdate(){
		if (WallMaterial.color != GradientColor[DecideColor] && isColorTransitioning) {
			WallMaterial.color = Color.Lerp (WallMaterial.color,GradientColor[DecideColor],Time.deltaTime*3);
		} else if (WallMaterial.color == GradientColor[DecideColor] && isColorTransitioning){
			isColorTransitioning = false;
			Invoke ("TransitionColor",ColorTransitioningInterval);
		}
	}

	private void TransitionColor(){
		DecideColor = Random.Range (0,GradientColor.Length);
		isColorTransitioning = true;
	}


}