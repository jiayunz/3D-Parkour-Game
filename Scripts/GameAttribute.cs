using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAttribute : MonoBehaviour {


    public static GameAttribute instance;
    public int multiply;
    public int life;
    public int coin;
    public Text TextCoin;
    public int init_life = 1;
	// Use this for initialization
	void Start () {
        instance = this;
        coin = 0;
        life = 1;
        multiply = 1;
	}
	
	// Update is called once per frame
	void Update () {
        TextCoin.text = coin.ToString();
	}

    public void Reset(){
        life = init_life;
        coin = 0;
        multiply = 1;
    }

    public void AddCoin(int coinNumber){
        coin += multiply * coinNumber;
    }
}
