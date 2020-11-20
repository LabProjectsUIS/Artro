using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtroUI : MonoBehaviour {

    [SerializeField]
    private GameObject _panel1;

    [SerializeField]
    private GameObject _panel2;

    [SerializeField]
    private GameObject _panel3;

    [SerializeField]
    private GameObject _panel4;

    [SerializeField]
    private GameObject _panel5;

    [SerializeField]
    private GameObject _panel6;

    [SerializeField]
    private GameObject _panel7;

    [SerializeField]
    private GameObject _panel8;

    [SerializeField]
    private GameObject _panel9;

    [SerializeField]
    private GameObject _panel10;

    [SerializeField]
    private GameObject _homePanel;

    [SerializeField]
    private GameObject _horizontalPanel;

    private int _currIndx = 1;
       
    // Use this for initialization
    void Start () {
        HidePanels();
        _horizontalPanel.SetActive(false);
    }

    public void StartApp()
    {
        _homePanel.SetActive(false);
        _panel1.SetActive(true);
        _horizontalPanel.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PrevPanel();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextPanel();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartApp();
        }
    }

    public void HidePanels()
    {
        _panel1.SetActive(false);
        _panel2.SetActive(false);
        _panel3.SetActive(false);
        _panel4.SetActive(false);
        _panel5.SetActive(false);
        _panel6.SetActive(false);
        _panel7.SetActive(false);
        _panel8.SetActive(false);
        _panel9.SetActive(false);
        _panel10.SetActive(false);
    }

    public void ShowPanel(int indx)
    {
        HidePanels();
        switch(indx)
        {
            case 1:
                _panel1.SetActive(true);
                break;

            case 2:
                _panel2.SetActive(true);
                break;

            case 3:
                _panel3.SetActive(true);
                break;

            case 4:
                _panel4.SetActive(true);
                break;

            case 5:
                _panel5.SetActive(true);
                break;

            case 6:
                _panel6.SetActive(true);
                break;

            case 7:
                _panel7.SetActive(true);
                break;

            case 8:
                _panel8.SetActive(true);
                break;

            case 9:
                _panel9.SetActive(true);
                break;

            case 10:
                _panel10.SetActive(true);
                break;
        }
    }

    public void NextPanel()
    {
        if(_currIndx < 10)
        {
            _currIndx++;
        }
        ShowPanel(_currIndx);
    }

    public void PrevPanel()
    {
        if (_currIndx > 1)
        {
            _currIndx--;
        }
        ShowPanel(_currIndx);
    }
}
