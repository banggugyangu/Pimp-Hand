using UnityEngine;
using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;

[Serializable]
public class GameInformation : MonoBehaviour {

	public static int gameCounter;
	public static int HighScore;
	public static bool newHighScore;
	public static bool isGameOver;
	public static NetworkReachability internetReachability;
	public static float musicLevel;
	public static float sFXLevel;
	public static float voicesLevel;
	

	void Awake (){
		DontDestroyOnLoad(gameObject);
		musicLevel = 0f;
		sFXLevel = 0f;
		voicesLevel = 0f;
	}
	
	void Start (){
		newHighScore = false;
		gameCounter = 0;
	}
	
	void Update(){
		internetReachability = Application.internetReachability;
	}
	
	public static void ShowAd(){
		Debug.Log(Advertisement.IsReady());
		Debug.Log("GameCounter:  " + gameCounter);
		Debug.Log(internetReachability);
		if(Advertisement.IsReady()){
			if(gameCounter == 0){
				Debug.Log("Add Played");
				Advertisement.Show();
				Debug.Log(Application.internetReachability);
			}
			if(gameCounter == 10){
				Advertisement.Show();
				gameCounter = 0;
			}else{
				
			}
		}
	}
	
	public static bool HasConnection(){
    	try{
    	   	using (var client = new WebClient())
       		using (var stream = new WebClient().OpenRead("http://www.google.com")){
            	return true;
        	}
     	}	
     	catch{
        return false;
		}
	}
 
	bool CheckConnection(string URL){
    	try{
        	HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
        	request.Timeout = 5000;
        	request.Credentials = CredentialCache.DefaultNetworkCredentials;
        	HttpWebResponse response = (HttpWebResponse)request.GetResponse();
 
        	if (response.StatusCode == HttpStatusCode.OK) return true;
        	else return false;
    	}
    	catch{
        	return false;
    	}
	}
	
	public void Exit(){
		Application.Quit();
	}
	
}
