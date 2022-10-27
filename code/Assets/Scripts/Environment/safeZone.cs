using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class safeZone : MonoBehaviour
{
    private GameObject player;
    private GameObject girl;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        girl = GameObject.FindWithTag("Girl");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Girl")){
            girl.GetComponent<girl>().safe = true;
        }
        if (other.CompareTag("Player") && girl.GetComponent<girl>().safe){
            player.GetComponent<Player>().safe = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Girl")){
            girl.GetComponent<girl>().safe = true;
        }
        if (other.CompareTag("Player") && girl.GetComponent<girl>().safe){
            player.GetComponent<Player>().safe = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")){
            player.GetComponent<Player>().safe = false;
        }      
        if (other.CompareTag("Girl")){
            girl.GetComponent<girl>().safe = false;
        }      
    }
}
