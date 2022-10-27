using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public GameObject girl;

    private GameObject toofar;

    /* ----- Related to girl -------------------------------- */
    public List<Vector3> positionList;
    int distance = 5;

    public float connectDistance = 0.6f;

    private float girlForce = 9f;

    [SerializeField]
    private float jumpForce = 20f;

    // linkedState: 0-linked; 1-waiting & girl is not quiet; 2-totally depart
    // [HideInInspector]
    public int linkedState = 0;

    [HideInInspector]
    public bool safe = false;

    [SerializeField]
    private float moveForce = 10f;

    /* ----------------- Move and Jump --------------------- */
    public bool isGrounded = true;

    [HideInInspector]
    public float movementX = 1f;

    private Rigidbody2D myBody;
    private Animator anim;
    [HideInInspector]
    public SpriteRenderer sr;

    private bool hasCollided = false;

    private float offset = 0.4f;

    int layerMask;

    int originJump = -1;

    int JumpFrame;

    /* ------------------------------------------------------- */

    private string WALK_ANIMATION = "Walk";
    private string GROUND_TAG = "Ground";
    private string ENEMY_TAG = "Enemy";
    private string TRICK_TAG = "Stair";


    /* ----------------- Bonus and UI --------------------- */

    [HideInInspector]
    public int bonus = 0;
    [HideInInspector]
    public int bigBonus = 0;
    private bool hasTrigger = false;

    [HideInInspector]
    public Vector3 rebirth;


    private void Awake() {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rebirth = new Vector3(0, 0, 233);
        layerMask = (LayerMask.GetMask("Ground"));
        JumpFrame = originJump;
    }

    void Start()
    {
        if (girl == null) {
            girl = GameObject.FindWithTag("Girl");
        }
        if (girl) {
            girlForce = girl.GetComponent<girl>().jumpForce;
            linkedState = 0;
        }
        else {
            linkedState = 2;
        }
        positionList.Add(transform.position);

        foreach (Transform child in transform)
        {
            GameObject stair = child.gameObject;
            if (stair.CompareTag("text_effect"))
                toofar = stair;
        }
    }

    private void FixedUpdate() {
        if (JumpFrame >= 0)
        {
            JumpFrame--;
        }
        PlayerJump();

        // FOR GIRL TO FOLLOW
        positionList.Add(transform.position);
        if (positionList.Count > distance) {
            positionList.RemoveAt(0);
        }
    }


    void Update() {
        if (girl == null) {
            // Debug.Log("THE GIRL IS DEAD. YOU LOST!!");
            // Destroy(gameObject);
            PlayerMoveKeyboard();
            AnimatePlayer();
            PlayerJump();
            return;
        }

        // Determine the state when press R
        if (Input.GetKeyDown(KeyCode.R)) {
            // If I am inside the safe zone, girl's @beQuiet is flipped
            if (safe && Vector3.Distance(transform.position, girl.transform.position) < 3f) {
                girl.GetComponent<girl>().beQuiet = !girl.GetComponent<girl>().beQuiet;

                // If the girl can follow me now, I am waiting for her to be close enough
                if (!girl.GetComponent<girl>().beQuiet) {
                    linkedState = 1;
                }
                else {
                    linkedState = 2;
                }

                // No matter what, she is not inside now
                girl.GetComponent<girl>().inside = false;
            }
            // The girl will complain it is too dark
            else {
                girl.GetComponent<girl>().complain();
            }
        }

        // If the girl is not @beQuiet and we are close enough, we are automatically linked
        if (!girl.GetComponent<girl>().beQuiet &&
            Vector3.Distance(transform.position, girl.transform.position) <= connectDistance) {
            girl.GetComponent<girl>().inside = true;
            linkedState = 0;
        }

        PlayerMoveKeyboard();
        AnimatePlayer();
        PlayerJump();
    }

    void PlayerMoveKeyboard() {
        movementX = Input.GetAxisRaw("Horizontal"); // GetAxis: not only -1 and 1
        RaycastHit2D hit;
        if (movementX < 0) {
            hit = checkRayCast(new Vector2(-1, 0));
        }
        else
        {
            if (girl.transform.position.x < transform.position.x - 50f)
            {
                toofar.SetActive(true);
                return;
            }
            hit = checkRayCast(new Vector2(1, 0));
        }
        if (hit &&  hit.collider.name != "stair")
        {
            return;
        }
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }

    void AnimatePlayer() {
        if (movementX > 0) {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = false;
        }
        else if (movementX < 0) {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = true;
        }
        else {
            anim.SetBool(WALK_ANIMATION, false);
        }
    }

    void PlayerJump() {
        RaycastHit2D hit;
        hit = checkRayCast(new Vector2(0, 1));
        if (hit)
        {
            return;
        }
        if (Input.GetButtonDown("Jump")  && isGrounded){
            isGrounded = false;
            JumpFrame = originJump;
            if (linkedState != 2)
                myBody.AddForce(new Vector2(0f, girlForce), ForceMode2D.Impulse);
            else
                myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);          
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag(GROUND_TAG) || other.gameObject.CompareTag(TRICK_TAG)){
            isGrounded = true;
        }

        if (hasCollided){
            return;
        }
        hasCollided = true;

        if (other.gameObject.CompareTag(ENEMY_TAG)){
            var player = GetComponent<Collider2D>();
            var extents = player.bounds.extents.y;
            var pos = transform.position.y - extents;
            isGrounded = true;

            if (pos >= other.collider.bounds.center.y+0.1){            
                // PlayerJump();                                   
                myBody.AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
                anim.SetTrigger("jump");
                Destroy(other.gameObject);
            }
            else{
                re();      
                return;    
            }
        }

        if (other.gameObject.CompareTag("Boss")){
            var player = GetComponent<Collider2D>();
            var extents = player.bounds.extents.y;
            var pos = transform.position.y - extents;
            isGrounded = true;

            if (pos >= other.collider.bounds.center.y+1){
                if (transform.position.x < other.transform.position.x){
                    myBody.AddForce(new Vector2(-15f, 3f), ForceMode2D.Impulse);
                }  
                else{
                    myBody.AddForce(new Vector2(15f, 3f), ForceMode2D.Impulse);
                }
                other.gameObject.GetComponent<Boss>().health--;
                other.gameObject.GetComponent<Boss>().AnimatePlayer();
            }
            else if (pos >= other.collider.bounds.center.y)
            {
                if (transform.position.x < other.transform.position.x)
                {
                    myBody.AddForce(new Vector2(-10f, 3f), ForceMode2D.Impulse);
                }
                else
                {
                    myBody.AddForce(new Vector2(10f, 3f), ForceMode2D.Impulse);
                }
            }
            else{
                re();      
                return;    
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (hasTrigger){
            return;
        }
        hasTrigger = true;

        if (other.CompareTag(ENEMY_TAG)){
            var player = GetComponent<Collider2D>();
            var extents = player.bounds.extents.y;
            var pos = transform.position.y - extents;

            if (pos >= other.bounds.center.y + 0.1){
                Destroy(other.gameObject);
                myBody.AddForce(new Vector2(0f, 3f), ForceMode2D.Impulse);
            }
            else{
                re();
                return;
            }
        }
        
        if (other.gameObject.CompareTag("debris") && !other.GetComponent<bonus>().hasTrigger && linkedState == 0){
            Debug.Log("Happy!");
            bonus++;
        }
        if (other.gameObject.CompareTag("leucocyte")){
            Debug.Log("Very Happy!");
            bigBonus++;
        }
    }

    public void re(){
        // anim.SetTrigger("hurt"); 
        if (rebirth.z < 200){
            transform.position = rebirth;
            girl.transform.position = rebirth;
            girl.GetComponent<girl>().inside = true;
            girl.GetComponent<girl>().beQuiet = false;
            isGrounded = true;
            girl.GetComponent<girl>().isGrounded = true;
        }
        else{
            isGrounded = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private RaycastHit2D checkRayCast(Vector2 direction)
    {
        return Physics2D.BoxCast(transform.position, new Vector2(0.5f, 0.3f), 0f, direction, offset, layerMask); // 3 
    }

    private void LateUpdate() {
        hasCollided = false;
        hasTrigger = false;
    }
};
