using System;
using System.IO;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	[Serializable]
	private class WarehouseConfig
	{
		public int RowsCount;
		public float RowWidth;
		public int RacksInRowCount;
		public float MaxShelfLoad;
	}

	[Header("Other parameters")]
	[SerializeField] private GameObject m_rackPrefab = null;

	[SerializeField, Tooltip("Width/Height/Depth")]
	private Vector3 m_rackSize = new Vector3(5f, 3f, 1f);

	private WarehouseConfig m_config = null;

	private void Start()
	{
		string directoryPath = Application.dataPath + "/Config";
		string filePath = directoryPath + "/config.json";
		
		if (!Directory.Exists(directoryPath))
			Directory.CreateDirectory(directoryPath);

		if (!File.Exists(filePath))
		{
			string basePath = Application.streamingAssetsPath + "/config.json";
			File.Copy(basePath, filePath);
		}

		var reader = new StreamReader(filePath);
		var content = reader.ReadToEnd();
		m_config = JsonUtility.FromJson<WarehouseConfig>(content);
		reader.Close();

		Vector3 instancePosition = Vector3.zero;

		for (int row = 0; row < m_config.RowsCount; row++)
		{
			for (int rack = 0; rack < m_config.RacksInRowCount; rack++)
			{
				instancePosition.x += m_rackSize.x;

				var rackInstance = Instantiate(m_rackPrefab, instancePosition + m_rackSize / 2f, Quaternion.identity, transform).GetComponent<Rack>();
				rackInstance.Initialize(m_config.MaxShelfLoad);
			}

			instancePosition.x = 0f;
			instancePosition.z += m_config.RowWidth + m_rackSize.z;
		}
	}
}
