using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float velocidad;
    [SerializeField] public int numEnemy;
    [SerializeField] GameManager1 gameManager;
    [SerializeField] Transform player;
    [SerializeField] GameObject proyectil;
    [SerializeField] float forceShot;
    [SerializeField] bool ableDisparar;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager1>();
    }
   
    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ableDisparar)
        {
            ableDisparar = false;
            StartCoroutine(Disparar());
        }

        transform.position = transform.position + new Vector3(0, 0, velocidad * Time.deltaTime);
        if (transform.position.z > 15 || transform.position.z > 7)
        {
            Dead();
        }
        transform.LookAt(player.transform.position);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Proyectil"))
        {
            other.gameObject.SetActive(false);
            gameManager.enemiesKilled++;
            Dead();
        }
    }


    void Dead()
    {
        ableDisparar = true;
        gameManager.SetAbleEnemy(numEnemy);
        gameObject.SetActive(false);
    }
    IEnumerator Disparar()
    {
        GameObject newProyectil = Instantiate(proyectil, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Rigidbody rb = newProyectil.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * forceShot, ForceMode.Impulse);
        yield return new WaitForSeconds(Random.Range(0.5f, 2));
        ableDisparar = true;
    }
}
