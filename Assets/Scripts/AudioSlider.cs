using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class AudioSlider : MonoBehaviour {

	public AudioMixer audioMixer;
	public Slider musicSlider;
	public Slider sfxSlider;
	public Slider voicesSlider;
	
	void Start(){
	}
	
	public void AdjustMusic(){
		audioMixer.SetFloat("MusicVolume", musicSlider.value);
		GameInformation.musicLevel = musicSlider.value;
	}
	
	public void AdjustSFX(){
		audioMixer.SetFloat("SFXVolume", sfxSlider.value);
		GameInformation.sFXLevel = sfxSlider.value;
	}
	
	public void AdjustVoices(){
		audioMixer.SetFloat("VoicesVolume", voicesSlider.value);
		GameInformation.voicesLevel = voicesSlider.value;
	}
	
	public void SetSliders(){
		musicSlider.value = GameInformation.musicLevel;
		audioMixer.SetFloat("MusicVolume", GameInformation.musicLevel);
		sfxSlider.value = GameInformation.sFXLevel;
		audioMixer.SetFloat("SFXVolume", GameInformation.sFXLevel);
		voicesSlider.value = GameInformation.voicesLevel;
		audioMixer.SetFloat("VoicesVolume", GameInformation.voicesLevel);
	}
}
