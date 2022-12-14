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

    private float offset = 0.1f;
    int layerMask;

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
    public float diffh = 0.12f;

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
        layerMask = (LayerMask.GetMask("Ground"));
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
            // ??????g??????????????????????????????????????????
            anim.SetBool(WALK_ANIMATION, false);
            return;
        }

        // ??????????????????
        changeDir();


        if (letter.GetComponent<Player>().isGrounded && letter.transform.position.y >= transform.position.y + diffh){
            return;
        }  

        // ??????????????????
        if (beQuiet){
            return;
        }

        // ??????????????????

        // 0.1f is for tolerance
        if (Vector3.Distance(transform.position, letter.transform.position) < letter.GetComponent<Player>().connectDistance-0.3f){
            // The letter is running to the opposite direction of the girl
            if (!letter.GetComponent<SpriteRenderer>().flipX && transform.position.x < letter.transform.position.x ||
            letter.GetComponent<SpriteRenderer>().flipX && transform.position.x > letter.transform.position.x){
                PlayerMoveKeyboard();
                AnimatePlayer();
                PlayerJump();
            }
            // ???????????????????????????????????????????????????
            else{
                movementX = 0;
                AnimatePlayer();
            }
        }

        // ???????????????????????????
        else {
            if (letter)
                follow();
        }  

    }

    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal"); // GetAxis: not only -1 and 1
        RaycastHit2D hit;
        if (movementX < 0)
        {
            hit = checkRayCast(new Vector2(-1, 0));
        }
        else
        {
            hit = checkRayCast(new Vector2(1, 0));
        }
        if (hit && hit.collider.name != "stair")
        {
            Debug.Log(hit.distance);
            return;
        }
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }

    private void follow(){
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

        RaycastHit2D hit;
        if (movementX < 0)
        {
            hit = checkRayCast(new Vector2(-1, 0));
        }
        else
        {
            hit = checkRayCast(new Vector2(1, 0));
        }
        if (hit && hit.collider.name != "stair")
        {
            Debug.Log(hit.distance);
            return;
        }

        // The letter is waiting
        if (letter.GetComponent<Player>().linkedState == 1 && Vector3.Distance(transform.position, letter.transform.position) < 3f)
        {
            transform.position += new Vector3(moveForce * movementX, 0f, 0f) * Time.deltaTime;            
        }
        else if (Vector3.Distance(transform.position, letter.transform.position) < 2f){
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
                // ?????????G?????????????????????????????????????????????????????????
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

    void PlayerJump()
    {
        RaycastHit2D hit;
        hit = checkRayCast(new Vector2(0, 1));
        if (hit)
        {
            return;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    public void complain(){
        complaint.GetComponent<Text>().color = new Color(1, 1, 1, 1f);
    }

    private RaycastHit2D checkRayCast(Vector2 direction)
    {
        return Physics2D.BoxCast(transform.position, new Vector2(0.3f, 0.2f), 0f, direction, offset, layerMask); // 3 
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
