using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class BaseTowerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    //public float shootingCooldown;
    protected Coroutine shoot;
    protected bool isShooting;
    protected Collider targetCollider;
    public Transform shootingPoint;
    public float baseDamage = 1f;
    public float baseFireSpeed = 1f;
    public float baseFiringRange; 

    // Can transfer these into another script responsible for handling tower upgrades 
    public int currentTowerLevel = 1; 
    public int maxTowerLevel = 5;
    float damageUpBoost, fireSpeedUpBoost; 

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
        //shootingCooldown = 2;
        currentTowerLevel = 1;  
    }
    protected virtual void Update()
    {
        if (targetCollider == null && isShooting) 
        { 
            StopShooting(); 
        }

        // Handles upgrade logic
        if (currentTowerLevel <= maxTowerLevel)
        {
            switch(currentTowerLevel)
            { 
                case 2:
                    damageUpBoost = 1;
                    fireSpeedUpBoost = 0.15f;
                    UpgradeTower();
                    break; 
                case 3:
                    damageUpBoost = 1.25f;
                    fireSpeedUpBoost = 0.15f;
                    UpgradeTower();
                    break;
                case 4:
                    damageUpBoost = 1.5f;
                    fireSpeedUpBoost = 0.20f;
                    UpgradeTower();
                    break;
                case 5:
                    damageUpBoost = 2.0f;
                    fireSpeedUpBoost = 0.25f;
                    UpgradeTower();
                    break;
                default:
                    break; 
            }
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
    // For tower upgrades
    //protected virtual void OnMouseDown()
    //{
    //    UIManager.Instance.ShowBuildingUpgradeMenu(this);
    //}
    protected virtual void StopShooting()
    {
        //StopCoroutine(shoot);
        isShooting = false;
        targetCollider = null;
        shoot = null;
    }
    public void UpgradeTower()
    {
        baseDamage += damageUpBoost;
        baseFireSpeed += fireSpeedUpBoost;
        currentTowerLevel++; 
    }
}
