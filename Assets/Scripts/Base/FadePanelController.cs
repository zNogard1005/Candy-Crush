using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePanelController : MonoBehaviour
{
    public Animator panelAnimator;
    public Animator gameInfoAnimator;
    
    public void OK()
    {
        if (panelAnimator != null && gameInfoAnimator != null)
        {
            panelAnimator.SetBool("Out", true);
            gameInfoAnimator.SetBool("Out", true);
            StartCoroutine(GameStartCo());
        }
    }

    public void GameOver()
    {
        panelAnimator.SetBool("Out", false);
        panelAnimator.SetBool("Game Over", true);
    }

    IEnumerator GameStartCo()
    {
        yield return new WaitForSeconds(1f);
        Board board = FindObjectOfType<Board>();
        board.currentState = GameState.move;

    }
}
