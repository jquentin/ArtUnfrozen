using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegPart : PointDraggable 
{
	
	protected override void OnInputBegin ()
	{
		foreach(HingeJoint joint in transform.parent.GetComponentsInChildren<HingeJoint>())
		{
			JointSpring spring = joint.spring;
			spring.spring = 2f;
			joint.spring = spring;
		}
	}

	protected override void OnInputEnd ()
	{
		foreach(HingeJoint joint in transform.parent.GetComponentsInChildren<HingeJoint>())
		{
			JointSpring spring = joint.spring;
			spring.spring = 50f;
			joint.spring = spring;
		}
	}

}
