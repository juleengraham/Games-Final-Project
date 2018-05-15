using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour {

	public GameObject player;
	private Camera theCamera;
	public float cameraOffset;

	void Start() {
		theCamera = GetComponent<Camera> ();
	}

	void FixedUpdate() {
		if (player.transform.position.y >= (theCamera.transform.position.y - cameraOffset)) {
			transform.position = new Vector3 (0, player.transform.position.y + cameraOffset, -10);
		}
	}
}
