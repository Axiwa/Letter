using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class girl : MonoBehaviour
{
    public GameObject letter;

    /* ----------------- Move and Jump --------------------- */

    [SerializeField]
    private float moveForce = 5f;

    public float jumpForce = 8f;

    private float movementX = 1f;

    // [HideInInspector]
    public bool isGrounded = true;

    private bool hasCollided = false;

    private Rigidbody2D myBody;
    private Animator anim;
    private SpriteRenderer sr;

    /* ------------------------------------------------------- */
    private string WALK_ANIMATION = "Walk";
    private string GROUND_TAG = "Ground";
    private string ENEMY_TAG = "Enemy";
    private string TRICK_TAG = "Stair";

    
    /* ----- Related to letter -------------------------------- */

    [SerializeField]
    private float runForce = 10f; // the speed for chasing letter

    [HideInInspector]
    public bool beQuiet; // stay inside the light

    [HideInInspector]
    public bool inside; // inmitating the letter

    [HideInInspector]
    public float diffh = 0.2f;

    [HideInInspector]
    public bool safe = false; // determine if I am inside the light

    [SerializeField]
    private GameObject complaint;

    void Awake(){
        beQuiet = false;
        inside = true;
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    
    void Start()
    {
        if (letter)
            transform.position = letter.transform.position;
        if (complaint == null){
            complaint = GameObject.FindWithTag("complaint");
        }
    }

    private void FixedUpdate() {
        if (!beQuiet && letter.GetComponent<Player>().isGrounded && letter.transform.position.y >= transform.position.y + diffh){
            return;
        }    
        if (inside)
            PlayerJump();
    }

    // Update is called once per frame
    void Update()
    {
        if (!letter){
            // 主角g了，小女孩哭泣动画，结束游戏
            anim.SetTrigger("cry"); 
            return;
        }

        // 方向朝着主角
        changeDir();


        if (letter.GetComponent<Player>().isGrounded && letter.transform.position.y >= transform.position.y + diffh){
            return;
        }  

        // 小女孩不要动
        if (beQuiet){
            return;
        }

        // 小女孩可以动

        // 0.1f is for tolerance
        if (Vector3.Distance(transform.position, letter.transform.position) < letter.GetComponent<Player>().connectDistance-0.3f){
            // The letter is running to the opposite direction of the girl
            if (!letter.GetComponent<SpriteRenderer>().flipX && transform.position.x < letter.transform.position.x ||
            letter.GetComponent<SpriteRenderer>().flipX && transform.position.x > letter.transform.position.x){
                PlayerMoveKeyboard();
                AnimatePlayer();
                PlayerJump();
            }
            // 如果主角正在向小女孩走，小女孩不动
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
        Debug.Log(letter.transform.position.y - transform.position.y);
        Debug.Log(letter.GetComponent<Player>().isGrounded);
        if (letter.GetComponent<Player>().isGrounded && letter.transform.position.y >= transform.position.y + diffh){
            return;
        }  

        movementX = 1;
        if ((letter.transform.position.x - transform.position.x) < 0){
            movementX = -1;
        }
        else if ((letter.transform.position.x - transform.position.x) == 0)
        {
            movementX = 0;
        }
        AnimatePlayer();
        
        // The letter is waiting
        if (letter.GetComponent<Player>().linkedState == 1){
            transform.position += new Vector3(moveForce * movementX, 0f, 0f) * Time.deltaTime;            
        }
        else{
            Vector3 newPosition = new Vector3(letter.GetComponent<Player>().positionList[0].x, letter.GetComponent<Player>().positionList[0].y - 0.024f, letter.GetComponent<Player>().positionList[0].z);
            transform.position = Vector3.Lerp(transform.position, newPosition, 20 * Time.deltaTime);
        }

        
    }


    private void LateUpdate() {
        if (!beQuiet && letter.GetComponent<Player>().isGrounded && letter.transform.position.y >= transform.position.y + diffh){
            return;
        }   
        if (beQuiet){
            movementX = 0;
            AnimatePlayer();
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag(GROUND_TAG)){
            isGrounded = true;
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
                letter.GetComponent<Player>().re();
                return;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other) {

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
                letter.GetComponent<Player>().re();
                return;
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
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    public void complain(){
        complaint.GetComponent<Text>().color = new Color(0.5566f, 0.5566f, 0.5566f, 0.8f);
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
