using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Skank : MonoBehaviour {

	private float start;
	private int threshold;
	private int stress;
	private float impact;
	private int addedStress;
	public int score;
	private bool readyToReduce;
	public GUIStyle Scoreboard;
	public GUIStyle Buttons;
	private GameObject pimpHand;
	public AudioClip slapSound;
	public AudioClip skankVoice1;
	public AudioClip skankVoice2;
	public AudioClip skankVoice3;
	public AudioClip skankVoice4;
	public AudioClip skankVoice5;
	public AudioClip pimpVoice1;
	public AudioClip pimpVoice2;
	public AudioClip pimpVoice3;
	public AudioClip pimpVoice4;
	public AudioClip pimpVoice5;
	public AudioClip moneySound;
	private AudioSource soundFXSlap;
	private AudioSource soundFXMoney;
	private string skankSelection;
	public Text moneyTXT;
	public Text highScoreTXT;
	public GameObject ScoreBoard;
	public GameObject GameOverScreen;
	public GameObject ClickSystem;
	public GameObject GOHIGH;
	public GameObject BackGround;
	public GameObject skank;
	public GameObject Hand;
	private string skankSelect;
	private SpriteRenderer skankSprite;
	private SpriteRenderer backgroundSprite;
	public Text GOMoney;
	public Text GOMoney2;
	public SpriteRenderer handSprite;
	public bool isPaused;
	private int runTimer;
	
	
	// Use this for initialization
	void Start () {
		//Determine Ad Status and play if appropriate
		GameInformation.ShowAd();
		
		//Randomize Start
		backgroundSprite = BackGround.GetComponent<SpriteRenderer>();
		skankSprite = skank.GetComponent<SpriteRenderer>();
		handSprite = Hand.GetComponent<SpriteRenderer>();
		int skankRandomizer = Random.Range (1,6);
		int pimpRandomizer = Random.Range (1,6);
		int streetRandomizer = Random.Range (1,3);
		backgroundSprite.sprite = Resources.Load<UnityEngine.Sprite>("Textures/Backgrounds/Street" + streetRandomizer);
		Debug.Log("Street" + streetRandomizer);
		if(skankRandomizer == 1 || skankRandomizer == 3 || skankRandomizer == 5){
			skankSelect = "LargeMarge";
		}if(skankRandomizer == 2 || skankRandomizer == 4 || skankRandomizer == 6){
			skankSelect = "sammy";
		}
		//Determining Stress Threshold at beginning of new game
		start = Random.value;
		while(start == 0f){
			start = Random.value;
		}
		threshold = (int)(start * 100f);
		while(threshold < 25){
			threshold *= 2;
		}
		stress = 0;
		addedStress = 0;
		score = 0;
		impact = 0f;
		runTimer = 0;
		GetComponent<Renderer>().enabled = true;
		
		//Beginning Stress Reduction
		readyToReduce = true;
		
		//Resetting GameOver Flag
		GameInformation.isGameOver = false;
		isPaused = false;
		pimpHand = GameObject.Find("PimpHand");
		pimpHand.GetComponent<Renderer>().enabled = false;
		
		//Increasing Game Counter
		GameInformation.gameCounter += 1;
		
		//AudioSource Definitions
		soundFXSlap = GameObject.Find("SoundFXSlap").GetComponent<AudioSource>();
		soundFXMoney = GameObject.Find("SoundFXMoney").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine(ReduceStress());
		moneyTXT.text = "Money:  $" + score;
		highScoreTXT.text = "Best Investment:  $" + GameInformation.HighScore;
		if(score <= 20){
			skankSprite.sprite = Resources.Load<UnityEngine.Sprite>("Textures/Sprites/" + skankSelect + "1");
		}if(score > 20 && score <= 50){
			skankSprite.sprite = Resources.Load<UnityEngine.Sprite>("Textures/Sprites/" + skankSelect + "2");
		}if(score > 50 && score <= 100){
			skankSprite.sprite = Resources.Load<UnityEngine.Sprite>("Textures/Sprites/" + skankSelect + "3");
		}if(score > 100 && score <= 150){
			skankSprite.sprite = Resources.Load<UnityEngine.Sprite>("Textures/Sprites/" + skankSelect + "4");
		}if(score > 150){
			skankSprite.sprite = Resources.Load<UnityEngine.Sprite>("Textures/Sprites/" + skankSelect + "5");
		}
		if(runTimer == 20){
			GetComponent<Renderer>().enabled = false;
			GameOver();
		}
	}
	
	IEnumerator ReduceStress(){
		if(!GameInformation.isGameOver && !isPaused){
			if(readyToReduce){
				readyToReduce = false;
				yield return new WaitForSeconds(1);
				if(stress > 0){
					stress -= 1;
				}
				runTimer += 1;
				Debug.Log("Stress:  " + stress);
				readyToReduce = true;
			}
		}
	}
	
	public void Slap(){
		//Begin Slap Animation and Sound FX
		StartCoroutine(ShowHand());
		runTimer = 0;
		
		//Determining strength of slap
		impact = Random.value;
		while(impact == 0f){
			impact = Random.value;
		}
		addedStress = (int)(impact * 6);
		if(addedStress <= 1){
			stress += 1;
			if (stress < threshold){
				score += 1;
			}else{
				GameOver();
			}
		}else{
			stress += addedStress;
			if(stress < threshold){
				score += (int)(addedStress * .5);
			}else{
				GameOver();
			}
			
		}
		
		//Determine which hand to show
		if(addedStress > 4){
			handSprite.sprite = Resources.Load<UnityEngine.Sprite>("Textures/Sprites/fishHand");
		}
		else{
			handSprite.sprite = Resources.Load<UnityEngine.Sprite>("Textures/Sprites/PimpHand1");
		}
		
		//Determining if new High Score
		if(score > GameInformation.HighScore){
			GameInformation.HighScore = score;
			GameInformation.newHighScore = true;
		}else{
			GameInformation.newHighScore = false;
		}
	}
	
	public void NewGame(){
		Start();
	}
	
	//Slap Animation and Sound FX
	IEnumerator ShowHand(){
		if(!GameInformation.isGameOver){
			soundFXSlap.Play();
			pimpHand.GetComponent<Renderer>().enabled = true;
			yield return new WaitForSeconds(.15f);
			pimpHand.GetComponent<Renderer>().enabled = false;
			soundFXMoney.Play();
		}
	}
	
	public void GameOver(){
		GOMoney.text = "Money:  $" + score;
		GOMoney2.text = "Money:  $" + score;
		GameOverScreen.SetActive(true);
		ClickSystem.SetActive(false);
		ScoreBoard.SetActive(false);
		if(GameInformation.newHighScore){
			GOHIGH.SetActive(true);
		}else{
			GOHIGH.SetActive(false);
		}
		GameInformation.isGameOver = true;
	}
	
	public void Paused(){
		isPaused = true;
	}
	
	public void unPaused(){
		isPaused = false;
	}
}
