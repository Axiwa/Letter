using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    [SerializeField]
    private GameObject window;
    public void PlayGame(){
        string clickedObj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(clickedObj);

        SceneManager.LoadScene("GamePlay");
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
