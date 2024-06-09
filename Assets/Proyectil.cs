using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [SerializeField] float timerSetDisable;
    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeInHierarchy)
        {
            StartCoroutine(SetDisable());
        }
    }

    IEnumerator SetDisable()
    {
        yield return new WaitForSeconds(timerSetDisable);
        this.gameObject.SetActive(false);
    }
}
