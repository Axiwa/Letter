using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bigBonus : MonoBehaviour
{
    private GameObject letter;

    [SerializeField]
    private float velocity = 1f;

    // [SerializeField]
    // private GameObject info;

    private Animator anim;
    private SpriteRenderer sr;

    void Awake(){
        letter = GameObject.FindWithTag("Player");
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();     
    }

    // Update is called once per frame
    void Update()
    {
        // Animation for player
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")){
            // 向玩家移动，在到达之前不会消失，透明度下降
            // if (info == null){
            //     info = GameObject.FindWithTag("popE");
            // }
            // info.SetActive(true);
            // Time.timeScale = 0f;
            Destroy(gameObject);
        }
    }

}
