using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMananger : MonoBehaviour
{
    public GameMngr manager;


    // Define texts
    public TMP_Text userOption;
    public TMP_Text userOptionDark;

    public TMP_Text comOption;
    public TMP_Text comOptionDark;

    public TMP_Text countdownTxt;

    public Button guideBtn;
    public Transform guidePanel;

    private int userWins=0;
    private int cpuWins=0;

    public TMP_Text userWinsText;
    public TMP_Text cpuWinsText;


    public void ShowHideGuide()
    {
        guidePanel.gameObject.SetActive(!guidePanel.gameObject.activeSelf);
    } 

        void Awake()
    {
        if(manager==null)
        manager = GameObject.Find("GameMananger").GetComponent<GameMngr>();
    }

    // Start is called before the first frame update
    void Start()
    {
        userOption.text = "...";
        userOptionDark.text = "...";

        comOption.text = "...";
        comOptionDark.text = "...";

        countdownTxt.text = "Thumb up to play!";

        guideBtn.onClick.AddListener(ShowHideGuide);

        userWinsText.text = "0";
        cpuWinsText.text = "0";
        userWins = 0;
        cpuWins = 0;
    }

    public void ShowChoices(string userChoice, string cpuChoice)
    {
        if (userChoice != "-")
        userOption.text = userOptionDark.text = userChoice;

        if (cpuChoice != "-")
        comOption.text = comOptionDark.text = cpuChoice;
    }


    public void animateCountdown(int countdown)
    {
        if (countdown <= 0)
        {
            countdownTxt.text = "";
        }
        else
        countdownTxt.text = countdown.ToString();
    }


    public void ShowWinner(string winner)
    {
        if (winner == "Player")
        {
            userWins++;
            userWinsText.text = userWins.ToString();
            countdownTxt.text = "You win!";
        }
        else if (winner == "CPU")
        {
            cpuWins++;
            cpuWinsText.text = cpuWins.ToString();
            countdownTxt.text = "You lost!";
        }
        else
        {
            countdownTxt.text = "It's a tie!";
        }
    }

}
