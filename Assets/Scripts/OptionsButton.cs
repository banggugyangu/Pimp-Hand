using UnityEngine;
using System.Collections;

public class OptionsButton : MonoBehaviour {

	public GameObject optionsMenu;
	public GameObject mainMenu;
	
	// Use this for initialization
	void Start () {
		optionsMenu = GameObject.Find("Options Canvas");
		mainMenu = GameObject.Find("Main Menu");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public IEnumerator OptionsClick(){
		mainMenu.SetActive(false);
		yield return new WaitForSeconds(1);
		optionsMenu.SetActive(true);
	}
}
