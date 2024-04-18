using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]

public class JugadorMovimiento : LivingEntity
{
    CharacterController characterController;
    Rigidbody rb;
    public Camera mainCamera;
    Vector3 moveInput, moveVelocity;
    public float speed = 4;
    public ParticleSystem particulas;
    Animator anim;
    public TMP_Text textoPremios;
    public TMP_Text textoLlavesNormales;
    public TMP_Text textoLlavesDoradas;
    public int vida = 3;
    public float gravity=-9.8f;
    public bool canAttack = true;
    public float attackRate = 0f;

    public AudioClip SwordAttackSound;
    public AudioClip HitSound;
    public AudioClip DeathSound;

    // Eventos
    public delegate void OnDeathJugador();
    public static event OnDeathJugador onDeathJugador;
    public delegate void OnLlaveRecogida(string tipoLlave);
    public static event OnLlaveRecogida onLlaveRecogida;
    //movimiento de la camara y recoleccion de objetos
    private Vector3 offset;
    private int premios = 0;
    public int llavesNormales = 0;
    public int llavesDoradas = 0;
    //ccambio de sprite de corazones
    [SerializeField] private List<GameObject> listaCorazones;
    [SerializeField] private Sprite corazonDesactivado;
    
    public float distanciaDesplazamiento = 0.5f;
    public float velocidadDesplazamiento = 5f;

    bool puedemoverse = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        offset = mainCamera.transform.position - transform.position;
        anim = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {

        if (puedemoverse){
        // Detecta el movimiento del jugador
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        bool Is_running = Input.GetKey(KeyCode.LeftShift);

        anim.SetBool("is_running", Is_running);

        if (Is_running){
            speed = 20;

        }else{
            speed = 15;
        }

        // Si el jugador se está moviendo, activa la animación de caminar
        if (moveInput.magnitude > 0 && Is_running == false)
        {
            anim.SetBool("is_walking",true);
        }
        else
        {
            // Si el jugador no se está moviendo, activa la animación de idle
             anim.SetBool("is_walking",false);
        }

        // Aplica movimiento al CharacterController
        characterController.Move(moveVelocity * speed * Time.deltaTime);

        // Rota el personaje para que mire hacia la dirección del movimiento
        if (moveInput.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.15f);
        }
        }
         if (Input.GetButtonDown("Fire1") /*&& canAttack*/)
        {
            Attack();
        }
    }

    public void Attack()
    {
        canAttack = false;
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("is_attacking");
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(SwordAttackSound);
        
        //StartCoroutine(AttackCooldown());
    }

    /*
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackRate);
        canAttack = true;
    }*/

    void LateUpdate()
    {
        // Actualiza la posición de la cámara en LateUpdate para que siga al jugador
        mainCamera.transform.position = transform.position + offset;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("premio"))
        {
            particulas.Play();
            Destroy(other.gameObject, particulas.main.duration);
            Debug.Log("Ha tocado una estrella");
            premios++;
            textoPremios.text = "X" + premios;
        }
        
        else if (other.gameObject.CompareTag("llaveNormal"))
        {
            Destroy(other.gameObject, particulas.main.duration);
            Debug.Log("Ha tocado una llave normal");
            llavesNormales++;
            textoLlavesNormales.text = "X" + llavesNormales;
            // Dispara el evento
            onLlaveRecogida?.Invoke("normal");
        }
        else if (other.gameObject.CompareTag("llaveDorada"))
        {
            Destroy(other.gameObject, particulas.main.duration);
            Debug.Log("Ha tocado una llave dorada");
            llavesDoradas++;
            textoLlavesDoradas.text = "X" + llavesDoradas;
            // Dispara el evento
            onLlaveRecogida?.Invoke("dorada");
        }
    }

    public void Morir()
    {
        anim.SetTrigger("hit");
        Vector3 direccionAtras = -transform.forward; // Dirección opuesta al frente del jugador
        Vector3 desplazamiento = direccionAtras * distanciaDesplazamiento;

        StartCoroutine(DesplazarHaciaAtras(desplazamiento));

        vida--;
        Image imagenCorazon = listaCorazones[vida].GetComponent<Image>();
        imagenCorazon.sprite = corazonDesactivado;
        if (vida == 0){
            anim.SetBool("dead", true);
            puedemoverse = false;
            AudioSource am = GetComponent<AudioSource>();
            am.PlayOneShot(DeathSound);
            //gameObject.SetActive(false);
        }
        AudioSource ah = GetComponent<AudioSource>();
        ah.PlayOneShot(HitSound);
    }

    void OnDestroy()
    {
        if (onDeathJugador != null)
        {
            onDeathJugador();
        }
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


}