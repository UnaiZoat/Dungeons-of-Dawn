using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject puerta;
    // Start is called before the first frame update
    void Start()
    {
        puerta.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            puerta.SetActive(true);
            Destroy(gameObject);
        }
    }
}
