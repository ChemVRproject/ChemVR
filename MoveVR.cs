using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

[RequireComponent(typeof(Rigidbody))]

public class MoveVR : MonoBehaviour
{
	public Rigidbody player;
	public float speed;
	public Camera mainCamera;
	public BoxCollider boxCollider;
	public GameObject head;
	public GameObject leftHand;
	private bool flag = false;
	
	public void Start()
	{
		Vector3 point_A = mainCamera.ScreenPointToRay(Vector2.zero).origin;

		// определяем размер коллайдера по ширине экрана
		Vector3 point_B = mainCamera.ScreenPointToRay(new Vector2(Screen.width, 0)).origin;

		float dist = Vector3.Distance(point_A, point_B);
		boxCollider.size = new Vector3(dist, boxCollider.size.y, 0.1f);

		// определяем размер бокса по высоте
		point_B = mainCamera.ScreenPointToRay(new Vector2(0, Screen.height)).origin;

		dist = Vector3.Distance(point_A, point_B);
		boxCollider.size = new Vector3(boxCollider.size.x, dist, 0.1f);

		boxCollider.center = new Vector3(0, 0, mainCamera.nearClipPlane);
	}
	
	void Update()
	{
		var joystickAxis = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick, OVRInput.Controller.LTouch);
		float fixedY = player.position.y;
		var joystickAxis2 = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick, OVRInput.Controller.RTouch); 
		
		player.position += (transform.right * joystickAxis.x + transform.up * joystickAxis2.y + transform.forward * joystickAxis.y) * Time.deltaTime * speed;
		player.position = new Vector3(player.position.x, player.position.y, player.position.z);
		
	}
}
