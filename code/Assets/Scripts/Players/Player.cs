using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject _girl;
    public GameObject girl{
        get {return _girl;}
        set {_girl = value;}
    }

    [SerializeField]
    private float moveForce = 10f;

    [SerializeField]
    private float originJumpForce = 20f;

    [SerializeField]
    private float girlForce = 20f;

    private float jumpForce = 20f;

    [HideInInspector]
    public bool isGrounded = true;

    private Rigidbody2D myBody;
    private Animator anim;

    private string WALK_ANIMATION = "Walk";
    private string GROUND_TAG = "Ground";
    private string ENEMY_TAG = "Enemy";
    private string TRICK_TAG = "Stair";

    [HideInInspector]
    public SpriteRenderer sr;

    [HideInInspector]
    public float movementX = 22f;

    [HideInInspector]
    public int bonus = 0;
    [HideInInspector]
    public int bigBonus = 0;
    private bool hasCollided = false;

    [HideInInspector]
    public bool waiting = false;


    private void Awake(){
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        waiting = false;
    }

    private void FixedUpdate() {
        PlayerJump();
    }

    // Update is called once per frame
    void Update() {
        if (girl == null){
            // Debug.Log("THE GIRL IS DEAD. YOU LOST!!");
            // Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.R)){
            girl.GetComponent<girl>().beQuiet = !girl.GetComponent<girl>().beQuiet;
            if (girl.GetComponent<girl>().beQuiet){
                waiting = true;
            }
            girl.GetComponent<girl>().inside = false;
        } 

        if (!girl.GetComponent<girl>().beQuiet && Vector3.Distance(transform.position, girl.transform.position) < 1f && girl.GetComponent<girl>().inside == false){
            girl.GetComponent<girl>().inside = true;
            // 建立连接，可能有动画
            jumpForce = girlForce;
            waiting = false;
            // girl.GetComponent<girl>().GetComponent<Collider2D>().isTrigger = true; 
        }

        // else if (Vector3.Distance(transform.position, girl.transform.position) > 1f && girl.GetComponent<girl>().inside == true){
        //     // girl.GetComponent<girl>().GetComponent<Collider2D>().isTrigger = false;  
        //     girl.GetComponent<girl>().inside = false;
        //     waiting = true;
        //     jumpForce = originJumpForce;     
        // }

        PlayerMoveKeyboard();
        AnimatePlayer();
        PlayerJump();
    }

    void PlayerMoveKeyboard(){
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
            if (!waiting)
                myBody.AddForce(new Vector2(0f, girlForce), ForceMode2D.Impulse);
            else
                myBody.AddForce(new Vector2(0f, originJumpForce), ForceMode2D.Impulse);          
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (hasCollided){
            return;
        }
        hasCollided = true;
        if (other.gameObject.CompareTag(GROUND_TAG) || other.gameObject.CompareTag(TRICK_TAG)){
            isGrounded = true;
            if (girl && girl.GetComponent<girl>().inside == false)
                jumpForce = originJumpForce;
        }
        if (other.gameObject.CompareTag("Girl")){
            isGrounded = true;
            jumpForce = 8f;
        }
        if (other.gameObject.CompareTag(ENEMY_TAG)){
            var player = GetComponent<Collider2D>();
            var extents = player.bounds.extents.y;
            var pos = transform.position.y - extents;
            // Debug.Log(pos + " " + other.collider.bounds.center.y);

            if (pos >= other.collider.bounds.center.y-0.1){
                Destroy(other.gameObject);
                myBody.AddForce(new Vector2(0f, 3f), ForceMode2D.Impulse);
            }
            else{
                Destroy(gameObject);
            }
        }
        if (other.gameObject.CompareTag("debris")){
            // 信封动画
            bonus++;
            Debug.Log("Haha, bonus");
        }
        if (other.gameObject.CompareTag("leucocyte")){
            bigBonus++;
            Debug.Log("Haha, BIG bonus");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (hasCollided){
            return;
        }
        hasCollided = true;
        if (other.CompareTag(ENEMY_TAG)){
            var player = GetComponent<Collider2D>();
            var extents = player.bounds.extents.y;
            var pos = transform.position.y - extents;
            // Debug.Log(pos + " " + other.bounds.center.y);

            if (pos >= other.bounds.center.y-0.1){
                Destroy(other.gameObject);
                myBody.AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
            }
            else{
                Destroy(gameObject);
            }
        }
        if (other.gameObject.CompareTag("debris")){
            // 信封动画
            Debug.Log("Happy!");
            bonus++;
        }
        if (other.gameObject.CompareTag("leucocyte")){
            Debug.Log("Very Happy!");
            bigBonus++;
        }
    }

    private void LateUpdate() {
        hasCollided = false;
    }
}