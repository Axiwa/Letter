using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [HideInInspector]
    public float speed;

    private Rigidbody2D myBody;

    void Awake(){
        myBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        myBody.velocity = new Vector2(speed, myBody.velocity.y);
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Now the monster speed is: " + speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
