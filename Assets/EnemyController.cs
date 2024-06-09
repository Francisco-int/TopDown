using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 10f;
    public float fireRate = 1f;
    public float shootRange = 8f;
    private int lifes;
    private float nextFireTime = 0f;
    [SerializeField] GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        lifes = 2;
        player = GameObject.FindAnyObjectByType<PlayerController>().GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(player.transform.position);

        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }
   
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = (player.position - bulletSpawn.position).normalized * bulletSpeed;
    }
    public void TakeDamage(int amount)
    {
        lifes -= amount;


        if (lifes <= 0)
        {
            StartCoroutine(DieEnemy());
        }
    }


    IEnumerator DieEnemy()
    {
        Spawner spawner = GameObject.Find("SpawnerManager").GetComponent<Spawner>();
        spawner.AddEnemyKilled();
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = true;
        yield return new WaitForSeconds(2);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject,4f);
    }
}
