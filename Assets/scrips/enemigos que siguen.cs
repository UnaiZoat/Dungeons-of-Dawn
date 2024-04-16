using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigosquesiguen : MonoBehaviour
{

    UnityEngine.AI.NavMeshAgent pathfinder;
    Transform target;
    bool gameover = false;
    bool jugadorTocado = false;

    public float tiempoResetJugadorTocado = 1f;


    // Start is called before the first frame update
    void Start()
    {
        pathfinder = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Jugador").transform;
        
        //PlayerController.onDeathJugador += GameOver; //Suscribirse al evento
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameover && !jugadorTocado){
                pathfinder.SetDestination(target.position);
        }
         
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jugador")  && !jugadorTocado)
        {
            JugadorMovimiento jugador = other.GetComponent<JugadorMovimiento>();
            if (jugador != null)
            {
                Debug.Log("Jugador tocado");
                jugador.Morir();
                Vector3 normalContacto = (transform.position - other.transform.position).normalized;
                jugadorTocado = true;
                 StartCoroutine(ResetearJugadorTocado());
            }
        }
    }

     IEnumerator ResetearJugadorTocado()
    {
        yield return new WaitForSeconds(tiempoResetJugadorTocado);
        jugadorTocado = false; // Restablecer jugadorTocado a false después de 2 segundos
    }

   void GameOver(){
        gameover = true;
    }

}
