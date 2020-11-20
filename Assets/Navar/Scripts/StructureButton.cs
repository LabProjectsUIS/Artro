using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.K))
        {
            GetComponent<Animator>().SetBool("Show", !GetComponent<Animator>().GetBool("Show"));
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("colisiono");
        GetComponent<Animator>().SetBool("Show", !GetComponent<Animator>().GetBool("Show"));
    }
}
