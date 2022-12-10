using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class BaseTowerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float shootingCooldown;
    protected Coroutine shoot;
    protected bool isShooting;
    protected Collider targetCollider;
    public Transform shootingPoint; 
    /// <summary>
    /// The initialization will be different for each type of projectile,
    /// which can be overwritten in their own class scripts
    /// </summary>
    /// <param name="target"></param>
    public virtual IEnumerator Shoot(Transform target)
    {
        throw new NotImplementedException(); 
    }
    protected virtual void Start()
    {
        shootingCooldown = 2;
    }
    protected virtual void Update()
    {
        if (targetCollider == null && isShooting) 
        { 
            StopShooting(); 
        }
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && shoot == null)
        {
            StartCoroutine(Shoot(other.transform));
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other == targetCollider)
        {
            StopShooting();
        }
    }
    protected virtual void StopShooting()
    {
        //StopCoroutine(shoot);
        isShooting = false;
        targetCollider = null;
        shoot = null;
    }
}
