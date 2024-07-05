using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityWebSocket;
[System.Serializable]
public class TransformInfo
{
	public string name;
	public Vector3 localPosition;
	public Quaternion localRotation;

	public TransformInfo(string name, Vector3 localPosition, Quaternion localRotation)
	{
		this.name = name;
		this.localPosition = localPosition;
		this.localRotation = localRotation;
	}
}

public class UnityToJsCommunication : MonoBehaviour
{
	public Transform RobotTra;
	private JointController _jointController;
	private bool isCanUpdate = false;
	private string ip;

	#region 重置模型节点
	public List<TransformInfo> transformInfos = new List<TransformInfo>();

	public void ResetTransforms(Transform trans)
	{
		int index = 0;
		ResetTransformsRecursive(trans, ref index);
	}
	void RecordTransforms(Transform parent)
	{
		foreach (Transform child in parent)
		{
			TransformInfo info = new TransformInfo(child.name, child.localPosition, child.localRotation);
			transformInfos.Add(info);

			// 递归调用，记录嵌套子节点
			RecordTransforms(child);
		}
	}

	void ResetTransformsRecursive(Transform parent, ref int index)
	{
		foreach (Transform child in parent)
		{
			if (index < transformInfos.Count)
			{
				TransformInfo info = transformInfos[index];
				if (child.name == info.name)
				{
					child.localPosition = info.localPosition;
					child.localRotation = info.localRotation;
					index++;
				}
			}

			// 递归调用，重置嵌套子节点
			ResetTransformsRecursive(child, ref index);
		}
	}
	#endregion

	string testData = "";
	
	private void Start()
	{
		_jointController = new JointController(RobotTra);
		RecordTransforms(RobotTra);
		

	}

	private void ReadConfig()
	{
		WebglConfigReader reader = new WebglConfigReader();
		StartCoroutine(reader.ReadData((json) =>
		{
			Root robotStates = JsonUtility.FromJson<Root>(json);
		}));
	}
    private void Update()
    {
		//if (isCanUpdate)
		//	_jointController?.Updata();
	}
	private IWebSocket _socket;

    #region 外部暴露出的供JS调用的方法

    public void StartConnect(string robotType)
	{
		string str = string.Format($"设置机器人类型为：{robotType}");
		isCanUpdate = true;
	}

    public void JointRote(string jsonData)
    {
		string[] datas = jsonData.Split(',');
		string jointName = datas[0];
		string jointData = datas[1];
		double roteData=double.Parse(jointData);
		if (_jointController == null) return;
		_jointController.RotateJoint(jointName, roteData);
	}

	public void EndConnect()
	{
		isCanUpdate = false;
		ResetTransforms(RobotTra);
	}
    #endregion
}