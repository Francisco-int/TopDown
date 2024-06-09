using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Boss : MonoBehaviour
{
    public Transform player;


   [SerializeField] int vida;
   [SerializeField] Transform cañonUno;
   [SerializeField] Transform cañonDos;
    [SerializeField] Transform cañonTres;
    [SerializeField] GameObject proyectil;
    [SerializeField] GameObject misilObject;
    [SerializeField] float forceShot;
   [SerializeField] float velocidad;
    [SerializeField] bool misilAble;
    [SerializeField] Text pressRToRestart;
    bool restartAble;
    [SerializeField] Text winText;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindAnyObjectByType<PlayerController>().GetComponent<Transform>();
        winText = GameObject.Find("Win").GetComponent<Text>();
        pressRToRestart = GameObject.Find("Restart").GetComponent<Text>();
        misilAble = true;
        restartAble = false;
        InvokeRepeating("Disparo", 1, 0.5f);
    }
    private void Awake()
    {
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * velocidad * Time.deltaTime;
        transform.LookAt(player.transform.position);


        if (misilAble)
        {
            misilAble = false;
           StartCoroutine(Misil());
        }
        if (Input.GetKeyDown(KeyCode.R) && restartAble)
        {
            SceneManager.LoadScene(1);
        }
    }
  

    void Disparo()
    {
       StartCoroutine(DisparoIntervalo());
       StartCoroutine(DisparoIntervalo1());
    }
    IEnumerator DisparoIntervalo()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        GameObject newProyectil = Instantiate(proyectil, cañonUno);
        Rigidbody rb = newProyectil.GetComponent<Rigidbody>();
        rb.AddForce(cañonUno.transform.forward * forceShot, ForceMode.Impulse);
    }
    IEnumerator DisparoIntervalo1()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        GameObject newProyectil = Instantiate(proyectil, cañonDos);
        Rigidbody rb = newProyectil.GetComponent<Rigidbody>();
        rb.AddForce(cañonDos.transform.forward * forceShot, ForceMode.Impulse);
    }
    IEnumerator Misil()
    {
        yield return new WaitForSeconds(Random.Range(2, 4));
        Instantiate(misilObject, new Vector3(cañonTres.position.x, cañonTres.position.y, cañonTres.position.z), Quaternion.identity);
        misilAble = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Proyectil"))
        {

            TakeDamage(1);
        }
    }
    void Defeat()
    {
        winText.enabled = true;
        pressRToRestart.enabled = true;
        restartAble = true;
        Time.timeScale = 0f;
    }
     void TakeDamage(int amount)
    {
        currentHealth -= amount;


        if (currentHealth <= 0)
        {
            Defeat();
        }
    }
 }

