using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class t0 : MonoBehaviour
{
    public Animator animator;

    private void Update() {
        if (Input.anyKeyDown){
            animator.SetBool("totrans", false);
            SceneManager.LoadScene("0");
        }
    }
    private void Awake(){
        animator = GetComponent<Animator>();
    }
    public void FadeToTrans(){
        animator.SetBool("totrans", true);
    }

    public void OnFadeComplete(){
        SceneManager.LoadScene("0");
    }
}
