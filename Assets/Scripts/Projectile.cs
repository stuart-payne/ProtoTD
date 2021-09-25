using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int Damage = 2;
    public GameObject Target;
    private StatusEffect<EnemyStat> m_StatusEffect;
    [SerializeField] float speed = 20.0f;
    [SerializeField] float lifeTime = 10.0f;

    public StatusEffect<EnemyStat> StatusEffect { get => m_StatusEffect; set => m_StatusEffect = value; }

    private void Start()
    {
        StartCoroutine("DestroyAfterDelay");
    }

    void Update()
    {
        if(Target != null)
        {
            var dir = (Target.transform.position - transform.position).normalized;
            transform.Translate(dir * Time.deltaTime * speed);
        } else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
