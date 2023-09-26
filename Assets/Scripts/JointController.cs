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
	/// 初始化机器人关节位置
	/// </summary>
	public void InitJoints()
	{
		if (_robotData == null) return;
		foreach (var joint in _robotData.Joints.Values)
		{
			CommonRotate(joint);
		}
	}
	#region 机器人关节旋转
	/// <summary>
	/// 控制节点旋转
	/// </summary>
	/// <param name="jointName"></param>
	/// <param name="radians"></param>
	public void RotateJoint(string jointName, double radians)
	{
		if (_robotData == null) return;
		var modelName = JointNameMapping.Ins.GetLocalName(jointName);
		var jointDegrees = RadiansToDegrees(radians);
		if (_robotData.Joints.TryGetValue(modelName, out JointInfo jointInfo))
		{
			CommonRotate(jointInfo, jointDegrees);
		}
	}

	/// <summary>
	/// 旋转节点
	/// </summary>
	/// <param name="jointInfo"></param>
	/// <param name="degrees"></param>
	private void CommonRotate(JointInfo jointInfo, float degrees = 0f)
	{
		var tra = jointInfo.mod.trans;
		var ax = jointInfo.mod.bone.ax;
		int initDeg = jointInfo.mod.bone.initDeg;
		var isR = jointInfo.reverse;
		var targetDeg = initDeg + degrees;
		if (isR) { targetDeg = -targetDeg; }
		switch (ax)
		{
			case Axis.X:
				tra.localEulerAngles = new Vector3(targetDeg, tra.localEulerAngles.y, tra.localEulerAngles.z);
				break;

			case Axis.Y:
				tra.localEulerAngles = new Vector3(tra.localEulerAngles.x, targetDeg, tra.localEulerAngles.z);
				break;

			case Axis.Z:
				tra.localEulerAngles = new Vector3(tra.localEulerAngles.x, tra.localEulerAngles.y, targetDeg);
				break;
		}
	}
	#endregion

	#region 滑动旋转机器人
	private bool _isRotating = false;
	private Vector2 _lastTouchPosition;
	private float _rotationSpeed = 0.5f;
	public void Update()
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

	#region 工具
	/// <summary>
	/// 弧度转角度
	/// </summary>
	/// <param name="radians"></param>
	/// <returns></returns>
	private float RadiansToDegrees(double radians)
	{
		return (float)(radians * (180.0 / Mathf.PI));
	}
	#endregion
}