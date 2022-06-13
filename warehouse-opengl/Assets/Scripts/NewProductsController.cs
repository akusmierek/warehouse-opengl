using UnityEngine;
using UnityEngine.UI;

public class NewProductsController : MonoBehaviour
{
	public bool IsPlaceTaken { get; set; } = false;
	
	[SerializeField] private GameObject m_prefab = null;
	[SerializeField] private Button m_button = null;
	[SerializeField] private TMPro.TMP_InputField m_weightInput = null;
	[SerializeField] private TMPro.TMP_InputField m_nameInput = null;

	public void AddNewProduct()
	{
		int weight = int.Parse(m_weightInput.text);
		if (weight <= 0)
			return;

		var newProduct = Instantiate(m_prefab, transform.position, Quaternion.identity, transform).GetComponent<Product>();
		newProduct.Initialize(m_nameInput.text, weight, this);

		IsPlaceTaken = true;
	}

	private void Update()
	{
		m_button.interactable = !IsPlaceTaken;
	}
}
