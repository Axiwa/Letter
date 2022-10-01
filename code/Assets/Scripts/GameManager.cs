using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Dictionary<string, int> table=new Dictionary<string, int>(){
        {"Button", 0},
        {"Button2", 1}
    };
    [SerializeField]
    private GameObject[] characters;

    private int __charIndex;
    public int CharIndex{
        get {return __charIndex;}
        set {__charIndex = value;}
    }

    private void Awake() {
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode){
        if (scene.name == "GamePlay"){
            Instantiate(characters[CharIndex]);
        }
    }
}
