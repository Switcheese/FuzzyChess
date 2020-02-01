using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 작성일자 : 2020-01-17-PM-5-12
// 작성자   : 배형영
// 간단설명 : 

namespace Managers
{
    public class UIManager : SingletonMono<UIManager>
    {
        // -------------  Variable  ------------- 
        [SerializeField]
        private Transform MainCanvas;

        private Stack<UIBase> stackUI;
        private EnumDictionary<UI_Kind, GameObject> dicUI;
        private Array optionValues;

        // -------------  Property  ------------- 



        // -------------  Behaviour  ------------- 
        private void Awake()
        {
            stackUI = new Stack<UIBase>();
            dicUI = new EnumDictionary<UI_Kind, GameObject>();
            optionValues = Enum.GetValues(typeof(UI_Option));

            OpenWindow(UI_Kind.UIWindow_Home, true);
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                OpenWindow(UI_Kind.UIWindow_Test, true);
            }
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(stackUI.Count > 0)
                    CloseWindow();
            }
        }

        // ------------- Private Method  ------------- 
        private GameObject LoadUI(UI_Kind kind)
        {
            GameObject loadUI = Common.ResourceLoad(string.Format($"{Common.UI_Path}{kind.ToString()}"));
            GameObject instUI = null;
            if (loadUI == null)
            {
#if UNITY_EDITOR
                Debug.LogWarning(string.Format($"{kind.ToString()} UI 리소스를 찾을 수 없습니다."));
#endif
            }
            else
            {
                instUI = Instantiate(loadUI, this.MainCanvas);
                instUI.SetActive(false);
            }

            return instUI;
        }

        private void VerifyOpen(UIBase _ui)
        {
            foreach (UI_Option option in optionValues)
            {
                if((_ui.Option & option) == option)
                {
                    switch (option)
                    {
                        case UI_Option.Overlay:
                            {

                            }
                            break;
                        case UI_Option.Close:
                            {
                                // 다 없애기
                                foreach (var pop in stackUI)
                                {
                                    this.DisEnableWindow(stackUI.Pop());
                                }
                                //if (stackUI.Count > 0)
                                //{
                                //    this.DisEnableWindow(stackUI.Pop());
                                //}
                            }
                            break;
                        case UI_Option.CallBackClose:
                            {
                                // 다 끄기
                                foreach (var pop in stackUI)
                                {
                                    this.DisEnableWindow(stackUI.Peek());
                                }
                                //if (stackUI.Count > 0)
                                //{
                                //    this.DisEnableWindow(stackUI.Peek());
                                //}
                            }
                            break;
                    }
                }
            }
            stackUI.Push(_ui);
        }

        private void VerifyClose()
        {
            if (stackUI.Count > 0)
            {
                var cntUI = stackUI.Peek();
                if (dicUI[cntUI.Kind].activeInHierarchy)
                {
                    return;
                }
                else
                {
                    cntUI.SortingOrder = stackUI.Count;    // 최상단에 띄운다.
                    cntUI.OpenUI();
                    dicUI[cntUI.Kind].SetActive(true);
                }
            }
            //foreach (UI_Option option in optionValues)
            //{
            //    if ((closeUI.Option & option) == option)
            //    {
            //        switch (option)
            //        {
            //            case UI_Option.Overlay:
            //                {
            //                    // 관리중인 UI
            //                    var pop = stackUI.Pop();

            //                    pop.SortingOrder = stackUI.Count;    // 최상단에 띄운다.
            //                    pop.OpenUI();
            //                    dicUI[pop.Kind].SetActive(true);
            //                }
            //                break;
            //            case UI_Option.CallBackClose:
            //                {
            //                    if (stackUI.Count > 0)
            //                    {
            //                        this.DisEnableWindow(stackUI.Peek());
            //                    }
            //                }
            //                break;
            //        }
            //    }
            //}
        }


        /// <summary>
        /// 끄기만 한다.
        /// </summary>
        /// <param name="ui"></param>
        private void DisEnableWindow(UIBase ui)
        {
            if (ui == null)
            {
                return;
            }
            if (dicUI.ContainsKey(ui.Kind))
            {
                dicUI[ui.Kind].SetActive(false);
            }
        }

        // ------------- ProtectedMethod  ------------- 



        // -------------  Public Method  ------------- 

        public UIBase OpenWindow(UI_Kind kind, bool isActive)
        {
            // 관리중인 UI가 아닐경우 Load
            if (!dicUI.ContainsKey(kind))
            {
                var inst = LoadUI(kind);
                if(inst != null)
                {
                    dicUI.Add(kind, inst);
                    var component = inst.GetComponent(typeof(UIBase)) as UIBase;
                    component.Init();
                }
                else
                    return null;
            }
            // 관리중인 UI
            var ui = dicUI[kind].GetComponent(typeof(UIBase)) as UIBase;

            // 켜는게 아니면 해당 UI 최상위 스크립트 반환
            if (isActive)
            {
                ui.SortingOrder = stackUI.Count;    // 최상단에 띄운다.
                ui.OpenUI();
                VerifyOpen(ui);
                dicUI[kind].SetActive(true);
            }
            return ui;
        }
        
        //public void CloseWindow(UIBase cntUI)
        //{
        //    DisEnableWindow(cntUI);

        //    if (stackUI.Count > 0)
        //    {
        //        // 관리중인 UI
        //        var pop = stackUI.Pop();

        //        pop.SortingOrder = stackUI.Count;    // 최상단에 띄운다.
        //        pop.OpenUI();
        //        dicUI[pop.Kind].SetActive(true);
        //    }
        //}

        public void CloseWindow()
        {
            if (stackUI.Count > 0)
            {
                //var closeUI = stackUI.Pop();
                DisEnableWindow(stackUI.Pop());
                VerifyClose();

                // 관리중인 UI
                //var pop = stackUI.Pop();

                //pop.SortingOrder = stackUI.Count;    // 최상단에 띄운다.
                //pop.OpenUI();
                //dicUI[pop.Kind].SetActive(true);
            }
        }

        public GameObject GetWindow(UI_Kind kind)
        {
            if (dicUI.ContainsKey(kind))
                return dicUI[kind];
            else
                return null;
        }
        public T GetWindowComponent<T>(UI_Kind kind) where T : class
        {
            if (dicUI.ContainsKey(kind))
                return dicUI[kind].GetComponent(typeof(T)) as T;
            else
                return null;
        }

    }
}