using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ZoneDetector : MonoBehaviour {

    [SerializeField]
    private Structure _structure;

    [SerializeField]
    private int _index;

    private MeshRenderer _renderer = null;

	// Use this for initialization
	void Start () {
        _renderer = GetComponent<MeshRenderer>();
        Assert.IsNotNull(_renderer, "no se encontro ensamblado un material en el componente");
        _renderer.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("colisiono");
        _renderer.enabled = true;
        _structure.gameObject.SetActive(true);
        _structure.SetStructure(_index);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("se salio");
        _renderer.enabled = false;
        _structure.gameObject.SetActive(false);
        /*Color matColor = _renderer.material.color;
        matColor.a = 100.0f;
        _renderer.material.SetColor("_Color", matColor);*/
        //_renderer.material.color = matColor;
    }
}
