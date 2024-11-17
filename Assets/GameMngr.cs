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

    public bool inGame = false; // Flag to prevent new game until current one is finished
    
    public UIMananger uiManager;
    public AnimationManager animationManager;

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
        //Debug.Log($"Received message: {option}");
        // Reduce text preview cluttering
        if (option == previousOption)
        {
            return;
        }

       // Debug.Log($"Last option: {previousOption}");
        previousOption = option;
      //  Debug.Log($"Received message: {option}");
        Debug.Log($"Current option: {previousOption}");


        if (inGame) // If currently in game, filter menu options
        {
            uiManager.ShowChoices(previousOption,"-"); // Show option on UI for user to see
        }

        if (!inGame) // If not in game, filter game options
        {
            if (previousOption == "Yes")
            {
                Debug.Log("Starting game...");
                StartCoroutine(StartCountdownAndPlay()); // Start a new game
            }
            else if (previousOption == "No")
            {
                Debug.Log("Quitting game...");
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"); // Go back to main menu
            }
        }
    }

    public Choice playerChoice;
    public Choice cpuChoice;

    public void PlayGame()
    {
        if (previousOption=="") 
        {
            Debug.Log("Player choice is empty");
            string[] options = { "Rock", "Paper", "Scissors" };
            previousOption = options[Random.Range(0, 3)];
        }

        cpuChoice = (Choice)Random.Range(0, 3);
        playerChoice = (Choice)System.Enum.Parse(typeof(Choice), previousOption);

        StartCoroutine(AnimationHandler(playerChoice.ToString(), cpuChoice.ToString()));
        }

    public IEnumerator AnimationHandler(string playerChoice,string cpuChoice)
    {
        uiManager.countdownTxt.text = ""; // Clear countdown text
        uiManager.ShowChoices(" ", " "); // Clear choices text

        animationManager.PlayUserAnimation(playerChoice); // Play user animation
        animationManager.PlayComputerAnimation(cpuChoice); 
        yield return new WaitForSeconds(7); // Wait for animations to finish

        uiManager.ShowChoices($"{playerChoice}", $"{cpuChoice}"); // Show choices on UI
        DetermineWinner(); // Determine the winner
        ResetVariables(); // Reset variables
    }

    private void ResetVariables()
    {
        inGame = false;
        previousOption = "";
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
        Debug.Log("Enter");
        inGame = true;
        int countdown = 5;
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
