using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bosshint : MonoBehaviour
{
    [SerializeField]
    private GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = boss.transform.position;
    }
}
