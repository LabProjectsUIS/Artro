using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private GameObject _tutorialButton;
    [SerializeField]
    private GameObject _tutorialPanel;
    [SerializeField]
    private GameObject _tutorialButtonsContainer;
    [SerializeField]
    private GameObject _tutorialContent;
    [SerializeField]
    private GameObject _toolsUI;
    [SerializeField]
    private GameObject _paramsUI;
    [SerializeField]
    private GameObject _mainUI;
    [SerializeField]
    private GameObject _tutorialsButtons;
    [SerializeField]
    private GameObject _showAnimButton;
    [SerializeField]
    private GameObject _hideAnimButton;

    private XmlNodeList _tutorialList;
    private List<XmlNode> _tutorials;
    private Animator _animator;
    
    void Start() {
        _animator = GetComponent<Animator>();
        Assert.IsNotNull(_tutorialButton, "no se encontro ensamblado el prefab de los botones para los tutoriales");
        Assert.IsNotNull(_tutorialPanel, "no se encontro ensamblado el panel para agregar los botones de los tutoriales");
        Assert.IsNotNull(_mainUI, "no se encontro ensamblado el componente de la interfaz principal");
        Assert.IsNotNull(_tutorialContent, "no se encontro ensamblado el contenedor de los tutoriales");
        Assert.IsNotNull(_tutorialsButtons, "no se encontro ensamblado el contenedor de los botones");
        Assert.IsNotNull(_toolsUI, "no se encontro ensamblado el contenedor de las herramientas");
        XmlDocument _xml = new XmlDocument();
        _xml.Load(Application.dataPath + "/Resources/Tutorial.xml");
        _tutorialList = _xml.GetElementsByTagName("tutorial");
        //Debug.Log(_tutorialList.Count + " tutoriales");
        //Debug.Log("ruta de la aplicación: " + Application.dataPath + "/Resources/Tutorial.xml");
        int dummy = 0;
        foreach(XmlNode node in _tutorialList)
        {
            int i = dummy;
            //Debug.Log(node.Attributes["name"].Value);
            GameObject dummyButton = Instantiate(_tutorialButton, _tutorialButtonsContainer.transform);
            dummyButton.GetComponentInChildren<Text>().text = node.Attributes["name"].Value;
            dummyButton.GetComponent<Button>().onClick.AddListener(delegate { ShowTutorial(i); });
            i++;
            dummy = i;
        }
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetBool("Show", !_animator.GetBool("Show"));
        }
    }

    public void ShowTools()
    {
        _mainUI.SetActive(false);
        _toolsUI.SetActive(true);
    }

    public void ShowTutorial(int tutorialIndx)
    {
        _tutorialsButtons.SetActive(false);
        _tutorialContent.SetActive(true);
        _tutorialContent.GetComponent<UITutorial>().InitTutorialUI(_tutorialList[tutorialIndx-1]);
    }

    public void HideTutorial()
    {
        _tutorialsButtons.SetActive(true);
        _tutorialContent.SetActive(false);
    }

    public void ShowTutorialsPanel()
    {
        _tutorialPanel.SetActive(true);
        _mainUI.SetActive(false);
    }
    public void ShowMainUI()
    {
        _mainUI.SetActive(true);
        _tutorialPanel.SetActive(false);
        _toolsUI.SetActive(false);
    }

    public void AnimUI(bool show)
    {
        if(_animator != null)
        {
            _animator.SetBool("Show", show);
        }
        if(show)
        {
            _showAnimButton.SetActive(false);
            _hideAnimButton.SetActive(true);
        }
        else
        {
            _showAnimButton.SetActive(true);
            _hideAnimButton.SetActive(false);
        }
    }

    public void ShowParams()
    {
        _mainUI.SetActive(false);
        _paramsUI.SetActive(true);
    }

    public void HideParams()
    {
        _mainUI.SetActive(true);
        _paramsUI.SetActive(false);
    }
}