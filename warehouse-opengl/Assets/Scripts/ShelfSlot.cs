using UnityEngine;

public class ShelfSlot : ObjectRenderTest
{
	[SerializeField] private Shelf m_shelf = null;

	private void Start()
	{
		m_canRender = false;
	}

	// TODO: Check if product can be placed and set proper color
	private void OnMouseEnter()
	{
		m_canRender = true;
	}

	private void OnMouseExit()
	{
		m_canRender = false;
	}

	private void OnMouseDown()
	{
		// TODO: Place product here
	}
}
