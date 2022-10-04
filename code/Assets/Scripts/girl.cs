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
    private float velocity = 1f;
    [SerializeField]
    private float jumpForce = 3f;
    private Rigidbody2D myBody;
    private Animator anim;
    private string WALK_ANIMATION = "Walk";
    private bool isGrounded = true;
    private string GROUND_TAG = "Ground";
    private string ENEMY_TAG = "Enemy";
    private SpriteRenderer sr;
    private float movementX = 22f;
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();     
    }

    // Update is called once per frame
    void Update()
    {
        if (!letter){
            Destroy(gameObject);
            return;
        }
        changeDir();
        if (abs(transform.position.x - letter.transform.position.x) > 2f && Vector3.Distance(transform.position, letter.transform.position) <= 10f){
            follow();
        }
        
    }

    private void LateUpdate() {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag(GROUND_TAG)){
            isGrounded = true;
        }  
        if (other.gameObject.CompareTag(ENEMY_TAG)){
            var player = GetComponent<Collider2D>();
            var extents = player.bounds.extents.y;
            var pos = transform.position.y - extents;
            Debug.Log(pos + " " + other.collider.bounds.center.y);

            if (pos >= other.collider.bounds.center.y-0.1){
                Destroy(other.gameObject);
                myBody.AddForce(new Vector2(0f, 3f), ForceMode2D.Impulse);
            }
            else{
                Destroy(gameObject);
                Debug.Log("YOU LOST!!! ");
            }
        }
    }

    private void changeDir(){
        if (transform.position.x - letter.transform.position.x > 0){
            sr.flipX = true;
        }
        else{
            sr.flipX = false;
        }
    }
    private void follow(){
        float movementX = (letter.transform.position.x - transform.position.x) * velocity;
        // myBody.AddForce(new Vector2(movementX, 0f), ForceMode2D.Impulse);
        if (isGrounded){
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);        
        }
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime;  
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(ENEMY_TAG)){
            var player = GetComponent<Collider2D>();
            var extents = player.bounds.extents.y;
            var pos = transform.position.y - extents;
            Debug.Log(pos + " " + other.bounds.center.y);

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

    float abs(float a){
        if (a > 0){
            return a;
        }
        else{
            return -a;
        }
    }
}
