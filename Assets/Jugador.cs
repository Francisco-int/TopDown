using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Jugador : MonoBehaviour
{
    [SerializeField] float velocidadJugador;
    [SerializeField] float intervaloEntreDisparos;
    [SerializeField] float fuerzaDisparo;
    [SerializeField] List<GameObject> proyectilInstances;
    [SerializeField] GameObject proyectilPrefab;
    [SerializeField] int proyectilADiparar;
    [SerializeField] bool ableDisparador;
    [SerializeField] Transform cañon;
    [SerializeField] Slider calentamientoSlider;
    [SerializeField] int cantidadProyectiles;
    [SerializeField] float medidorDeTemperatura;
    [SerializeField] float calentamientoPorDisparo;
    [SerializeField] bool ableEnfriamiento;
    [SerializeField] float cantEnfriamiento;
    [SerializeField] float sobreCalentamiento;
    [SerializeField] float timerEnfriamiento;
    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        calentamientoSlider.maxValue = sobreCalentamiento;
        ableDisparador = true;
        for (int i = 0; i < cantidadProyectiles; i++)
        {
            GameObject proyectilInstance = Instantiate(proyectilPrefab, Vector3.zero, Quaternion.identity);
            proyectilInstance.SetActive(false);
            proyectilInstances.Add(proyectilInstance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovimientoJugador();
        Ataque();
        Limits();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            SceneManager.LoadScene(0);
        }
    }
    void MovimientoJugador()
    {
       horizontal  = Input.GetAxis("Horizontal");
       vertical  = Input.GetAxis("Vertical");

       Vector3 direccion = new Vector3 (horizontal, 0, vertical);  

        transform.Translate(direccion * Time.deltaTime * -velocidadJugador);

    }
    void Ataque()
    {

        if (Input.GetKey(KeyCode.Space) && ableDisparador && medidorDeTemperatura < sobreCalentamiento)
        {
            ableDisparador = false;
            ableEnfriamiento = false;
            proyectilInstances[proyectilADiparar].transform.position = cañon.position;
            proyectilInstances[proyectilADiparar].SetActive(true);
            Rigidbody rb = proyectilInstances[proyectilADiparar].GetComponent<Rigidbody>();
            rb.AddForce(cañon.transform.forward * fuerzaDisparo, ForceMode.Impulse);
            proyectilADiparar++;

            if (proyectilADiparar == proyectilInstances.Count)
            {
                proyectilADiparar = 0;

            }
            medidorDeTemperatura += calentamientoPorDisparo;
            calentamientoSlider.value = medidorDeTemperatura;
            StartCoroutine(Enfriamiento());
            StartCoroutine(IntervaloDisparos());
         
        }
        if (ableEnfriamiento)
        {
            medidorDeTemperatura -= cantEnfriamiento;
            calentamientoSlider.value = medidorDeTemperatura;
            if (medidorDeTemperatura <= 0)
            {
                ableEnfriamiento = false;
                medidorDeTemperatura = 0;
            }
        }
    }

    IEnumerator IntervaloDisparos()
    {
        
        yield return new WaitForSeconds(intervaloEntreDisparos);
        ableDisparador = true; 

    }
    IEnumerator Enfriamiento()
    {
        yield return new WaitForSeconds(timerEnfriamiento);
        ableEnfriamiento = true;
    }
    void Limits()
    {
        if (transform.position.x > 10)
        {
            transform.position = new Vector3(10, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -10)
        {
            transform.position = new Vector3(-10, transform.position.y, transform.position.z);
        }
        if (transform.position.z < -3.46)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -3.46f);
        }
        if (transform.position.z > 3.46)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 3.46f);
        }
    }
}
