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
        if (Input.GetMouseButtonDown(0)) // Botón izquierdo del mouse
        {
            Shoot();
        }
    }

    void RotateTowardsMouse()
    {
        // Obtén la posición del mouse en la pantalla
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Convierte la posición del mouse en pantalla a coordenadas del mundo
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = transform.position.z; // Mantén la posición z del objeto

        // Calcula la dirección desde el objeto hasta el mouse
        Vector3 direction = mouseWorldPosition - transform.position;

        // Calcula el ángulo en el plano XY (2D)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Aplica la rotación al objeto
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Shoot()
    {
        // Instancia el proyectil en la posición de disparo
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

        // Obtén el Rigidbody2D del proyectil para aplicar la física
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Establece la velocidad del proyectil en la dirección en la que está mirando el jugador
        rb.velocity = shootPoint.right * projectileSpeed;
    }
}

