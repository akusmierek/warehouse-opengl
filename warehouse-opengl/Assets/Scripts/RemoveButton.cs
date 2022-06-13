using UnityEngine;

public class RemoveButton : MonoBehaviour
{
    public void RemoveProduct()
	{
		SelectionController.Instance.Selected.RemoveFromWarehouse();
	}
}
