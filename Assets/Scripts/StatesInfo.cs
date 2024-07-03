using System;
using System.Collections.Generic;
#region  T1
[Serializable]
public class Basestate
{
	/// <summary>
	/// hip roll
	/// </summary>
	public double a;

	/// <summary>
	/// hip Pitch
	/// </summary>
	public double b;

	/// <summary>
	/// hip Yaw
	/// </summary>
	public int c;

	/// <summary>
	/// not use
	/// </summary>
	public double va;

	/// <summary>
	/// not use
	/// </summary>
	public double vb;

	/// <summary>
	/// not use
	/// </summary>
	public double vc;

	/// <summary>
	/// 前进方向速度，单位m/s
	/// </summary>
	public double vx;

	/// <summary>
	/// 左右方向速度，单位m/s
	/// </summary>
	public double vy;

	/// <summary>
	/// not use
	/// </summary>
	public double vz;

	/// <summary>
	/// base X，站立时X位置
	/// </summary>
	public double x;

	/// <summary>
	/// base Y，站立时Y位置
	/// </summary>
	public double y;

	/// <summary>
	/// base Y，站立时Y位置
	/// </summary>
	public double z;
}
[Serializable]
public class Contactforce
{
	/// <summary>
	/// none
	/// </summary>
	public int fxL;

	/// <summary>
	/// none
	/// </summary>
	public int fxR;

	/// <summary>
	/// none
	/// </summary>
	public int fyL;

	/// <summary>
	/// none
	/// </summary>
	public int fyR;

	/// <summary>
	///none
	/// </summary>
	public int fzL;

	/// <summary>
	///none
	/// </summary>
	public int fzR;

	/// <summary>
	///none
	/// </summary>
	public int mxL;

	/// <summary>
	///none
	/// </summary>
	public int mxR;

	/// <summary>
	///none
	/// </summary>
	public int myL;

	/// <summary>
	///none
	/// </summary>
	public int myR;

	/// <summary>
	///none
	/// </summary>
	public int mzL;

	/// <summary>
	///none
	/// </summary>
	public int mzR;
}
[Serializable]
public class Fsmstatename
{
	/// <summary>
	/// 当前状态 Unknown、Start、Zero、Stand、Walk、Stop
	/// </summary>
	public string currentstatus;
}
[Serializable]
public class JointStatesItem
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
	public double tauc;
}
[Serializable]
public class Stanceindex
{
}
[Serializable]
public class States
{
	/// <summary>
	/// 机器人状态数据
	/// </summary>
	public Basestate basestate;

	/// <summary>
	/// 接触力数据 not use
	/// </summary>
	public Contactforce contactforce;

	/// <summary>
	/// 有关状态机状态的数据
	/// </summary>
	public Fsmstatename fsmstatename;

	/// <summary>
	/// 关节状态列表
	/// </summary>
	public List<JointStatesItem> jointStates;

	/// <summary>
	///
	/// </summary>
	public Stanceindex stanceindex;
}
[Serializable]
public class Timestamp
{
	/// <summary>
	/// none
	/// </summary>
	public int nanos;

	/// <summary>
	/// none
	/// </summary>
	public string seconds;
}
[Serializable]
public class Data
{
	/// <summary>
	/// none
	/// </summary>
	public States states;

	/// <summary>
	/// none
	/// </summary>
	public Timestamp timestamp;
}

[Serializable]
public class Root
{
	/// <summary>
	/// none
	/// </summary>
	public Data data;

	/// <summary>
	/// SonnieGetStates
	/// </summary>
	public string function;
}
#endregion

#region  T2
[Serializable]
public class FsmstatenameT2
{
	/// <summary>
	/// 
	/// </summary>
	public int currentstatus;
}
[Serializable]
public class StatesT2
{
	/// <summary>
	/// 
	/// </summary>
	public double stanceindex;
	/// <summary>
	/// 
	/// </summary>
	public FsmstatenameT2 fsmstatename;
}
[Serializable]
public class DataT2
{
	/// <summary>
	/// 
	/// </summary>
	public string all_init;
	/// <summary>
	/// 
	/// </summary>
	public StatesT2 states;
	/// <summary>
	/// 
	/// </summary>
	public string upper_action;
	/// <summary>
	/// 
	/// </summary>
	public int frequence;
}
[Serializable]
public class Joint_statesT2
{
	/// <summary>
	/// 
	/// </summary>
	public double waist_roll;
	/// <summary>
	/// 
	/// </summary>
	public double left_ankle_pitch;
	/// <summary>
	/// 
	/// </summary>
	public double right_hip_pitch;
	/// <summary>
	/// 
	/// </summary>
	public double right_knee_pitch;
	/// <summary>
	/// 
	/// </summary>
	public double right_ankle_roll;
	/// 
	/// </summary>
	public double left_hip_roll;
	/// <summary>
	/// 
	/// </summary>
	public double waist_pitch;
	/// <summary>
	/// 
	/// </summary>
	public double head_roll;
	/// 
	/// </summary>
	public double left_hip_yaw;
	/// <summary>
	/// 
	/// </summary>
	public double waist_yaw;
	/// <summary>
	/// 
	/// </summary>
	public double head_pitch;
	/// <summary>
	/// 
	/// </summary>
	public double right_hip_yaw;
	/// <summary>
	/// 
	/// </summary>
	public double head_yaw;
	/// <summary>
	/// 
	/// </summary>
	public double right_ankle_pitch;
	/// <summary>
	/// 
	/// </summary>
	public double right_hip_roll;
	/// <summary>
	/// 
	/// </summary>
	public double left_ankle_roll;
	/// <summary>
	/// 
	/// </summary>
	public double left_knee_pitch;
	/// <summary>
	/// 
	/// </summary>
	public double left_hip_pitch;
}
[Serializable]
public class RootT2
{
	/// <summary>
	/// 
	/// </summary>
	public DataT2 data;
	/// <summary>
	/// 
	/// </summary>
	public Joint_statesT2 joint_states;
	/// <summary>
	/// 
	/// </summary>
	public string function;
}
#endregion