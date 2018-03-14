using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item {

    public override void HitItem()
    {
        base.HitItem();
        GameAttribute.instance.AddCoin(1);

    }
    public override void PlayHitAudio()
    {
        AudioManager.instance.PlayCoinAudio();
    }
}
