using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : LivingEntity
{
    [SerializeField] List<Transform> wayPoints;
    public float moveSpeed = 10f;
    public float distanciaCambio = 2f;
    byte siguientePosicion = 0;
    bool gameover = false;


    // Start is called before the first frame update
    void Start()
    {
        //PlayerController.onDeathJugador += GameOver; //Suscribirse al evento
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[siguientePosicion].transform.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, wayPoints[siguientePosicion].transform.position) < distanciaCambio) {
            siguientePosicion++;
            if (siguientePosicion >= wayPoints.Count){ 
                siguientePosicion = 0;
            }
        }

        // Aplicar rotación cuando cambia de waypoint o vuelve al inicio
        if (siguientePosicion < wayPoints.Count) {
            Vector3 direction = (wayPoints[siguientePosicion].transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        } else {
            // Rotar hacia atrás al primer waypoint
            Vector3 direction = (wayPoints[0].transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
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
