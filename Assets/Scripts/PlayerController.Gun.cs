using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public partial class PlayerController
{
    [Header("Gun")]
    public float bulletSpeed = 20;

    [SerializeField] GameObject gun;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawn;

    [SerializeField] SpriteRenderer spriteRenderer;


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
        Vector3 position = bulletSpawn.position;
        Quaternion rotation = gun.transform.rotation;
        Vector3 velocity = bulletSpawn.right * bulletSpeed;

        SpawnBullet(position, rotation, velocity);
        ShootServer(position, rotation, velocity);
    }

    private void SpawnBullet(Vector3 position, Quaternion rotation, Vector3 velocity)
    {
        GameObject bulletClone = Instantiate(bullet);
        bulletClone.transform.position = position;
        bulletClone.transform.rotation = rotation;
        bulletClone.GetComponent<Rigidbody2D>().velocity = velocity;

        Physics2D.IgnoreCollision(bulletClone.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
    }

    [ServerRpc]
    private void ShootServer(Vector3 position, Quaternion rotation, Vector3 velocity)
    {
        ShootClient(position, rotation, velocity);
    }

    [ObserversRpc]
    private void ShootClient(Vector3 position, Quaternion rotation, Vector3 velocity)
    {
        SpawnBullet(position, rotation, velocity);
    }

    private void FlipSprite(float rotZ)
    {
        // Flips player sprite when weapon is rotated more on the left side of the player.
        if (Mathf.Abs(rotZ) > 90)
        {
            spriteRenderer.flipX = true;
            FlipSpriteServer(gameObject, true);
        }
        else
        {
            spriteRenderer.flipX = false;
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
        player.GetComponent<PlayerController>().spriteRenderer.flipX = flipped;
    }
}
