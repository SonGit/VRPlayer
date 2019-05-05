using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class SwiftForUnity : MonoBehaviour
{
    #region Declare external C interface

#if UNITY_IOS && !UNITY_EDITOR
        
    [DllImport("__Internal")]
    private static extern string _sayHiToUnity();
    
#endif

    #endregion

    #region Wrapped methods and properties

    /// <summary>
    /// Get string array of video on ios phone and also build thumbnail to Application.persistentDatapath/localTemp folder
    /// </summary>
    /// <returns>String.</returns>
    public static string getURLAndBuildThumbnail()
    {
#if UNITY_IOS && !UNITY_EDITOR
        return _sayHiToUnity();
#else
        return "No Swift found!";
#endif
    }

    #endregion

    #region Singleton implementation

    private static SwiftForUnity _instance;

    public static SwiftForUnity Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject("SwiftUnity");
                _instance = obj.AddComponent<SwiftForUnity>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    #endregion
}
