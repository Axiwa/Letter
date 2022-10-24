using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endZone : MonoBehaviour
{

    [SerializeField]
    private GameObject oldmusic;

    [SerializeField]
    private GameObject t;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") == false && other.CompareTag("Girl") == false){
            return;
        }
        if (!oldmusic){
            oldmusic = GameObject.FindWithTag("Sound");
        }
        // Change music
        oldmusic.GetComponent<AudioSource>().Pause();
        t.SetActive(true);
    }
}
