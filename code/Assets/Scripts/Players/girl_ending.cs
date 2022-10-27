using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girl_ending : MonoBehaviour
{
    [SerializeField]
    float moveForce = 3f;

    private Animator anim;

    bool walk;

    private void Awake() {
        anim = GetComponent<Animator>();
        walk = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (walk){
            transform.position += new Vector3(1f, 0f, 0f) * Time.deltaTime * moveForce;
        }
    }

    public void stop(){
        walk = false;
        anim.SetBool("idle", true);
    }
}
