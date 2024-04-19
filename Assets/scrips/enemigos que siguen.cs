using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigosquesiguen : MonoBehaviour
{
    CharacterController characterController;
    public JugadorMovimiento jugadorMovimiento;

    UnityEngine.AI.NavMeshAgent pathfinder;
    Transform target;
    bool gameover = false;
    bool jugadorTocado = false;

    public float tiempoResetJugadorTocado = 0.01f;
    public int vida = 3;

    private float distanciaDesplazamiento = 50f;
    private float velocidadDesplazamiento = 5f;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
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
            GetComponent<Animator>().SetTrigger("Hit");
            Vector3 direccionAtras = -transform.forward; // Dirección opuesta al frente del jugador
            Vector3 desplazamiento = direccionAtras * distanciaDesplazamiento;

            StartCoroutine(DesplazarHaciaAtras(desplazamiento));

            vida--;
            if (vida == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator ResetearJugadorTocado()
    {
        yield return new WaitForSeconds(tiempoResetJugadorTocado);
        jugadorTocado = false; // Restablecer jugadorTocado a false después de 2 segundos
    }

    private IEnumerator DesplazarHaciaAtras(Vector3 desplazamiento)
    {
        float tiempo = 0f;
        Vector3 posicionInicial = transform.position;
        Vector3 objetivo = posicionInicial + desplazamiento;

        while (tiempo < 1f)
        {
            tiempo += Time.deltaTime * velocidadDesplazamiento;
            characterController.Move(Vector3.Lerp(posicionInicial, objetivo, tiempo) - transform.position);
            yield return null;
        }
    }

   void GameOver(){
        gameover = true;
    }

}
