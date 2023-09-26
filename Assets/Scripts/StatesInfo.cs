using System;
using System.Collections.Generic;
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
	/// ǰ�������ٶȣ���λm/s
	/// </summary>
	public double vx;

	/// <summary>
	/// ���ҷ����ٶȣ���λm/s
	/// </summary>
	public double vy;

	/// <summary>
	/// not use
	/// </summary>
	public double vz;

	/// <summary>
	/// base X��վ��ʱXλ��
	/// </summary>
	public double x;

	/// <summary>
	/// base Y��վ��ʱYλ��
	/// </summary>
	public double y;

	/// <summary>
	/// base Y��վ��ʱYλ��
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
	/// ��ǰ״̬ Unknown��Start��Zero��Stand��Walk��Stop
	/// </summary>
	public string currentstatus;
}
[Serializable]
public class JointStatesItem
{
	/// <summary>
	/// �ؽ�����
	/// </summary>
	public string name;

	/// <summary>
	/// ��ʵ�ĹؽڽǶȣ���λ��rad�����ȣ�
	/// </summary>
	public double qa;

	/// <summary>
	/// �����Ĺؽ��ٶȣ���λ��rad
	/// </summary>
	public double qc;

	/// <summary>
	/// ��ʵ�Ĺؽ��ٶȣ���λ��rad/s������/�룩
	/// </summary>
	public double qdota;

	/// <summary>
	/// �����Ĺؽ��ٶȣ���λ��rad/s������/�룩
	/// </summary>
	public double qdotc;

	/// <summary>
	/// ��ʵ��Ť�أ���λ:n*m
	/// </summary>
	public double taua;

	/// <summary>
	/// �����Ĺؽ�Ť�أ���λ��unit:n*m
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
	/// ������״̬����
	/// </summary>
	public Basestate basestate;

	/// <summary>
	/// �Ӵ������� not use
	/// </summary>
	public Contactforce contactforce;

	/// <summary>
	/// �й�״̬��״̬������
	/// </summary>
	public Fsmstatename fsmstatename;

	/// <summary>
	/// �ؽ�״̬�б�
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