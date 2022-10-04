using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pingpong : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;
    private float minX = 0;
    [SerializeField]
    private float maxX = 15;

    private Rigidbody2D myBody;
    private SpriteRenderer sr;
    private void Awake(){
        myBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        minX = transform.position.x;
    }
    void Start()
    {
        sr.flipX = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed, 0f, 0f) * Time.deltaTime;
        if (transform.position.x >= maxX){
            sr.flipX = true;
            speed = -speed;
        }
        else if (transform.position.x <= minX){
            sr.flipX = false;
            speed = -speed;
        }
    }
}
