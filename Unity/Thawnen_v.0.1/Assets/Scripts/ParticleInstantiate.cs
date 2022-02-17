using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleInstantiate : MonoBehaviour
{

    float delay;
    // Start is called before the first frame update
    void Start()
    {
        delay = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, delay);
    }

   
}
