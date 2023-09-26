using System.Collections.Generic;
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
		LoadDefaultMaterials(RobotTra);
		HandleDropdownValueChanged(7);

		#region test data

		/*string msg = @"{
			""jointStates"": [
                {
                    ""name"": ""left_hip_roll"",
                    ""qa"": -0.02546578258147633,
                    ""qc"": -0.013560129743458845,
                    ""qdota"": 0.262213045107391,
                    ""qdotc"": 0.20297288339132072,
                    ""taua"": 43.712521314478366,
                    ""tauc"": 42.559748828697934
                },
                {
                    ""name"": ""left_hip_yaw"",
                    ""qa"": 0.12786941742318994,
                    ""qc"": 0.1278365100435194,
                    ""qdota"": -0.11849363338582102,
                    ""qdotc"": 0.0007707965668590758,
                    ""taua"": 2.471961856060173,
                    ""tauc"": 2.320619890555112
                },
                {
                    ""name"": ""left_hip_pitch"",
                    ""qa"": -0.43733414749870114,
                    ""qc"": -0.44218208059582126,
                    ""qdota"": 0.1536461559018828,
                    ""qdotc"": 0.13045365866527125,
                    ""taua"": -10.692760093098855,
                    ""tauc"": -10.345011723161106
                },
                {
                    ""name"": ""left_knee_pitch"",
                    ""qa"": 0.8945902029037744,
                    ""qc"": 0.9011493817485747,
                    ""qdota"": 0.000908667748393708,
                    ""qdotc"": -0.033014887085466085,
                    ""taua"": -65.275913209951,
                    ""tauc"": -65.50897533346014
                },
                {
                    ""name"": ""left_ankle_pitch"",
                    ""qa"": -0.46108483027723235,
                    ""qc"": -0.46405943113246306,
                    ""qdota"": -0.07861382704346707,
                    ""qdotc"": -0.07155541668821655,
                    ""taua"": 1.2604937245989296,
                    ""tauc"": 1.269695323227402
                },
                {
                    ""name"": ""left_ankle_roll"",
                    ""qa"": 0.015996082774432104,
                    ""qc"": 0.016492350263179835,
                    ""qdota"": -0.17536353151552994,
                    ""qdotc"": -0.20131725574100748,
                    ""taua"": 2.290014269353575,
                    ""tauc"": 2.295743898724797
                },
                {
                    ""name"": ""right_hip_roll"",
                    ""qa"": -0.013226038750305591,
                    ""qc"": -0.015761387897264903,
                    ""qdota"": -0.09132647188670433,
                    ""qdotc"": -0.13482055250557218,
                    ""taua"": -5.24349113247272,
                    ""tauc"": -2.1957253632288394
                },
                {
                    ""name"": ""right_hip_yaw"",
                    ""qa"": -0.14171780803301487,
                    ""qc"": -0.1426230999390583,
                    ""qdota"": 0.003757868337257311,
                    ""qdotc"": 0.005926004700085291,
                    ""taua"": -0.7516647678422792,
                    ""tauc"": 0.13968897309946543
                },
                {
                    ""name"": ""right_hip_pitch"",
                    ""qa"": -0.6233623510230288,
                    ""qc"": -0.6169001132968169,
                    ""qdota"": 1.883107212222108,
                    ""qdotc"": 1.7247157426753472,
                    ""taua"": -1.2689395268865145,
                    ""tauc"": -3.0289892856127247
                },
                {
                    ""name"": ""right_knee_pitch"",
                    ""qa"": 1.212956817018927,
                    ""qc"": 1.2104137874344816,
                    ""qdota"": -4.022938885631675,
                    ""qdotc"": -3.955161939997631,
                    ""taua"": 1.9193756905555925,
                    ""tauc"": 2.0694751806148193
                },
                {
                    ""name"": ""right_ankle_pitch"",
                    ""qa"": -0.5452237334344903,
                    ""qc"": -0.5395203740598727,
                    ""qdota"": 2.3318917536948613,
                    ""qdotc"": 2.2494887791601204,
                    ""taua"": -0.027668259293422516,
                    ""tauc"": -0.0428235759162249
                },
                {
                    ""name"": ""right_ankle_roll"",
                    ""qa"": 0.022262416357754417,
                    ""qc"": 0.0030048727350427515,
                    ""qdota"": 0.07874262534566905,
                    ""qdotc"": 0.1336004453082492,
                    ""taua"": -0.0189883051599207,
                    ""tauc"": -0.00021387743939403332
                },
                {
                    ""name"": ""waist_yaw"",
                    ""qa"": -2.2511574384568186e-05,
                    ""qc"": 0,
                    ""qdota"": 0.00469288655042478,
                    ""qdotc"": 0,
                    ""taua"": -0.012118080361299043,
                    ""tauc"": 0
                },
                {
                    ""name"": ""waist_pitch"",
                    ""qa"": -0.0019126039205978884,
                    ""qc"": 0,
                    ""qdota"": -0.009583431421211718,
                    ""qdotc"": 0,
                    ""taua"": 1.9866755218744905,
                    ""tauc"": 0
                },
                {
                    ""name"": ""waist_roll"",
                    ""qa"": -0.0021898636854364995,
                    ""qc"": 0,
                    ""qdota"": -0.018525595807805113,
                    ""qdotc"": 0,
                    ""taua"": 2.337377638927798,
                    ""tauc"": 0
                },
                {
                    ""name"": ""head_yaw"",
                    ""qa"": 0,
                    ""qc"": 0,
                    ""qdota"": 0,
                    ""qdotc"": 0,
                    ""taua"": 0,
                    ""tauc"": 0
                },
                {
                    ""name"": ""head_pitch"",
                    ""qa"": 0,
                    ""qc"": 0,
                    ""qdota"": 0,
                    ""qdotc"": 0,
                    ""taua"": 0,
                    ""tauc"": 0
                },
                {
                    ""name"": ""head_roll"",
                    ""qa"": 0,
                    ""qc"": 0,
                    ""qdota"": 0,
                    ""qdotc"": 0,
                    ""taua"": 0,
                    ""tauc"": 0
                }
            ]
		}";
		ParseData(msg);*/

		#endregion test data
	}

	public void Update()
	{
		_jointController?.Update();
	}

	#region websocket

	private IWebSocket _socket;

	private void CreateWebSocket()
	{
		// 创建实例ws://192.168.11.62:8080
		string address = "ws://192.168.11.62:8088";
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
		Debug.Log(string.Format("Receive Bytes ({1}): {0}", e.Data, e.RawData.Length));
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
		JointStatesInfo joitStates = JsonUtility.FromJson<JointStatesInfo>(msg);
		if (joitStates == null) { return; }
		foreach (JointStates stateItem in joitStates.jointStates)
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

	#region Model Material

	private Material _defaultMaterial;
	private Dictionary<SkinnedMeshRenderer, int> _skinnedMeshDic = new Dictionary<SkinnedMeshRenderer, int>();

	private void HandleDropdownValueChanged(int value)
	{
		if (value == 0)
		{
			if (_defaultMaterial != null)
			{
				ChangeMaterials(_defaultMaterial);
			}
		}
		else
		{
			string formattedNumber = string.Format("{0:D2}", value);
			var materialName = $"M_YFMM_{formattedNumber}";
			Material loadedMaterial = Resources.Load<Material>($"Materials/{materialName}");
			if (loadedMaterial != null)
			{
				ChangeMaterials(loadedMaterial);
			}
		}
	}

	private void LoadDefaultMaterials(Transform tra)
	{
		var jointsRootTra = tra.GetComponentsInChildren<Transform>();
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

	private void ChangeMaterials(Material cM)
	{
		foreach (var kvp in _skinnedMeshDic)
		{
			var skinnedMesh = kvp.Key;
			var materials = skinnedMesh.materials;
			materials[kvp.Value] = cM;
			skinnedMesh.materials = materials;
		}
	}

	#endregion Model Material
}