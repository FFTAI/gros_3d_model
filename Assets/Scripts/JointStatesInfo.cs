using System;
using System.Collections.Generic;

[Serializable]
public class JointStates
{
	/// <summary>
	/// 关节名称
	/// </summary>
	public string name;
	/// <summary>
	/// 真实的关节角度，单位：rad（弧度）
	/// </summary>
	public double qa;
	/// <summary>
	/// 期望的关节速度，单位：rad
	/// </summary>
	public double qc;
	/// <summary>
	/// 真实的关节速度，单位：rad/s（弧度/秒）
	/// </summary>
	public double qdota;
	/// <summary>
	/// 期望的关节速度，单位：rad/s（弧度/秒）
	/// </summary>
	public double qdotc;
	/// <summary>
	/// 真实的扭矩，单位:n*m
	/// </summary>
	public double taua;
	/// <summary>
	/// 期望的关节扭矩，单位：unit:n*m
	/// </summary>
	public double tauca;
}

[Serializable]
public class JointStatesInfo
{
	/// <summary>
	/// 机器人关节状态列表
	/// </summary>
	public List<JointStates> jointStates;
}