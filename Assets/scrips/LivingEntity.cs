using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamage
{
    public float healthStart;
    protected float health;
    protected bool dead;

    public delegate void OnDeadEnemy();
    public static event OnDeadEnemy onDeadEnemy;
   
    public void TakeHit(float damage, RaycastHit hit)
    {
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0 && !dead){
            Die();
        }
    }
    public void Die()
    {
        dead = true;
        GameObject.Destroy(gameObject);
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        health = healthStart;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
