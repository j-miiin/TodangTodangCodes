using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UI_Popup;

public class UIManager : Singleton<UIManager>
{
    // Resources/Prefabs/UI 안의 UI 컴포넌트 프리팹들을 저장하는 딕셔너리
    private Dictionary<string, UI_Base> _uiDic = new Dictionary<string, UI_Base>();
    private Dictionary<string, UI_Popup> _uiPopupDic = new Dictionary<string, UI_Popup>();

    public T GetUIComponent<T>() where T : UI_Base
    {
        string key = typeof(T).Name;
        if (!_uiDic.ContainsKey(key))
        {
            // 요청 시점에 해당 UI 컴포넌트가 없다면 Load
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/UI/{key}");
            if (!prefab)
            {
#if UNITY_EDITOR
                Debug.LogError($"UI Prefab 로드 실패 : {key}");
#endif
                return null;
            }
            GameObject obj = Instantiate(prefab);
            if (!obj.TryGetComponent<T>(out T component))
            {
#if UNITY_EDITOR
                Debug.LogError($"Get UI Component 실패 : {key}");
#endif
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
            if (!prefab)
            {
#if UNITY_EDITOR
                Debug.LogError($"UI Prefab 로드 실패 : {key}");
#endif
                uiComponent = null;
                return false;
            }
            GameObject obj = Instantiate(prefab);
            if (!obj.TryGetComponent<T>(out T component))
            {
#if UNITY_EDITOR
                Debug.LogError($"Get UI Component 실패 : {key}");
#endif
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
            if (!prefab)
            {
#if UNITY_EDITOR
                Debug.LogError($"UI Popup Prefab 로드 실패 : {key}");
#endif
                return null;
            }
            GameObject obj = Instantiate(prefab);
            if (!obj.TryGetComponent<T>(out T component))
            {
#if UNITY_EDITOR
                Debug.LogError($"Get UI Component 실패 : {key}");
#endif
                return null;
            }
            obj.transform.SetParent(transform);
            _uiPopupDic.Add(key, obj.GetComponent<T>());
        }

        _uiPopupDic[key].ShowPopup(popupParameter);

        return _uiPopupDic[key] as T;
    }
}
