using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class t2 : MonoBehaviour
{
    [SerializeField]
    private GameObject oldmusic;

    // Start is called before the first frame update
    void Start()
    {
        if (oldmusic == null){
            oldmusic = GameObject.FindGameObjectWithTag("Sound");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void bye(){
        Destroy(oldmusic);
        SceneManager.LoadScene("end");
    }
}
