using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip button;
	public AudioClip coin;
	public AudioClip getItem;
	public AudioClip hit;
    public AudioClip slide;

    public static AudioManager instance;

    void PlayAudio(AudioClip clip){
        AudioSource.PlayClipAtPoint(clip,PlayerController.instance.transform.position);
    }

    public void PlayButtonAudio(){
        PlayAudio(button);
    }
	public void PlayCoinAudio()
	{
		PlayAudio(coin);
	}
	public void PlayGetItemAudio()
	{
		PlayAudio(getItem);
	}
	public void PlayHitAudio()
	{
		PlayAudio(hit);
	}
	public void PlaySlideAudio()
	{
		PlayAudio(slide);
	}

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
