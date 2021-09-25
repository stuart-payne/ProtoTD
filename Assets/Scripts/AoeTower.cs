using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeTower : BaseTower
{
    [SerializeField] private ParticleSystem m_AoeEffect;
    protected override void Fire()
    {
        m_AoeEffect.Play();
        var raycastHits = Physics.SphereCastAll(transform.position, Stats.FiringRange, Vector3.up);
        foreach(var raycasthit in raycastHits)
        {
            if (raycasthit.collider.CompareTag("Enemy"))
            {
                var enemy = raycasthit.collider.GetComponent<Enemy>();
                enemy.DealDamage(Stats.Damage);
                if(Stats.AppliesStatusEffect)
                    enemy.Stats.StatusEffects.Add(Stats.StatusEffect);
            }
        }
    }
}
