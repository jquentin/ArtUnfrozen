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


public class PointDraggable : MonoBehaviour {

	public float force = 600;
	public float slerpForce = 600;
	public float damping = 6;

	Dictionary<int, Transform> jointTrans = new Dictionary<int, Transform>();
	float dragDepth;

	void OnETMouseDownIncludingChildren (Gesture gesture)
	{
		HandleInputBegin (gesture.position, gesture.fingerIndex);
		OnInputBegin();
	}

	void OnETMouseUpIncludingChildren (Gesture gesture)
	{
		HandleInputEnd (gesture.position, gesture.fingerIndex);
		OnInputEnd();
	}

	void OnETMouseDragIncludingChildren (Gesture gesture)
	{
		HandleInput (gesture.position, gesture.fingerIndex);
		OnInputUpdate();
	}

	public void HandleInputBegin (Vector3 screenPosition, int touchIndex)
	{
		var ray = Camera.main.ScreenPointToRay (screenPosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
//			if (hit.transform.gameObject.layer == LayerMask.NameToLayer ("Interactive")) {
				dragDepth = CameraPlane.CameraToPointDepth (Camera.main, hit.point);
				Vector3 worldPos = Camera.main.ScreenToWorldPoint (screenPosition);
				jointTrans[touchIndex] = AttachJoint (hit.rigidbody, worldPos);
				Vector3 targetPos = CameraPlane.ScreenToWorldPlanePoint (Camera.main, dragDepth, screenPosition);
				Debug.LogFormat("targetPos = {0} ; worldPos = {1}", targetPos, worldPos);
//			}
		}
	}

	public void HandleInput (Vector3 screenPosition, int touchIndex)
	{
		if (jointTrans == null)
			return;
		Vector3 worldPos = Camera.main.ScreenToWorldPoint (screenPosition);
		Vector3 targetPos = CameraPlane.ScreenToWorldPlanePoint (Camera.main, dragDepth, screenPosition);
		Debug.LogFormat("targetPos = {0} ; worldPos = {1}", targetPos, worldPos);
		jointTrans[touchIndex].position = worldPos;
	}

	public void HandleInputEnd (Vector3 screenPosition, int touchIndex)
	{
		Destroy (jointTrans[touchIndex].gameObject);
		jointTrans.Remove(touchIndex);
	}

	protected virtual void OnInputBegin(){}
	protected virtual void OnInputUpdate(){}
	protected virtual void OnInputEnd(){}

	Transform AttachJoint (Rigidbody rb, Vector3 attachmentPosition)
	{
		GameObject go = new GameObject ("Attachment Point");
//		go.hideFlags = HideFlags.HideInHierarchy; 
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

		return go.transform;
	}

	private JointDrive NewJointDrive (float force, float damping)
	{
		JointDrive drive = new JointDrive ();
		drive.mode = JointDriveMode.Position;
		drive.positionSpring = force;
		drive.positionDamper = damping;
		drive.maximumForce = Mathf.Infinity;
		return drive;
	}
}
