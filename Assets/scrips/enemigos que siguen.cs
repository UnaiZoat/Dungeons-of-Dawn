using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigosquesiguen : MonoBehaviour
{

    UnityEngine.AI.NavMeshAgent pathfinder;
    Transform target;
    bool gameover = false;


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
        if (!gameover){
                pathfinder.SetDestination(target.position);
        }
         
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            JugadorMovimiento jugador = other.GetComponent<JugadorMovimiento>();
            if (jugador != null)
            {
                jugador.Morir();
            }
        }
    }

   void GameOver(){
        gameover = true;
    }
    
}
