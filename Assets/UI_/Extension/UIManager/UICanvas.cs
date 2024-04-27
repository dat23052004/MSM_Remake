using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    [SerializeField] bool isDestroyOnClose = false;

    private void Awake()
    {
        // Xu ly tai tho
            RectTransform rect = GetComponent<RectTransform>();
        float ratio = (float)Screen.width / (float)Screen.height;
        if(ratio > 2.1f)
        {
            Vector2 leftBottom = rect.offsetMin;
            Vector2 rigthTop = rect.offsetMax;

            leftBottom.y = 0f;
            rigthTop.y = -100f;

            rect.offsetMin = leftBottom;
            rect.offsetMax = rigthTop;
        }
    }

    // Goi truoc khi canvas duoc active 
    public virtual void Setup()
    {

    }

    // Goi sau khi duoc active
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    // Tat canvas sau time (s)
    public virtual void Close(float time)
    {
        Invoke(nameof(CloseDirectly), time);
    }

    // Tat canvas truc tiep
    public virtual void CloseDirectly()
    {
        if(isDestroyOnClose)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
