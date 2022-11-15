using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlankGoal
{
    public int numberNeeded;
    public int numberCollected;
    public Sprite goalSprite;
    public string matchValue;
}
public class GoalManager : MonoBehaviour
{
    public BlankGoal[] levelGoals;
    public List<GoalPanel> currentGoals = new List<GoalPanel>();
    public GameObject goalPrefab;
    public GameObject goalIntroParents;
    public GameObject goalGameParents;
    private Board board;

    private EndGameManager endGame;

    void Start()
    {
        board = FindObjectOfType<Board>();
        endGame = FindObjectOfType<EndGameManager>();
        GetGoals();
        SetUpGoals();
    }

    public void GetGoals()
    {
        if (board != null)
        {
            if(board.world != null)
            {
                if (board.level < board.world.levels.Length)
                {
                    if (board.world.levels[board.level] != null)
                    {
                        levelGoals = board.world.levels[board.level].levelGoals;
                        for(int i = 0; i < levelGoals.Length; i++)
                        {
                            levelGoals[i].numberCollected = 0;
                        }
                    }
                }
            }
        }
    }
    public void SetUpGoals()
    {
        for(int i=0; i< levelGoals.Length; i++)
        {
            //Create a new Goal Panel at the goalIntroParents Position;
            GameObject goal = Instantiate(goalPrefab, goalIntroParents.transform.position, Quaternion.identity);
            goal.transform.SetParent(goalIntroParents.transform, false);
            //Set the image and text of the goal: 
            GoalPanel panel = goal.GetComponent<GoalPanel>();
            panel.thisSprite = levelGoals[i].goalSprite;
            panel.thisString = "0/" + levelGoals[i].numberNeeded;


            //Create a new Goal Panel at the goalGameParents Position;
            GameObject gameGoal = Instantiate(goalPrefab, goalGameParents.transform.position, Quaternion.identity);
            gameGoal.transform.SetParent(goalGameParents.transform, false);
            //Set the image and text of the goal: 
            panel = gameGoal.GetComponent<GoalPanel>();
            currentGoals.Add(panel);
            panel.thisSprite = levelGoals[i].goalSprite;
            panel.thisString = "0/" + levelGoals[i].numberNeeded;
        }
    }

    public void UpdateGoals()
    {
        int goalsCompleted = 0;
        for (int i = 0; i < levelGoals.Length; i++)
        {
            currentGoals[i].thisText.text = "" + levelGoals[i].numberCollected + "/" + levelGoals[i].numberNeeded;
            if(levelGoals[i].numberCollected >= levelGoals[i].numberNeeded)
            {
                goalsCompleted++;
                currentGoals[i].thisText.text = "" + levelGoals[i].numberNeeded + "/" + levelGoals[i].numberNeeded;
            }
        }
        if(goalsCompleted >= levelGoals.Length)
        {
            if(endGame != null)
            {
                endGame.WinGame();
            }
            Debug.Log("You win!");
        }
    }

    public void CompareGoal(string goaltoCompare)
    {
        for(int i = 0; i < levelGoals.Length; i++)
        {
            if(goaltoCompare == levelGoals[i].matchValue)
            {
                levelGoals[i].numberCollected++;

            }
        }
    }
}
