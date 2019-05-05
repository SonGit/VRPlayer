using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace NavigationDrawer.UI
{
	public class AccessMenu : BasicMenu, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
		[Header("Components")]
		private BasicButtonMenu[] basicButtonMenus;
		private BasicButtonMenu btnMyStorage;
		private BasicButtonMenu btnLogin;
		private BasicButtonMenu btnLogout;
		private BasicButtonMenu btnMyVideo;
		private BasicButtonMenu btnFavorite;
		private BasicButtonMenu btnDownload;
		private BasicButtonMenu btnInbox;
		private BasicButtonMenu btnSettings;
		private BasicButtonMenu btnButtonUsageInformation;
		private BasicButtonMenu btnCloseSideMenu;

		[HideInInspector] public RawImage checkerMyStorage;
		[HideInInspector] public RawImage checkerMyVideo;
		[HideInInspector] public RawImage checkerFavorite;
		[HideInInspector] public RawImage checkerDownload;
		[HideInInspector] public RawImage checkerInbox;
		[HideInInspector] public RawImage checkerSettings;
		[HideInInspector] public RawImage checkerUsageInformation;
		public RawImage currentchecker;

		public event Action OnMyStorage;
		public event Action OnSettings;
		public event Action OnLogin;
		public event Action OnLogout;
		public event Action OnMyVideo;
		public event Action OnClose;
		public event Action OnFavorite;
		public event Action OnDownload;
		public event Action OnInbox;

		private RectTransform rectTransformS;

		public enum ENavigation
		{
			Left,
			Right
		}
			
		public Image ImgBackground;
		public GameObject PanelLayer;
		public GameObject handle;

		[SerializeField, Header("Properties")]
		public ENavigation NavigationType;
		public bool DarkenBackground = true;
		public bool TapBackgroundToClose;
		public bool OpenOnStart;
		public float AnimationDuration = 0.5f;

		private int _animState;
		private float _maxPosition;
		private float _minPosition;
		private float _animStartTime;
		private float _animDeltaTime;
		private float _currentBackgroundAlpha;

		private RectTransform _rectTransform;
		private RectTransform _backgroundRectTransform;

		private GameObject _backgroundGameObject;

		private CanvasGroup _backgroundCanvasGroup;

		private Vector2 _currentPos;
		private Vector2 _tempVector2;

		private void Awake()
		{
			base.Awake ();
			_rectTransform = gameObject.GetComponent<RectTransform>();
			_backgroundRectTransform = ImgBackground.GetComponent<RectTransform>();
			_backgroundCanvasGroup = ImgBackground.GetComponent<CanvasGroup>();
		}

		protected override void Start()
		{
			base.Start ();

			basicButtonMenus = GetComponentsInChildren<BasicButtonMenu> ();
			if (basicButtonMenus.Length > 0) {
				foreach (BasicButtonMenu basicButtonMenu in basicButtonMenus) {
					switch (basicButtonMenu.name) {
					case "MyStorage": 
						btnMyStorage = basicButtonMenu;
						break;
					case "ButtonLogin": 
						btnLogin = basicButtonMenu;
						break;
					case "ButtonLogout": 
						btnLogout = basicButtonMenu;
						break;
					case "ButtonVideoList": 
						btnMyVideo = basicButtonMenu;
						break;
					case "ButtonFavorite": 
						btnFavorite = basicButtonMenu;
						break;
					case "ButtonDownload": 
						btnDownload = basicButtonMenu;
						break;
					case "ButtonInbox": 
						btnInbox = basicButtonMenu;
						break;
					case "ButtonPreferences": 
						btnSettings = basicButtonMenu;
						break;
					case "ButtonUsageInformation": 
						btnButtonUsageInformation = basicButtonMenu;
						break;
					case "BackgroundCloseSideMenu": 
						btnCloseSideMenu = basicButtonMenu;
						break;
					default:
						break;
					}
				}
			} else {
				Debug.LogError ("basicButtonMenus[] Null");
			}

			rectTransformS = this.GetComponent<RectTransform> ();

			// Init event button
			if (btnMyStorage != null) {
				btnMyStorage.OnClick += btnMyStorage_OnClick;
				checkerMyStorage = btnMyStorage.GetComponentInChildren<RawImage> ();
				if (checkerMyStorage != null){
					checkerMyStorage.enabled = true;
					currentchecker = checkerMyStorage;
				}

			} else {
				Debug.LogError ("btnMyStorage null");
			}

			if (btnSettings != null) {
				btnSettings.OnClick += btnSettings_OnClick;
				checkerSettings = btnSettings.GetComponentInChildren<RawImage> ();
				if (checkerSettings != null){
					checkerSettings.enabled = false;
				}
			} else {
				Debug.LogError ("btnSettings null");
			}

			if (btnLogin != null) {
				btnLogin.OnClick += btnLogin_OnClick;
			} else {
				Debug.LogError ("btnLogin null");
			}

			if (btnLogout != null) {
				btnLogout.OnClick += btnLogout_OnClick;
			} else {
				Debug.LogError ("btnLogout null");
			}

			if (btnMyVideo != null) {
				btnMyVideo.OnClick += btnMyVideo_OnClick;
				checkerMyVideo = btnMyVideo.GetComponentInChildren<RawImage> ();
				if (checkerMyVideo != null){
					checkerMyVideo.enabled = false;
				}
			} else {
				Debug.LogError ("btnMyVideo null");
			}

			if (btnCloseSideMenu != null) {
				btnCloseSideMenu.OnClick+= btnCloseSideMenu_OnClose;
			} else {
				Debug.LogError ("btnCloseSideMenu null");
			}

			if (btnFavorite != null) {
				btnFavorite.OnClick+= btnFavorite_OnClick;
				checkerFavorite = btnFavorite.GetComponentInChildren<RawImage> ();
				if (checkerFavorite != null){
					checkerFavorite.enabled = false;
				}
			} else {
				Debug.LogError ("btnBookmark null");
			}

			if (btnDownload != null) {
				btnDownload.OnClick+= btnDownload_OnClick;
				checkerDownload = btnDownload.GetComponentInChildren<RawImage> ();
				if (checkerDownload != null){
					checkerDownload.enabled = false;
				}
			} else {
				Debug.LogError ("btnDownload null");
			}

			if (btnInbox != null) {
				btnInbox.OnClick+= btnbtnInbox_OnClick;
				checkerInbox = btnInbox.GetComponentInChildren<RawImage> ();
				if (checkerInbox != null){
					checkerInbox.enabled = false;
				}
			} else {
				Debug.LogError ("btnInbox null");
			}

			if (btnButtonUsageInformation != null) {
				//btnRecommend.OnClick += btnMyVideo_OnClick;
				checkerUsageInformation = btnButtonUsageInformation.GetComponentInChildren<RawImage> ();
				if (checkerUsageInformation != null){
					checkerUsageInformation.enabled = false;
				}
			} else {
				Debug.LogError ("btnButtonUsageInformation null");
			}
			// Init event button


			if (NavigationType == ENavigation.Left)
			{
				_maxPosition = _rectTransform.rect.width / 2;
			}
			else if (NavigationType == ENavigation.Right)
			{
				_maxPosition = -_rectTransform.rect.width / 2;
			}

			_minPosition = -_maxPosition;

			RefreshBackgroundSize();

			_backgroundGameObject = ImgBackground.gameObject;

			if (OpenOnStart)
			{
				Open();
			}
			else
			{
				_backgroundGameObject.SetActive(false);
				PanelLayer.SetActive(false);
			}
		}

		// Event click Button
		private void btnMyStorage_OnClick()
		{
			if(MainAllController.instance != null){
				MainAllController.instance.PlayButtonSound ();
			}

			if (OnMyStorage != null)
			{
				OnMyStorage();
			}
		}

		private void btnSettings_OnClick()
		{
			if(MainAllController.instance != null){
				MainAllController.instance.PlayButtonSound ();
			}

			if (OnSettings != null)
			{
				OnSettings();
			}
		}

		private void btnLogin_OnClick()
		{
			if(MainAllController.instance != null){
				MainAllController.instance.PlayButtonSound ();
			}

			if (OnLogin != null)
			{
				OnLogin();
			}
		}

		private void btnLogout_OnClick()
		{
			if(MainAllController.instance != null){
				MainAllController.instance.PlayButtonSound ();
			}

			if (OnLogout != null)
			{
				OnLogout();
			}
		}

		private void btnMyVideo_OnClick()
		{
			if(MainAllController.instance != null){
				MainAllController.instance.PlayButtonSound ();
			}

			if (OnMyVideo != null)
			{
				OnMyVideo();
			}
		}

		private void btnCloseSideMenu_OnClose()
		{
			if (OnClose != null)
			{
				OnClose();
			}
		}

		private void btnFavorite_OnClick()
		{
			if(MainAllController.instance != null){
				MainAllController.instance.PlayButtonSound ();
			}

			if (OnFavorite != null)
			{
				OnFavorite();
			}
		}

		private void btnDownload_OnClick()
		{
			if(MainAllController.instance != null){
				MainAllController.instance.PlayButtonSound ();
			}

			if (OnDownload != null)
			{
				OnDownload();
			}
		}

		private void btnbtnInbox_OnClick()
		{
			if(MainAllController.instance != null){
				MainAllController.instance.PlayButtonSound ();
			}

			if (OnInbox != null)
			{
				OnInbox();
			}
		}
		// Event click Button

		public void InitViewable(){
			
			// HasUserLoggedIn ???
			if (MainAllController.instance != null && !MainAllController.instance.HasUserLoggedIn ()) {
				LogoutViewable (false);
				LoginViewable (true);
			}else{
				LogoutViewable (true);
				LoginViewable (false);
			}
		}

		public void MovePos(Vector2 position){
			rectTransformS.anchoredPosition = position;
		}


        private void Update()
        {
            if (_animState == 1)
            {
                _animDeltaTime = Time.realtimeSinceStartup - _animStartTime;

                if (_animDeltaTime <= AnimationDuration)
                {
                    _rectTransform.anchoredPosition = QuintOut(_currentPos, new Vector2(_maxPosition, _rectTransform.anchoredPosition.y), _animDeltaTime, AnimationDuration);
                    if (DarkenBackground)
                    {
                        _backgroundCanvasGroup.alpha = QuintOut(_currentBackgroundAlpha, 1f, _animDeltaTime, AnimationDuration);
                    }
                }
                else
                {
                    _rectTransform.anchoredPosition = new Vector2(_maxPosition, _rectTransform.anchoredPosition.y);
                    if (DarkenBackground)
                    {
                        _backgroundCanvasGroup.alpha = 1f;
                    }
                    _animState = 0;
                }
            }
            else if (_animState == 2)
            {
                _animDeltaTime = Time.realtimeSinceStartup - _animStartTime;
                if (_animDeltaTime <= AnimationDuration)
                {
                    _rectTransform.anchoredPosition = QuintOut(_currentPos, new Vector2(_minPosition, _rectTransform.anchoredPosition.y), _animDeltaTime, AnimationDuration);
                    if (DarkenBackground)
                    {
                        _backgroundCanvasGroup.alpha = QuintOut(_currentBackgroundAlpha, 0f, _animDeltaTime, AnimationDuration);
                    }
                }
                else
                {
                    _rectTransform.anchoredPosition = new Vector2(_minPosition, _rectTransform.anchoredPosition.y);
                    if (DarkenBackground)
                    {
                        _backgroundCanvasGroup.alpha = 0f;
                    }
                    _backgroundGameObject.SetActive(false);
                    PanelLayer.SetActive(false);

                    _animState = 0;
                }
            }

            if (NavigationType == ENavigation.Left)
            {
                _rectTransform.anchoredPosition = new Vector2(Mathf.Clamp(_rectTransform.anchoredPosition.x, _minPosition, _maxPosition), _rectTransform.anchoredPosition.y);
            }
            else if (NavigationType == ENavigation.Right)
            {
                _rectTransform.anchoredPosition = new Vector2(Mathf.Clamp(_rectTransform.anchoredPosition.x, _maxPosition, _minPosition), _rectTransform.anchoredPosition.y);
            }
        }

        public void BackgroundTap()
        {
            if (TapBackgroundToClose)
            {
                Close();
            }
        }

        public void Open()
        {
            RefreshBackgroundSize();
            _backgroundGameObject.SetActive(true);
            PanelLayer.SetActive(true);
            _currentPos = _rectTransform.anchoredPosition;
            _currentBackgroundAlpha = _backgroundCanvasGroup.alpha;
            _backgroundCanvasGroup.blocksRaycasts = true;
            _animStartTime = Time.realtimeSinceStartup;
            _animState = 1;

			InitViewable ();
        }

        public void Close()
        {
            _currentPos = _rectTransform.anchoredPosition;
            _currentBackgroundAlpha = _backgroundCanvasGroup.alpha;
            _backgroundCanvasGroup.blocksRaycasts = false;
            _animStartTime = Time.realtimeSinceStartup;
            _animState = 2;
        }

        private void RefreshBackgroundSize()
        {
            if (NavigationType == ENavigation.Left)
            {
                _backgroundRectTransform.sizeDelta = new Vector2(Screen.width *1.5f, _backgroundRectTransform.sizeDelta.y);
            }
            else if(NavigationType == ENavigation.Right)
            {
                _backgroundRectTransform.sizeDelta = new Vector2(Screen.width, _backgroundRectTransform.sizeDelta.y);
                _backgroundRectTransform.localPosition = new Vector2(-(_rectTransform.rect.width / 2), 0);
            }
        }

        public void OnBeginDrag(PointerEventData data)
        {
            RefreshBackgroundSize();

            _animState = 0;

            _backgroundGameObject.SetActive(true);
            PanelLayer.SetActive(true);

        }

        public void OnDrag(PointerEventData data)
        {
            _tempVector2 = _rectTransform.anchoredPosition;
            _tempVector2.x += data.delta.x;

            _rectTransform.anchoredPosition = _tempVector2;

            if (DarkenBackground)
            {
                _backgroundCanvasGroup.alpha = 1 - (_maxPosition - _rectTransform.anchoredPosition.x) / (_maxPosition - _minPosition);
            }
        }

        public void OnEndDrag(PointerEventData data)
        {
            if (NavigationType == ENavigation.Left)
            {
                if (Mathf.Abs(data.delta.x) >= 0.4f)
                {
					if (data.delta.x > 0.4f)
                    {
                        Open();
                    }
                    else
                    {
                        Close();
                    }
                }
                else
                {
                    if ((_rectTransform.anchoredPosition.x - _minPosition) >
                        (_maxPosition - _rectTransform.anchoredPosition.x))
                    {
                        Open();
                    }
                    else
                    {
                        Close();
                    }
                }
            }
            else if(NavigationType == ENavigation.Right)
            {
                if (Mathf.Abs(data.delta.x) >= 0.5f)
                {
                    if (data.delta.x < 0.5f)
                    {
                        Open();
                    }
                    else
                    {
                        Close();
                    }
                }
                else
                {
                    if ((_rectTransform.anchoredPosition.x - _minPosition) <
                        (_maxPosition - _rectTransform.anchoredPosition.x))
                    {
                        Open();
                    }
                    else
                    {
                        Close();
                    }
                }
            }
        }

        private Vector2 QuintOut(Vector2 startValue, Vector2 endValue, float time, float duration)
        {
            var tempVector2 = startValue;
            tempVector2.x = QuintOut(startValue.x, endValue.x, time, duration);
            tempVector2.y = QuintOut(startValue.y, endValue.y, time, duration);
            return tempVector2;
        }

        protected virtual float QuintOut(float startValue, float endValue, float time, float duration)
        {
            var differenceValue = endValue - startValue;
            time = Mathf.Clamp(time, 0f, duration);
            time /= duration;

            if (time == 0f)
            {
                return startValue;
            }

            if (time == 1f)
            {
                return endValue;
            }

            time--;
            return differenceValue * (time * time * time * time * time + 1) + startValue;
        }


		public void LogoutViewable(bool visible)
		{
			if (btnLogout != null)
				btnLogout.gameObject.SetActive (visible);
			else {
				Debug.LogError ("btnLogout null!");
			}
		}

		public void LoginViewable(bool visible)
		{
			if (btnLogout != null)
				btnLogin.gameObject.SetActive (visible);
			else {
				Debug.LogError ("btnLogout null!");
			}
		}

		public void CheckerViewable(RawImage checker, bool visible)
		{
			if (currentchecker != checker) {
				currentchecker.enabled = !visible;
				checker.enabled = visible;
				currentchecker = checker;
			}
		}

		public void SetHandleViewable(bool visible)
		{
			if (handle != null){
				handle.SetActive (visible);
			}else {
				Debug.LogError ("handle null!");
			}
		}
    }
}