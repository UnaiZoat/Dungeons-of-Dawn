using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Puerta : MonoBehaviour
{
    public JugadorMovimiento jugadorMovimiento;
    public int llavesNormalesNecesarias = 1;
    public int llavesDoradasNecesarias = 1;
    void Start()
    {
        // Suscríbete al evento
        //JugadorMovimiento.onLlaveRecogida += LlaveRecogida;
    }

/*
    private void LlaveRecogida(string tipoLlave)
    {
        if (tipoLlave == "normal")
        {
            Debug.Log("Es una llave normal");
            jugadorMovimiento.llavesNormales++;
            jugadorMovimiento.textoLlavesNormales.text = "X" + jugadorMovimiento.llavesNormales;
            
        }
        else if (tipoLlave == "dorada")
        {
            jugadorMovimiento.llavesDoradas++;
            jugadorMovimiento.textoLlavesDoradas.text = "X" + jugadorMovimiento.llavesDoradas;
            
        }
    }*/
    
    private void OnTriggerEnter(Collider other) {
        //Debug.Log("He chocado con la puerta");

        if (other.gameObject.CompareTag("Jugador"))
        {
            
            if (jugadorMovimiento.llavesNormales >= llavesNormalesNecesarias)
            {
                Debug.Log(jugadorMovimiento.llavesNormales);
                jugadorMovimiento.llavesNormales -= llavesNormalesNecesarias;
                jugadorMovimiento.textoLlavesNormales.text = "X" + jugadorMovimiento.llavesNormales;
                Destroy(gameObject);
            }
            
            if (jugadorMovimiento.llavesDoradas >= llavesDoradasNecesarias)
            {
                Debug.Log(jugadorMovimiento.llavesDoradas);
                jugadorMovimiento.llavesDoradas -= llavesDoradasNecesarias;
                jugadorMovimiento.textoLlavesDoradas.text = "X" + jugadorMovimiento.llavesDoradas;
                Destroy(gameObject);
            }
            
            
        }   
    }
}