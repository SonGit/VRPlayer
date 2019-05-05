using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

	public GameObject parentPiece;

    // Update is called once per frame
    void Update()
    {
		Vector3 pos = transform.position;
		transform.position = parentPiece.transform.position;
		transform.rotation =  parentPiece.transform.rotation;
    }
}
