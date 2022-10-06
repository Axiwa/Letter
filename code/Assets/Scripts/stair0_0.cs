using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stair0_0 : MonoBehaviour
{
    [SerializeField]
    Vector3 start;
    [SerializeField]
    Vector3 target;
    [SerializeField]
    float speed = 5;

    void Awake(){
        start = transform.position;
        lineInfo.moveInfo += Move;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update(){
        
    }

    void Move(){
        transform.position = target;
    }
}
