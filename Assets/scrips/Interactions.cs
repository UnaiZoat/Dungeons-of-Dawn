using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    private bool isInsideTrigger = false; //variable que permitira al jugador interactuar con el cofre
    private bool isOpen = false;
    private Animator chestAnimatorRef;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInsideTrigger) //Colisiona con cofre
        { 
            if(Input.GetButtonDown("E"))
            {
                isOpen = !isOpen; //Cofre abierto?
                chestAnimatorRef.SetBool("isOpen", isOpen); //Activar animación de abrir cofre
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
