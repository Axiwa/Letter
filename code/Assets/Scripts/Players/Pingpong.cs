using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pingpong : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float minX = 0;
    [SerializeField]
    private float maxX = 15;

    private Rigidbody2D myBody;
    private SpriteRenderer sr;
    private Animator anim;

    Vector3 oldPosition;
    Vector3 newPosition;
    bool toright;

    private void Awake(){
        myBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        minX = transform.position.x;
        toright = true;
        if (minX > maxX){
            (minX, maxX) = (maxX, minX);
        }
    }
    void Start()
    {
        anim.SetBool("move", true);
        sr.flipX = true;
        oldPosition = transform.position;
        newPosition = new Vector3(maxX, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (toright){
            sr.flipX = true;
            transform.position += new Vector3(speed, 0f, 0f) * Time.deltaTime;
            if (transform.position.x >= maxX-0.1){
                anim.SetTrigger("turn");
                toright = false;
                
            }
        }
        else{
            sr.flipX = false;
            transform.position += new Vector3(-speed, 0f, 0f) * Time.deltaTime;
            if (transform.position.x <= minX+0.1){
                anim.SetTrigger("turn");
                toright = true;
            }
        }
    }
}
