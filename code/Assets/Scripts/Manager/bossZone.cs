using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossZone : MonoBehaviour
{
    [SerializeField]
    private GameObject oldmusic;

    [SerializeField]
    private GameObject bossTr;

    bool hasTrigger = false;
    bool hasExit = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") == false){
            return;
        }

        if (hasTrigger){
            return;
        }

        hasTrigger = true;

        if (!oldmusic){
            oldmusic = GameObject.FindWithTag("Sound");
        }


        // Change music
        oldmusic.GetComponent<AudioSource>().Pause();

        // Activate Boss
        bossTr.GetComponent<Boss>().appear();
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player") == false){
            return;
        }

        if (hasExit){
            return;
        }

        hasExit = true;

        if (!oldmusic){
            oldmusic = GameObject.FindWithTag("Sound");
        }

        if (bossTr == null){
            // Change music
            oldmusic.GetComponent<AudioSource>().Stop();
            oldmusic.GetComponent<AudioSource>().Play();
        }

    }
}
