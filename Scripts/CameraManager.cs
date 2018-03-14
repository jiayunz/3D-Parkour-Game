using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    public GameObject target;
    public float height;
    public float distance;
    Vector3 pos;
    Vector3 rot;
    bool isShaking = false;
    public static CameraManager instance;

	private void Start()
    {
        instance = this;
        pos = transform.position;
        rot = transform.localEulerAngles;
    }

    public void CameraShake(){
        if(!isShaking){
            StartCoroutine(ShakeCoroutine());
        }

    }

    IEnumerator ShakeCoroutine(){
        isShaking = true;
        float time = 0.5f;
        while(time > 0){
            if(PlayerController.instance.sideOnRunning == SideOnRunning.Side1){
				transform.position = new Vector3(target.transform.position.x + Random.Range(-0.1f, 0.1f),
								                 target.transform.position.y + height,
								                 target.transform.position.z - distance);
            }
			else if (PlayerController.instance.sideOnRunning == SideOnRunning.Side2)
			{
				transform.position = new Vector3(target.transform.position.x + distance,
								                 target.transform.position.y + height,
                                                 target.transform.position.z + Random.Range(-0.1f, 0.1f));
			}
			else if (PlayerController.instance.sideOnRunning == SideOnRunning.Side3)
			{
				transform.position = new Vector3(target.transform.position.x + Random.Range(-0.1f, 0.1f),
								                 target.transform.position.y + height,
                                                 target.transform.position.z + distance);
			}
			else if (PlayerController.instance.sideOnRunning == SideOnRunning.Side4)
			{
				transform.position = new Vector3(target.transform.position.x - distance,
												 target.transform.position.y + height,
                                                 target.transform.position.z + Random.Range(-0.1f, 0.1f));
			}
            time -= Time.deltaTime;
            yield return null;
        }
        isShaking = false;
    }

    private void LateUpdate()
    {
        if(!isShaking && GameController.instance.isPlay && !GameController.instance.isPause){
            //Debug.Log(target.transform.localEulerAngles.y);
            //if (target.transform.localEulerAngles.y == 90)
            //Debug.Log(gameObject);
            if(PlayerController.instance.sideOnRunning == SideOnRunning.Side4)
			{
                pos.x = Mathf.Lerp(pos.x,target.transform.position.x - distance, Time.deltaTime * 20);
                pos.z = Mathf.Lerp(pos.z,target.transform.position.z, Time.deltaTime * 20);

			}
            //else if (target.transform.localEulerAngles.y == 180)
            else if(PlayerController.instance.sideOnRunning == SideOnRunning.Side3)
			{
				pos.x = Mathf.Lerp(pos.x, target.transform.position.x, Time.deltaTime * 20);
				pos.z = Mathf.Lerp(pos.z, target.transform.position.z + distance, Time.deltaTime * 20);
			}

            //else if (target.transform.localEulerAngles.y == 270)
            else if(PlayerController.instance.sideOnRunning == SideOnRunning.Side2)
			{
				pos.x = Mathf.Lerp(pos.x, target.transform.position.x + distance, Time.deltaTime * 20);
				pos.z = Mathf.Lerp(pos.z, target.transform.position.z, Time.deltaTime * 20);

			}

            //else if (target.transform.localEulerAngles.y <= 0)
            else if(PlayerController.instance.sideOnRunning == SideOnRunning.Side1)
			{
                pos.x = Mathf.Lerp(pos.x, target.transform.position.x, Time.deltaTime * 20);
				pos.z = Mathf.Lerp(pos.z, target.transform.position.z - distance, Time.deltaTime * 20);


			}

			pos.y = Mathf.Lerp(pos.y, 92.0f + height, Time.deltaTime * 2);
			rot.y = target.transform.localEulerAngles.y;

			transform.position = pos;
			transform.localEulerAngles = rot;
		}
	}
}
