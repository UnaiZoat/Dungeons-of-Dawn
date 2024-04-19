using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CollisionDetection : MonoBehaviour
{
    public JugadorMovimiento jugadorMovimiento;
    public ParticleSystem HitParticle;
    BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        boxCollider.enabled = jugadorMovimiento.isAttacking;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
