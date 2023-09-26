using System.Collections.Generic;
using UnityEngine;

public class RobotMaterial : MonoBehaviour
{
	private Material _defaultMaterial;
	private Dictionary<SkinnedMeshRenderer, int> _skinnedMeshDic = new Dictionary<SkinnedMeshRenderer, int>();

	void Start()
	{
		LoadDefaultMaterials();
		SetMetallicMaterials();

	}

	private void LoadDefaultMaterials()
	{
		var jointsRootTra = transform.GetComponentsInChildren<Transform>();
		foreach (var jointRoot in jointsRootTra)
		{
			if (jointRoot.TryGetComponent<SkinnedMeshRenderer>(out var skinnedMesh))
			{
				var materials = skinnedMesh.materials;
				for (int i = 0; i < materials.Length; i++)
				{
					var item = materials[i];
					if (item != null && item.name.Equals("01 - Default (Instance)"))
					{
						_skinnedMeshDic.Add(skinnedMesh, i);
						_defaultMaterial = item;
					}
				}
			}
		}
	}

	private void SetDefaultMaterials()
	{
		if (_defaultMaterial != null)
		{
			SetMaterials(_defaultMaterial);
		}
	}

	private void SetMetallicMaterials()
	{
		Material loadedMaterial = Resources.Load<Material>($"Materials/M_YFMM_07");
		if (loadedMaterial != null)
		{
			SetMaterials(loadedMaterial);
		}
	}

	private void SetMaterials(Material cM)
	{
		foreach (var kvp in _skinnedMeshDic)
		{
			var skinnedMesh = kvp.Key;
			var materials = skinnedMesh.materials;
			materials[kvp.Value] = cM;
			skinnedMesh.materials = materials;
		}
	}

}
