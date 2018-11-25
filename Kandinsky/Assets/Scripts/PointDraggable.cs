using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Utility class for working with planes relative to a camera.
/// </summary>
public static class CameraPlane
{
	/// <summary>
	/// Returns world space position at a given viewport coordinate for a given depth.
	/// </summary>
	public static Vector3 ViewportToWorldPlanePoint (Camera theCamera, float zDepth, Vector2 viewportCord)
	{
		Vector2 angles = ViewportPointToAngle (theCamera, viewportCord);
		float xOffset = Mathf.Tan (angles.x) * zDepth;
		float yOffset = Mathf.Tan (angles.y) * zDepth;
		Vector3 cameraPlanePosition = new Vector3 (xOffset, yOffset, zDepth);
		cameraPlanePosition = theCamera.transform.TransformPoint (cameraPlanePosition);
		return cameraPlanePosition;
	}

	public static Vector3 ScreenToWorldPlanePoint (Camera camera, float zDepth, Vector3 screenCoord)
	{
		var point = Camera.main.ScreenToViewportPoint (screenCoord);
		return ViewportToWorldPlanePoint (camera, zDepth, point);
	}

	/// <summary>
	/// Returns X and Y frustum angle for the given camera representing the given viewport space coordinate.
	/// </summary>
	public static Vector2 ViewportPointToAngle (Camera cam, Vector2 ViewportCord)
	{
		float adjustedAngle = AngleProportion(cam.fieldOfView/2, cam.aspect) * 2;
		float xProportion = ((ViewportCord.x - .5f)/.5f);
		float yProportion = ((ViewportCord.y - .5f)/.5f);
		float xAngle = AngleProportion(adjustedAngle/2, xProportion) * Mathf.Deg2Rad;
		float yAngle = AngleProportion(cam.fieldOfView/2, yProportion) * Mathf.Deg2Rad;
		return new UnityEngine.Vector2 (xAngle, yAngle);
	}

	/// <summary>
	/// Distance between the camera and a plane parallel to the viewport that passes through a given point.
	/// </summary>
	public static float CameraToPointDepth (Camera cam, Vector3 point)
	{
		Vector3 localPosition = cam.transform.InverseTransformPoint (point);
		return localPosition.z;
	}

	public static float AngleProportion (float angle, float proportion)
	{
		float oppisite = Mathf.Tan (angle * Mathf.Deg2Rad);
		float oppisiteProportion = oppisite * proportion;
		return Mathf.Atan (oppisiteProportion) * Mathf.Rad2Deg;
	}
}

public class RigidBodySavedSettings
{
	public bool usingGravity;
	public Dictionary<SpringJoint2D, JointSpring> springs = new Dictionary<SpringJoint2D, JointSpring>();
}

public class PointDraggable : MonoBehaviour {

	public float force = 600;
	public float slerpForce = 600;
	public float damping = 6;

	public float maxDistance = -1f;
	public float maxDistanceX = -1f;
	public float maxDistanceY = -1f;
	public Transform maxDistTarget;

	public bool isBeingDragged
	{
		get
		{
			return (jointTrans.Count > 0);
		}
	}

	Dictionary<int, Transform> jointTrans = new Dictionary<int, Transform>();
	Dictionary<Rigidbody, bool> wasUsingGravity = new Dictionary<Rigidbody, bool>();
	Dictionary<Rigidbody, RigidBodySavedSettings> rigidBodySavedSettings = new Dictionary<Rigidbody, RigidBodySavedSettings>();
	float dragDepth;

	void OnETMouseDownIncludingChildren (Gesture gesture)
	{
		List<Transform> children = transform.GetChildrenList(true);
		foreach (PointDraggable pd in gesture.pickedObject.transform.GetComponentsInParent<PointDraggable>())
		{
			if (children.Contains(pd.transform))
				return;
		}
		if (gesture.pickedObject.GetComponent<Rigidbody>() != null)
			HandleInputBegin (gesture.position, gesture.fingerIndex);
		else if (gesture.pickedObject.GetComponent<Rigidbody2D>() != null)
			HandleInputBegin (gesture.pickedObject, gesture.position, gesture.fingerIndex);
	}

	void OnETMouseUpIncludingChildren (Gesture gesture)
	{
		List<Transform> children = transform.GetChildrenList(true);
		foreach (PointDraggable pd in gesture.pickedObject.transform.GetComponentsInParent<PointDraggable>())
		{
			if (children.Contains(pd.transform))
				return;
		}
		HandleInputEnd (gesture.position, gesture.fingerIndex);
	}

	void OnETMouseDragIncludingChildren (Gesture gesture)
	{
		HandleInput (gesture.position, gesture.fingerIndex);
	}

	public void HandleInputBegin (Vector3 screenPosition, int touchIndex)
	{
		var ray = Camera.main.ScreenPointToRay (screenPosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			dragDepth = CameraPlane.CameraToPointDepth (Camera.main, hit.point);
			if (hit.rigidbody != null)
				jointTrans[touchIndex] = AttachJoint (hit.rigidbody, hit.point);
			else if (hit.transform.GetComponent<Rigidbody2D>() != null)
				jointTrans[touchIndex] = AttachJoint (hit.transform.GetComponent<Rigidbody2D>(), hit.point);
		}
	}

	public void HandleInputBegin (GameObject go, Vector3 screenPosition, int touchIndex)
	{
		Vector3 hitPoint = Camera.main.ScreenToWorldPoint (screenPosition);
		hitPoint.z = go.transform.position.z;
		if (go.GetComponent<Rigidbody>() != null)
			jointTrans[touchIndex] = AttachJoint (go.GetComponent<Rigidbody>(), hitPoint);
		else if (go.GetComponent<Rigidbody2D>() != null)
			jointTrans[touchIndex] = AttachJoint (go.GetComponent<Rigidbody2D>(), hitPoint);
	}

	public void HandleInput (Vector3 screenPosition, int touchIndex)
	{
		if (jointTrans == null || jointTrans.Count == 0)
			return;
		var worldPos = Camera.main.ScreenToWorldPoint (screenPosition);
		Vector3 v = Camera.main.ScreenToWorldPoint (screenPosition);
		v.z = jointTrans[touchIndex].position.z;
		if (maxDistTarget != null)
		{
			Vector3 dif = (v - maxDistTarget.position);
			float mag = dif.magnitude;
			float distX = Mathf.Abs(dif.x);
			float distY = Mathf.Abs(dif.y);
			float signX = ((dif.x < 0) ? -1f : 1f);
			float signY = ((dif.y < 0) ? -1f : 1f);
			if (maxDistance > 0f && mag > maxDistance)
				v = maxDistTarget.position + dif * maxDistance / mag;
			if (maxDistanceX > 0f && distX > maxDistanceX)
				v = new Vector3(maxDistTarget.position.x + signX * maxDistanceX, v.y);
			if (maxDistanceY > 0f && distY > maxDistanceY)
				v = new Vector3(v.x, maxDistTarget.position.y + signY * maxDistanceY);
		}
		jointTrans[touchIndex].position = v;
	}

	public void HandleInputEnd (Vector3 screenPosition, int touchIndex)
	{
		if (jointTrans.ContainsKey(touchIndex))
		{
			if (jointTrans[touchIndex].GetComponent<ConfigurableJoint>() != null)
			{
				Rigidbody rb = jointTrans[touchIndex].GetComponent<ConfigurableJoint>().connectedBody;
//				LoadRigidbodySettings(rb);
			}
			if (jointTrans[touchIndex].GetComponent<SpringJoint2D>() != null)
			{
				Rigidbody2D rb = jointTrans[touchIndex].GetComponent<SpringJoint2D>().connectedBody;
				rb.drag = 0.5f;
				//				LoadRigidbodySettings(rb);
			}
			Destroy (jointTrans[touchIndex].gameObject);
			jointTrans.Remove(touchIndex);
		}
	}

	public void Release()
	{
		List<int> touchIndexes = new List<int>();
		foreach(int touchIndex in jointTrans.Keys)
		{
			touchIndexes.Add(touchIndex);
		}
		foreach(int touchIndex in touchIndexes)
		{
			HandleInputEnd(Vector3.zero, touchIndex);
		}
	}

	Transform AttachJoint (Rigidbody rb, Vector3 attachmentPosition)
	{
		GameObject go = new GameObject ("Attachment Point");
		go.transform.position = attachmentPosition;

		var newRb = go.AddComponent<Rigidbody> ();
		newRb.isKinematic = true;

		var joint = go.AddComponent<ConfigurableJoint> ();
		joint.connectedBody = rb;
		joint.configuredInWorldSpace = true;
		joint.xDrive = NewJointDrive (force, damping);
		joint.yDrive = NewJointDrive (force, damping);
		joint.zDrive = NewJointDrive (force, damping);
		joint.slerpDrive = NewJointDrive (slerpForce, damping);
		joint.rotationDriveMode = RotationDriveMode.Slerp;

//		SaveRigidbodySettings(rb);
//		SetRigidbodySettingsForDrag(rb);

		return go.transform;
	}

	Transform AttachJoint (Rigidbody2D rb, Vector3 attachmentPosition)
	{
		GameObject go = new GameObject ("Attachment Point 2D");
		go.transform.position = attachmentPosition;

		var newRb = go.AddComponent<Rigidbody2D> ();
		newRb.isKinematic = true;

		var joint = go.AddComponent<SpringJoint2D> ();
		joint.connectedBody = rb;
		joint.autoConfigureConnectedAnchor = false;
		joint.connectedAnchor = Vector2.zero;
		joint.distance = 0f;
		joint.dampingRatio = this.damping;
		joint.frequency = this.force;
		joint.autoConfigureDistance = false;
		joint.connectedBody.drag = 5f;

		return go.transform;
	}

//	void SetRigidbodySettingsForDrag(Rigidbody rb)
//	{
//		rb.useGravity = false;
//		foreach(SpringJoint2D hj in rb.GetComponentsInParent<SpringJoint2D>())
//		{
//			JointSpring js = hj.spring;
//			js.spring = js.spring * 0.2f;
//			js.damper = js.damper * 0.2f;
//			hj.spring = js;
//		}
//	}
//
//	void SaveRigidbodySettings(Rigidbody rb)
//	{
//		rigidBodySavedSettings[rb] = new RigidBodySavedSettings();
//		foreach(SpringJoint2D hj in rb.GetComponentsInParent<SpringJoint2D>())
//		{
//			rigidBodySavedSettings[rb].springs[hj] = hj.spring;
//		}
//		rigidBodySavedSettings[rb].usingGravity = rb.useGravity;
//	}
//
//	void LoadRigidbodySettings(Rigidbody rb)
//	{
//		if (!rigidBodySavedSettings.ContainsKey(rb))
//			return;
//		foreach(SpringJoint2D hj in rb.GetComponentsInParent<SpringJoint2D>())
//		{
//			hj.spring = rigidBodySavedSettings[rb].springs[hj];
//		}
//		rb.useGravity = rigidBodySavedSettings[rb].usingGravity;
//		rigidBodySavedSettings.Remove(rb);
//	}

	private JointDrive NewJointDrive (float force, float damping)
	{
		JointDrive drive = new JointDrive ();
		drive.mode = JointDriveMode.Position;
		drive.positionSpring = force;
		drive.positionDamper = damping;
		drive.maximumForce = force;
		return drive;
	}
}
