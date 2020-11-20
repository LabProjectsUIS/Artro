using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KneeConstructor : MonoBehaviour {

    [SerializeField]
    private GameObject _pointer;

    [SerializeField]
    private GameObject _interactiveCube;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_interactiveCube, _pointer.transform.position, Quaternion.identity, transform);
        }
	}
}
