using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PipeTransfer : MonoBehaviour {

	public GameObject player;
	public bool currCollider; //false for left collider and true for right collider
	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*void OnTriggerEnter2D (Collider col) {
		bool right = Input.GetKey ("d") || Input.GetKey ("right");

		if (right) {
			col.gameObject.SetActive (false);
		}
	}*/

	void OnTriggerEnter2D(Collider2D col) {
		string scene = SceneManager.GetActiveScene ().name;

		if (scene == "MainScene") {
			
			if (Input.GetKey ("s") || Input.GetKey ("down")) {
				SceneManager.LoadScene ("Scene2", LoadSceneMode.Single);
			}

		} else if (scene == "Scene1") {

			if (Input.GetKey ("right") || Input.GetKey ("d")) {
				SceneManager.LoadScene ("Scene2", LoadSceneMode.Single);
			}

		} else if (scene == "Scene2") {
			
			if (currCollider && (Input.GetKey ("right") || Input.GetKey ("d"))) {
				SceneManager.LoadScene ("Scene3", LoadSceneMode.Single);
			} else if (!currCollider && (Input.GetKey ("a") || Input.GetKey ("left"))) {
				SceneManager.LoadScene ("Scene1", LoadSceneMode.Single);
			} else if (currCollider && Input.GetKey ("w") || Input.GetKey ("up")) {
				//if Kristin adds pipe at top of scene 2, then add option to return to Main Scene. Also would need to change currCollider to int 0-2 (left, right, top)
			}

		} else if (scene == "Scene3") {

			if (Input.GetKey ("a") || Input.GetKey ("left")) {
				SceneManager.LoadScene ("Scene2", LoadSceneMode.Single);
			}

		} else if (scene == "Scene3Ladder") {

			if (currCollider && Input.GetKey ("w") || Input.GetKey ("up")) {
				//to be used to end game??? go up the ladder which loads an end of game scene??
			}

		}

	}

}
