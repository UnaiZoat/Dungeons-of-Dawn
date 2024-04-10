using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Puerta : MonoBehaviour
{
    public JugadorMovimiento jugadorMovimiento;
    public int llavesNecesarias = 1;

    void Start()
    {
        // Suscríbete al evento
        JugadorMovimiento.onLlaveRecogida += LlaveRecogida;
    }


    void LlaveRecogida(string tipoLlave)
    {
        Debug.Log("Llave recogida");
        if (tipoLlave == "normal" && jugadorMovimiento.llavesNormales > 0)
        {
            Debug.Log("Es una llave normal");
            jugadorMovimiento.llavesNormales++;
            jugadorMovimiento.textoLlavesNormales.text = "X" + jugadorMovimiento.llavesNormales;
            
        }
        else if (tipoLlave == "dorada" && jugadorMovimiento.llavesDoradas > 0)
        {
            jugadorMovimiento.llavesDoradas++;
            jugadorMovimiento.textoLlavesDoradas.text = "X" + jugadorMovimiento.llavesDoradas;
            
        }
    }
    /*
    void AbrirPuerta()
    {
        Destroy(gameObject);
    }*/
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Jugador"))
        {
            if (jugadorMovimiento.llavesNormales >= llavesNecesarias)
            {
                jugadorMovimiento.llavesNormales -= llavesNecesarias;
                jugadorMovimiento.textoLlavesNormales.text = "X" + jugadorMovimiento.llavesNormales;
                Destroy(gameObject);
            }
        }   
    }
}