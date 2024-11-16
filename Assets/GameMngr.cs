using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMngr : MonoBehaviour
{
    public enum Choice
    {
        Rock,
        Paper,
        Scissors
    }

    public bool newGame = false; // Flag to prevent new game until current one is finished
    
    public UIMananger uiManager;

    public string previousOption = "";


    void Awake()
    {
        previousOption = "";
        if (uiManager == null)
        {
            uiManager = GameObject.Find("UIManager").GetComponent<UIMananger>();
        }
        
    }


    // Update is called once per frame
     void Update()
     {
         if (Input.GetKeyDown(KeyCode.Space))
         {
             StartCoroutine(StartCountdownAndPlay());
         }
     }


    public void UpdateUserOption(string option)
    {

        if (option != previousOption)
        {
            if (option=="Yes" && !newGame)
            {
            StartCoroutine(StartCountdownAndPlay());
            }
            else if (option == "OK" || option == "Pointer" || (option == "Yes" && newGame) || (option == "No" && newGame))
            {
                option = "";
            }
            else if (option == "No" && !newGame)
            {
                uiManager.ShowChoices("", "Goodbye!");
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
            Debug.Log($"Received option: {option}");
            previousOption = option;
        }
    }

    public Choice playerChoice;
    public Choice cpuChoice;

    public void PlayGame()
    {
        newGame = false; // Do not allow new game until this one is finished
        if (previousOption=="") 
        {
            Debug.Log("Player choice is empty");
            string[] options = { "Rock", "Paper", "Scissors" };
            previousOption = options[Random.Range(0, 3)];
        }

        cpuChoice = (Choice)Random.Range(0, 3);
        playerChoice = (Choice)System.Enum.Parse(typeof(Choice), previousOption);
        uiManager.ShowChoices($"{playerChoice}", $"{cpuChoice}");
        DetermineWinner();
        previousOption = ""; // Reset the player choice 
        }

    private void DetermineWinner()
    {
        if (playerChoice == cpuChoice)
        {
            uiManager.ShowWinner("Draw");
        }
        else if ((playerChoice == Choice.Rock && cpuChoice == Choice.Scissors) ||
                 (playerChoice == Choice.Paper && cpuChoice == Choice.Rock) ||
                 (playerChoice == Choice.Scissors && cpuChoice == Choice.Paper))
        {
            uiManager.ShowWinner("Player");
        }
        else
        {
            uiManager.ShowWinner("CPU");
        }
    }

    public IEnumerator StartCountdownAndPlay()
    {
        int countdown = 3;
        newGame = true;
        uiManager.ShowChoices("", "Thinking..."); // Clear the choices
        while (countdown > 0)
        {
            uiManager.animateCountdown(countdown);
            yield return new WaitForSeconds(1);
            countdown--;
        }
        uiManager.animateCountdown(countdown);
        PlayGame();
    }


}
