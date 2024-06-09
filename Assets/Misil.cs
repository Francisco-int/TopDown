using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misil : MonoBehaviour
{
    [SerializeField] Vector3 transformPlayer;
    [SerializeField] float velocidadMisil;
    Vector3 direccion;
    // Start is called before the first frame update
    void Start()
    {
        transformPlayer = GameObject.Find("Player").GetComponent<Transform>().transform.position;
        transform.LookAt(transformPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * velocidadMisil * Time.deltaTime);
        if(transform.position == transformPlayer) 
        {
            Destroy(gameObject);
        }
    }
}
