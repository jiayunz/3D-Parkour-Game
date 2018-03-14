using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPassTrap : Trap {
    public override void OnTriggerEnter(Collider other)
    {
        if(!PlayerController.instance.isJumping){
            base.OnTriggerEnter(other);
        }
    }
}
