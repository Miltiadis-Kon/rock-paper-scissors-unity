using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator userAnimator;
    public Animator computerAnimator;

    public Transform blackHand;

    public void PlayAnimation(string option, bool isUser)
    {
        Animator animator = isUser ? userAnimator : computerAnimator;
        switch (option)
        {
            case "Rock":
                animator.SetBool("rock", true);
                break;
            case "Paper":
                animator.SetBool("paper", true);
                break;
            case "Scissors":
                animator.SetBool("scissors", true);
                break;
            default:
                break;
        }
    }

    public void ResetAnimations()
    {
        userAnimator.SetBool("rock", false);
        userAnimator.SetBool("paper", false);
        userAnimator.SetBool("scissors", false);

        computerAnimator.SetBool("rock", false);
        computerAnimator.SetBool("paper", false);
        computerAnimator.SetBool("scissors", false);
    }
}
