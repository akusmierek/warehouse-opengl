using UnityEngine;

public class ObjectRenderTest : MonoBehaviour
{
	[SerializeField] private MeshFilter m_meshFilter = null;
	[SerializeField] private MeshRenderer m_meshRenderer = null;

	protected bool m_canRender = true;
	private bool m_setTint = false;
	private Color m_tint = Color.white;

	public void RenderObject(int pass)
	{
		if (!m_canRender || !isActiveAndEnabled)
			return;

		GL.PushMatrix();
		m_meshRenderer.sharedMaterial.SetPass(pass);
		if (m_setTint)
			m_meshRenderer.material.SetColor("_Tint", m_tint);
		GL.Begin(GL.TRIANGLES);

		var indices = m_meshFilter.sharedMesh.GetIndices(0);
		var vertices = m_meshFilter.sharedMesh.vertices;
		var uvs = m_meshFilter.sharedMesh.uv;
		var normals = m_meshFilter.sharedMesh.normals;

		foreach (int i in indices)
		{
			Vector3 vertex = vertices[i];
			Vector2 uv = uvs[i];
			Vector3 normal = normals[i];

			GL.MultiTexCoord2(0, uv.x, uv.y); // Main texture
			GL.MultiTexCoord(1, m_meshRenderer.localToWorldMatrix.MultiplyVector(normal)); // Normals in world space
			GL.Vertex(m_meshRenderer.localToWorldMatrix.MultiplyPoint3x4(vertex)); // Vertex in world space
		}

		GL.End();
		GL.PopMatrix();
	}

	public void SetTint(Color tint)
	{
		m_tint = tint;
		m_setTint = true;
	}
}
