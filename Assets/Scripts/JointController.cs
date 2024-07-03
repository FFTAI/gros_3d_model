using System.Reflection;
using UnityEngine;

public class JointController
{
	private readonly RobotData _robotData;
	private readonly Transform _robotCenterTra;
	public JointController(Transform transform)
	{
		_robotCenterTra = transform.Find("Centering");
		_robotData = new RobotData(transform);
	}

	/// <summary>
	/// 初始化关节角度
	/// </summary>
	public void InitJoints()
	{
		if (_robotData == null) return;
		foreach (var joint in _robotData.Joints.Values)
		{
			CommonRotate(joint);
		}
	}
	#region 关节旋转
	/// <summary>
	/// 旋转关节
	/// </summary>
	/// <param name="jointName"></param>
	/// <param name="radians"></param>
	public void RotateJoint(string jointName, double radians)
	{
		if (_robotData == null) return;
		var modelName = JointNameMapping.Ins.GetLocalName(jointName);
		var jointDegrees = (float)radians * Mathf.Rad2Deg;
		if (_robotData.Joints.TryGetValue(modelName, out JointInfo jointInfo))
		{
			CommonRotate(jointInfo, jointDegrees);
		}
	}
	/// <summary>
	/// T2旋转关节
	/// </summary>
	/// <param name="jointName"></param>
	/// <param name="radians"></param>
	public void RotateJoint(Joint_statesT2 joint_StatesT2)
    {
		if (joint_StatesT2 == null) return;
		RotateJoint("waist_roll", joint_StatesT2.waist_roll);
		RotateJoint("left_ankle_pitch", joint_StatesT2.left_ankle_pitch);
		RotateJoint("right_hip_pitch", joint_StatesT2.right_hip_pitch);
		RotateJoint("right_knee_pitch", joint_StatesT2.right_knee_pitch);
		RotateJoint("right_ankle_roll", joint_StatesT2.right_ankle_roll);
		RotateJoint("left_hip_roll", joint_StatesT2.left_hip_roll);
		RotateJoint("waist_pitch", joint_StatesT2.waist_pitch);
		RotateJoint("head_roll", joint_StatesT2.head_roll);
		RotateJoint("left_hip_yaw", joint_StatesT2.left_hip_yaw);
		RotateJoint("waist_yaw", joint_StatesT2.waist_yaw);
		RotateJoint("head_pitch", joint_StatesT2.head_pitch);
		RotateJoint("right_hip_yaw", joint_StatesT2.right_hip_yaw);
		RotateJoint("head_yaw", joint_StatesT2.head_yaw);
		RotateJoint("right_ankle_pitch", joint_StatesT2.right_ankle_pitch);
		RotateJoint("right_hip_roll", joint_StatesT2.right_hip_roll);
		RotateJoint("left_ankle_roll", joint_StatesT2.left_ankle_roll);
		RotateJoint("left_knee_pitch", joint_StatesT2.left_knee_pitch);
		RotateJoint("left_hip_pitch", joint_StatesT2.left_hip_pitch);
	}

	public void RotateJoint(string jointName,string jointData)
	{
        switch (jointName)
        {
			case "left_hip_roll":
				RotateJoint("left_hip_roll", double.Parse(jointData));
				break;
			case "left_hip_yaw":
				RotateJoint("left_hip_yaw", double.Parse(jointData));
				break;
			case "left_hip_pitch":
				RotateJoint("left_hip_pitch", double.Parse(jointData));
				break;
			case "left_knee_pitch":
				RotateJoint("left_knee_pitch", double.Parse(jointData));
				break;
			case "left_ankle_pitch":
				RotateJoint("left_ankle_pitch", double.Parse(jointData));
				break;
			case "left_ankle_roll":
				RotateJoint("left_ankle_roll", double.Parse(jointData));
				break;
			case "right_hip_roll":
				RotateJoint("right_hip_roll", double.Parse(jointData));
				break;
			case "right_hip_yaw":
				RotateJoint("right_hip_yaw", double.Parse(jointData));
				break;
			case "right_hip_pitch":
				RotateJoint("right_hip_pitch", double.Parse(jointData));
				break;
			case "right_knee_pitch":
				RotateJoint("right_knee_pitch", double.Parse(jointData));
				break;
			case "right_ankle_pitch":
				RotateJoint("right_ankle_pitch", double.Parse(jointData));
				break;
			case "right_ankle_roll":
				RotateJoint("right_ankle_roll", double.Parse(jointData));
				break;
			case "waist_yaw":
				RotateJoint("waist_yaw", double.Parse(jointData));
				break;
			case "waist_pitch":
				RotateJoint("waist_pitch", double.Parse(jointData));
				break;
			case "waist_roll":
				RotateJoint("waist_roll", double.Parse(jointData));
				break;
			case "head_yaw":
				RotateJoint("head_yaw", double.Parse(jointData));
				break;
			case "head_pitch":
				RotateJoint("head_pitch", double.Parse(jointData));
				break;
			case "head_roll":
				RotateJoint("head_roll", double.Parse(jointData));
				break;
			case "right_pitch_shoulder":
				RotateJoint("right_pitch_shoulder", double.Parse(jointData));
				break;
			case "right_roll_shoulder":
				RotateJoint("right_roll_shoulder", double.Parse(jointData));
				break;
			case "right_yaw_shoulder":
				RotateJoint("right_yaw_shoulder", double.Parse(jointData));
				break;
			case "right_elbow":
				RotateJoint("right_elbow", double.Parse(jointData));
				break;
			case "left_pitch_shoulder":
				RotateJoint("left_pitch_shoulder", double.Parse(jointData));
				break;
			case "left_roll_shoulder":
				RotateJoint("left_roll_shoulder", double.Parse(jointData));
				break;
			case "left_yaw_shoulder":
				RotateJoint("left_yaw_shoulder", double.Parse(jointData));
				break;
			case "left_elbow":
				RotateJoint("left_elbow", double.Parse(jointData));
				break;
			case "left_roll_wrist":
				RotateJoint("left_roll_wrist", double.Parse(jointData));
				break;
			case "left_pitch_wrist":
				RotateJoint("left_pitch_wrist", double.Parse(jointData));
				break;
			case "left_yaw_wrist":
				RotateJoint("left_yaw_wrist", double.Parse(jointData));
				break;
			case "right_roll_wrist":
				RotateJoint("right_roll_wrist", double.Parse(jointData));
				break;
			case "right_pitch_wrist":
				RotateJoint("right_pitch_wrist", double.Parse(jointData));
				break;
			case "right_yaw_wrist":
				RotateJoint("right_yaw_wrist", double.Parse(jointData));
				break;

			default:
                break;
        }
        
	}

	/// <summary>
	/// 通用的关节旋转
	/// </summary>
	/// <param name="jointInfo"></param>
	/// <param name="degrees"></param>
	private void CommonRotate(JointInfo jointInfo, float degrees = 0f)
	{
		var tra = jointInfo.mod.trans;
		var ax = jointInfo.mod.bone.axIdx;
		int initDeg = jointInfo.mod.bone.initDeg;
		var isR = jointInfo.reverse;
		if (isR) { degrees = -degrees; }
		var targetDeg = initDeg + degrees;
		//if (isR) { targetDeg = -targetDeg; }
		Vector3 orignVec3;
		if (ax == 1)
		{
			orignVec3 = tra.localEulerAngles;
		}
		else 
		{
			orignVec3 = GetRotationInspector(tra);
		}
		orignVec3[ax] = targetDeg;
		SetRotationInspector(tra, orignVec3);
	}
	#endregion

	#region 关节旋转
	private bool _isRotating = false;
	private Vector2 _lastTouchPosition;
	private float _rotationSpeed = 0.5f;
	public void Updata()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_isRotating = true;
			_lastTouchPosition = Input.mousePosition;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			_isRotating = false;
		}

		if (_isRotating)
		{
			Vector2 deltaMouse = (Vector2)Input.mousePosition - _lastTouchPosition;
			_robotCenterTra.Rotate(Vector3.forward, -deltaMouse.x * _rotationSpeed);
			_lastTouchPosition = Input.mousePosition;
		}
	}

	#endregion

	#region 转换工具
	public Vector3 GetRotationInspector(Transform t)
	{
		var type = t.GetType();
		var mi = type.GetMethod("GetLocalEulerAngles", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
		var rotationOrderPro = type.GetProperty("rotationOrder", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
		var rotationOrder = rotationOrderPro.GetValue(t, null);
		var EulerAnglesInspector = mi.Invoke(t, new[] { rotationOrder });
		return (Vector3)EulerAnglesInspector;
	}

	public void SetRotationInspector(Transform t, Vector3 v)
	{
		var type = t.GetType();
		var mi = type.GetMethod("SetLocalEulerAngles", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
		var rotationOrderPro = type.GetProperty("rotationOrder", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
		var rotationOrder = rotationOrderPro.GetValue(t, null);
		mi.Invoke(t, new[] { v, rotationOrder });
	}
	#endregion
}