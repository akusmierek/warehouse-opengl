using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Forklift : MonoBehaviour
{
	public static Forklift Instance { get; private set; }

    [SerializeField] private NavMeshAgent m_agent = null;
	[SerializeField] private GameObject m_pointPrefab = null;

	private List<GameObject> m_points = new List<GameObject>();
	private Vector3 m_target = Vector3.zero;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Debug.LogError($"Duplicate singelton {nameof(Forklift)} on {gameObject.name}");
			Destroy(this);
			return;
		}
		else
			Instance = this;
	}

	private void Update()
	{
		m_agent.SetDestination(m_target);
		m_agent.isStopped = true;

		for (int i = 0; i < m_agent.path.corners.Length; i++)
		{
			if (m_points.Count <= i)
			{
				var newPoint = Instantiate(m_pointPrefab, transform);
				m_points.Add(newPoint);
			}

			m_points[i].SetActive(true);
			m_points[i].transform.position = m_agent.path.corners[i];
		}

		for (int i = m_agent.path.corners.Length; i < m_points.Count; i++)
			m_points[i].SetActive(false);
    }

	public void SetPosition(Vector3 newPosition)
	{
		newPosition.y = 0f;
		m_agent.Warp(newPosition);
	}

	public void SetTarget(Vector3 target)
	{
		target.y = 0f;
		m_target = target;
	}
}
