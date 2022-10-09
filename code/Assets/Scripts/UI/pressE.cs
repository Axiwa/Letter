using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class pressE : MonoBehaviour
{
    public float blinkSpeed;//闪烁速度
    private bool isAddAlpha;//是否增加透明度
    private float timer;//计时器
    public float timeval = 2;//时间间隔

    private Text t;//指向自身图片

    private void Awake() {
        blinkSpeed = 0.8f;
        isAddAlpha = true;
        timer = 0;
    }
    private void Start()
    {
       t  = GetComponent<Text>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }

        timer += Time.unscaledDeltaTime;
        if (isAddAlpha)
        {
            t.color += new Color(0, 0, 0, Time.unscaledDeltaTime * blinkSpeed);
            if (timer >= timeval)
            {
                t.color = new Color(t.color.r, t.color.g, t.color.b, 1);
                isAddAlpha = false;
                timer = 0;
            }
        }
        else
        {
            t.color -= new Color(0, 0, 0, Time.unscaledDeltaTime * blinkSpeed);
            if (timer >= timeval)
            {
                t.color = new Color(t.color.r,t.color.g, t.color.b, 0);
                isAddAlpha = true;
                timer = 0;
            }
        }
    }

}