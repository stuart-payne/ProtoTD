using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : BaseTower
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject projectileSpawnPoint;
    [SerializeField] private AudioSource m_ProjectileSound;

    protected override void Update()
    {
        if(m_TargetSelector.CurrentTarget != null)
            transform.LookAt(m_TargetSelector.CurrentTarget.transform);
        base.Update();
    }

    protected override void Fire()
    {
        GameObject proj = Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, projectilePrefab.transform.rotation);
        var projComp = proj.GetComponent<Projectile>();
        projComp.Target = m_TargetSelector.CurrentTarget;
        projComp.Damage = Stats.Damage;
        if (Stats.AppliesStatusEffect)
            projComp.StatusEffect = Stats.StatusEffect;
        m_ProjectileSound.Play();
    }
}
