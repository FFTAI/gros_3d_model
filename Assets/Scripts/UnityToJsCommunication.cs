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
	RobotType robotType = RobotType.T2;

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
    private enum RobotType
	{ 
	 T1,
	 T2
	}

	public void StartWebSocket(string ip)
	{
		ConnectWebSocket(ip);
	}

	public void CloseWebSocket()
	{
		DisconnectWebSocket();
	}
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
	#region websocket

	private IWebSocket _socket;

    #region 外部暴露出的供JS调用的方法

    public void StartConnect(string robotType)
	{
		string str = string.Format($"设置机器人类型为：{robotType}");
		isCanUpdate = true;
		CheckRobotType(robotType);
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

    public void JointRote(string jointName,string roteData)
	{
		if (_jointController == null) return;
		_jointController.RotateJoint(jointName,roteData);
	}

	public void EndConnect()
	{
		isCanUpdate = false;
		ResetTransforms(RobotTra);
	}
    #endregion

    public void ConnectWebSocket(string robotName,string robotType)
	{
		CheckRobotType(robotType);
		ConnectWebSocket(robotName);
	}

	public void ConnectWebSocket(string robotName)
	{
		string finallIP = ip + "remote/" + robotName;
		if (_socket != null)
		{
			DisconnectWebSocket();
		}
		CreateWebSocket(finallIP);
	}

	private void CheckRobotType(string robotType)
	{
		switch (robotType)
		{
			case "T1":
				this.robotType = RobotType.T1;
				break;
			case "T2":
				this.robotType = RobotType.T2;
				break;
			default:
				this.robotType = RobotType.T1;
				break;
		}
	}

	private void CreateWebSocket(string ip)
	{
		// 创建实例
		string address = ip;
		_socket = new WebSocket(address);

		// 注册回调
		_socket.OnOpen += Socket_OnOpen;
		_socket.OnClose += Socket_OnClose;
		_socket.OnMessage += Socket_OnMessage;
		_socket.OnError += Socket_OnError;

		// 连接
		_socket.ConnectAsync();
	}

	private void DisconnectWebSocket()
	{
		if (_socket != null)
		{
			// 关闭 WebSocket 连接
			_socket.CloseAsync();
			// 可选：清理回调事件
			_socket.OnOpen -= Socket_OnOpen;
			_socket.OnClose -= Socket_OnClose;
			_socket.OnMessage -= Socket_OnMessage;
			_socket.OnError -= Socket_OnError;

			// 可选：置空 WebSocket 实例
			_socket = null;
		}
	}

	private void Socket_OnOpen(object sender, OpenEventArgs e)
	{
		isCanUpdate = true;
		Debug.Log("Socket_OnOpen");
	}

	private void Socket_OnMessage(object sender, MessageEventArgs e)
	{
		Debug.Log(string.Format("Receive Bytes ({1}): {0}", e.Data, e.RawData.Length));
		ReceiveMsg(e.Data);
	}

	private void Socket_OnClose(object sender, CloseEventArgs e)
	{
		Debug.Log("Socket_OnClose");
		isCanUpdate = false;

	}

	private void Socket_OnError(object sender, ErrorEventArgs e)
	{
		Debug.Log(string.Format("Socket_OnError: {0}", e.Message));
		isCanUpdate = false;

	}

	/// <summary>
	/// 接收前端传递的机器人实时数据
	/// </summary>
	/// <param name="msg"></param>
	public void ReceiveMsg(string msg)
	{
		if (robotType == RobotType.T1)
		{
			Root robotStates = JsonUtility.FromJson<Root>(msg);
			var jointState = robotStates.data.states.jointStates;
			if (jointState == null) { return; }
			foreach (JointStatesItem stateItem in jointState)
			{
				_jointController.RotateJoint(stateItem.name, stateItem.qa);
				Debug.Log("stateItem.name" + stateItem.name);
				Debug.Log("stateItem.qa" + stateItem.qa);
			}
		}
		else if (robotType == RobotType.T2)
		{
			RootT2 robotStates = JsonUtility.FromJson<RootT2>(msg);
			Joint_statesT2 jointState = robotStates.joint_states;
			if (jointState == null) { return; }
			_jointController.RotateJoint(jointState);
		}
	}



	


	private void OnApplicationQuit()
	{
		if (_socket != null && _socket.ReadyState != WebSocketState.Closed)
		{
			_socket.CloseAsync();
		}
	}

	public static bool TryConvertToDouble(string input, out double result)
	{
		if (string.IsNullOrWhiteSpace(input))
		{
			result = 0;
			return false;
		}

		return double.TryParse(input, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out result);
	}


	#endregion websocket
}