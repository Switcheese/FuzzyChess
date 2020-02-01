using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성일자 : 2020-01-17-PM-5-24
// 작성자   : 배형영
// 간단설명 :

public class UIBase : MonoBehaviour
{
    // -------------  Variable  ------------- 

    private UI_Option option;
    private UI_Kind kind;
    private int sortingOrder;
    protected Canvas canvas;

    // -------------  Property  ------------- 
    public UI_Option Option
    {
        get { return option; }
        protected set { option = value; }
    }
    public UI_Kind Kind
    {
        get { return kind; }
        protected set { kind = value; }
    }
    public int SortingOrder
    {
        get { return sortingOrder; }
        set { sortingOrder = value; }
    }


    // -------------  Behaviour  ------------- 

    // ------------- Private Method  ------------- 



    // ------------- ProtectedMethod  ------------- 



    // -------------  Public Method  ------------- 
    public virtual void Init()
    {
        canvas = GetComponent(typeof(Canvas)) as Canvas;
        canvas.overrideSorting = true;
    }
    public virtual void OpenUI()
    {
        canvas.sortingOrder = this.sortingOrder;
    }

    public virtual void OnClose()
    {
        Manager.UI.CloseWindow();
    }

    public virtual void OnEsc()
    {
        OnClose();
    }

}
