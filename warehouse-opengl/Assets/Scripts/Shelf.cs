using UnityEngine;

public class Shelf : MonoBehaviour
{
	private float m_maxLoad = 0f;

    public void Initialize(float maxLoad)
	{
		m_maxLoad = maxLoad;
	}
}
