using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UI_Popup;

public class UIManager : Singleton<UIManager>
{
    // Resources/Prefabs/UI 안의 UI 컴포넌트 프리팹들을 저장하는 딕셔너리
    private Dictionary<string, UI_Base> _uiDic = new Dictionary<string, UI_Base>();
    private Dictionary<string, UI_Popup> _uiPopupDic = new Dictionary<string, UI_Popup>();

    private Stack<UI_Base> _uiOnScreen = new Stack<UI_Base>();

    public T GetUIComponent<T>() where T : UI_Base
    {
        string key = typeof(T).Name;
        if (!_uiDic.ContainsKey(key))
        {
            // 요청 시점에 해당 UI 컴포넌트가 없다면 Load
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/UI/{key}");
            if (prefab == null)
            {
                Debug.LogError($"UI Prefab 로드 실패 : {key}");
                return null;
            }
            GameObject obj = Instantiate(prefab);
            if (!obj.TryGetComponent<T>(out T component))
            {
                Debug.LogError($"Get UI Component 실패 : {key}");
                return null;
            }
            _uiDic.Add(key, obj.GetComponent<T>());
        }
        return  _uiDic[key] as T;
    }

    public bool TryGetUIComponent<T>(out T uiComponent) where T : UI_Base
    {
        string key = typeof(T).Name;
        if (!_uiDic.ContainsKey(key))
        {
            // 요청 시점에 해당 UI 컴포넌트가 없다면 Load
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/UI/{key}");
            if (prefab == null)
            {
                Debug.LogError($"UI Prefab 로드 실패 : {key}");
                uiComponent = null;
                return false;
            }
            GameObject obj = Instantiate(prefab);
            if (!obj.TryGetComponent<T>(out T component))
            {
                Debug.LogError($"Get UI Component 실패 : {key}");
                uiComponent = null;
                return false;
            }
            _uiDic.Add(key, component);
        }
        uiComponent = _uiDic[key] as T;
        return true;
    }

    public void RemoveUIComponent<T>() where T : UI_Base
    {
        string key = typeof(T).Name;
        if (_uiDic.ContainsKey(key))
        {
            _uiDic.Remove(key);
        }
    }

    public void RemoveAllUIComponent()
    {
        _uiDic.Clear();
    }

    public T ShowPopup<T>(PopupParameter popupParameter) where T : UI_Popup
    {
        string key = typeof(T).Name;
        if (!_uiPopupDic.ContainsKey(key))
        {
            // 요청 시점에 해당 UI 컴포넌트가 없다면 Load
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/UI/Popup/{key}");
            if (prefab == null)
            {
                Debug.LogError($"UI Popup Prefab 로드 실패 : {key}");
                return null;
            }
            GameObject obj = Instantiate(prefab);
            if (!obj.TryGetComponent<T>(out T component))
            {
                Debug.LogError($"Get UI Component 실패 : {key}");
                return null;
            }
            obj.transform.SetParent(transform);
            _uiPopupDic.Add(key, obj.GetComponent<T>());
        }

        _uiPopupDic[key].ShowPopup(popupParameter);

        return _uiPopupDic[key] as T;
    }

    public void SetUIOnScreen(UI_Base ui, bool isOpen)
    {
        if (isOpen)
        {
            if (ui is UI_Dialog) return;
            if (_uiOnScreen.Contains(ui)) return;

            _uiOnScreen.Push(ui);
        }
        else
        {
            if (!_uiOnScreen.Contains(ui)) return;

            while (_uiOnScreen.Peek() != ui)
            {
                _uiOnScreen.Pop();
            }

            _uiOnScreen.Pop();
        }
    }

    public UI_Base GetCurrentUI()
    {
        if (_uiOnScreen.TryPeek(out UI_Base ui)) return ui;
        return null;
    }

    public void CloseAllPopUps()
    {
        foreach (var popUp in _uiPopupDic)
        {
            popUp.Value.ClosePopup(PopupButtonType.Cancel);
        }
    }

    //public T ShowPopup<T>(string content = "", Callback confirmAction = null, Callback cancelAction = null
    //        /*, Action<int> valueConfirmAction = null, int sliderMaxValue = -1*/) where T : UI_Popup
    //{
    //    string key = typeof(T).Name;
    //    if (!uiPopupDic.ContainsKey(key))
    //    {
    //        // 요청 시점에 해당 UI 컴포넌트가 없다면 Load
    //        GameObject obj = Instantiate(Resources.Load($"Prefabs/UI/Popup/{key}")) as GameObject;
    //        if (obj == null)
    //        {
    //            Debug.LogError($"UI Popup Prefab 로드 실패 : {key}");
    //            return null;
    //        }
    //        obj.transform.SetParent(transform);
    //        uiPopupDic.Add(key, obj.GetComponent<T>());
    //    }

    //    //if (valueConfirmAction == null || sliderMaxValue == -1)
    //    //{
    //    //    UI_DefaultPopup uiPopup = uiPopupDic[key] as UI_DefaultPopup;
    //    //    uiPopup.ShowPopup(confirmAction, cancelAction, content);
    //    //} else
    //    //{
    //    //    UI_SliderPopup uiSliderPopup = uiPopupDic[key] as UI_SliderPopup;
    //    //    uiSliderPopup.ShowPopup(confirmAction, cancelAction, valueConfirmAction, sliderMaxValue, content);
    //    //}

    //    uiPopupDic[key].ShowPopup(content, confirmAction, cancelAction);

    //    return uiPopupDic[key] as T;
    //}
}
