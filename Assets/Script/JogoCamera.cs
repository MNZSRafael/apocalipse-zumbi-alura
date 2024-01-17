using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogoCamera : MonoBehaviour
{
    public GameObject Protagonista;
    Vector3 distCompensar;

    // Start is called before the first frame update
    void Start()
    {
        distCompensar = transform.position - Protagonista.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = distCompensar + Protagonista.transform.position;
    }
}
