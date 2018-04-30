using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public TurnControl Controller;
    public string HouseName;
    public Character Patriarch;
    public bool IsAIPlayer;

    private bool isAwake;

    public void StartTurn()
    {
        isAwake = true;
    }

    void Update()
    {
        if (IsAIPlayer && isAwake)
        {
            Debug.Log(string.Format("Playing turn for house {0}", HouseName));
            EndTurn();
        }
    }

    public void EndTurn()
    {
        isAwake = false;
        Controller.TurnEnded(this);
    }
}
