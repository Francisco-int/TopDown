using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootShoot : MonoBehaviour
{
   
    public GameObject projectilePrefab;  // Prefab del proyectil
    public Transform shootPoint;         // Punto desde donde se dispara el proyectil
    public float projectileSpeed = 10f;  // Velocidad del proyectil

    void Update()
    {
        // Rotar el objeto hacia el mouse
        RotateTowardsMouse();

        // Disparar el proyectil
        if (Input.GetMouseButtonDown(0)) // Bot�n izquierdo del mouse
        {
            Shoot();
        }
    }

    void RotateTowardsMouse()
    {
        // Obt�n la posici�n del mouse en la pantalla
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Convierte la posici�n del mouse en pantalla a coordenadas del mundo
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = transform.position.z; // Mant�n la posici�n z del objeto

        // Calcula la direcci�n desde el objeto hasta el mouse
        Vector3 direction = mouseWorldPosition - transform.position;

        // Calcula el �ngulo en el plano XY (2D)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Aplica la rotaci�n al objeto
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Shoot()
    {
        // Instancia el proyectil en la posici�n de disparo
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

        // Obt�n el Rigidbody2D del proyectil para aplicar la f�sica
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Establece la velocidad del proyectil en la direcci�n en la que est� mirando el jugador
        rb.velocity = shootPoint.right * projectileSpeed;
    }
}

