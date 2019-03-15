using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public Transform[] startingRooms;// 起始房間"位置"
    public GameObject[] beginRoom; // 起始"房間"(放置玩家生成傳送門)
    public GameObject[] rooms;// 一般房間，index表示房間種類(LR為左右開口型房間)。index 0 --> LR, index 1 --> LRB, index 2 --> LRT, index 3 --> LRTB
    public GameObject[] endRoom;//最後一個房間。index 0~5

    private int direction;// 房間生成的方向。
    public int moveAmount;// 生成距離間隔。

    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    public LayerMask room;//限制偵測類型用，意指只偵測為"Room"layer且有box collider的物件。
    private int downCounter  ;//記錄往下生成房間的次數器

    public float minX;//X軸最小值
    public float maxX;//X軸最大值
    public float minY;//Y軸最小值
    public bool stopGen;//停止產生器的布林值
    

    private void Start()
    {
        // 起始房間位置陣列：從地圖最頂部的四間房間，隨機選取其中一個"位置"(StartRoomPos)
        int randStartingRoom = Random.Range(0, startingRooms.Length);
        transform.position = startingRooms[randStartingRoom].position;
        // 在選取到的位置上生成房間
        int randBeginRoom = Random.Range(0, beginRoom.Length);
        //生成"起始房間"
        Instantiate(beginRoom[randBeginRoom], transform.position,Quaternion.identity);
        //隨機給個生成房間的"方向"
        direction = Random.Range(1, 6);
    }

    private void Update()
    {
        if (timeBtwRoom <= 0 && stopGen == false)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    private void Move()
    {
        
            if (direction == 1 || direction == 2)// 往右邊生成房間 !!

            {
                if (transform.position.x < maxX)//小於最大X值是為了"在固定的寬度內生成房間"
                {
                    downCounter = 0;//往下紀錄器歸零
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                    transform.position = newPos;

                    int rand = Random.Range(0, rooms.Length);
                    Instantiate(rooms[rand], transform.position, Quaternion.identity);


                    //尋找新的方向產生房間。
                    direction = Random.Range(1, 6);
                    if (direction == 3)// 如果要往左(往回)，那就等於2繼續往右吧！！
                    {
                        direction = 1;
                    }
                    else if (direction == 4)// 如果要往左(往回)，那就等於5往下吧！！
                    {
                        direction = 5;
                    }
                }
                else
                {
                    direction = 5;
                }
            }


            else if (direction == 3 || direction == 4)// Move Left !!
            {

                if (transform.position.x > minX)
                {
                    downCounter = 0;//往下紀錄器歸零
                    Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                    transform.position = newPos;

                    int rand = Random.Range(0, rooms.Length);
                    Instantiate(rooms[rand], transform.position, Quaternion.identity);
                //              陣列          位置                 防止旋轉

                    //尋找新的方向產生房間，繼續往左或往下。
                    direction = Random.Range(3, 6);
                }
                else
                {
                    direction = 5;
                }
            }
            else if (direction == 5)// 往下生成房間 !!
            {
                downCounter++;

                if (transform.position.y > minY)
                {
                    //製造一個偵測器去偵測:當房間往下生成時，會確保往下生成前會有一個LRB或LRTB(下方有開口的房間存在)。
                    Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                    
                    //index 0 --> LR, index 1 --> LRB, index 2 --> LRT, index 3 --> LRTB

                    //如果上方不是 index 1 & index 3 的房間，毀掉那個房間，再生成index 1 或是 index 3的房間。
                    if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
                    {
                        if (downCounter >= 2) //如果房間連續往下生成2次...
                        {
                            //摧毀房間...
                            roomDetection.GetComponent<RoomType>().RoomDestruction();
                            //生成index 3 房間(四開口)
                            Instantiate(rooms[3], transform.position, Quaternion.identity);
                        }
                        else
                        {
                            //摧毀房間...
                            roomDetection.GetComponent<RoomType>().RoomDestruction();

                            int randBottomRoom = Random.Range(1, 4);
                            if (randBottomRoom == 2)//因為index 2沒有下開口
                            {
                            randBottomRoom = 1;
                            }
                            Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                        }
                    }


                    Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                    transform.position = newPos;

                    int rand = Random.Range(2, 4);
                    Instantiate(rooms[rand], transform.position, Quaternion.identity);


                    direction = Random.Range(1, 6);
                }
                else
                {
                    //Stop the LevelGen!!
                    //在生成完房間後，砍掉最後一間房間，再從endRoom陣列抽出一個房間作為最後一間房間(有傳送門，玩家才能繼續接關)
                    Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);//偵測最後一間房間
                    roomDetection.GetComponent<RoomType>().RoomDestruction();//摧毀
                Destroy(roomDetection);  
                int randEndRoom = Random.Range(0, endRoom.Length);//宣告
                    Instantiate(endRoom[randEndRoom], transform.position, Quaternion.identity);//生成最後一間房間
                    stopGen = true;//停止生成器繼續動作。
                    

            }
            }
        

            
            
    }

}

