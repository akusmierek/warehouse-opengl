using UnityEngine;

public class ObjectRenderTest : MonoBehaviour
{
	[SerializeField] private MeshFilter m_meshFilter = null;
	[SerializeField] private MeshRenderer m_meshRenderer = null;

	// Fast way to view it in edit mode
	private void OnDrawGizmos()
	{
		OnRenderObject();
	}

	private void OnRenderObject()
	{
		GL.PushMatrix();
		m_meshRenderer.sharedMaterial.SetPass(0);
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
}
