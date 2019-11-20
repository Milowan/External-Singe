using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoNoS : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            enabled = false;
        }
    }
}
