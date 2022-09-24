using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 10f;
    [SerializeField]
    private float jumpForce = 11f;
    private Rigidbody2D myBody;
    private Animator anim;
    private string WALK_ANIMATION = "Walk";
    private bool isGrounded = true;
    private string GROUND_TAG = "Ground";
    private SpriteRenderer sr;
    private float movementX = 22f;


    private void Awake(){
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate() {
        PlayerJump();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMOveKeyboard();
        AnimatePlayer();
        PlayerJump();
    }

    void PlayerMOveKeyboard(){
        movementX = Input.GetAxisRaw("Horizontal"); // GetAxis: not only -1 and 1
        
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }

    void AnimatePlayer(){
        if (movementX > 0){
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = false;
        }
        else if (movementX < 0){
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = true;
        }
        else{
            anim.SetBool(WALK_ANIMATION, false);
        }
        
    }

    void PlayerJump(){
        if (Input.GetButtonDown("Jump") && isGrounded){ // GetButtonUp: once you leave the button // GetButton: you hold and it will continue to be triggered
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag(GROUND_TAG)){
            isGrounded = true;
        }
    }
}
