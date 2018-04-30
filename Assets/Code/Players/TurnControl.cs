using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TurnControl : MonoBehaviour
{
    public int PlayerCount;
    public GameObject PlayerPrefab;
    public List<Player> ActivePlayers;
    public Player CurrentPlayer;
    public EndTurnBtn button;

    public void TurnEnded(Player player)
    {
        var lastIndex = ActivePlayers.IndexOf(player);
        var nextIndex = lastIndex == ActivePlayers.Count - 1 ? 0 : lastIndex + 1;
        ActivePlayers[nextIndex].StartTurn();
    }
    
	// Use this for initialization
	void Start () {
        CreatePlayers();
        CurrentPlayer = ActivePlayers.First();
        CurrentPlayer.StartTurn();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void CreatePlayers()
    {
        ActivePlayers = new List<Player>();
        var ip = Instantiate(PlayerPrefab, this.transform) as GameObject;
        var human = ip.GetComponent<Player>();
        human.Controller = this;
        human.HouseName = NameGenerator.GenerateHouseName();
        human.IsAIPlayer = false;
        ActivePlayers.Add(human);
        button.DesignatedPlayer = human;

        for (int i = 1; i < PlayerCount; i++)
        {
            ip = Instantiate(PlayerPrefab, this.transform) as GameObject;
            var player = ip.GetComponent<Player>();
            player.HouseName = NameGenerator.GenerateHouseName();
            player.Controller = this;
            while (ActivePlayers.Any(x => x.HouseName.Equals(player.HouseName)))
            {
                player.HouseName = NameGenerator.GenerateHouseName();
            }

            player.IsAIPlayer = true;
            ActivePlayers.Add(player);
        }
    }
}
