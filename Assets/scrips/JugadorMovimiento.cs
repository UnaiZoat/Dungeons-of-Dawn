using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorMovimiento : MonoBehaviour
{
    CharacterController characterController;
    public Camera mainCamera;
    UnityEngine.Vector3 moveInput, moveVelocity;
    public float speed = 8;
    public ParticleSystem particulas;

    private Rigidbody rb;
    private Vector3 offset;
    private int premios = 0;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        offset = mainCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        mainCamera.transform.position = transform.position + offset;
        Plane groundPlane = new UnityEngine.Plane(UnityEngine.Vector3.up, UnityEngine.Vector3.zero);
        if (groundPlane.Raycast(ray, out float rayDistance)){
            UnityEngine.Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            transform.LookAt(new UnityEngine.Vector3(point.x, transform.position.y,point.z));
        }

        moveInput = new UnityEngine.Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        characterController.Move(moveVelocity * speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("premio")){
            
            particulas.Play();
            
            Destroy(other.gameObject, particulas.main.duration);
            Debug.Log("Ha tocado una estrella");
            premios++;
            //texto.text = "Puntuacion: " + premios;
        }
    }
}
