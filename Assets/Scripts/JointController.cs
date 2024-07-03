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