using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent pathfinder;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        pathfinder = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Jugador").transform;

    }

    // Update is called once per frame
    void Update()
    {
        
        pathfinder.SetDestination(target.position);

    }

}
