using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGun : HitScan {
    protected override IEnumerator ShotEffect()
    {
        float wait = 1 / fireRate - 0.02f;
        laserLine.enabled = true;
        yield return new WaitForSeconds(wait);
        laserLine.enabled = false;
    }
}
