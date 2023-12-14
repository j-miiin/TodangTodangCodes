![header](https://capsule-render.vercel.app/api?type=cylinder&color=A1B6FF&height=150&section=header&text=UIManager&fontSize=60&fontColor=ECFBFF&animation=fadeIn)

<br>


## :crescent_moon: 목차

| [🐰 개요 🐰](#rabbit-개요) |
| :---: |
| [🍡 주요 메서드 🍡](#dango-주요-메서드) |
| [🍵 활용 🍵](#tea-활용) |
| [🥕 트러블 슈팅 🥕](#carrot-트러블-슈팅) |

<br>

* * *

<br>

## :rabbit: 개요  
- UIManager를 통해 UI Component들에 쉽게 접근하고 관리한다.
- UIManager를 통해 Popup들을 편리하게 사용한다.

<br>

* * *

<br>

## :dango: 주요 메서드

### UIManager

|메서드|기능|
|:---:|:---:|
|[GetUIComponent](https://github.com/j-miiin/TodangTodangCodes/blob/bcb8da4e827f8da3155b97215df1687f5cb33bc6/UIManager%EB%A5%BC%20%ED%86%B5%ED%95%9C%20UI%20%EA%B4%80%EB%A6%AC/UIManager.cs#L12-L37)|요청 받은 UI Component가 Dictionary에 있다면 반환하고,<br>없다면 Resources 폴더에서 Load하여 Dictionary에 저장한 뒤 반환한다.|
|[TryGetUIComponent](https://github.com/j-miiin/TodangTodangCodes/blob/bcb8da4e827f8da3155b97215df1687f5cb33bc6/UIManager%EB%A5%BC%20%ED%86%B5%ED%95%9C%20UI%20%EA%B4%80%EB%A6%AC/UIManager.cs#L39-L67)|GetUIComponent를 응용하여 만든 메서드로, 예외 처리를 보다 쉽게 하기 위해서 추가한 메서드이다.<br>TryGetComponent 메서드처럼 out 매개변수를 사용하여 UI Component를 반환한다.<br>요청 받은 UI Component를 가져오는데 성공하면 true, 실패하면 false를 반환한다.|
|[RemoveUIComponent](https://github.com/j-miiin/TodangTodangCodes/blob/bcb8da4e827f8da3155b97215df1687f5cb33bc6/UIManager%EB%A5%BC%20%ED%86%B5%ED%95%9C%20UI%20%EA%B4%80%EB%A6%AC/UIManager.cs#L69-L76)|UI Component를 저장한 Dictionary에서 요청받은 해당 UI Component를 삭제한다.<br>Scene이 변경되었을 때 오브젝트가 파괴되어 더 이상 참조할 수 없는 UI Component에 접근하는 것을 방지하기 위해 사용한다.|
|[RemoveAllUIComponent](https://github.com/j-miiin/TodangTodangCodes/blob/bcb8da4e827f8da3155b97215df1687f5cb33bc6/UIManager%EB%A5%BC%20%ED%86%B5%ED%95%9C%20UI%20%EA%B4%80%EB%A6%AC/UIManager.cs#L78-L81)|UI Component를 저장한 Dictionary에 존재하는 모든 UI Component를 삭제한다.|
|[ShowPopup](https://github.com/j-miiin/TodangTodangCodes/blob/bcb8da4e827f8da3155b97215df1687f5cb33bc6/UIManager%EB%A5%BC%20%ED%86%B5%ED%95%9C%20UI%20%EA%B4%80%EB%A6%AC/UIManager.cs#L83-L112)|요청 받은 UI Popup을 Show 한다.<br>GetUIComponent와 마찬가지로 요청 받은 Popup이 Dictionary에 존재하지 않는다면 Resources 폴더에서 Load하여 저장한 뒤 반환한다.|
|[SetUIOnScreen](https://github.com/j-miiin/TodangTodangCodes/blob/a68581871a2d51263ae616105fbd18294d054b87/UIManager%EB%A5%BC%20%ED%86%B5%ED%95%9C%20UI%20%EA%B4%80%EB%A6%AC/UIManager.cs#L114-L134)|UI가 열리면 Stack에 해당 UI를 Push한다.<br>UI가 닫힐 때는 Stack에서 Pop한다.|
|[GetCurrentUI](https://github.com/j-miiin/TodangTodangCodes/blob/a68581871a2d51263ae616105fbd18294d054b87/UIManager%EB%A5%BC%20%ED%86%B5%ED%95%9C%20UI%20%EA%B4%80%EB%A6%AC/UIManager.cs#L136-L140)|현재 열려있는 UI를 저장하는 Stack에서 가장 위에 있는 UI를 반환한다.|
|[CloseAllPopUps](https://github.com/j-miiin/TodangTodangCodes/blob/a68581871a2d51263ae616105fbd18294d054b87/UIManager%EB%A5%BC%20%ED%86%B5%ED%95%9C%20UI%20%EA%B4%80%EB%A6%AC/UIManager.cs#L153-L160)|현재 열려있는 Popup을 모두 닫는다.<br>Scene 이동 시 열린 팝업들을 닫기 위해 사용한다.|

<br>

### UI_Popup
|메서드|기능|
|:---:|:---:|
|[ShowPopup](https://github.com/j-miiin/TodangTodangCodes/blob/bcb8da4e827f8da3155b97215df1687f5cb33bc6/UIManager%EB%A5%BC%20%ED%86%B5%ED%95%9C%20UI%20%EA%B4%80%EB%A6%AC/UI_Popup.cs#L37-L44)|PopupParameter를 통해 전달 받은 Popup의 내용과 콜백을 정의한 뒤, 해당 Popup을 Open 한다.|
|[ClosePopup](https://github.com/j-miiin/TodangTodangCodes/blob/bcb8da4e827f8da3155b97215df1687f5cb33bc6/UIManager%EB%A5%BC%20%ED%86%B5%ED%95%9C%20UI%20%EA%B4%80%EB%A6%AC/UI_Popup.cs#L46-L52)|Button의 Type(확인/취소)에 따라 ShowPopup에서 전달 받았던 Callback 메서드를 실행한 뒤, Popup을 Close 한다.|

<br>

[🌙 목차로 돌아가기](#crescent_moon-목차)

<br>

* * *

<br>

## :tea: 활용 

### UIManager를 통해 UI Component에 접근하기

```cs
private void OpenInventory()
{
    if (_uiInventory == null)
    {
        if (!_uiManager.TryGetUIComponent<UI_Inventory>(out _uiInventory))
        {
            Debug.LogError("Null Exception : UI_Inventory");
            return;
        }
    }
    _uiInventory.OpenUI();

    ...
}
```

<br>

### UIManager와 PopupParameter를 활용한 Popup 사용
```cs
UIManager.Instance.ShowPopup<UI_SliderPopup>(
    new SliderPopupParameter(
          sliderMaxValue: quantity
          , valueConfirmAction: valueConfirmAction
        )
    );
```

<br>

[🌙 목차로 돌아가기](#crescent_moon-목차)

<br>

* * *

<br>

## :carrot: 트러블 슈팅

### ⚠️ 문제
- 기존 방식은 모든 팝업의 매개변수를 UIManager의 ShowPopup 메서드에 전달하던 방식
  ```cs
  public T ShowPopup<T>(string content = "", Callback confirmAction = null, Callback cancelAction = null
            , Action<int> valueConfirmAction = null, int sliderMaxValue = -1) where T : UI_Popup
  {
      ...

      if (valueConfirmAction == null || sliderMaxValue == -1)
      {
          UI_DefaultPopup uiPopup = uiPopupDic[key] as UI_DefaultPopup;
          uiPopup.ShowPopup(confirmAction, cancelAction, content);
      }
      else
      {
          UI_SliderPopup uiSliderPopup = uiPopupDic[key] as UI_SliderPopup;
          uiSliderPopup.ShowPopup(confirmAction, cancelAction, valueConfirmAction, sliderMaxValue, content);
      }

      return _uiPopupDic[key] as T;
  }
  ```
  - Popup의 종류가 증가할수록 ShowPopup 메서드의 매개변수가 늘어나는 단점
  - Popup의 종류가 증가하면 예외 처리가 복잡해지고, 오류 발생 가능성이 높아짐<br>
    -> 확장성 저하
    
<br>

### 🛠️ 시도
- ShowPopup 메서드에는 모든 Popup들의 공통 Parameter만 전달하고, Popup을 반환 받아서 추가 Parameter를 Set 하는 방식
  ```cs
  UIManager.Instance.ShowPopup<UI_SliderPopup>()
      ?.SetPopupValue(valueConfirmAction: valueConfirmAction, sliderMaxValue: quantity);
  ```
  - Unity 개발을 할 때 ? 이나 ?? 같은 Null 체크 연산자를 사용하는 것은 고쳐야 하는 코딩 습관이라는 것을 알게 됨
- 임시 변수에 반환 받은 Popup을 할당한 뒤, Null 체크 후 Set 하는 방식<br>
  -> Popup을 Open 하는 동작과 필요한 Parameter를 할당하는 동작을 ShowPopup 메서드를 호출하는 부분에서 끝낼 수는 없을까?

<br>

### 💡 선택
- Popup에 필요한 Parameter를 담는 PopupParameter 클래스 생성
- 각각 세분화 되는 Popup들은 PopupParameter 클래스를 상속 받아서 필요한 Parameter를 선언하여 전달
  ```cs
  public class SliderPopupParameter : PopupParameter
  {
      private int _sliderMaxValue;
      private Action<int> _valueConfirmAction; 
  
      public SliderPopupParameter(int sliderMaxValue, Action<int> valueConfirmAction, 
          string content = "", Callback confirmCallback = null, Callback cancelCallback = null)
      : base(content, confirmCallback, cancelCallback)
      {
          _sliderMaxValue = sliderMaxValue;
          _valueConfirmAction = valueConfirmAction;
      }
  
      public int GetSliderMaxValue()
      {
          return _sliderMaxValue;
      }
  
      public Action<int> GetValueConfirmAction()
      {
          return _valueConfirmAction;
      }
  }
  ```
- 팝업 내부에서 필요한 Parameter 클래스 타입으로 형 변환하여 사용
  ```cs
  public class UI_SliderPopup : UI_Popup
  {
      ...
  
      public override void ShowPopup(PopupParameter popupParameter)
      {
          base.ShowPopup(popupParameter);
  
          SliderPopupParameter parameter = popupParameter as SliderPopupParameter;
  
          _max = parameter.GetSliderMaxValue();
          _value = (int)(_max * 0.5);
          _slider.maxValue = _max;
          _slider.value = _value;
          _quantityText.text = $"{_value}";
  
          OnValueCallback = null;
          OnValueCallback += parameter.GetValueConfirmAction();
      }

      ...
  }
  ```

[🌙 목차로 돌아가기](#crescent_moon-목차)

<br>
