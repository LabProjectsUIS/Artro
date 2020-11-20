using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ClickDetector : MonoBehaviour {
        
    private BoxCollider _renderer = null;
    private ArtroUI _kneeManager = null;

	// Use this for initialization
	void Start () {
        _renderer = GetComponent<BoxCollider>();
        Assert.IsNotNull(_renderer, "no se encontro ensamblado un material en el componente");
        _kneeManager = GameObject.FindObjectOfType<ArtroUI>();
        Assert.IsNotNull(_kneeManager, "no se encontro ensamblado un material en el componente");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("colisiono");
        _kneeManager.StartApp();
    }

    private void OnTriggerExit(Collider other)
    {

    }
}
