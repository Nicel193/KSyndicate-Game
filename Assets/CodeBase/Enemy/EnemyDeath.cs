using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
  [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator), typeof(NavMeshAgent))]
  public class EnemyDeath : MonoBehaviour
  {
    public EnemyHealth Health;
    public EnemyAnimator Animator;
    public GameObject DeathFx;
    
    private NavMeshAgent _navMeshAgent;

    public event Action Happaned;

    private void Start()
    {
      _navMeshAgent = GetComponent<NavMeshAgent>();

      Health.HealthChanged += OnHealthChanged;
    }

    private void OnDestroy()
    {
      Health.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged()
    {
      if (Health.Current <= 0)
        Die();
    }

    private void Die()
    {
      Health.HealthChanged -= OnHealthChanged;
      
      Animator.PlayDeath();
      SpawnDeathFx();

      _navMeshAgent.speed = 0;

      StartCoroutine(DestroyTimer());
      
      Happaned?.Invoke();
    }

    private void SpawnDeathFx()
    {
      Instantiate(DeathFx, transform.position, Quaternion.identity);
    }

    private IEnumerator DestroyTimer()
    {
      yield return new WaitForSeconds(3);
      Destroy(gameObject);
    }
  }
}