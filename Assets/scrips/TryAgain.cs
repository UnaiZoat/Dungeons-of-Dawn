using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TryAgain : MonoBehaviour
{
    public Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(Pulsado);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Pulsado(){

        SceneManager.LoadScene("MAINMENU");
    }
}
