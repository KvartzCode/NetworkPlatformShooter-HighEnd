using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float bulletSpeed = 50;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawn;

    Camera mainCam; // use for aim and camera movement.


    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    void Shoot()
    {
        GameObject bulletClone = Instantiate(bullet);
        bulletClone.transform.position = bulletSpawn.position;
        bulletClone.transform.rotation = transform.rotation;

        bulletClone.GetComponent<Rigidbody2D>().velocity = bulletSpawn.right * bulletSpeed;
    }
}
