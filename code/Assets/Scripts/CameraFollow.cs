using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private Vector3 tempPos;

    [SerializeField]
    private float minX = -100, maxX = 200;
    private float minY = -100, maxY = 1000;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindWithTag("Player")){
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!player){
            return;
        }
        
        tempPos = transform.position;
        tempPos.x = player.position.x;
        tempPos.y = player.position.y+2;

        if (tempPos.x < minX){
            tempPos.x = minX;
        }
        if (tempPos.x > maxX){
            tempPos.x = maxX;
        }
        if (tempPos.y < minY){
            tempPos.y = minY;
        }
        if (tempPos.y > maxY){
            tempPos.y = maxY;
        }

        transform.position = tempPos;
    }
}
