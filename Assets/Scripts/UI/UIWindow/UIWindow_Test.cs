using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성일자 : 2020-01-29-PM-4-43
// 작성자   : 배형영
// 간단설명 :

public class UIWindow_Test : UIBase
{
    // -------------  Variable  ------------- 



    // -------------  Property  ------------- 



    // -------------  Behaviour  ------------- 



    // ------------- Private Method  ------------- 



    // ------------- ProtectedMethod  ------------- 



    // -------------  Public Method  ------------- 

    public override void OpenUI()
    {
        base.OpenUI();
        this.Kind = UI_Kind.UIWindow_Test;
        this.Option = UI_Option.CallBackClose;
    }


}
