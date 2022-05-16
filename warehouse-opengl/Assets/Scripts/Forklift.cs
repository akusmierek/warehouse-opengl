using UnityEngine;
using UnityEngine.AI;

public class Forklift : MonoBehaviour
{
    [SerializeField] private NavMeshAgent m_agent = null;

    private void Start()
    {
        m_agent.SetDestination(new Vector3(20f, 0f, 20f));
    }
}
