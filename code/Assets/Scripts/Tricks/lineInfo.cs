using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineInfo : MonoBehaviour
{
    public delegate void shouldMove();
    public static event shouldMove moveInfo;
    [HideInInspector]
    public bool hasCollided = false;
    private string TRICK_TAG = "Stair";
    // Start is called before the first frame update
    private GameObject parent;

    [HideInInspector]
    public Transform[] childrenStair;

    void Awake(){
        parent = transform.parent.gameObject;
    }
    void Start(){
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate(){
        hasCollided = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (hasCollided){
            return;
        }
        hasCollided = true;
        if (other.gameObject.CompareTag(TRICK_TAG)){
            // 播放音效
            // INFORM stair to play animation
            foreach (Transform child in parent.transform){ 
                GameObject stair = child.gameObject;
                if (stair.CompareTag("Ground"))
                    stair.GetComponent<Stair>().Move();
            }
        }
    }
}
