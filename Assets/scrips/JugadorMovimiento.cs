using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JugadorMovimiento : MonoBehaviour
{
    CharacterController characterController;
    public Camera mainCamera;
    Vector3 moveInput, moveVelocity;
    public float speed = 8;
    public ParticleSystem particulas;
    Animator anim;
    public Text texto;

    private Vector3 offset;
    private int premios = 0;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        offset = mainCamera.transform.position;
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

        // Actualiza la posición de la cámara
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        mainCamera.transform.position = transform.position + offset;
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        if (groundPlane.Raycast(ray, out float rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            transform.LookAt(new Vector3(point.x, transform.position.y, point.z));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("premio"))
        {
            particulas.Play();
            Destroy(other.gameObject, particulas.main.duration);
            Debug.Log("Ha tocado una estrella");
            premios++;
            texto.text = "X" + premios;
        }

    }
    
}
