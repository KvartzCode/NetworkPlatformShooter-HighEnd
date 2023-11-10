using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerDeathComponents
{
    public SpriteRenderer sprite;
    public PlayerController playerController;
    public BoxCollider2D collider;
    public Rigidbody2D rigidbody;
}

public partial class PlayerController
{
    [Header("Death")]
    [SerializeField] float spawnDelay = 5;
    [SerializeField] PlayerDeathComponents playerDeathComponents;


    private void Die()
    {
        HidePlayer();
        Invoke(nameof(Spawn), spawnDelay);
    }

    private void Spawn()
    {
        //NetworkManager.point
        ShowPlayer();
    }

    public void ShowPlayer()
    {
        playerDeathComponents.sprite.enabled = true;
        playerDeathComponents.playerController.enabled = true;
        playerDeathComponents.collider.enabled = true;
        playerDeathComponents.rigidbody.simulated = true;

    }

    public void HidePlayer()
    {
        playerDeathComponents.sprite.enabled = false;
        playerDeathComponents.playerController.enabled = false;
        playerDeathComponents.collider.enabled = false;
        playerDeathComponents.rigidbody.simulated = false;
    }
}
