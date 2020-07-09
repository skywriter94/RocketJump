using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby_UI : MonoBehaviour
{
    GameObject inputPanel;
    GameObject optionPanel;
    GameObject rankPanel;

    Button startButton;
    Button optionButton;
    Button rankButton;
    Button exitButton;

    GameObject nameInput;

    private void Start()
    {
        inputPanel = GameObject.Find("Canvas").transform.Find("InputNamePanel").gameObject; // 비활성화 오브젝트는 이렇게 찾음.
        optionPanel = GameObject.Find("Canvas").transform.Find("OptionPanel").gameObject;
        rankPanel = GameObject.Find("Canvas").transform.Find("RankPanel").gameObject;

        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        optionButton = GameObject.Find("OptionButton").GetComponent<Button>();
        rankButton = GameObject.Find("RankButton").GetComponent<Button>();
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();

        nameInput = GameObject.Find("Canvas").transform.GetChild(1).transform.GetChild(2).transform.GetChild(1).gameObject;
    }


    public void StartButton()
    {
        inputPanel.SetActive(true);
        DeactiveMenuButtons();
    }
    public void OptionButton()
    {
        optionPanel.SetActive(true);
        DeactiveMenuButtons();
    }
    public void RankButton()
    {
        rankPanel.SetActive(true);
        DeactiveMenuButtons();
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void ConfirmButton()
    {
        if(nameInput.GetComponent<Text>().text.Length != 0)
        {
            UsefulTools.instance.ChangeScene(1);
        }
    }

    public void CloseInputPanel()
    {
        inputPanel.SetActive(false);
        ActiveMenuButtons();
    }
    public void CloseOptionPanel()
    {
        optionPanel.SetActive(false);
        ActiveMenuButtons();
    }
    public void CloseRankPanel()
    {
        rankPanel.SetActive(false);
        ActiveMenuButtons();
    }

    void ActiveMenuButtons()
    {
        startButton.interactable = true;
        optionButton.interactable = true;
        rankButton.interactable = true;
        exitButton.interactable = true;
    }
    void DeactiveMenuButtons()
    {
        startButton.interactable = false;
        optionButton.interactable = false;
        rankButton.interactable = false;
        exitButton.interactable = false;
    }
}