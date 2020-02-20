using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingIcon : MonoBehaviour
{
    public Transform loading;
    // Start is called before the first frame update
    public void On()
    {
        gameObject.SetActive(true);
    }
    public void Off()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        loading.localEulerAngles += new Vector3(0,0,1) * Time.deltaTime * 100;
    }
}
