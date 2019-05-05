using UnityEngine;

public class BasicPanel : MonoBehaviour
{
    [SerializeField] private GameObject Root;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
		if (_canvasGroup == null) {
			_canvasGroup = Root.AddComponent<CanvasGroup>();
			_canvasGroup.alpha = 0.0f;
		}

    }

    protected virtual void Start()
    {

    }

    public void SetActive(bool value)
    {
        Root.SetActive(value);
        StopAllCoroutines();

		if (_canvasGroup == null) {
			_canvasGroup = Root.AddComponent<CanvasGroup>();
			_canvasGroup.alpha = 0.0f;
		}

        StartCoroutine(value
            ? Utils.FadeIn(_canvasGroup, 1.0f, 0.5f)
            : Utils.FadeOut(_canvasGroup, 0.0f, 0.5f));
    }
}