using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    private Transform target;
    private Vector3 initialPosition, targetPosition;
    private float speed;
    private float startTime;
    private bool hasInitialized = false;
    private Rigidbody rb;
    public ProjectileType projectileType;

    public float explosionRadius = 25;


    private void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody>(); 
        // Auto destroy spell gameobject after 10s
        Destroy(this.gameObject, 10f);
    }
    private void Update()
    {
        if (!hasInitialized) return;
        if (target != null)
        {
            if (projectileType == ProjectileType.Arrow)
            {
                if (target == null) Destroy(gameObject);
                FireArrow();
            }
            else if (projectileType == ProjectileType.Cannonball)
            {
                if (targetPosition == null) Destroy(gameObject);
                FireCannon();

                if (transform.position == targetPosition)
                {
                    DealAOEDamage(explosionRadius);
                }
            }             
        }
             
    }
    public void Initialize(Transform target, float speed)
    {
        this.target = target;
        this.speed = speed;
        startTime = Time.time;
        hasInitialized = true;
    }
    public void Initialize(Vector3 target, float speed)
    {
        targetPosition = target;
        this.speed = speed;
        startTime = Time.time;
        hasInitialized = true;
    }

    private void FireCannon()
    {
        Vector3 dir = target.position - transform.position;
        rb.velocity = dir * speed;
        //transform.position = Vector3.MoveTowards(transform.position, target.position, 50 * speed * Time.deltaTime);
         
    }
    private void FireArrow()
    {
        Vector3 dir = target.position - transform.position;
        rb.velocity = dir * speed;

        //transform.position = Vector3.MoveTowards(transform.position, target.position, 50 * speed * Time.deltaTime);
        //transform.LookAt(target); 
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && projectileType == ProjectileType.Arrow)
        {
            other.GetComponent<EnemyTarget>().TakeDamage(1); 
            Destroy(gameObject);
        }
        else if (other.CompareTag("Enemy") && projectileType == ProjectileType.Cannonball)
        {
            DealAOEDamage(explosionRadius);
        }
    }
    private void DealAOEDamage(float aoe)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, aoe);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<EnemyTarget>().TakeDamage(2);
            }
        }
        Destroy(gameObject);
    }
}
