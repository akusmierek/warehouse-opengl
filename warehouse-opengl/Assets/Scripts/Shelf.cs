using UnityEngine;

public class Shelf : ObjectRenderTest
{
	private float m_maxLoad = 0f;

    public void Initialize(float maxLoad)
	{
		m_maxLoad = maxLoad;

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
