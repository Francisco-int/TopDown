using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
    public float rotationAngle = 0.0f;
    public float rotationSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        rotationAngle += rotationSpeed * Time.deltaTime;


        rotationAngle = Mathf.Clamp(rotationAngle, 90, 0);

        transform.Rotate(Vector3.up, rotationAngle * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeRot(float rot)
    {
        //transform.rotation = new Quaternion(transform.rotation.x, rot, transform.rotation.z, 0);
    }
}
