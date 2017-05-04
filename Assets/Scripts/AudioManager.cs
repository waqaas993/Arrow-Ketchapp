using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	private static AudioManager instance;
	public static AudioManager Instance{
		get { return instance; }
	}

	public AudioSource Collectible;
	public AudioSource Gem;
	public AudioSource Gameover;
	public AudioSource Button;
	public AudioSource BackgroundMusic;

	void Awake(){
		instance = this;
	}

	public void PlayCollectible(){
		Collectible.Play ();
	}

	public void PlayGem(){
		Gem.Play ();
	}

	public void PlayGameover(){
		BackgroundMusic.Stop ();
		Gameover.Play ();
	}

	public void PlayButton(){
		Button.Play ();
	}

	public void Init(){
		Gameover.Stop ();
		BackgroundMusic.Play ();
	}

}