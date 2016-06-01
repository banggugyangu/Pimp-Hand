using UnityEngine;
using System.Collections;

public class SkankBGM : MonoBehaviour {

	public AudioClip Intro;
	public AudioClip Loop;
	
	void Start(){
		StartCoroutine(playBGM());
	}
	
	IEnumerator playBGM(){
		GetComponent<AudioSource>().clip = Intro;
		GetComponent<AudioSource>().Play();
		yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
		GetComponent<AudioSource>().clip = Loop;
		GetComponent<AudioSource>().Play();
		GetComponent<AudioSource>().loop = true;
	}
}
