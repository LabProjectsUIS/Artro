using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class Structure : MonoBehaviour {

    [SerializeField]
    private Text _indxText;

    [SerializeField]
    private Text _estructuraText;

    [SerializeField]
    private Text _titleText;

    [SerializeField]
    private Image _estructuraImage;

    [SerializeField]
    private GameObject _rodilla;

    [SerializeField]
    private GameObject _container;

    [SerializeField]
    private GameObject _structureButton;

    [SerializeField]
    private GameObject _structureInfo;

    [SerializeField]
    private GameObject _backButton;

    [SerializeField]
    private GameObject _nextButton;

    [SerializeField]
    private GameObject _prevButton;

    private XmlNodeList _estructurasList;
    private List<XmlNode> _estructuras;
    private int _currentIndx = 0;
    private int _currentStructure = 0;

    void Start () {
        XmlDocument _xml = new XmlDocument();
        _xml.Load(Application.dataPath + "/Resources/Estructuras.xml");
        _estructurasList = _xml.GetElementsByTagName("estructura");
        Debug.Log(_estructurasList.Count + " estructuras");
        Debug.Log("ruta de la aplicación: " + Application.dataPath + "/Resources/Tutorial.xml");
        int dummy = 0;
        foreach (XmlNode node in _estructurasList)
        {
            int i = dummy;
            Debug.Log(node.ChildNodes[0].Attributes["image"].Value);
            //node.ChildNodes[0].Attributes["image"];
            GameObject dummyButton = Instantiate(_structureButton, _container.transform);
            dummyButton.GetComponentInChildren<Text>().text = node.Attributes["name"].Value;
            dummyButton.GetComponent<Button>().onClick.AddListener(delegate { ShowKneePart(i-1); });
            dummyButton.GetComponent<Button>().onClick.AddListener(delegate { SetStructure(i-1); });
            i++;
            dummy = i;
        }
        _estructuraImage.sprite = Resources.Load<Sprite>(_estructurasList[0].ChildNodes[0].Attributes["image"].Value);
    }

    void Update () {
		
	}

    public void SetStructure(int indx)
    {
        _currentIndx = 0;
        _currentStructure = indx;
        _estructuraImage.sprite = Resources.Load<Sprite>(_estructurasList[_currentStructure].ChildNodes[0].Attributes["image"].Value);
        _estructuraText.text = _estructurasList[_currentStructure].ChildNodes[0].InnerText;
        _titleText.text = _estructurasList[_currentStructure].Attributes["name"].Value;
        _indxText.text = "1 / " + (_estructurasList[_currentStructure].ChildNodes.Count).ToString();
    }

    public void NextPage()
    {
        if(_currentIndx >= _estructurasList[_currentStructure].ChildNodes.Count-1)
        {
            _nextButton.SetActive(false);
        }
        else
        {
            _nextButton.SetActive(true);
            _currentIndx++;
        }
        _estructuraImage.sprite = Resources.Load<Sprite>(_estructurasList[_currentStructure].ChildNodes[_currentIndx].Attributes["image"].Value);
        _estructuraText.text = _estructurasList[_currentStructure].ChildNodes[_currentIndx].InnerText;
        _titleText.text = _estructurasList[_currentStructure].Attributes["name"].Value;
        _indxText.text = (_currentIndx+1) + " / " + (_estructurasList[_currentStructure].ChildNodes.Count).ToString();
        if(_currentIndx > 0)
        {
            _prevButton.SetActive(true);
            _backButton.SetActive(false);
        }
    }

    public void PrevPage()
    {
        if (_currentIndx <= 0)
        {
            _prevButton.SetActive(false);
            _backButton.SetActive(true);
        }
        else
        {
            _prevButton.SetActive(true);
            _backButton.SetActive(false);
            _currentIndx--;
        }
        _estructuraImage.sprite = Resources.Load<Sprite>(_estructurasList[_currentStructure].ChildNodes[_currentIndx].Attributes["image"].Value);
        _estructuraText.text = _estructurasList[_currentStructure].ChildNodes[_currentIndx].InnerText;
        _titleText.text = _estructurasList[_currentStructure].Attributes["name"].Value;
        _indxText.text = (_currentIndx+1) + " / " + (_estructurasList[_currentStructure].ChildNodes.Count).ToString();
        if (_currentIndx <= 0)
        {
            _prevButton.SetActive(false);
            _backButton.SetActive(true);
        }
    }

    public void ShowKneePart(int part)
    {
        BoxCollider[] kneeParts = _rodilla.GetComponentsInChildren<BoxCollider>(true);

        foreach(BoxCollider kneePart in kneeParts)
        {
            kneePart.gameObject.SetActive(false);
        }

        kneeParts[part].gameObject.SetActive(true);
        gameObject.SetActive(false);
        _structureInfo.SetActive(true);
    }
}
