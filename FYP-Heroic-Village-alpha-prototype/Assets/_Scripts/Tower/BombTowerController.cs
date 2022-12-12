using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTowerController : BaseTowerController
{
    private float bulletSpeed = 0.5f;
    public override IEnumerator Shoot(Transform target)
    {
        isShooting = true;

        while (isShooting)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.transform.position = shootingPoint.position;
            projectile.transform.rotation = shootingPoint.rotation;
            projectile.GetComponent<TowerProjectile>().Initialize(target, bulletSpeed, baseDamage);
            yield return new WaitForSeconds(baseFireSpeed);
        }
        yield return null;
    }
}
