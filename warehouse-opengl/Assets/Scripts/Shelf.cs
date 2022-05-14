using UnityEngine;

public class Shelf : MonoBehaviour
{
	private float m_maxLoad = 0f;
	private float m_currentLoad = 0f;

    public void Initialize(float maxLoad)
	{
		m_maxLoad = maxLoad;
	}

	public bool CanPlace(float weight)
	{
		return m_currentLoad + weight < m_maxLoad;
	}

	public void AddWeight(float weight)
	{
		m_currentLoad += weight;
	}

	public void RemoveWeight(float weight)
	{
		m_currentLoad -= weight;
	}
}
