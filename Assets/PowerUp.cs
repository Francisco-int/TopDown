using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { Health, Speed }
    public PowerUpType powerUpType;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (powerUpType == PowerUpType.Health)
            {
                player.currentHealth++;
            }
            else if (powerUpType == PowerUpType.Speed)
            {
                player.speed++;
            }
            Destroy(gameObject);
        }
    }
}
