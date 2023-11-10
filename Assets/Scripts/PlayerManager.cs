using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;

public class PlayerManager : NetworkBehaviour
{
    public static PlayerManager instance;
    private void Awake()
    {
        instance = this;
    }

    public Dictionary<int, Player> players = new Dictionary<int, Player>();
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();

    public void DamagePlayer(int playerID, int damage)
    {
        if (!base.IsServer)
            return;

        players[playerID].health -= damage;
        print("Player " + playerID.ToString() + " health is " + players[playerID].health);

        if (players[playerID].health <= 0)
        {
            PlayerKilled(playerID);
        }
    }

    void PlayerKilled(int playerID)
    {
        //print("Player " + playerID.ToString() + " was killed by " + attackerID.ToString());
        players[playerID].deaths++;
        players[playerID].health = 100;
        //players[attackerID].kills++;

        RespawnPlayer(players[playerID].connection, players[playerID].playerObject, Random.Range(0, spawnPoints.Count));
        players[playerID].health = players[playerID].maxHealth;
    }

    [TargetRpc]
    void RespawnPlayer(NetworkConnection conn, GameObject player, int spawn)
    {
        player.transform.position = spawnPoints[spawn].position;
    }

    public class Player
    {
        public int maxHealth = 100;
        public int health = 100;
        public GameObject playerObject;
        public NetworkConnection connection;
        public int kills = 0;
        public int deaths = 0;
    }
}