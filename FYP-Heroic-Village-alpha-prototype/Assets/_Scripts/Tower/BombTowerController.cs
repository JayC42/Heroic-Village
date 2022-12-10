using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTowerController : BaseTowerController
{
    public override IEnumerator Shoot(Transform target)
    {
        isShooting = true;

        while (isShooting)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.transform.position = shootingPoint.position;
            projectile.transform.rotation = shootingPoint.rotation;
            projectile.GetComponent<TowerProjectile>().Initialize(target, 0.5f);
            yield return new WaitForSeconds(shootingCooldown);
        }
        yield return null;
    }
}
