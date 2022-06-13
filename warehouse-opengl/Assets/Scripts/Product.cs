using UnityEngine;

public class Product : ObjectRenderTest
{
	public float Weight { get; private set; } = 100f;
	public string Name { get; private set; } = "New product";

	private NewProductsController m_newProductsController = null;
	private ShelfSlot m_slot = null;
	private bool m_isNew = true;

	public void Initialize(string name, float weight, NewProductsController controller)
	{
		Name = name;
		Weight = weight;
		m_newProductsController = controller;
	}

	public void Select()
	{
		SetTint(Color.yellow);
	}

	public void Deselect()
	{
		SetTint(Color.white);
	}

	public void SetSlot(ShelfSlot slot)
	{
		if (m_isNew)
		{
			m_newProductsController.IsPlaceTaken = false;
			m_isNew = false;
		}

		if (m_slot != null)
			m_slot.RemoveProduct(this);

		m_slot = slot;

		Forklift.Instance.SetPosition(transform.position);
		Forklift.Instance.SetTarget(slot.transform.position);

		transform.position = slot.transform.position;
	}

	public void RemoveFromWarehouse()
	{
		if (m_isNew)
			m_newProductsController.IsPlaceTaken = false;

		if (m_slot != null)
			m_slot.RemoveProduct(this);

		SelectionController.Instance.Deselect();

		Destroy(gameObject);
	}

	private void OnMouseEnter()
	{
		if (SelectionController.Instance.Selected != this)
			SetTint(Color.green);
	}

	private void OnMouseExit()
	{
		if (SelectionController.Instance.Selected != this)
			SetTint(Color.white);
	}

	private void OnMouseDown()
	{
		SelectionController.Instance.Select(this);
	}
}