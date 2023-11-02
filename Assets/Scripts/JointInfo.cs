using System.Collections.Generic;
using UnityEngine;

//轴
public enum Axis
{
	X = 0,
	Y = 1,
	Z = 2,
}

//方向
public enum Dir
{
	Front,
	Back,
	Left,
	Right,
	Up,
	Down,
	R, //反向,
	S, //同向,
}

//Unity骨骼
public class UnityBone
{
	//名称
	public string name;

	//哪个轴管用
	public Axis ax;

	public int axIdx
	{ get { return (int)ax; } }

	//轴初始角度
	public int initDeg = 0;

	public UnityBone(string boneName, Axis a, int initDeg)
	{
		name = boneName;
		ax = a;
		this.initDeg = initDeg;
	}

	public UnityBone(string boneName, Axis a)
	{
		name = boneName;
		ax = a;
	}
}

//Unity骨骼朝向数据
public class UnityData
{
	public UnityBone bone;

	// 正值方向 （眼睛看的方向）
	public Dir posDir;

	//模型Transform
	public Transform trans = null;
}

public class JointData
{
	//对应的3D模型信息
	public UnityData mod = new UnityData();

	//对应关节电机名称（唯一）
	public string jointName = null;

	//正值方向和实体机器人相反
	public bool reverse = false;

	protected JointData(UnityBone unity_bone, Dir unity_posDir, string jointName, Dir rbtDir)
	{
		this.jointName = jointName ?? unity_bone.name;
		reverse = rbtDir == Dir.R;

		mod.posDir = unity_posDir;
		mod.bone = unity_bone;
	}
}

//实体机器人信息
public class JointInfo : JointData
{
	public static List<JointInfo> GetJointDatasByUnityTransform(Transform boneTrans)
	{
		var jds = FindJointDatasByUnityBoneName(boneTrans.name);
		foreach (var jd in jds)
		{
			jd.mod.trans = boneTrans;
		}
		return jds;
	}

	private static List<JointInfo> FindJointDatasByUnityBoneName(string boneName)
	{
		var jds = new List<JointInfo>();

		foreach (var jd in JOINTS)
		{
			if (jd.mod.bone.name == boneName)
			{
				jds.Add(jd);
			}
		}
		return jds;
	}

	private JointInfo(UnityBone unity_bone, Dir unity_posDir, Dir rbtDir, string jointName)
		: base(unity_bone, unity_posDir, jointName, rbtDir) { }

	private JointInfo(UnityBone unity_bone, Dir unity_posDir, string jointName)
		: base(unity_bone, unity_posDir, jointName, Dir.S) { }

	private JointInfo(UnityBone unity_bone, Dir unity_posDir, Dir rbtDir)
		: base(unity_bone, unity_posDir, null, rbtDir) { }

	private JointInfo(UnityBone unity_bone, Dir unity_posDir)
		: base(unity_bone, unity_posDir, null, Dir.S) { }

	private static JointInfo[] JOINTS = new JointInfo[]
	{
			new JointInfo(new UnityBone("WaistYaw", Axis.X), Dir.Left),
			new JointInfo(new UnityBone("WaistPitch", Axis.Z), Dir.Front),
			new JointInfo(new UnityBone("WaistRoll", Axis.Y, -90), Dir.Right),

			new JointInfo(new UnityBone("NeckYaw", Axis.Z), Dir.Right, Dir.R),
			new JointInfo(new UnityBone("NeckPitchRoll", Axis.Y), Dir.Left, Dir.R, "NeckRoll"),
			new JointInfo(new UnityBone("NeckPitchRoll", Axis.X), Dir.Front, "NeckPitch"),

			new JointInfo(new UnityBone("ShoulderPitchLeft", Axis.X), Dir.Back, Dir.R ),
			new JointInfo(new UnityBone("ShoulderRollLeft", Axis.Y, -25), Dir.Left, Dir.R),
			new JointInfo(new UnityBone("ShoulderYawLeft", Axis.Z), Dir.Right, Dir.R),
			new JointInfo(new UnityBone("ElbowLeft", Axis.X), Dir.Back, Dir.R),
			new JointInfo(new UnityBone("WristYawLeft", Axis.Z), Dir.Right, Dir.R),
			new JointInfo(new UnityBone("WristPitchRollLeft", Axis.X), Dir.Back, "WristRollLeft"),
			new JointInfo(new UnityBone("WristPitchRollLeft", Axis.Y), Dir.Left, Dir.R, "WristPitchLeft"),

			new JointInfo(new UnityBone("ShoulderPitchRight", Axis.X), Dir.Back),
			new JointInfo(new UnityBone("ShoulderRollRight", Axis.Y, 25), Dir.Left, Dir.R),
			new JointInfo(new UnityBone("ShoulderYawRight", Axis.Z), Dir.Right, Dir.R),
			new JointInfo(new UnityBone("ElbowRight", Axis.X), Dir.Back),
			new JointInfo(new UnityBone("WristYawRight", Axis.Z), Dir.Right, Dir.R),
			new JointInfo(new UnityBone("WristPitchRollRight", Axis.X), Dir.Back, Dir.R,  "WristRollRight") ,
			new JointInfo(new UnityBone("WristPitchRollRight", Axis.Y), Dir.Left, "WristPitchRight"),

			new JointInfo(new UnityBone("LegRollLeft", Axis.Y), Dir.Left),
			new JointInfo(new UnityBone("LegYawLeft", Axis.Z), Dir.Right, Dir.R ),
			new JointInfo(new UnityBone("LegPitchLeft", Axis.X), Dir.Back),
			new JointInfo(new UnityBone("KneeLeft", Axis.X), Dir.Back),
			new JointInfo(new UnityBone("AnklePitchRollLeft", Axis.X), Dir.Down,  "AnklePitchLeft"),
			new JointInfo(new UnityBone("AnklePitchRollLeft", Axis.Y), Dir.Left, "AnkleRollLeft"),

			new JointInfo(new UnityBone("LegRollRight", Axis.Y), Dir.Left),
			new JointInfo(new UnityBone("LegYawRight", Axis.Z), Dir.Right, Dir.R ),
			new JointInfo(new UnityBone("LegPitchRight", Axis.X), Dir.Back ),
			new JointInfo(new UnityBone("KneeRight", Axis.X), Dir.Back),
			new JointInfo(new UnityBone("AnklePitchRollRight", Axis.X), Dir.Down, "AnklePitchRight"),
			new JointInfo(new UnityBone("AnklePitchRollRight", Axis.Y), Dir.Left, "AnkleRollRight"),
	};
}