using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Switch
{
    public GameObject gameObject;
    public bool LeverUp = false;
    public Animator switchAnimatorRef;

    public Switch(GameObject gameObject)
    {
        this.gameObject = gameObject;
        Transform switchRef = gameObject.transform.Find("Lever");
        if (switchRef != null)
            {
                this.switchAnimatorRef = switchRef.GetComponent<Animator>();
                if (this.switchAnimatorRef == null)
                {
                    Debug.LogError("El objeto 'Lever' no tiene un componente Animator.");
                }
            }
        //this.switchAnimatorRef = switchRef.GetComponent<Animator>();
    }

    public void Toggle()
    {
        LeverUp = !LeverUp;
        //Debug.Log("LeverUp: " + LeverUp);
        switchAnimatorRef.SetBool("LeverUp", LeverUp);
    }

    public void Untoggle()
    {
        LeverUp = false;
        switchAnimatorRef.SetBool("LeverUp", LeverUp);
    }
}

public class Interactions : MonoBehaviour
{
    public Rigidbody objectref; //objeto dentro del cofre
    public GameObject PuertaAcertijo;

    private bool isInsideTriggerChest = false; //variable que permitira al jugador interactuar con el cofre
    private bool isOpen = false;
    private Animator chestAnimatorRef;
    private Transform objectCreateRef;
    private bool objectCreated = false;
    private bool chestOpened = false;

    private bool isInsideTriggerSwitch = false;
    //private bool LeverUp = false;
   // private Animator switchAnimatorRef;
    //private bool switchActive = false;
    private bool isInsideTriggerSign1 = false;
    private bool isInsideTriggerSign2 = false;
    private bool isInsideTriggerSign3 = false;
    private bool isInsideTriggerSign4 = false;

    private bool isSignActive1 = false;
    private bool isSignActive2 = false;
    private bool isSignActive3 = false;
    private bool isSignActive4 = false;

    [SerializeField] private GameObject cajaTexto1;
    [SerializeField] private GameObject cajaTexto2;
    [SerializeField] private GameObject cajaTexto3;
    [SerializeField] private GameObject cajaTexto4;
    [SerializeField] private TMP_Text textoDialogo;
    [SerializeField] private JugadorMovimiento jugadorMovimiento;
    //[SerializeField, TextArea(3,10)] private string[] arrayTextos;
    
    public List<Switch> switches = new List<Switch>();
    public List<GameObject> interruptores; // Lista para almacenar los interruptores
    private List<GameObject> interruptoresActivados = new List<GameObject>(); // Lista para almacenar los interruptores activados por el jugador
    public List<GameObject> ordenCorrecto; // Lista que contiene el orden correcto de los interruptores
    // Start is called before the first frame update
    void Start()
    {
        jugadorMovimiento = GetComponent<JugadorMovimiento>();
        foreach (GameObject interruptorGameObject in interruptores)
        {
            Switch interruptor = new Switch(interruptorGameObject);
            switches.Add(interruptor);
        }
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
            //Debug.Log("Colisiona con switch");

            // Encuentra el interruptor más cercano al jugador
            Switch closestSwitch = null;
            float closestDistance = float.MaxValue;
            foreach (Switch interruptor in switches)
            {
                float distance = Vector3.Distance(this.gameObject.transform.position, interruptor.gameObject.transform.position);
                if (distance < closestDistance)
                {
                    closestSwitch = interruptor;
                    closestDistance = distance;
                }
            }
            //Debug.Log("interruptor más cercano: " + closestSwitch.gameObject.name);

            // Si se encontró un interruptor y se presionó "E", activa el interruptor
            if (closestSwitch != null && Input.GetButtonDown("E"))
            {
                Debug.Log("Interaccion con switch");
                closestSwitch.Toggle();
                InterruptorActivado(closestSwitch.gameObject);
                isInsideTriggerSwitch = false;

            }
        }
        /*
        Debug.Log("is: " + isInsideTriggerSign1);
        Debug.Log("sign: " + isSignActive1);
        Debug.Log("jm: " + jugadorMovimiento.puedemoverse);
        */
    if(isInsideTriggerSign1 && !isSignActive1)
    {
        if(Input.GetButtonDown("E"))
        {
            jugadorMovimiento.puedemoverse = false; // Luego establece la capacidad de moverse del jugador
            isSignActive1 = true; // Cambia el estado del cartel primero
            ActivaDesactivaCajaTextos1(true); // Finalmente, activa el cartel
        }
    }
        
    if(isInsideTriggerSign2 && !isSignActive2)
    {
        if(Input.GetButtonDown("E"))
        {
            jugadorMovimiento.puedemoverse = false; // Luego establece la capacidad de moverse del jugador
            isSignActive2 = true; // Cambia el estado del cartel primero
            ActivaDesactivaCajaTextos2(true); // Finalmente, activa el cartel
        }
    }
        
    if(isInsideTriggerSign3 && !isSignActive3)
    {
        if(Input.GetButtonDown("E"))
        {
            jugadorMovimiento.puedemoverse = false; // Luego establece la capacidad de moverse del jugador
            isSignActive3 = true; // Cambia el estado del cartel primero
            ActivaDesactivaCajaTextos3(true); // Finalmente, activa el cartel
        }
    }
        
    if(isInsideTriggerSign4 && !isSignActive4)
    {
        if(Input.GetButtonDown("E"))
        {
            jugadorMovimiento.puedemoverse = false; // Luego establece la capacidad de moverse del jugador
            isSignActive4 = true; // Cambia el estado del cartel primero
            ActivaDesactivaCajaTextos4(true); // Finalmente, activa el cartel
        }
    }

    if(isSignActive1){
        if(Input.GetButtonDown("E"))
        {
            StartCoroutine(WaitAndToggleSign1());
        }
    }
       
    if(isSignActive2){
        if(Input.GetButtonDown("E"))
        {
            StartCoroutine(WaitAndToggleSign2());
        }
    }

    if(isSignActive3){
        if(Input.GetButtonDown("E"))
        {
            StartCoroutine(WaitAndToggleSign3());
        }
    }
       
    if(isSignActive4){
        if(Input.GetButtonDown("E"))
        {
            StartCoroutine(WaitAndToggleSign4());
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
            
        }

        if(other.gameObject.CompareTag("Sign1"))
        {
            //Debug.Log("Colisiona con señal");

            isInsideTriggerSign1 = true;
        }
        if(other.gameObject.CompareTag("Sign2"))
        {
            //Debug.Log("Colisiona con señal");

            isInsideTriggerSign2 = true;
        }
        if(other.gameObject.CompareTag("Sign3"))
        {
            //Debug.Log("Colisiona con señal");

            isInsideTriggerSign3 = true;
        }
        if(other.gameObject.CompareTag("Sign4"))
        {
            //Debug.Log("Colisiona con señal");

            isInsideTriggerSign4 = true;
        }

         if(other.gameObject.CompareTag("AreaCaida"))
        {
            jugadorMovimiento.vida =  0;
            jugadorMovimiento.Morir();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cofre")) //Colisiona con cofre
        {
            isInsideTriggerChest = false;
            
        }
         if (other.CompareTag("Acertijo"))
        {
            Debug.Log("¡Saliste del área del acertijo!");
            ReiniciarAcertijo();
        }
        if(other.gameObject.CompareTag("Switch"))
        {
            isInsideTriggerSwitch = false;
        }
        if(other.gameObject.CompareTag("Sign1"))
        {
            isInsideTriggerSign1 = false;
        }
        if(other.gameObject.CompareTag("Sign2"))
        {
            isInsideTriggerSign2 = false;
        }
        if(other.gameObject.CompareTag("Sign3"))
        {
            isInsideTriggerSign3 = false;
        }
        if(other.gameObject.CompareTag("Sign4"))
        {
            isInsideTriggerSign4 = false;
        }
    }
    // Método para verificar si se ha completado el acertijo
    private void VerificarAcertijo()
    {
        bool acertijoCompletado = true;
        if (interruptoresActivados.Count == ordenCorrecto.Count)
        {
            for (int i = 0; i < interruptoresActivados.Count; i++)
            {
                if (interruptoresActivados[i] != ordenCorrecto[i])
                {
                    acertijoCompletado = false;
                    break;
                }
            }
            if (acertijoCompletado)
            {
                Debug.Log("¡Acertijo completado!");
                Destroy(PuertaAcertijo);
            }
            else
            {
                Debug.Log("¡El acertijo no está completo! Inténtalo de nuevo.");
                ReiniciarAcertijo();
            }
        }
    }

    // Método para manejar la activación de los interruptores por parte del jugador
    public void InterruptorActivado(GameObject interruptor)
    {
        interruptoresActivados.Add(interruptor);
        VerificarAcertijo();
    }

    // Método para reiniciar el acertijo si el jugador comete un error
    public void ReiniciarAcertijo()
    {
        interruptoresActivados.Clear();
        foreach (Switch interruptor in switches)
        {
            interruptor.Untoggle();
        }
    }

    public void ActivaDesactivaCajaTextos1(bool activado){
        cajaTexto1.SetActive(activado);
    }
    
    public void ActivaDesactivaCajaTextos2(bool activado){
        cajaTexto2.SetActive(activado);
    }
    public void ActivaDesactivaCajaTextos3(bool activado){
        cajaTexto3.SetActive(activado);
    }
    public void ActivaDesactivaCajaTextos4(bool activado){
        cajaTexto4.SetActive(activado);
    }


    public void ShowText(string texto){
        //cajaTexto.SetActive(true);
        textoDialogo.text = texto.ToString();
    }
    
    IEnumerator WaitAndToggleSign1()
    {
        yield return new WaitForSeconds(10); // Espera 15 segundos
        jugadorMovimiento.puedemoverse = true;
        ActivaDesactivaCajaTextos1(false);
        isSignActive1 = !isSignActive1;
        isInsideTriggerSign1 = false;
    }

    IEnumerator WaitAndToggleSign2()
    {
        yield return new WaitForSeconds(10); // Espera 15 segundos
        jugadorMovimiento.puedemoverse = true;
        ActivaDesactivaCajaTextos2(false);
        isSignActive2 = !isSignActive2;
        isInsideTriggerSign2 = false;
    }
        
    IEnumerator WaitAndToggleSign3()
    {
        yield return new WaitForSeconds(10); // Espera 15 segundos
        jugadorMovimiento.puedemoverse = true;
        ActivaDesactivaCajaTextos3(false);
        isSignActive3 = !isSignActive3;
        isInsideTriggerSign3 = false;
    }
        
    IEnumerator WaitAndToggleSign4()
    {
        yield return new WaitForSeconds(15); // Espera 15 segundos
        jugadorMovimiento.puedemoverse = true;
        ActivaDesactivaCajaTextos4(false);
        isSignActive4 = !isSignActive4;
        isInsideTriggerSign4 = false;
    }
}
