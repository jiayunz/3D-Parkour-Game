using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSetter : MonoBehaviour {

    public GameObject Side1;
    public GameObject Side2;
    public GameObject Side3;
    public GameObject Side4;
    bool added = false;
    public static RoadSetter instance;

    public void RemoveItem(GameObject side)
	{
		var item = side.transform.Find("Item");
		if (item != null)
		{
			foreach (var child in item)
			{
				Transform childTranform = child as Transform;
				if (childTranform != null)
				{
					Destroy(childTranform.gameObject);
				}
			}
		}
	}

	public void AddItem(GameObject side)
	{
        //Debug.Log("enter add item");
		var item = side.transform.Find("Item");
		if (item != null)
		{
			var patternManager = PatternManager.instance;
			if (patternManager != null && patternManager.Patterns != null && patternManager.Patterns.Count > 0)
			{
                var pattern = patternManager.Patterns[Random.Range(0, patternManager.Patterns.Count)];
                if(side == Side1){
					pattern = patternManager.Patterns[Random.Range(0, 3)];
				}
                else if(side == Side2){
					pattern = patternManager.Patterns[Random.Range(3, 6)];
				}
				else if (side == Side3)
				{
					pattern = patternManager.Patterns[Random.Range(6, 9)];
				}
				else if (side == Side4)
				{
					pattern = patternManager.Patterns[Random.Range(9, 12)];
				}

				if (pattern != null && pattern.PatternItems != null && pattern.PatternItems.Count > 0)
				{
					foreach (var patternItem in pattern.PatternItems)
					{
						var newObj = Instantiate(patternItem.gameobject);
						newObj.transform.parent = item;
						newObj.transform.localPosition = patternItem.position;
					}
				}
			}
		}
	}
	
    // Use this for initialization
	void Start()
	{
        instance = this;
	}

	// Update is called once per frame
	void Update () {
        //Debug.Log(PlayerController.instance.sideOnRunning);
        if (PlayerController.instance.sideOnRunning == SideOnRunning.Side1)
        {
            if (transform.position.z >= 600.0f && !added){
				RemoveItem(Side1);
				AddItem(Side2);
				added = true;
            }
            else if (transform.position.z < 600.0f){
                added = false;
            }
        }
        else if (PlayerController.instance.sideOnRunning == SideOnRunning.Side2)
        {
            if(transform.position.x <= -540.0f && !added){
				RemoveItem(Side2);
				AddItem(Side3);
                added = true;
            }
            else if (transform.position.x > -540.0f){
                added = false;
            }
        }
        else if (PlayerController.instance.sideOnRunning == SideOnRunning.Side3)
        {
			if (transform.position.z <= -545.0f && !added){
				RemoveItem(Side3);
				AddItem(Side4);
                added = true;
            }
            else if(transform.position.z > -545.0f){
                added = false;
            }
        }
        else if (PlayerController.instance.sideOnRunning == SideOnRunning.Side4)
        {
            if (transform.position.x >= 550.0f && !added)
            {
                RemoveItem(Side4);
                AddItem(Side1);
                added = true;
            }
            else if (transform.position.x < 550.0f){
                added = false;
            }
        }
	}
}
