using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemoent : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;
    private float horizontalMove = 0f ;
    bool jump = false;
    bool crouch = false;
    
    void Update()
    {
        //左右移動input
        horizontalMove = Input.GetAxisRaw("Horizontal")*runSpeed;
        //跳躍input
        if (Input.GetButtonDown("Jump")) 
        {
            jump = true;
            
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }
    //由fixedupdate處理物理運算
    void FixedUpdate()
    {
        //move our charcter!!
        controller.Move(horizontalMove*Time.fixedDeltaTime,crouch,jump);
        jump = false;
    }
}
