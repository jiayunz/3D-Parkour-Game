﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speed = 1;
    public float init_speed = 1;
    float maxSpeed = 50;
    float speedAddRate = 1.0f;
    float speedAddDistance = 30;
    float speedAddCount = 0;
    float gravity = 10;
    InputDirection inputDirection;
    Vector3 mousePos;
    bool activeInput;

    private Animator anim;
    //left & right
    public Position standPosition;
    Position fromPosition;
    Vector3 LeftRightDirection;
    Vector3 moveDirection;
    CharacterController characterController;
    //sideOnRunning
    public SideOnRunning sideOnRunning;

    //jump & roll
    public bool isJumping;
    public bool isRolling;

    //speed up
    float saveSpeed;
    float quickMoveDuration = 3;
    public float quickMoveTimeLeft;
    bool isQuickMoving = false;
    IEnumerator QuickMoveCor;

    //magnet
    float magnetDuration = 9;
    public float magnetTimeLeft;
    IEnumerator MagnetCor;
    public GameObject MagnetCollider;

    //multiply
    float multiplyDuration = 6;
    public float multiplyTimeLeft;
    IEnumerator MultiplyCor;

    int life = 1;
    bool isPlay = true;
    bool isPause = true;

    //UI text
    public Text TextStar;
    public Text TextMagnet;
    public Text TextMultiply;

    public static PlayerController instance;

    // Use this for initialization
    void Start()
    {
        instance = this;
        speed = init_speed;
        anim = this.GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        standPosition = Position.Middle;
        sideOnRunning = SideOnRunning.Side1;
        StartCoroutine(UpdateAction());
    }

    public void Play(){
        GameController.instance.isPause = false;
        GameController.instance.isPlay = true;
        StartCoroutine(UpdateAction());
    }

    void SetSpeed(float newSpeed){
        if(newSpeed <= maxSpeed){
            speed = newSpeed;
        }
        else{
            speed = maxSpeed;
        }

    }

    IEnumerator UpdateAction()
    {
        while (life > 0)
        {
            if (isPlay && !isPause)
            {
                GetInputDirection();
                MoveLeftRight();
                MoveForward();
                UpdateSpeed();
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
            yield return 0;
        }
        speed = 0;
        GameController.instance.isPlay = false;
        LeftRightDirection = Vector3.zero;
        anim.SetTrigger("isDead");
        //avoid dead again
        if(sideOnRunning == SideOnRunning.Side1){
            iTween.MoveTo(gameObject, gameObject.transform.position - new Vector3(0, 0, 100), 3.0f);
        }
        else if(sideOnRunning == SideOnRunning.Side2){
            iTween.MoveTo(gameObject, gameObject.transform.position - new Vector3(-100, 0, 0), 3.0f);
        }
		else if (sideOnRunning == SideOnRunning.Side3)
		{
			iTween.MoveTo(gameObject, gameObject.transform.position - new Vector3(0, 0, -100), 3.0f);
		}
		else if (sideOnRunning == SideOnRunning.Side4)
		{
			iTween.MoveTo(gameObject, gameObject.transform.position - new Vector3(100, 0, 0), 3.0f);
		}
        anim.SetBool("isRunning", false);
        yield return new WaitForSeconds(2.0f);
        UIController.instance.ShowRestartUI();
        UIController.instance.HidePauseUI();
    }

    void MoveLeft()
    {
        if (standPosition != Position.Left)
        {
            if (sideOnRunning == SideOnRunning.Side1)
            {
                LeftRightDirection = new Vector3(-1, 0, 0);
            }
            else if (sideOnRunning == SideOnRunning.Side2)
            {
                LeftRightDirection = new Vector3(0, 0, -1);
            }
            else if (sideOnRunning == SideOnRunning.Side3)
            {
                LeftRightDirection = new Vector3(1, 0, 0);
            }
            else if (sideOnRunning == SideOnRunning.Side4)
            {
                LeftRightDirection = new Vector3(0, 0, 1);
            }

            if (standPosition == Position.Middle)
            {
                anim.SetTrigger("isTurningLeft");
                standPosition = Position.Left;
                fromPosition = Position.Middle;
            }
            else if (standPosition == Position.Right)
            {
                anim.SetTrigger("isTurningLeft");
                standPosition = Position.Middle;
                fromPosition = Position.Right;
            }
        }
    }

    void MoveRight()
    {
        if (standPosition != Position.Right)
        {
            if (sideOnRunning == SideOnRunning.Side1)
            {
                LeftRightDirection = new Vector3(1, 0, 0);
            }
            else if (sideOnRunning == SideOnRunning.Side2)
            {
                LeftRightDirection = new Vector3(0, 0, 1);
            }
            else if (sideOnRunning == SideOnRunning.Side3)
            {
                LeftRightDirection = new Vector3(-1, 0, 0);
            }
            else if (sideOnRunning == SideOnRunning.Side4)
            {
                LeftRightDirection = new Vector3(0, 0, -1);
            }

            if (standPosition == Position.Middle)
            {
                anim.SetTrigger("isTurningRight");
                standPosition = Position.Right;
                fromPosition = Position.Middle;
            }
            else if (standPosition == Position.Left)
            {
                anim.SetTrigger("isTurningRight");
                standPosition = Position.Middle;
                fromPosition = Position.Left;
            }
        }
    }
    void MoveLeftRight()
    {
        if (inputDirection == InputDirection.Left)
        {
            MoveLeft();
        }
        else if (inputDirection == InputDirection.Right)
        {
            MoveRight();
        }
        if (sideOnRunning == SideOnRunning.Side1)
        {
            if (standPosition == Position.Left)
            {
                if (transform.position.x <= 554.0f)
                {
                    LeftRightDirection = Vector3.zero;
                    transform.position = new Vector3(554.0f, transform.position.y, transform.position.z);
                }
            }
            if (standPosition == Position.Middle)
            {
                if (fromPosition == Position.Left)
                {
                    if (transform.position.x > 574.0f)
                    {
                        LeftRightDirection = Vector3.zero;
                        transform.position = new Vector3(574.0f, transform.position.y, transform.position.z);
                    }
                }
                if (fromPosition == Position.Right)
                {
                    if (transform.position.x < 574.0f)
                    {
                        LeftRightDirection = Vector3.zero;
                        transform.position = new Vector3(574.0f, transform.position.y, transform.position.z);
                    }
                }
            }
            if (standPosition == Position.Right)
            {
                if (transform.position.x >= 594.0f)
                {
                    LeftRightDirection = Vector3.zero;
                    transform.position = new Vector3(594.0f, transform.position.y, transform.position.z);
                }
            }
        }
        else if (sideOnRunning == SideOnRunning.Side2)
        {
            if (standPosition == Position.Left)
            {
                if (transform.position.z <= 609.0f)
                {
                    LeftRightDirection = Vector3.zero;
                    transform.position = new Vector3(transform.position.x, transform.position.y, 609.0f);
                }
            }
            if (standPosition == Position.Middle)
            {
                if (fromPosition == Position.Left)
                {
                    if (transform.position.z > 629.0f)
                    {
                        LeftRightDirection = Vector3.zero;
                        transform.position = new Vector3(transform.position.x, transform.position.y, 629.0f);
                    }
                }
                if (fromPosition == Position.Right)
                {
                    if (transform.position.z < 629.0f)
                    {
                        LeftRightDirection = Vector3.zero;
                        transform.position = new Vector3(transform.position.x, transform.position.y, 629.0f);
                    }
                }
            }
            if (standPosition == Position.Right)
            {
                if (transform.position.z >= 649.0f)
                {
                    LeftRightDirection = Vector3.zero;
                    transform.position = new Vector3(transform.position.x, transform.position.y, 649.0f);
                }
            }
        }
        else if (sideOnRunning == SideOnRunning.Side3)
        {
            if (standPosition == Position.Left)
            {
                if (transform.position.x >= -547.0f)
                {
                    LeftRightDirection = Vector3.zero;
                    transform.position = new Vector3(-547.0f, transform.position.y, transform.position.z);
                }
            }
            if (standPosition == Position.Middle)
            {
                if (fromPosition == Position.Left)
                {
                    if (transform.position.x < -567.0f)
                    {
                        LeftRightDirection = Vector3.zero;
                        transform.position = new Vector3(-567.0f, transform.position.y, transform.position.z);
                    }
                }
                if (fromPosition == Position.Right)
                {
                    if (transform.position.x > -567.0f)
                    {
                        LeftRightDirection = Vector3.zero;
                        transform.position = new Vector3(-567.0f, transform.position.y, transform.position.z);
                    }
                }
            }
            if (standPosition == Position.Right)
            {
                if (transform.position.x <= -587.0f)
                {
                    LeftRightDirection = Vector3.zero;
                    transform.position = new Vector3(-587.0f, transform.position.y, transform.position.z);
                }
            }
        }
        else if (sideOnRunning == SideOnRunning.Side4)
        {
            if (standPosition == Position.Left)
            {
                if (transform.position.z >= -551.0f)
                {
                    LeftRightDirection = Vector3.zero;
                    transform.position = new Vector3(transform.position.x, transform.position.y, -551.0f);
                }
            }
            if (standPosition == Position.Middle)
            {
                if (fromPosition == Position.Left)
                {
                    if (transform.position.z < -571.0f)
                    {
                        LeftRightDirection = Vector3.zero;
                        transform.position = new Vector3(transform.position.x, transform.position.y, -571.0f);
                    }
                }
                if (fromPosition == Position.Right)
                {
                    if (transform.position.z > -571.0f)
                    {
                        LeftRightDirection = Vector3.zero;
                        transform.position = new Vector3(transform.position.x, transform.position.y, -571.0f);
                    }
                }
            }
            if (standPosition == Position.Right)
            {
                if (transform.position.z <= -591.0f)
                {
                    LeftRightDirection = Vector3.zero;
                    transform.position = new Vector3(transform.position.x, transform.position.y, -591.0f);
                }
            }
        }
    }

    void UpdateSpeed(){
        speedAddCount += speed * Time.deltaTime;
        if(speedAddCount >= speedAddDistance){
            SetSpeed(speed+speedAddRate);
            speedAddCount = 0;
        }
    }

    void MoveForward()
    {
        //Debug.Log(transform.position);
        if (inputDirection == InputDirection.Down)
        {
            anim.SetTrigger("isRolling");
        }
        if (inputDirection == InputDirection.Up)
        {
            anim.SetTrigger("isJumping");
        }

        AnimatorStateInfo animatorInfo;
        animatorInfo = anim.GetCurrentAnimatorStateInfo(0);
        if ((animatorInfo.normalizedTime < 1.0f) && (animatorInfo.IsName("Jumping")))
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }

        if ((animatorInfo.normalizedTime < 1.0f) && (animatorInfo.IsName("Rolling")))
        {
            isRolling = true;
        }
        else
        {
            isRolling = false;
        }

        anim.SetBool("isRunning", true);

        //turn left if necessary
        if (sideOnRunning == SideOnRunning.Side1)
        {
            moveDirection.z = speed;
            moveDirection.y -= gravity * Time.deltaTime;

            if (transform.position.z >= 610.0f)
            {
                transform.localEulerAngles = new Vector3(0, -90, 0);
                sideOnRunning = SideOnRunning.Side2;
            }

            if (standPosition == Position.Middle)
            {
				Vector3 pos = transform.position;
				pos.x = Mathf.Lerp(pos.x, 574.0f, Time.deltaTime * 10);
				pos.y = Mathf.Lerp(pos.y, 91.66f, Time.deltaTime * 10);
				pos.z = transform.position.z;
				transform.position = pos;
                //transform.position = new Vector3(574.0f, transform.position.y, transform.position.z);
            }
            else if (standPosition == Position.Left)
            {
				Vector3 pos = transform.position;
				pos.x = Mathf.Lerp(pos.x, 554.0f, Time.deltaTime * 10);
				pos.y = Mathf.Lerp(pos.y, 91.66f, Time.deltaTime * 10);
				pos.z = transform.position.z;
                transform.position = pos;
                //transform.position = new Vector3(554.0f, transform.position.y, transform.position.z);
            }
            else if (standPosition == Position.Right)
            {
				Vector3 pos = transform.position;
				pos.x = Mathf.Lerp(pos.x, 594.0f, Time.deltaTime * 10);
				pos.y = Mathf.Lerp(pos.y, 91.66f, Time.deltaTime * 10);
				pos.z = transform.position.z;
                transform.position = pos;
                //transform.position = new Vector3(594.0f, transform.position.y, transform.position.z);
            }

        }
        else if (sideOnRunning == SideOnRunning.Side2)
        {
            moveDirection.x = -speed;

            if (transform.position.x <= -548.0f)
            {
                transform.localEulerAngles = new Vector3(0, -180, 0);
                sideOnRunning = SideOnRunning.Side3;
            }
            if (standPosition == Position.Middle)
            {
				Vector3 pos = transform.position;
                pos.x = transform.position.x;
				pos.y = Mathf.Lerp(pos.y, 91.66f, Time.deltaTime * 10);
				pos.z = Mathf.Lerp(pos.z, 629.0f, Time.deltaTime * 10);
				transform.position = pos;
                //transform.position = new Vector3(transform.position.x, transform.position.y, 629.0f);
            }
            else if (standPosition == Position.Left)
            {
				Vector3 pos = transform.position;
				pos.x = transform.position.x; 
                pos.y = Mathf.Lerp(pos.y, 91.66f, Time.deltaTime * 10);
				pos.z = Mathf.Lerp(pos.z, 609.0f, Time.deltaTime * 10);
				transform.position = pos;
                //transform.position = new Vector3(transform.position.x, transform.position.y, 609.0f);
            }
            else if (standPosition == Position.Right)
            {
				Vector3 pos = transform.position;
				pos.x = transform.position.x;
				pos.y = Mathf.Lerp(pos.y, 91.66f, Time.deltaTime * 10);
				pos.z = Mathf.Lerp(pos.z, 649.0f, Time.deltaTime * 10);
				transform.position = pos;
                //transform.position = new Vector3(transform.position.x, transform.position.y, 649.0f);
            }
        }
        else if (sideOnRunning == SideOnRunning.Side3)
        {
            moveDirection.z = -speed;

            if (transform.position.z <= -552.0f)
            {
                transform.localEulerAngles = new Vector3(0, 90, 0);
                sideOnRunning = SideOnRunning.Side4;
            }
            if (standPosition == Position.Middle)
            {
                Vector3 pos = transform.position;
				pos.x = Mathf.Lerp(pos.x, -567.0f, Time.deltaTime * 10);
                pos.y = Mathf.Lerp(pos.y, 91.66f, Time.deltaTime * 10);
                pos.z = transform.position.z;
                transform.position = pos;
				//pos.z = Mathf.Lerp(pos.z, target.transform.position.z - distance, Time.deltaTime * 50);

				//transform.position = new Vector3(-567.0f, transform.position.y, transform.position.z);
			}
            else if (standPosition == Position.Left)
            {
				Vector3 pos = transform.position;
				pos.x = Mathf.Lerp(pos.x, -547.0f, Time.deltaTime * 10);
				pos.y = Mathf.Lerp(pos.y, 91.66f, Time.deltaTime * 10);
				pos.z = transform.position.z;
                transform.position = pos;
				//transform.position = new Vector3(-547.0f, transform.position.y, transform.position.z);
			}
            else if (standPosition == Position.Right)
            {
				Vector3 pos = transform.position;
				pos.x = Mathf.Lerp(pos.x, -587.0f, Time.deltaTime * 10);
				pos.y = Mathf.Lerp(pos.y, 91.66f, Time.deltaTime * 10);
				pos.z = transform.position.z;
                transform.position = pos;
                //transform.position = new Vector3(-587.0f, transform.position.y, transform.position.z);
			}
        }
        else if (sideOnRunning == SideOnRunning.Side4)
        {
            moveDirection.x = speed;

            if (transform.position.x >= 555.0f)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
                sideOnRunning = SideOnRunning.Side1;
            }
            if (standPosition == Position.Middle)
            {
				Vector3 pos = transform.position;
				pos.x = transform.position.x;
				pos.y = Mathf.Lerp(pos.y, 91.66f, Time.deltaTime * 10);
				pos.z = Mathf.Lerp(pos.z, -571.0f, Time.deltaTime * 10);
				transform.position = pos;
                //transform.position = new Vector3(transform.position.x, transform.position.y, -571.0f);
            }
            else if (standPosition == Position.Left)
            {
				Vector3 pos = transform.position;
				pos.x = transform.position.x;
				pos.y = Mathf.Lerp(pos.y, 91.66f, Time.deltaTime * 10);
				pos.z = Mathf.Lerp(pos.z, -551.0f, Time.deltaTime * 10);
				transform.position = pos;
                //transform.position = new Vector3(transform.position.x, transform.position.y, -551.0f);
            }
            else if (standPosition == Position.Right)
            {
				Vector3 pos = transform.position;
				pos.x = transform.position.x;
				pos.y = Mathf.Lerp(pos.y, 91.66f, Time.deltaTime * 10);
				pos.z = Mathf.Lerp(pos.z, -591.0f, Time.deltaTime * 10);
				transform.position = pos;
                //transform.position = new Vector3(transform.position.x, transform.position.y, -591.0f);
            }
        }
        characterController.Move((LeftRightDirection * 50 + moveDirection) * Time.deltaTime);

    }

    public void QuickMove()
    {
        //Debug.Log("QuickMove");
        if (QuickMoveCor != null)
        {
            StopCoroutine(QuickMoveCor);
        }
        QuickMoveCor = QuickMoveCoroutine();
        StartCoroutine(QuickMoveCor);
    }

    IEnumerator QuickMoveCoroutine()
    {
        quickMoveTimeLeft = quickMoveDuration;
        if (!isQuickMoving)
        {
            saveSpeed = speed;
        }
        speed = 100;
        isQuickMoving = true;
        //yield return new WaitForSeconds(quickMoveDuration);
        while (quickMoveTimeLeft >= 0)
        {
            if (CanPlay())
            {
                quickMoveTimeLeft -= Time.deltaTime;
            }
            yield return null;
        }
        speed = saveSpeed;
        isQuickMoving = false;
    }

    public void UseMagnet()
    {
        if (MagnetCor != null)
        {
            StopCoroutine(MagnetCor);
        }
        MagnetCor = MagnetCoroutine();
        StartCoroutine(MagnetCor);
    }

    IEnumerator MagnetCoroutine()
    {
        MagnetCollider.SetActive(true);
        magnetTimeLeft = magnetDuration;
        while (magnetTimeLeft >= 0)
        {
            if (CanPlay())
            {
                magnetTimeLeft -= Time.deltaTime;
            }
            yield return null;
        }
        MagnetCollider.SetActive(false);
    }

    public void Multiply()
    {
        if (MultiplyCor != null)
        {
            StopCoroutine(MultiplyCor);
        }
        MultiplyCor = MultiplyCoroutine();
        StartCoroutine(MultiplyCor);
    }

    IEnumerator MultiplyCoroutine()
    {
        multiplyTimeLeft = multiplyDuration;
        GameAttribute.instance.multiply = 2;
        while (multiplyTimeLeft >= 0)
        {
            if (CanPlay())
            {
                multiplyTimeLeft -= Time.deltaTime;
            }
            yield return null;
        }
        GameAttribute.instance.multiply = 1;
    }

    private bool CanPlay()
    {
        return !GameController.instance.isPause && GameController.instance.isPlay;
    }

    void GetInputDirection()
    {
        inputDirection = InputDirection.NULL;
        if (Input.GetMouseButtonDown(0))
        {
            activeInput = true;
            mousePos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0) && activeInput)
        {
            Vector3 vec = Input.mousePosition - mousePos;
            if (vec.magnitude > 20)
            {
                var angleY = Mathf.Acos(Vector3.Dot(vec.normalized, Vector2.up)) * Mathf.Rad2Deg;
                var angleX = Mathf.Acos(Vector3.Dot(vec.normalized, Vector2.right)) * Mathf.Rad2Deg;
                //Debug.Log(angle);
                if (angleY <= 45)
                {
                    inputDirection = InputDirection.Up;
                    AudioManager.instance.PlaySlideAudio();
                }
                else if (angleY >= 135)
                {
                    inputDirection = InputDirection.Down;
                    AudioManager.instance.PlaySlideAudio();
                }
                if (angleX <= 45)
                {
                    inputDirection = InputDirection.Right;
                    AudioManager.instance.PlaySlideAudio();
                }
                else if (angleX >= 135)
                {
                    inputDirection = InputDirection.Left;
                    AudioManager.instance.PlaySlideAudio();
                }
                activeInput = false;
                //Debug.Log(inputDirection);
            }

        }
    }

    public void Reset()
    {
        Debug.Log("reset");
        speed = init_speed = 1;
        inputDirection = InputDirection.NULL;
        activeInput = false;

        //left & right
        standPosition = Position.Middle;
        LeftRightDirection = Vector3.zero;
        moveDirection = Vector3.zero;
        //sideOnRunning
        sideOnRunning = SideOnRunning.Side1;
        transform.localEulerAngles = new Vector3(0, 0, 0);
        //jump & roll
        isJumping = false;
        isRolling = false;
        //speed up
        quickMoveTimeLeft = 0;
        isQuickMoving = false;
        //magnet
        magnetTimeLeft =0;
        //multiply
        multiplyTimeLeft = 0;
        //add speed
        speedAddCount = 0;
        //life
        life = 1;

        gameObject.transform.position = new Vector3(574f, 91.66f, -560f);
        Camera.main.transform.position = new Vector3(574f, 156.16f, -525.5f);
        anim.SetBool("isRunning", true);
        RoadSetter.instance.RemoveItem(RoadSetter.instance.Side1);
        RoadSetter.instance.RemoveItem(RoadSetter.instance.Side2);
        RoadSetter.instance.RemoveItem(RoadSetter.instance.Side3);
        RoadSetter.instance.RemoveItem(RoadSetter.instance.Side4);
        RoadSetter.instance.AddItem(RoadSetter.instance.Side1);

}

    // Update is called once per frame
    void Update()
    {
        life = GameAttribute.instance.life;
        isPlay = GameController.instance.isPlay;
        isPause = GameController.instance.isPause;
        UpdateItemTime();

	}
    private void UpdateItemTime(){
        TextStar.text = GetTime(quickMoveTimeLeft);
        TextMagnet.text = GetTime(magnetTimeLeft);
        TextMultiply.text = GetTime(multiplyTimeLeft);
    }

    private string GetTime(float time){
        if(time <= 0){
            return "0";
        }
        //return Mathf.RoundToInt(time).ToString();
        return ((int)time + 1).ToString();
    }
}

public enum InputDirection
{
    NULL,
    Left,
    Right,
    Up,
    Down
}

public enum Position
{
    Left,
    Middle,
    Right
}

public enum SideOnRunning{
    Side1,
    Side2,
    Side3,
    Side4
}