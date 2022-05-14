using UnityEngine;

public class Product : ObjectRenderTest
{
	public float Weight { get; private set; } = 100f;

	private NewProductsController m_newProductsController = null;
	private ShelfSlot m_slot = null;
	private bool m_isNew = true;

	public void Initialize(float weight, NewProductsController controller)
	{
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
		transform.position = slot.transform.position;
	}

	public void RemoveFromWarehouse()
	{
		if (m_slot != null)
			m_slot.RemoveProduct(this);
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