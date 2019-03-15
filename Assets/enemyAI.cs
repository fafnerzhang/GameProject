using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour {

    public int speed = 5;
    public int attack = 10;
    public int enemyHp = 10;
    public float distant;

    private bool movingRight = true;

    public Transform groungDetection;
    public Transform wallDetection;

	// Update is called once per frame
	void Update () {

        transform.Translate(Vector2.right * speed * Time.deltaTime);
        //moving transform  right.

        RaycastHit2D groundInfo = Physics2D.Raycast(groungDetection.position, Vector2.down, distant);
        //偵測到沒有地板可以繼續走
        if (groundInfo.collider==false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
        //RaycastHit2D wallInfo = Physics2D.Raycast(wallDetection.position, Vector2.right, distant);
        //偵測到前面有東西
        //if (wallInfo.collider == true)
        //{
         //   if (movingRight == true)
          //  {
          //      transform.eulerAngles = new Vector3(0, -180, 0);
          //      movingRight = false;
        //    }
          //  else
          //  {
          //      transform.eulerAngles = new Vector3(0, 0, 0);
          //      movingRight = true;
          //  }
        //}
    }
}
