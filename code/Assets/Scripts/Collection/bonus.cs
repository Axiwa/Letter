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
    private float fade = 2;

    int count = 0;

    Vector3 offset;
    Vector3 offset2;

    void Awake(){
        
    }
    void Start()
    {
        letter = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();     
        offset = new Vector3(Random.Range(-1.5f,1.5f), Random.Range(-0.5f,1.5f), 0);
        offset2 = new Vector3(0, 0, Random.Range(-30f, 30f));
    }

    // Update is called once per frame
    void Update()
    {
        // Animation for player
        if (letter == null){
            Destroy(gameObject);
        }
        if (hasTrigger){
            if (count > 720){
                offset = new Vector3(Random.Range(-1.5f,1.5f), Random.Range(-0.5f,1.5f), 0);
                offset2 = new Vector3(0, 0, Random.Range(-30f, 30f));
                count = 0;
            }
            count++;
            transform.position = Vector3.Lerp(transform.position, letter.transform.position + offset, fade * Time.unscaledDeltaTime);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, letter.transform.eulerAngles + offset2, fade * Time.unscaledDeltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (hasTrigger){
            return;
        }
        if (other.CompareTag("Player") && other.gameObject.GetComponent<Player>().linkedState == 0){
            // 向玩家移动，在到达之前不会消失，透明度下降
            hasTrigger = true;
            if (info == null){
                foreach (Transform child in transform){ 
                    info = child.gameObject;
                    if (info.CompareTag("popE"))
                        info.SetActive(true);
                }
            }
            else{
                info.SetActive(true);
            }
            Time.timeScale = 0f;
            other.GetComponent<Player>().rebirth = transform.position;
            anim.SetBool("Dying", true);
        }
    }

}
