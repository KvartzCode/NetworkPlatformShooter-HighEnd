using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public partial class PlayerController
{
    [Header("Gun")]
    public float bulletSpeed = 50;

    [SerializeField] GameObject gun;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawn;

    //public Transform childTransform; // Reference to the child object
    public float flipThreshold = 90; // Adjust this threshold to control when the flip happens
    //public float childRotation;

    [SerializeField] SpriteRenderer spriteRenderer;
    public bool flipped = false;


    private void UpdateGun()
    {
        if (!playerCamera)
            return;

        Vector3 mousePos = playerCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - gun.transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0, 0, rotZ);

        FlipSprite(rotZ);

        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    private void Shoot()
    {
        GameObject bulletClone = Instantiate(bullet);
        bulletClone.transform.position = bulletSpawn.position;
        bulletClone.transform.rotation = gun.transform.rotation;

        bulletClone.GetComponent<Rigidbody2D>().velocity = bulletSpawn.right * bulletSpeed;
    }

    private void FlipSprite(float rotZ)
    {
        if (rotZ > flipThreshold && rotZ < 270 && !flipped)
        {
            spriteRenderer.flipX = true;
            flipped = true;
            FlipSpriteServer(gameObject, true);
        }
        else if (rotZ < flipThreshold || rotZ > 270 && flipped)
        {
            spriteRenderer.flipX = false;
            flipped = false;
            FlipSpriteServer(gameObject, false);
        }
    }

    [ServerRpc]
    private void FlipSpriteServer(GameObject player, bool flipped)
    {
        FlipSpriteClient(player, flipped);
    }

    [ObserversRpc]
    private void FlipSpriteClient(GameObject player, bool flipped)
    {
        player.GetComponent<PlayerController>().flipped = flipped;
        player.GetComponent<PlayerController>().spriteRenderer.flipX = flipped;
    }
}
