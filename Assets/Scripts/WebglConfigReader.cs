using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class WebglConfigReader : MonoBehaviour
{
	public string ServerIP;
	public IEnumerator ReadData(Action<string> callback)
	{
		string path = Path.Combine(Application.streamingAssetsPath, "Configs/ServerIp.txt");
		Debug.Log(path);
		UnityWebRequest request = UnityWebRequest.Get(path);
		yield return request.SendWebRequest();
		if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.LogError("缺少地址配置文件");
		}
		else
		{
			ServerIP = request.downloadHandler.text;
			callback?.Invoke(ServerIP);
		}
	}
}
