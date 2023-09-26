using System.Collections.Generic;
using UnityEngine;

public class RobotData
{
	public Dictionary<string, JointInfo> Joints = new Dictionary<string, JointInfo>();

	public RobotData(Transform tra)
	{
		CacheJoints(tra);
	}

	public void CacheJoints(Transform tra)
	{
		var trans = tra.GetComponentsInChildren<Transform>();
		foreach (var tran in trans)
		{
			var jds = JointInfo.GetJointDatasByUnityTransform(tran);
			if (jds.Count == 0)
			{
				Debug.LogWarning($"No Unity bones named \"{tran.name}\"");
			}

			foreach (var jd in jds)
			{
				Joints.Add($"{jd.jointName}", jd);
			}
		}
	}
}