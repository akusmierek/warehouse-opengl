using UnityEngine;

public class Rack : MonoBehaviour
{
	[SerializeField] private Shelf[] m_shelves = null;

    public void Initialize(float maxShelfLoad)
	{
		foreach (var shelf in m_shelves)
			shelf.Initialize(maxShelfLoad);
	}
}
