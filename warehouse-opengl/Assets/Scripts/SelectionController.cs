using UnityEngine;

public class SelectionController : MonoBehaviour
{
	public static SelectionController Instance { get; private set; }

	public Product Selected { get; private set; } = null;

	[SerializeField] private GameObject m_productPanel = null;
	[SerializeField] private TMPro.TMP_Text m_productName = null;
	[SerializeField] private TMPro.TMP_Text m_productWeight = null;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Debug.LogError($"Duplicate singelton {nameof(SelectionController)} on {gameObject.name}");
			Destroy(this);
			return;
		}
		else
			Instance = this;

		m_productPanel.SetActive(false);
	}

	public void Select(Product newSelected)
	{
		if (Selected != null)
			Selected.Deselect();

		if (Selected == newSelected)
		{
			OnSelectedNull();
			return;
		}

		newSelected.Select();
		Selected = newSelected;

		m_productPanel.SetActive(true);
		m_productName.text = newSelected.Name;
		m_productWeight.text = newSelected.Weight.ToString();
	}

	public void Deselect()
	{
		if (Selected != null)
			Selected.Deselect();

		OnSelectedNull();
	}

	private void OnSelectedNull()
	{
		Selected = null;
		m_productPanel.SetActive(false);
	}
}
