using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    [SerializeField]
    private GameObject window;
    [SerializeField]
    private GameObject t0;
    [SerializeField]
    private GameObject music;
    public void PlayGame(){
        string clickedObj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        window.SetActive(false);

        t0.SetActive(true);
        music.SetActive(true);

        // SceneManager.LoadScene("0");
    }

    public void EndGame(){
        window.SetActive(true);        
    }

    public void Yes(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif 
    }

    public void No(){
        window.SetActive(false);        
    }
}
