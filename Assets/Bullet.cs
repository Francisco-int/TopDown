using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float destroyTimer;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 4);  
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<PlayerController>())
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();

            player.TakeDamage(damage);
        }
        if (other.gameObject.GetComponent<EnemyController>())
        {
            EnemyController player = other.gameObject.GetComponent<EnemyController>();

            player.TakeDamage(damage);
        }
        

        Destroy(gameObject);
    }
}
