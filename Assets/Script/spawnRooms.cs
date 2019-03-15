using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnRooms : MonoBehaviour {

	
    public LayerMask whatIsRoom; // 偵測是否有房間在。
    public LevelGenerator LevelGen; // 呼叫LevelGeneration 腳本。
    public GameObject[] hideRoom;//"隱藏房間"
    
        
    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);//偵測
        //如果沒偵測到有房間(表示該處沒有房間)且地圖生成器已經停止
        if (roomDetection == null && LevelGen.stopGen == true)
        {
            int rand = Random.Range(0, hideRoom.Length);
            Instantiate(hideRoom[rand], transform.position, Quaternion.identity);//生成隱藏房間(等同填滿空缺的部分)
            Destroy(gameObject);//生成完房間一次，砍掉StartRoomPos(放這個腳本的gameObject).
            
        }
        



    }
}

