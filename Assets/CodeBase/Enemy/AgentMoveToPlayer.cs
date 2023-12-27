using CodeBase.Infrastructure;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentMoveToPlayer : MonoBehaviour
{
    private const float MinDistanceToPlayer = 1f;

    private NavMeshAgent _agent;
    private IGameFactory _gameFactory;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _gameFactory = ServiceLocator.Contener.Single<IGameFactory>();
    }

    private void Update()
    {
        if (_gameFactory.Hero != null)
        {
            Transform _player = _gameFactory.Hero.transform;

            if (Vector3.Distance(_agent.transform.position, _player.position) >= MinDistanceToPlayer)
                _agent.destination = _player.position;
        }
    }
}
