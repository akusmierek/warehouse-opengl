using UnityEngine;
using UnityEngine.AI;

public class Forklift : MonoBehaviour
{
    [SerializeField] private NavMeshAgent m_agent = null;
    [SerializeField] private float m_distanceToStop = 0.5f;

    private void Start()
    {
        m_agent.SetDestination(new Vector3(20f, 0f, 20f));
    }

	private void Update()
	{
        if (!m_agent.pathPending && m_agent.remainingDistance <= m_distanceToStop)
		{
            // TODO: destination reached
		}
    }
}
