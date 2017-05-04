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

	void Awake(){
		instance = this;
		if (PlayerPrefs.HasKey("BestScore") && PlayerPrefs.HasKey("Gems")) {
			BestScore = PlayerPrefs.GetInt ("BestScore");
			Gems = PlayerPrefs.GetInt ("Gems");
		} else {
			BestScore = 0;
			PlayerPrefs.SetInt ("BestScore",BestScore);
			Gems = 0;
			PlayerPrefs.SetInt ("Gems",Gems);
		}
	}

	public void SetBestScore(int BestScore){
		this.BestScore = BestScore;
		PlayerPrefs.SetInt ("BestScore",this.BestScore);
	}

	public void SetGems(int Gems){
		this.Gems += Gems;
		PlayerPrefs.SetInt ("BestScore",this.Gems);
	}

	public void SetScore(int Score){
		this.Score += Score;
		EventsManager.Instance.UpdateScore (this.Score);
	}

	public void Init(){
		Score = 0;
	}

}