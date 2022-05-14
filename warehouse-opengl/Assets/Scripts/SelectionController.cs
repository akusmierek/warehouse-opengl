using UnityEngine;

public class SelectionController : MonoBehaviour
{
	public static SelectionController Instance { get; private set; }

	public Product Selected { get; private set; } = null;

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
	}

	public void Select(Product newSelected)
	{
		if (Selected != null)
			Selected.Deselect();

		if (Selected == newSelected)
		{
			Selected = null;
			return;
		}

		newSelected.Select();
		Selected = newSelected;
	}

	public void Deselect()
	{
		if (Selected != null)
			Selected.Deselect();

		Selected = null;
	}
}
