using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    public Rigidbody objectref; //objeto dentro del cofre

    private bool isInsideTrigger = false; //variable que permitira al jugador interactuar con el cofre
    private bool isOpen = false;
    private Animator chestAnimatorRef;
    private Transform objectCreateRef;
    private bool objectCreated = false;
    private bool chestOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInsideTrigger) //Colisiona con cofre
        { 
            if(Input.GetButtonDown("E") && !chestOpened)
            {
                isOpen = !isOpen; //Cofre abierto?
                chestAnimatorRef.SetBool("isOpen", isOpen); //Activar animación de abrir cofre
            }
            if(isOpen && !objectCreated) //Crear objeto dentro del cofre
            {
                Rigidbody objectInstance;
                Quaternion rotation = Quaternion.Euler(57.95f, -1.746f, -0.887f); // Cambia estos valores a la rotación que desees
                objectInstance = Instantiate(objectref, objectCreateRef.position, rotation) as Rigidbody;
                objectInstance.AddForce(0,700,-700f);
                objectCreated = true;
                chestOpened = true;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colisiona con cofre");
        if (other.gameObject.CompareTag("Cofre")) //Colisiona con cofre
        {
            //Debug.Log("Colisiona con cofre");
            isInsideTrigger = true;
            //Referencias a los hijos del cofre
            Transform chestRef = other.transform.parent.Find("Cofre");
            //Debug.Log(chestRef);
            Animator chestAnimator = chestRef.GetComponent<Animator>();
            chestAnimatorRef = chestAnimator; 
            objectCreateRef = other.transform.parent.Find("objectCreatePoint");

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cofre")) //Colisiona con cofre
        {
            isInsideTrigger = false;
            
        }
    }
}
