using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	[Header("User parameters")]
    [SerializeField] private int m_rowsCount = 5;
    [SerializeField] private float m_rowWidth = 4f;
    [SerializeField] private int m_racksInRowCount = 10;
    [SerializeField] private float m_maxShelfLoad = 500f;

	[Header("Other parameters")]
	[SerializeField] private GameObject m_rackPrefab = null;

	[SerializeField, Tooltip("Width/Height/Depth")]
	private Vector3 m_rackSize = new Vector3(5f, 3f, 1f);

	private void Start()
	{
		Vector3 instancePosition = Vector3.zero;

		for (int row = 0; row < m_rowsCount; row++)
		{
			for (int rack = 0; rack < m_racksInRowCount; rack++)
			{
				instancePosition.x += m_rackSize.x;

				var rackInstance = Instantiate(m_rackPrefab, instancePosition + m_rackSize / 2f, Quaternion.identity, transform).GetComponent<Rack>();
				rackInstance.Initialize(m_maxShelfLoad);
			}

			instancePosition.x = 0f;
			instancePosition.z += m_rowWidth + m_rackSize.z;
		}
	}
}
