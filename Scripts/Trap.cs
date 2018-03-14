using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    public int hurtValue = 1;
    public int xmoveSpeed = 0;
    public int ymoveSpeed = 0;
    public int zmoveSpeed = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(xmoveSpeed * Time.deltaTime, ymoveSpeed * Time.deltaTime,zmoveSpeed * Time.deltaTime);
	}

    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            AudioManager.instance.PlayHitAudio();
            CameraManager.instance.CameraShake();
            GameAttribute.instance.life -= hurtValue;
        }
    }
}
