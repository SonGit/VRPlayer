using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLoading : MonoBehaviour
{
	public static ScreenLoading instance;

	[SerializeField] private GameObject rootLoading;

	void Awake()
	{
		instance = this;
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Play()
	{
		if (rootLoading != null) {
			rootLoading.SetActive(true);
		}
	}

	public void Stop()
	{
		if (rootLoading != null) {
			rootLoading.SetActive(false);
		}
	}
}
