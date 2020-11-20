using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class UITutorial : MonoBehaviour {

    [SerializeField]
    private Text _tutorialText;
    [SerializeField]
    private GameObject _backButton;
    [SerializeField]
    private GameObject _nextButton;
    [SerializeField]
    private GameObject _prevButton;
    

    private int _currentPage = 0;

	// Use this for initialization
	void Start () {
        Assert.IsNotNull(_tutorialText, "no se ensamblo el texto del tutorial");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InitTutorialUI(XmlNode xmlNode)
    {        
        _currentPage = 0;
        _tutorialText.text = xmlNode.ChildNodes[_currentPage].InnerText;
        //Debug.Log("texto mostrado mostrado: " + xmlNode.ChildNodes[_currentPage].InnerText);
        _backButton.SetActive(_currentPage + 1 == xmlNode.ChildNodes.Count ? true : false);
        if(_currentPage + 1 == xmlNode.ChildNodes.Count)
        {
            _nextButton.SetActive(false);
            _prevButton.SetActive(false);
        }
        else
        {
            _nextButton.SetActive(true);
            _prevButton.SetActive(false);
        }
    }

    public void BackTutorials()
    {

    }
}
