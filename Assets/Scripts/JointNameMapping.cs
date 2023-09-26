using System.Collections.Generic;

public class JointNameMapping
{
	private Dictionary<string, string> mapping = new Dictionary<string, string>();
	private static JointNameMapping instance;

	public static JointNameMapping Ins
	{
		get { return instance ?? (instance = new JointNameMapping()); }
	}

	public JointNameMapping()
	{
		// 初始化关节名称映射
		mapping["left_hip_roll"] = "LegRollLeft";
		mapping["left_hip_yaw"] = "LegYawLeft";
		mapping["left_hip_pitch"] = "LegPitchLeft";

		mapping["left_knee_pitch"] = "KneeLeft";
		mapping["left_ankle_pitch"] = "AnklePitchLeft";
		mapping["left_ankle_roll"] = "AnkleRollLeft";

		mapping["right_hip_roll"] = "LegRollRight";
		mapping["right_hip_yaw"] = "LegYawRight";
		mapping["right_hip_pitch"] = "LegPitchRight";

		mapping["right_knee_pitch"] = "KneeRight";
		mapping["right_ankle_pitch"] = "AnklePitchRight";
		mapping["right_ankle_roll"] = "AnkleRollRight";

		mapping["waist_yaw"] = "WaistYaw";
		mapping["waist_pitch"] = "WaistPitch";
		mapping["waist_roll"] = "WaistRoll";

		mapping["head_yaw"] = "NeckYaw";
		mapping["head_pitch"] = "NeckPitch";
		mapping["head_roll"] = "NeckRoll";
	}

	public string GetLocalName(string jsonName)
	{
		if (mapping.ContainsKey(jsonName))
		{
			return mapping[jsonName];
		}
		else
		{
			return jsonName;
		}
	}
}