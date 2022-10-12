using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    private Transform player;
    private Vector3 tempPos;

    [SerializeField]
    private float minX = -100, maxX = 200;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindWithTag("MainCamera")){
            player = GameObject.FindWithTag("MainCamera").transform;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!player){
            return;
        }
        
        tempPos = transform.position;
        tempPos.x = 0.8f * player.position.x - 7f;

        if (tempPos.x < minX){
            tempPos.x = minX;
        }
        if (tempPos.x > maxX){
            tempPos.x = maxX;
        }

        transform.position = tempPos;
    }
}
