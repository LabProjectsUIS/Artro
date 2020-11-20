using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KneeUIManager : MonoBehaviour {

    [SerializeField]
    private GameObject _homeUI;

    [SerializeField]
    private GameObject _horizontalUI;

    [SerializeField]
    private GameObject _verticalUI;

    [SerializeField]
    private GameObject _introUI;

    [SerializeField]
    private GameObject _backButton;

    [SerializeField]
    private GameObject _forwardButton;

    [SerializeField]
    private GameObject _optionsPanel;

    [SerializeField]
    private GameObject _horizontalTitle;

    [SerializeField]
    private GameObject _objective;

    [SerializeField]
    private GameObject _leftContainer;

    [SerializeField]
    private GameObject _rightContainer;

    [SerializeField]
    private GameObject _rotulaMarker;

    [SerializeField]
    private GameObject _indications;

    [SerializeField]
    private GameObject _information;

    [SerializeField]
    private GameObject _succed;

    private int _navigationIndx = 0;
    // Use this for initialization
    void Start () {
        HideUIElements();
        _backButton.SetActive(false);
        _forwardButton.SetActive(false);
    }

    private void HideUIElements()
    {
        _homeUI.SetActive(true);
        _introUI.SetActive(false);
        _horizontalUI.SetActive(false);
        _verticalUI.SetActive(false);
        _optionsPanel.SetActive(true);
        //_backButton.SetActive(false);
        //_forwardButton.SetActive(false);
        _horizontalTitle.SetActive(false);
        _leftContainer.SetActive(false);
        _rightContainer.SetActive(false);
        _rotulaMarker.SetActive(false);
        _objective.SetActive(false);
        _indications.SetActive(false);
        _information.SetActive(false);
        _succed.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            NavigateBackward();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NavigateForward();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InitApp();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            InitWork();
        }
    }

    public void InitApp()
    {
        _homeUI.SetActive(false);
        _introUI.SetActive(true);
    }

    public void InitWork()
    {
        _introUI.SetActive(false);
        _horizontalUI.SetActive(true);
        _verticalUI.SetActive(true);
        _optionsPanel.SetActive(true);
        _backButton.SetActive(false);
        _forwardButton.SetActive(true);
        _horizontalTitle.SetActive(false);
        _leftContainer.SetActive(false);
        _rightContainer.SetActive(false);
        _rotulaMarker.SetActive(false);
        _objective.SetActive(false);
        _indications.SetActive(false);
        _information.SetActive(false);
    }

    public void NavigateForward()
    {
        if (_navigationIndx >= 4)
            return;
        ++_navigationIndx;
        //_backButton.SetActive(_navigationIndx == 0 ? false : true);
        HideUIElements();
        _horizontalUI.SetActive(true);
        _verticalUI.SetActive(true);
        SetPage(_navigationIndx);
    }

    public void SetPage(int indx)
    {
        switch (_navigationIndx)
        {
            case 0:
                _optionsPanel.SetActive(true);
                _backButton.SetActive(false);
                //_forwardButton.SetActive(true);
                _rotulaMarker.SetActive(false);
                _homeUI.SetActive(false);
                _horizontalTitle.SetActive(false);
                break;
            case 1:
                _homeUI.SetActive(false);
                _backButton.SetActive(true);
                //_forwardButton.SetActive(true);
                _optionsPanel.SetActive(false);
                _objective.SetActive(true);
                _horizontalTitle.SetActive(true);
                _rotulaMarker.SetActive(false);
                break;
            case 2:
                _homeUI.SetActive(false);
                //_backButton.SetActive(true);
                //_forwardButton.SetActive(true);
                _leftContainer.SetActive(true);
                _objective.SetActive(true);
                _optionsPanel.SetActive(false);
                _horizontalTitle.SetActive(true);
                _rightContainer.SetActive(true);
                _rotulaMarker.SetActive(true);
                break;
            case 3:
                _homeUI.SetActive(false);
                //_backButton.SetActive(true);
                _forwardButton.SetActive(true);
                _leftContainer.SetActive(false);
                _objective.SetActive(true);
                _rightContainer.SetActive(false);
                _horizontalTitle.SetActive(true);
                _optionsPanel.SetActive(false);
                _indications.SetActive(true);
                _rotulaMarker.SetActive(true);
                _information.SetActive(true);
                break;
            case 4:
                //_backButton.SetActive(true);
                _homeUI.SetActive(false);
                _information.SetActive(false);
                _objective.SetActive(true);
                _optionsPanel.SetActive(false);
                _forwardButton.SetActive(false);
                _indications.SetActive(true);
                _horizontalTitle.SetActive(true);
                _rotulaMarker.SetActive(true);
                _succed.SetActive(true);
                break;
        }
    }

    public void NavigateBackward()
    {
        if (_navigationIndx <= 0)
            return;
        --_navigationIndx;
        //_forwardButton.SetActive(_navigationIndx == 5 ? false : true);
        HideUIElements();
        _horizontalUI.SetActive(true);
        _verticalUI.SetActive(true);
        SetPage(_navigationIndx);
    }
}
