using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState{
	Gameplay,
	Gameover
}

public class EventsManager : MonoBehaviour {

	[HideInInspector]
	public int CurrentScreen;
	//It's better to use a string like that
	//Score.text = "" is a bad practice as it creates value type variable on backend stack data structure
	private string EmptyString = "";

	[Header("Screens and Panels")]
	public GameObject Gameplay;
	public GameObject Gameover;

	[Header("Elements from Gameover Screen")]
	public Text Score;
	public Text BestScore;
	public Text TotalGems;

	[Header("Elements from Gameplay Screen")]
	public Text gameplayScore;
	public Animator ScoreAnim;
	public GameObject TutorialText;

	private static EventsManager instance;
	public static EventsManager Instance{
		get { return instance;}
	}

	void Start(){
		instance = this;
		CurrentScreen = (int)GameState.Gameover;
		DisplayGameplay ();
	}

	public void Init(){
		Score.text = EmptyString;
		TotalGems.text = EmptyString;
		gameplayScore.text = 0.ToString ();
		if (Gamedata.Instance.GamePlayedCount < 3) {
			TutorialText.SetActive (true);
			Invoke ("DisableTutorialText", 5);
		}
	}

	public void DisplayGameover(){
		if (CurrentScreen == (int)GameState.Gameplay ) {
			CurrentScreen = (int)GameState.Gameover;
			Gameplay.SetActive (false);
			Score.text = Gamedata.Instance.Score.ToString ();
			if (Gamedata.Instance.Score > Gamedata.Instance.BestScore)
				Gamedata.Instance.SetBestScore (Gamedata.Instance.Score);
			BestScore.text = Gamedata.Instance.BestScore.ToString ();
			TotalGems.text = Gamedata.Instance.Gems.ToString ();
			Gameover.SetActive (true);
			//In case the player died within 3 second
			if (Gamedata.Instance.GamePlayedCount < 3)
				CancelInvoke ("DisableTutorialText");
			Gamedata.Instance.AddGamePlayedCount(1);
		}
	}

	//Restart On Click listener
	public void DisplayGameplay(){
		if (CurrentScreen == (int)GameState.Gameover ) {
			//Initialize everything to make it go back to its default states
			this.Init ();
			AudioManager.Instance.Init ();
			Controller.Instance.Init ();
			Gamedata.Instance.Init ();
			EndlessScroller.Instance.Init ();
			CameraFollow.Instance.Init ();
			CurrentScreen = (int)GameState.Gameplay;
			Gameover.SetActive (false);
			Gameplay.SetActive (true);
		}
	}

	public void UpdateScore(int Score){
		gameplayScore.text = Score.ToString();
		//Using Mecanim (Unity's built-in animation system) for punching scale of the Score Text
		ScoreAnim.SetTrigger ("Scale");
	}

	//For disabling the in-game tutorial text
	public void DisableTutorialText(){
		TutorialText.SetActive (false);
	}
}