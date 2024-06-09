using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 moveDirection;
    public GameObject playerMesh;
    [SerializeField] Text gameOver;
    [SerializeField] Text textLifes;
    public int currentHealth;
    bool repeatScene;
    [SerializeField] Text pressRToRestart;
    [SerializeField] GameObject explosion;

    void Start()
    {
        pressRToRestart.enabled = false;
        repeatScene = false;
        gameOver.enabled = false;
        Time.timeScale = 1;
    }

    void Update()
    {

        textLifes.text = "Lives left: " + currentHealth.ToString();

        float horizontal = Input.GetAxis("Horizontal");
       float vertical = Input.GetAxis("Vertical");

        Vector3 direccion = new Vector3(horizontal, 0, vertical);

        transform.Translate(direccion * Time.deltaTime * speed);

        float rotY;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rotY = -90;
            playerMesh.transform.Rotate(0, rotY, 0, 0);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            rotY = 90;
            playerMesh.transform.Rotate(0, rotY, 0, 0);
        }
        if(repeatScene == true && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKey(KeyCode.C))
        {
            speed = 10;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyController>())
        {
            Die();
        }
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 2)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        pressRToRestart.enabled = true;
        gameOver.enabled = true;
        repeatScene = true;
        Time.timeScale = 0;
    }
}
