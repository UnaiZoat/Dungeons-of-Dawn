using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public void SetUp()
    {
        Debug.Log("Llamada a Game Over");
        gameObject.SetActive(true);
    }
}
