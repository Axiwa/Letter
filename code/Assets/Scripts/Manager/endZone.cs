using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endZone : MonoBehaviour
{

    [SerializeField]
    private GameObject oldmusic;

    [SerializeField]
    private GameObject t;

    [SerializeField]
    bool pl = false;
    [SerializeField]
    bool gi = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!oldmusic)
        {
            oldmusic = GameObject.FindWithTag("Sound");
        }
    }

    private void Update()
    {
        if (!oldmusic)
        {
            oldmusic = GameObject.FindWithTag("Sound");
        }
        if (pl && gi)
        {
            // Change music
            oldmusic.GetComponent<AudioSource>().Pause();
            t.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") == false && other.CompareTag("Girl") == false){
            return;
        }
        if (other.CompareTag("Player"))
        {
            pl = true;
        }
        if (other.CompareTag("Girl"))
        {
            gi = true;
        }

    }
}
