using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ending_letter : MonoBehaviour
{
    bool begin;
    bool end;
    private Animator anim;

    [SerializeField]
    float speed = 3;

    [SerializeField]
    GameObject girl;

    [SerializeField]
    Transform target;

    private void Awake()
    {
        begin = false;
        end = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (end)
        {
            anim.SetBool("Walk", false);
            return;
        }
        else if (begin)
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            if (transform.position.x <= target.position.x)
            {
                end = true;
            }
        }
        else
        {
            if (Vector3.Distance(girl.transform.position, transform.position) < 10f)
            {
                begin = true;
                anim.SetBool("Walk", true);
            }
        }
    }
}
