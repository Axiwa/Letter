using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class music0 : MonoBehaviour
{
    private static GameObject instance; 
    GameObject obj = null;  
    private void Awake() {
        if (instance == null){
            instance = this.gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }

    }
}
