using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;
    public int health = 100;

    void Update()
    {
        if (health <= 0)
        {
            FindObjectOfType<GameManager>().Victory();
            Destroy(gameObject);
        }

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
