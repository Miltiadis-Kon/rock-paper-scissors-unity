using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMananger : MonoBehaviour
{
    public GameMngr manager;

    //Define life and death assets
    public GameObject life;
    public GameObject death;

    public Transform userLives;
    public Transform cpuLives;

    // Define texts
    public TMP_Text userOption;
    public TMP_Text userOptionDark;

    public TMP_Text comOption;
    public TMP_Text comOptionDark;

    public TMP_Text countdownTxt;

    public Button guideBtn;
    public Transform guidePanel;

    // Test variables
    public int total_lives = 5;
    public int user_lives = 5;
    public int cpu_lives = 5;
    

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

        SetupLives();
    }

    public void ShowChoices(string userChoice, string cpuChoice)
    {
        userOption.text = userOptionDark.text = userChoice;
        comOption.text = comOptionDark.text = cpuChoice;
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.newGame)
        {
        string _option = manager.previousOption;
        userOption.text = userOptionDark.text =  _option;
        }

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


    private void SetupLives()
    {
        UpdateLives(total_lives, user_lives, userLives);
        UpdateLives(total_lives, cpu_lives, cpuLives);
    }

    public void ShowWinner(string winner)
    {
        if (winner == "Player")
        {
            cpu_lives--;
            UpdateLives(total_lives, cpu_lives, cpuLives);
            LogWinner(1);
        }
        else if (winner == "CPU")
        {
            user_lives--;
            UpdateLives(total_lives, user_lives, userLives);
            LogWinner(-1);
        }
        else
        {
            LogWinner(0);
        }
    }

    private void LogWinner(int winner)
    {
        switch (winner)
        {
            case 1:
                countdownTxt.text = "You win!";
                break;
            case -1:
                countdownTxt.text = "You lost!";
                break;
            case 0:
                countdownTxt.text = "It's a tie!";
                break;
            case 2:
                if (user_lives > cpu_lives && cpu_lives <= 0)
                {
                    userOption.text = userOptionDark.text = "You won!";
                }
                else
                {
                    userOption.text = userOptionDark.text = "You lost!";
                }
                ResetGame();
                break;
        }
    }

    private void ResetGame()
    {
        user_lives = total_lives;
        cpu_lives = total_lives;
        manager.newGame = false;

        countdownTxt.text = "Thumb up to play!";
        SetupLives();
    }


    private void UpdateLives(int total_lives,int lives,Transform parent)
    {
        if (lives< 0)
        {
           LogWinner(2);
            // Game end
        }
        else
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < lives; i++)
            {
                GameObject newLife = Instantiate(life, parent);
            }

            for (int i = 0; i < total_lives - lives; i++)
            {
                GameObject newDeath = Instantiate(death, parent);
            }
        }
    }

}
