using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class bonus : MonoBehaviour
{
    private GameObject letter;

    [SerializeField]
    private float velocity = 1f;

    [SerializeField]
    private GameObject info;

    private Animator anim;
    private SpriteRenderer sr;

    [HideInInspector]
    public bool hasTrigger = false;
    private float fade = 3;

    int count = 0;

    Vector3 offset;

    void Awake(){
        
    }
    void Start()
    {
        letter = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();     
        offset = new Vector3(Random.Range(-1.5f,1.5f), Random.Range(-1.5f,1.5f), Random.Range(-1.5f,1.5f));
    }

    // Update is called once per frame
    void Update()
    {
        // Animation for player
        if (letter == null){
            Destroy(gameObject);
        }
        if (hasTrigger){
            if (count > 360){
                offset = new Vector3(Random.Range(-1.5f,1.5f), Random.Range(-1.5f,1.5f), Random.Range(-1.5f,1.5f));
                count = 0;
            }
            count++;
            transform.position = Vector3.Lerp(transform.position, letter.transform.position + offset, fade * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (hasTrigger){
            return;
        }
        if (other.CompareTag("Player")){
            // 向玩家移动，在到达之前不会消失，透明度下降
            hasTrigger = true;
            if (info == null){
                info = GameObject.FindWithTag("popE");
            }
            info.SetActive(true);
            Time.timeScale = 0f;
            other.GetComponent<Player>().rebirth = transform.position;
            anim.SetBool("Dying", true);
        }
    }

}
