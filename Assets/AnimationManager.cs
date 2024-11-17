using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator userAnimator;
    public Animator computerAnimator;

    public void PlayUserAnimation(string option)
    {
        switch (option)
        {
            case "Rock":
                userAnimator.Play("White Rock");
                break;
            case "Paper":
                userAnimator.Play("White Paper");
                break;
            case "Scissors":
                userAnimator.Play("White Scissors");
                break;
            default:
                break;
        }
    }

    public void PlayComputerAnimation(string option)
    {
        switch (option)
        {
            case "Rock":
                computerAnimator.Play("Rock Black");
                break;
            case "Paper":
                computerAnimator.Play("Paper Black");
                break;
            case "Scissors":
                computerAnimator.Play("Scissors Black");
                break;
            default:
                break;
        }
    }


}
