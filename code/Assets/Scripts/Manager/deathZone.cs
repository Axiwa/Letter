using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        var p = collider.gameObject.GetComponent<Player>();
        var g = collider.gameObject.GetComponent<girl>();
        if (p != null){
            p.re();
        }
        else if (g != null){
            GameObject.FindWithTag("Player").GetComponent<Player>().re();
        }
    }
}
