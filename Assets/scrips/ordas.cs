using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ordas : MonoBehaviour
{
    public enemigosquesiguen[] enemigos;
    public enemigosquesiguen enemigoActual;
    float tiempoEspera;
    int numOrdaActual = 0;
    public GameObject puerta; // Referencia al GameObject de la puerta que quieres destruir
    // Start is called before the first frame update
    void Start()
    {
        NextOrda();   
    }

    // Update is called once per frame
    void Update()
    {
        bool todosMuertos = true;
        foreach (enemigosquesiguen enemigo in enemigos)
        {
            if (enemigo.vida > 0)
            {
                todosMuertos = false;
                break;
            }
        }

        if (todosMuertos)
        {
            DestruirPuerta();
        }
    }

    void NextOrda(){
        numOrdaActual++;
        enemigoActual = enemigos[numOrdaActual-1];
    }

    void DestruirPuerta()
    {
        if (puerta != null)
        {
            Destroy(puerta);
        }
    }
}
