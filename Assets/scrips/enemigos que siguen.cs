using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigosquesiguen : MonoBehaviour
{

    public JugadorMovimiento jugadorMovimiento;

    UnityEngine.AI.NavMeshAgent pathfinder;
    Transform target;
    bool gameover = false;
    bool jugadorTocado = false;

    public float tiempoResetJugadorTocado = 0.01f;


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
        
        if (other.CompareTag("Espada") && jugadorMovimiento.isAttacking)
        {
            Debug.Log("COLISION CON ESPADA");
            //GetComponent<Animator>().SetTrigger("Hit");
            Transform child = other.transform.Find("Snow slash");
            if (child != null)
            {
                ParticleSystem hitParticle = child.GetComponent<ParticleSystem>();
                if (hitParticle != null)
                {
                    hitParticle.Play();
                }
                else
                {
                    Debug.LogError("No se encontró el sistema de partículas en el objeto hijo.");
                }
            }
            else
            {
                Debug.LogError("No se encontró un objeto hijo con el nombre especificado.");
            }
            //Instantiate(HitParticle, other.transform.position, Quaternion.identity);
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
