using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    public Rigidbody objectref; //objeto dentro del cofre

    private bool isInsideTriggerChest = false; //variable que permitira al jugador interactuar con el cofre
    private bool isOpen = false;
    private Animator chestAnimatorRef;
    private Transform objectCreateRef;
    private bool objectCreated = false;
    private bool chestOpened = false;

    private bool isInsideTriggerSwitch = false;
    private bool LeverUp = false;
    private Animator switchAnimatorRef;
    private bool switchActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInsideTriggerChest) //Colisiona con cofre
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
        if(isInsideTriggerSwitch)
        {
            if(Input.GetButtonDown("E") && !switchActive)
            {
                LeverUp = !LeverUp;
                Debug.Log("LeverUp: " + LeverUp);
                switchAnimatorRef.SetBool("LeverUp", LeverUp);
                switchActive = true;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Colisiona con cofre");
        if (other.gameObject.CompareTag("Cofre")) //Colisiona con cofre
        {
            //Debug.Log("Colisiona con cofre");
            isInsideTriggerChest = true;
            //Referencias a los hijos del cofre
            Transform chestRef = other.transform.parent.Find("Cofre");
            //Debug.Log(chestRef);
            Animator chestAnimator = chestRef.GetComponent<Animator>();
            chestAnimatorRef = chestAnimator; 
            objectCreateRef = other.transform.parent.Find("objectCreatePoint");

        }
        if(other.gameObject.CompareTag("Switch"))
        {
            isInsideTriggerSwitch = true;
            Transform switchRef = other.transform.parent.Find("Lever");
            Animator switchAnimator = switchRef.GetComponent<Animator>();
            switchAnimatorRef = switchAnimator;
            
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cofre")) //Colisiona con cofre
        {
            isInsideTriggerChest = false;
            
        }
    }
}
