using UnityEngine;

public class ShelfSlot : ObjectRenderTest
{
	[SerializeField] private Shelf m_shelf = null;

	private bool m_isTaken = false;

	public void RemoveProduct(Product product)
	{
		m_shelf.RemoveWeight(product.Weight);
		m_isTaken = false;
	}

	private void Start()
	{
		m_canRender = false;
	}

	private void OnMouseEnter()
	{
		if (m_isTaken)
			return;

		var selected = SelectionController.Instance.Selected;
		if (selected == null)
				return;

		m_canRender = true;
		if (m_shelf.CanPlace(selected.Weight))
			SetTint(Color.red);
		else
			SetTint(Color.green);
	}

	private void OnMouseExit()
	{
		m_canRender = false;
	}

	private void OnMouseDown()
	{
		if (m_isTaken)
			return;

		var selected = SelectionController.Instance.Selected;
		if (selected == null)
			return;

		if (m_shelf.CanPlace(selected.Weight))
		{
			selected.SetSlot(this);
			m_shelf.AddWeight(selected.Weight);
			m_isTaken = true;
			SelectionController.Instance.Deselect();
		}
	}
}
