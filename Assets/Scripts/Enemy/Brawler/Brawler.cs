using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Core;

public class Brawler : Enemy {
    // Use this for initialization
    [SerializeField]
    private float attackRechargeTime, attackRadius, attackZ, attackY;
    private float lastAttackTime;
    private bool attacking;

    protected new void Awake()
    {
        dead = false;
        currentHealth = BaseHealth;
        lastAttackTime = -attackRechargeTime;
    }

    public void Attack()
    {
        Transform self = GetComponent<Transform>();
        Debug.Log("Winding up attack");
        Debug.Log("Dealing damage");
        Vector3 detectorLocation = self.position + new Vector3(0, attackY, attackZ);
        Collider[] detector = Physics.OverlapSphere(detectorLocation, attackRadius, LayerMask.GetMask("Player"));
        if(detector.Length > 0)
        {
            Player.GetInstance().ReceiveDamage(attackDamage);

        }
        lastAttackTime = Time.time;
    }
    protected override IEnumerator Die()
    {
        yield return null;
        Destroy(gameObject);
    }

    public float GetLastAttackTime()
    {
        return lastAttackTime;
    }
}
