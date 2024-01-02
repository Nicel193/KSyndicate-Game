using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
  public class AgentMoveToPlayer : Follow
  {
    private const float MinimalDistance = 1;
    
    public NavMeshAgent Agent;
    
    private Transform _heroTransform;

    private void Update()
    {
      if(IsInitialized() && IsHeroNotReached())
        Agent.destination = _heroTransform.position;
    }

    public void Construct(Transform hero)
    {
      _heroTransform = hero;
    }

    private bool IsInitialized() => 
      _heroTransform != null;

    private bool IsHeroNotReached() => 
      Agent.transform.position.SqrMagnitudeTo(_heroTransform.position) >= MinimalDistance;
  }
}