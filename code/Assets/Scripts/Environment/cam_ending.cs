using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_ending : MonoBehaviour
{
    [SerializeField]
    GameObject girl;

    Vector3 newpos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        newpos = new Vector3(girl.transform.position.x + 1f, girl.transform.position.y + 3.5f, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newpos, Time.deltaTime * 1.5f);
    }
}
