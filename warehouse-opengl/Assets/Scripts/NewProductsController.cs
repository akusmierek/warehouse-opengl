using UnityEngine;

public class NewProductsController : MonoBehaviour
{
	public bool IsPlaceTaken { get; set; } = false;
	
	[SerializeField] private GameObject m_prefab = null;
    [SerializeField] private float[] m_weights = null;

	private int m_nextWeightId = 0;

	private void Update()
	{
		if (m_weights == null || m_nextWeightId >= m_weights.Length)
			return;

		if (!IsPlaceTaken)
		{
			var newProduct = Instantiate(m_prefab, transform.position, Quaternion.identity, transform).GetComponent<Product>();
			newProduct.Initialize(m_weights[m_nextWeightId], this);

			m_nextWeightId++;
			IsPlaceTaken = true;
		}
	}
}
