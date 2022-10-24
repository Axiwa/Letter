using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class pressE : MonoBehaviour
{
    public float blinkSpeed;//闪烁速度
    private bool isAddAlpha;//是否增加透明度
    private float timer;//计时器
    public float timeval = 2f;//时间间隔
    private Color newColor;
    private Color oldColor;

    [HideInInspector]
    public Image t;//指向自身图片

    private void Awake() {
        blinkSpeed = 3f;
        isAddAlpha = true;
        timeval = 2f;
        timer = 0;
    }
    private void Start()
    {
       t  = GetComponent<Image>();
       oldColor = t.color;
       newColor = new Color(t.color.r, t.color.g, t.color.b, 0);
       t.color = newColor;
    }

    private void Update()
    {
        timer += Time.unscaledDeltaTime;
       
        if (isAddAlpha)
        {
            t.color = Color.Lerp(t.color, oldColor, Time.unscaledDeltaTime * blinkSpeed);
            if (timer >= timeval)
            {
                t.color = new Color(t.color.r, t.color.g, t.color.b, 1);
                isAddAlpha = false;
                timer = 0;
            }
        }
        else
        {
            t.color = Color.Lerp(t.color, newColor, Time.unscaledDeltaTime * blinkSpeed);
            if (t.color.a < 0.05)
            {
                t.color = new Color(t.color.r,t.color.g, t.color.b, 0);
                isAddAlpha = true;
                Time.timeScale = 1;
                gameObject.SetActive(false);
            }
        }
    }

}