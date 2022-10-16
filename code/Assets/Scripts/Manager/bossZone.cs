using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossZone : MonoBehaviour
{
    [SerializeField]
    private GameObject oldmusic;

    [SerializeField]
    private GameObject bossTr;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") == false){
            return;
        }

        if (!oldmusic){
            oldmusic = GameObject.FindWithTag("Sound");
        }


        // Change music
        oldmusic.GetComponent<AudioSource>().Pause();

        // Activate Boss
        bossTr.GetComponent<Boss>().appear();
    }
}
