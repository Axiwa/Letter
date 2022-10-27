using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class home : MonoBehaviour
{
    GameObject obj = null;
    public void returnhome(){
        obj = GameObject.FindGameObjectWithTag("Sound");
        Destroy(obj);
        SceneManager.LoadScene("UI");
    }
}
