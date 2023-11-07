using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class PlayerColorNetwork : NetworkBehaviour
{
    public GameObject body;
    public Color endColor;


    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {

        }
        else
        {
            GetComponent<PlayerColorNetwork>().enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeColorServer(gameObject, Random.ColorHSV(0, 1, 0, 1, 0, 1, 1, 1));
        }
    }


    [ServerRpc]
    public void ChangeColorServer(GameObject player, Color color)
    {
        ChangeColor(player, color);
    }

    [ObserversRpc]
    public void ChangeColor(GameObject player, Color color)
    {
        player.GetComponent<PlayerColorNetwork>().body.GetComponent<Renderer>().material.color = color;
    }
}
