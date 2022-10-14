using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stair0_0 : Stair
{
    [SerializeField]
    Vector3 start;
    [SerializeField]
    Vector3 target;
    [SerializeField]
    float speed = 1.5f;

    private bool begin = false;

    void Awake(){
        start = transform.position;
        lineInfo.moveInfo += Move;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update(){
        if (begin){
            transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
        }
    }

    public override void Move() {
        begin = true;
    }

}
