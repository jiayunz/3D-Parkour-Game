using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollPassTrap : Trap {

	public override void OnTriggerEnter(Collider other)
	{
		if (!PlayerController.instance.isRolling)
		{
			base.OnTriggerEnter(other);
		}
	}
}
