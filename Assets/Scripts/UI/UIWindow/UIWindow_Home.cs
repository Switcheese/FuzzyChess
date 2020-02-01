using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성일자 : 2020-01-20-PM-6-45
// 작성자   : 배형영
// 간단설명 :

public class UIWindow_Home : UIBase
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
        this.Kind = UI_Kind.UIWindow_Home;
        this.Option = UI_Option.Close;
    }

}
