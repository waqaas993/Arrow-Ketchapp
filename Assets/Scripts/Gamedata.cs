using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamedata : MonoBehaviour {

	private static Gamedata instance;
	public static Gamedata Instance{
		get {return instance;}
	}

	[HideInInspector]
	public int BestScore;
	[HideInInspector]
	public int Gems;
	[HideInInspector]
	public int Score;
	[HideInInspector]
	public int GamePlayedCount;

	void Awake(){
		instance = this;
		if (PlayerPrefs.HasKey("BestScore") && PlayerPrefs.HasKey("Gems") && PlayerPrefs.HasKey("GamePlayedCount")) {
			BestScore = PlayerPrefs.GetInt ("BestScore");
			Gems = PlayerPrefs.GetInt ("Gems");
			GamePlayedCount = PlayerPrefs.GetInt ("GamePlayedCount");
		} else {
			BestScore = 0;
			PlayerPrefs.SetInt ("BestScore",BestScore);
			Gems = 0;
			PlayerPrefs.SetInt ("Gems",Gems);
			GamePlayedCount = 0;
			PlayerPrefs.SetInt ("GamePlayedCount",GamePlayedCount);
		}
	}

	public void AddGamePlayedCount(int GamePlayedCount){
		this.GamePlayedCount += GamePlayedCount;
		PlayerPrefs.SetInt ("GamePlayedCount",this.GamePlayedCount);
	}


	public void SetBestScore(int BestScore){
		this.BestScore = BestScore;
		PlayerPrefs.SetInt ("BestScore",this.BestScore);
	}

	public void AddGems(int Gems){
		this.Gems += Gems;
		PlayerPrefs.SetInt ("BestScore",this.Gems);
	}

	public void AddScore(int Score){
		this.Score += Score;
		EventsManager.Instance.UpdateScore (this.Score);
	}

	public void Init(){
		Score = 0;
	}

}