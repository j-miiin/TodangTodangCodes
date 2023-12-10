![header](https://capsule-render.vercel.app/api?type=cylinder&color=A1B6FF&height=150&section=header&text=Inventory&fontSize=60&fontColor=ECFBFF&animation=fadeIn)

<br>


## :crescent_moon: 목차

| [🐰 개요 🐰](#rabbit-개요) |
| :---: |
| [🐇 기술 도입 배경 🐇](#rabbit2-기술-도입-배경) |
| [🍡 주요 메서드 🍡](#dango-주요-메서드) |
| [🥕 트러블 슈팅 🥕](#carrot-트러블-슈팅) |

<br>

* * *

<br>

## :rabbit: 개요  
- View - Controller - Data 클래스의 역할을 분리한다.
- 데이터와 View의 의존성을 줄인다.

<br>

* * *

<br>

## :rabbit2: 기술 도입 배경
> 문제점<br>
> 인벤토리 안에서 다루는 Data는 3가지 종류로 분류되며, View에 보여지기 전 각각 다른 처리가 필요하다.<br>
> Data를 처리하는 로직을 UI 클래스 내에서 다루면 클래스 하나가 여러 역할을 수행하게 되고,<br>
> 이는 코드 수정으로 인한 사이드 이펙트가 커지는 문제점으로 이어질 수 있다.<br>
- View와 Data를 처리하는 Controller를 나누어 처리함으로써 클래스의 역할을 분리한다.

<br>

* * *

<br>

## :dango: 주요 메서드

### InventoryHandler
- InventoryController들을 관리한다.

|메서드|기능|
|:---:|:---:|
|[Init]()|Controller들의 Data를 초기화하며, 현재 Controller를 첫 번째 Tab의 Controller로 설정한다.|
|[CallOnOpenInventory]()|인벤토리 UI가 Open 되었을 때, Init 메서드를 실행한다.|
|[CallOnChangeTab]()|인벤토리의 탭이 변경되면 현재 Controller를 변경한 뒤 RefreshTab을 요청한다.|
|[CallOnChangeSlot]()|인벤토리 ScrollView의 슬롯 상태가 변경되면 Controller에게 RefreshSlot을 요청한다.|
|[CallOnRefreshDetail]()|인벤토리 상세 정보 창 상태가 변경되면 Controller에게 RefreshDetail을 요청한다.|

<br>

### InventoryController
- 각 Tab을 다루는 Controller들의 부모 클래스이다.

|메서드|기능|
|:---:|:---:|
|[InitDatas]()|Controller에서 공통으로 사용하는 Manager와 데이터 클래스를 캐싱한다.<br>각 Tab에 대응하는 Controller들은 해당 메서드를 오버라이드하며, 추가적으로 필요한 Data들을 설정한다.|
|[RefreshPlayerMoney]()|UI_Inventory에게 Player의 재화 정보 Update를 요청한다.|
|[RefreshTab]()|각 Tab에 대응하는 Controller들은 해당 메서드를 오버라이드하여,<br>현재 자신이 다루는 List로 UI_Inventory의 ScrollView를 Update한다.|
|[RefreshDetail]()|각 Tab에 대응하는 Controller들은 해당 메서드를 오버라이드하여,<br>현재 자신이 다루는 Data의 상세 정보 UI를 Update한다.|

<br>

### UI_Inventory
- Inventory에서 View를 담당하는 클래스이다.

|메서드|기능|
|:---:|:---:|
|[RefreshScrollView]()|ScrollView를 갱신한 뒤, 첫 번째 슬롯이 선택된 상태로 변경한다.|
|[UpdatePlayerMoneyUI]()|Player의 재화 정보 UI를 업데이트한다.|
|[InitTab]()|Tab 리스트 초기 설정을 담당한다.<br>각 Tab 버튼에 클릭 Listener를 연결한 뒤, 첫 번째 탭이 선택된 상태로 설정한다.|
|[ChangeTab]()|Tab 버튼의 클릭 Listener로 연결되는 메서드이다.<br>ScrollView를 맨 위로 이동한 뒤, 선택된 탭을 변경한다.|
|[InitSlots]()|받아온 데이터 개수만큼 오브젝트 풀링을 이용하여 ScrollView의 슬롯 오브젝트를 생성한다.|
|[OnSelectedSlotChanged]()|ScrollView 슬롯 오브젝트의 클릭 Listener로 연결되는 메서드이다.<br>선택된 슬롯을 변경한 뒤, 슬롯에 대한 상세 정보 Update 이벤트를 실행한다.|

<br>

[🌙 목차로 돌아가기](#crescent_moon-목차)

<br>

* * *

<br>

## :carrot: 트러블 슈팅

### ⚠️ 문제 1
- 초기에는 MVC 패턴처럼 View에서 Data를 구독하여, Data가 갱신되면 View도 갱신되는 방식 사용
- 하지만 Scene이 변경된 후 View를 참조할 수 없는 상태에서 Data가 갱신되는 문제 발생

<br>

### 🛠️ 시도
- UI 오브젝트가 Destroy 될 때, 구독한 Data 이벤트를 해제하는 방식<br>
-> 일일이 구독을 해제해야 되는 불편함<br>
-> View와 Data의 의존성을 줄일 수 없을까?

<br>

### 💡 선택
- View에서 전달 받은 입력으로 Data를 갱신하는 동작과 Data의 갱신으로 인한 View를 Update하는 동작을 모두 Controller에서 수행하도록 변경<br>
-> View와 Data의 의존성을 줄이는 MVP 구조를 응용하여 문제를 해결

<br><br>

### ⚠️ 문제 2
- Inventory에서 다루는 모든 Data에 대한 처리를 InventoryController 내부에서 수행
- 하나의 Controller에서 모든 Data에 대한 UI 갱신을 처리하면 코드가 매우 길고 복잡해질 것이라고 판단하여,<br>
UI 클래스에서 Controller를 참조하여 필요한 Data를 가져오도록 구현
- 하지만 이는 결과적으로 Controller와 View가 상호 참조 관계를 가지며, 의존성이 상당히 높아지게 됨<br>
-> Tab의 종류가 늘어나면 확장성이 떨어지며 예외 처리 증가로 인한 오류 발생 가능성이 높아질 수 있음

<br>

### 🛠️ 시도
- InventoryController -> UI_Inventory의 단방향 방식으로 변경<br>
-> InventoryController 내부에서 다루는 Data 종류가 다양하며, 이를 switch문이나 if문을 사용하여 처리하는 것은 확장성이 떨어진다고 판단<br>
-> Tab의 종류가 증가해도 쉽게 확장할 수 있는 구조를 만들 수는 없을까?

<br>

### 💡 선택
- 각 Tab에 대한 Data를 처리하는 Controller를 따로 생성
-> 해당 Controller들은 기존의 InventoryController를 상속<br>
- Controller들을 관리하는 InventoryHandler를 추가
-> Tab이 변경되면 InventoryHandler는 현재 Tab에 대응하는 Controller로 바꾸어 동작 실행



[🌙 목차로 돌아가기](#crescent_moon-목차)

<br>
