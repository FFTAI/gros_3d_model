using UnityEngine;
using UnityWebSocket;

public class UnityToJsCommunication : MonoBehaviour
{
	public Transform RobotTra;
	private JointController _jointController;

	private void Start()
	{
		_jointController = new JointController(RobotTra);
		CreateWebSocket();
	}

	public void Update()
	{
		_jointController?.Update();
	}

	#region websocket

	private IWebSocket _socket;

	private void CreateWebSocket()
	{
		// 创建实例
		string address = "ws://192.168.11.146:8001/ws";
		_socket = new WebSocket(address);

		// 注册回调
		_socket.OnOpen += Socket_OnOpen;
		_socket.OnClose += Socket_OnClose;
		_socket.OnMessage += Socket_OnMessage;
		_socket.OnError += Socket_OnError;

		// 连接
		_socket.ConnectAsync();
	}

	private void Socket_OnOpen(object sender, OpenEventArgs e)
	{
		Debug.Log("Socket_OnOpen");
	}

	private void Socket_OnMessage(object sender, MessageEventArgs e)
	{
		//Debug.Log(string.Format("Receive Bytes ({1}): {0}", e.Data, e.RawData.Length));
		ReceiveMsg(e.Data);
	}

	private void Socket_OnClose(object sender, CloseEventArgs e)
	{
		Debug.Log("Socket_OnClose");
	}

	private void Socket_OnError(object sender, ErrorEventArgs e)
	{
		Debug.Log(string.Format("Socket_OnError: {0}", e.Message));
	}

	/// <summary>
	/// 接收前端传递的机器人实时数据
	/// </summary>
	/// <param name="msg"></param>
	public void ReceiveMsg(string msg)
	{
		Root robotStates = JsonUtility.FromJson<Root>(msg);
		var jointState = robotStates.data.states.jointStates;
		if (jointState == null) { return; }
		foreach (JointStatesItem stateItem in jointState)
		{
			_jointController.RotateJoint(stateItem.name, stateItem.qa);
		}
	}

	private void OnApplicationQuit()
	{
		if (_socket != null && _socket.ReadyState != WebSocketState.Closed)
		{
			_socket.CloseAsync();
		}
	}

	#endregion websocket
}