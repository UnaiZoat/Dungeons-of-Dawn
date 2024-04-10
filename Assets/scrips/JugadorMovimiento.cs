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
    public float speed = 8;
    public ParticleSystem particulas;
    Animator anim;
    public TMP_Text textoPremios;
    public TMP_Text textoLlavesNormales;
    public TMP_Text textoLlavesDoradas;

    // Eventos
    public delegate void OnDeathJugador();
    public static event OnDeathJugador onDeathJugador;
    public delegate void OnLlaveRecogida(string tipoLlave);
    public static event OnLlaveRecogida onLlaveRecogida;

    private Vector3 offset;
    private int premios = 0;
    public int llavesNormales = 0;
    public int llavesDoradas = 0;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        offset = mainCamera.transform.position - transform.position;
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Detecta el movimiento del jugador
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        // Si el jugador se está moviendo, activa la animación de caminar
        if (moveInput.magnitude > 0)
        {
            anim.SetFloat("movimientos", 1.0f);
        }
        else
        {
            // Si el jugador no se está moviendo, activa la animación de idle
            anim.SetFloat("movimientos", 0.0f);
        }

        // Aplica movimiento al CharacterController
        characterController.Move(moveVelocity * speed * Time.deltaTime);

        // Rota el personaje para que mire hacia la dirección del movimiento
        if (moveInput.magnitude > 0)
        {
            transform.LookAt(transform.position + moveInput.normalized);
        }
    }

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
            /*llavesNormales++;
            textoLlavesNormales.text = "X" + llavesNormales;*/
            // Dispara el evento
            onLlaveRecogida?.Invoke("normal");
        }
        else if (other.gameObject.CompareTag("llaveDorada"))
        {
            Destroy(other.gameObject, particulas.main.duration);
            Debug.Log("Ha tocado una llave dorada");
            /*llavesDoradas++;
            textoLlavesDoradas.text = "X" + llavesDoradas;*/
            // Dispara el evento
            onLlaveRecogida?.Invoke("dorada");
        }
    }

    void OnDestroy()
    {
        if (onDeathJugador != null)
        {
            onDeathJugador();
        }
    }
}