using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ending_toack : MonoBehaviour
{
    [SerializeField]
    GameObject ack;

    [SerializeField]
    GameObject girl;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void toack(){
        ack.SetActive(true);
        girl.GetComponent<girl_ending>().stop();
    }
}
