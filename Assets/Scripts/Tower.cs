using System.Collections;
using UnityEngine;

namespace ProtoTD
{
    public class Tower : BaseTower
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private GameObject projectileSpawnPoint;
        [SerializeField] private AudioSource m_ProjectileSound;
        [SerializeField] private LineRenderer m_LineRenderer;

        protected override void Update()
        {
            // if(m_TargetSelector.CurrentTarget != null)
            //     transform.LookAt(m_TargetSelector.CurrentTarget.transform);
            base.Update();
        }

        protected override void Fire()
        {
            var enemyComp = m_TargetSelector.CurrentTarget;
            enemyComp.DealDamage(Stats.Damage);
            if (Stats.AppliesStatusEffect)
                enemyComp.Stats.StatusEffects.Add(Stats.StatusEffect);
            m_ProjectileSound.Play();
            StartCoroutine(DisplayBeamEffect(new[] {transform.position, m_TargetSelector.CurrentTarget.transform.position}));
        }
        
        IEnumerator DisplayBeamEffect(Vector3[] linePositions)
        {
            m_LineRenderer.gameObject.SetActive(true);
            m_LineRenderer.positionCount = linePositions.Length;
            m_LineRenderer.SetPositions(linePositions);
            yield return new WaitForSeconds(0.1f);
            m_LineRenderer.gameObject.SetActive(false);
        }
    }
}
