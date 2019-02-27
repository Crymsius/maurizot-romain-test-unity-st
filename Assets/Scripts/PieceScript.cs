using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour {

	float verticalSpeed;
	float horizontalSpeed;
	float timeRemainingVertical;


	// Use this for initialization
	void Start () {
		verticalSpeed = 1f;
		horizontalSpeed = 1f;
		timeRemainingVertical = verticalSpeed;
	}
	
	// Update is called once per frame
	void Update () {


		timeRemainingVertical -= Time.deltaTime;
		if ( timeRemainingVertical < 0 )
		{
			GoDown();
			timeRemainingVertical = verticalSpeed;
		}


		if (Input.GetButtonDown("Horizontal")) {
			float direction = Mathf.Sign(Input.GetAxis("Horizontal"));
			float distance = Mathf.Floor(horizontalSpeed) * direction;
			gameObject.transform.Translate(distance,0,0);
		}

		if (Input.GetButtonDown("Rotation")) {
			float direction = Mathf.Sign(Input.GetAxis("Rotation"));
			float rotation = 90f * direction;
			gameObject.transform.GetChild(0).Rotate(new Vector3(0,0,rotation));
		}
	}

	void GoDown() {
		//if possible
		gameObject.transform.Translate(Vector3.down);
	}



}
