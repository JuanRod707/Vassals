using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndTurnBtn : MonoBehaviour
{
    public Player DesignatedPlayer;

    public void EndTurn()
    {
        this.DesignatedPlayer.EndTurn();
    }
}
