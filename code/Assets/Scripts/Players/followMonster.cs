using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMonster : MonoBehaviour
{
    public float speed = 2f;

    private GameObject letter;
    private Animator anim;
    private Rigidbody2D myBody;

    public float scale = 0.8f;

    void Awake(){
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        letter = GameObject.FindWithTag("Player");
        speed = Random.Range(1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (!letter){
            return;
        }
        if (letter.transform.position.x - transform.position.x > 0){
            transform.localScale = new Vector3(scale, scale, scale);
        }
        else{
            transform.localScale = new Vector3(-scale, scale, scale);
        }

        if (Vector3.Distance(letter.transform.position, transform.position) < 5f & abs(letter.transform.position.y - transform.position.y) < 2f){
            anim.SetBool("Walk", true);
            follow();
        }
        else{
            anim.SetBool("Walk", false);
        }
    }

    void follow(){
        transform.position = Vector3.Lerp(transform.position, letter.transform.position, speed * Time.deltaTime); // new Vector3(runForce * movementX, 0f, 0f) * Time.deltaTime;
        // AnimatePlayer();        
    }

    float abs(float a){
        if (a > 0){
            return a;
        }
        else{
            return -a;
        }
    }
}
