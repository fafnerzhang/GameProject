using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour {

    public GameObject[] objects;

    private void Start()
    {
        // "生成物件用陣列"，寫下這兩段就可以放進你要隨機產生的物件在你放置的spawn point 位置了。

        int rand = Random.Range(0, objects.Length);
        GameObject instance = (GameObject)Instantiate(objects[rand], transform.position, Quaternion.identity);
        //    child                   parent
        instance.transform.parent = transform;
    }
}
