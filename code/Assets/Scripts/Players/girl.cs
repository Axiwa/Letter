using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girl : MonoBehaviour
{
    private GameObject _letter;
    public GameObject letter{
        get {return _letter;}
        set {_letter = value;}
    }

    [SerializeField]
    private float moveForce = 6f;
    [SerializeField]
    private float runForce = 6f;

    [SerializeField]
    private float jumpForce = 1f;

    // [HideInInspector]
    public bool isGrounded = true;

    private Rigidbody2D myBody;
    private Animator anim;

    private string WALK_ANIMATION = "Walk";
    private string GROUND_TAG = "Ground";
    private string ENEMY_TAG = "Enemy";
    private string TRICK_TAG = "Stair";

    private SpriteRenderer sr;
    private float movementX = 1f;

    // [HideInInspector]
    public bool beQuiet;
    // [HideInInspector]
    public bool inside;

    private bool hasCollided = false;

    void Awake(){
        beQuiet = false;
        inside = false;
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        if (letter)
            transform.position = letter.transform.position;
    }

    private void FixedUpdate() {
        if (inside)
            PlayerJump();
    }

    // Update is called once per frame
    void Update()
    {
        if (!letter){
            // 主角g了，小女孩哭泣动画，结束游戏
            Destroy(gameObject);
            return;
        }

        // 方向朝着主角
        changeDir();

        // 小女孩不要动
        if (beQuiet){
            return;
        }

        // 小女孩可以动
        // 足够近，小女孩完全同步，不能跳跃，速度没有主角高
        // 如果主角正在向小女孩走，小女孩不动
        if (Vector3.Distance(transform.position, letter.transform.position) < 1f){
            if (!letter.GetComponent<SpriteRenderer>().flipX && transform.position.x < letter.transform.position.x ||
            letter.GetComponent<SpriteRenderer>().flipX && transform.position.x > letter.transform.position.x){
                PlayerMoveKeyboard();
                AnimatePlayer();
                PlayerJump();
            }
            else{
                movementX = 0;
                AnimatePlayer();
            }
        }

        // 小女孩跑起来跟着信
        else {
            if (letter)
                follow();
        }      

    }

    void PlayerMoveKeyboard(){
        movementX = Input.GetAxisRaw("Horizontal"); // GetAxis: not only -1 and 1
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }

    private void follow(){
        movementX = 1;
        if ((letter.transform.position.x - transform.position.x) < 0){
            movementX = -1;
        }
        else if ((letter.transform.position.x - transform.position.x) == 0)
        {
            movementX = 0;
        }
        // myBody.AddForce(new Vector2(movementX, 0f), ForceMode2D.Impulse);

        transform.position += new Vector3(runForce * movementX, 0f, 0f) * Time.deltaTime;
        AnimatePlayer();
    }


    private void LateUpdate() {
        if (beQuiet){
            movementX = 0;
        }
        AnimatePlayer();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag(GROUND_TAG)){
            isGrounded = true;
        }
        if (other.gameObject.CompareTag("Edge")){
            beQuiet = true;
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
                // 小女孩G了，信的光芒逐渐减弱，信消失，游戏结束
                Destroy(gameObject);
                Debug.Log("YOU LOST!!! ");
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Edge")){
            beQuiet = true;
            inside = false;
            letter.GetComponent<Player>().waiting = true;
            anim.SetBool(WALK_ANIMATION, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
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
                Debug.Log("YOU LOST!!! ");
            }
        }
    }

    private void changeDir(){
        if (inside){
            sr.flipX = letter.GetComponent<Player>().sr.flipX;
            return;
        }
        if (transform.position.x - letter.transform.position.x > 0){
            sr.flipX = true;
        }
        else{
            sr.flipX = false;
        }
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
            Debug.Log("why!!" + jumpForce);
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
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
