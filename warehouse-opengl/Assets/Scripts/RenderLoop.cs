using UnityEngine;

public class RenderLoop : MonoBehaviour
{
	[SerializeField] private Light[] m_lights = null;
	public Light[] Lights => m_lights;

	[SerializeField] private ObjectRenderTest[] m_objectsToRender = null;

	// Fast way to view it in edit mode
	private void OnDrawGizmos()
	{
		OnPostRender();
	}
	
	private void OnPostRender()
	{
		// _Light0.xyz - position when directional light / direction when point light
		// _Light0.w - 0 means directional light / else it is range of the point light
		// _Light0Color.xyz - color of the light
		// _Light0Color.w - intensity of the light

		Vector4 light0;
		Vector4 light0Color;

		for (int i = 0; i < m_lights.Length; i++)
		{
			if (m_lights[i].type == LightType.Directional)
			{
				light0 = -m_lights[i].transform.forward;
				light0.w = 0;
			}
			else
			{
				light0 = m_lights[i].transform.position;
				light0.w = m_lights[i].range;
			}
			light0Color = m_lights[i].color;
			light0Color.w = m_lights[i].intensity;
			Shader.SetGlobalVector(Shader.PropertyToID("_Light0"), light0);
			Shader.SetGlobalVector(Shader.PropertyToID("_Light0Color"), light0Color);

			foreach (var objectToRender in m_objectsToRender)
				objectToRender.RenderObject(i);
		}
	}
}
