using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    bool dispararAble;
    private void Start()
    {
        dispararAble = true;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFireTime && dispararAble)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
        if (Input.GetKey(KeyCode.C))
        {
            dispararAble = false;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            dispararAble = true;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = bulletSpawn.forward * bulletSpeed;
    }
}
