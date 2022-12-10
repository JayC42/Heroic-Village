using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTowerController : BaseTowerController
{
    public override IEnumerator Shoot(Transform target)
    {
        GameObject projectile = Instantiate(projectilePrefab);
        yield return projectile;
    }
}
