using UnityEngine;

public class LightsManager : MonoBehaviour
{
	[SerializeField] private Light[] m_lights = null;
	public Light[] Lights => m_lights;

	// Fast way to view it in edit mode
	private void OnDrawGizmos()
	{
		Update();
	}

	private void Update()
	{
		// Main directional light
		Vector4 lightVector = -m_lights[0].transform.forward;
		lightVector.w = m_lights[0].type == LightType.Directional ? 0 : 1;
		Shader.SetGlobalVector(Shader.PropertyToID("_Light0"), lightVector);
		Shader.SetGlobalColor(Shader.PropertyToID("_Light0Color"), m_lights[0].color);
	}
}
