using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private float alphaSpeed = 10;

    private bool isShow;

    private UnityAction hideCallBack;

    protected virtual void Awake()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
        if(canvasGroup == null)
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
    }

    protected virtual void Start()
    {
        Init();
    }

    public abstract void Init();

    public virtual void showMe()
    {
        isShow = true;
        canvasGroup.alpha = 0;
    }

    public virtual void HideMe(UnityAction callBack)
    {
        isShow = false;
        canvasGroup.alpha = 1;
        hideCallBack = callBack;
    }

    private void Update()
    {
        if(isShow && canvasGroup.alpha!=1)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1)
                canvasGroup.alpha = 1;
        }
        else if(!isShow && canvasGroup.alpha!=0)
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
            if(canvasGroup.alpha<=0)
            {
                canvasGroup.alpha = 0;
                hideCallBack?.Invoke();
            }
        }
    }
}
