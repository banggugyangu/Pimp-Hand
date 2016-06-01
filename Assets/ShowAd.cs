using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class ShowAd : MonoBehaviour {

	public void showAd(){
		Debug.Log(Advertisement.IsReady());
		if(Advertisement.IsReady()){
			Advertisement.Show();
			Debug.Log(Advertisement.IsReady());
		}
	}
}
