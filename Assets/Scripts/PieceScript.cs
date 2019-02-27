using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour {

	float verticalSpeed;
	float horizontalSpeed;
	float timeRemainingVertical;
	float timeRemainingFreeze;

	public GameObject grid;


	// Use this for initialization
	void Start () {

		grid = GameObject.Find("Grid");
		verticalSpeed = 1f;
		horizontalSpeed = 1f;
		timeRemainingVertical = verticalSpeed;
		timeRemainingFreeze = 2f;
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


			Vector3Int move = new Vector3Int((int)distance, 0, 0);

			if (grid.GetComponent<GridScript>().IsTranslateOk(move, GetPieceTiles())) {
				gameObject.transform.Translate(distance,0,0);
			}
		}

		if (Input.GetButtonDown("Vertical")) {
			float direction = Mathf.Sign(Input.GetAxis("Vertical"));
			if (direction > 0) {
				//TODO : teleport down.
				Debug.Log("to be implemented");
			} else {
				GoDown();
			}
		}

		if (Input.GetButtonDown("Rotation")) {
			float direction = Mathf.Sign(Input.GetAxis("Rotation"));
			float rotation = 90f * direction;

			//if possible
			PieceRotation(rotation);
		}
	}

	void GoDown() {
		Vector3Int move = Vector3Int.down;

		if (grid.GetComponent<GridScript>().IsTranslateOk(move, GetPieceTiles())) {
			gameObject.transform.Translate(Vector3.down);
		} else {
		// timeRemainingFreeze -= Time.deltaTime;
		// 	if ( timeRemainingFreeze < 0 )
		// 	{
				Vector3Int currentOrigin = Vector3Int.FloorToInt(transform.position);
				grid.GetComponent<GridScript>().PutPiece(currentOrigin, GetPieceTiles(), GetPieceTilesTransform());
				// this.GetComponent<PieceScript>().enabled = false;
				StoreTiles();
			// }
		}
	}



	Vector3Int[] GetPieceTiles() {
		//return an array of all the coordinates of the tile of the current piece.
		// Coordinates are relatives to a specific tile in the piece.
		Vector3Int[] coordinates = new Vector3Int[4];
		
		int i= 0;
		foreach (Transform tile in transform.GetChild(0).transform.GetComponentInChildren<Transform>())
		{
			Vector3 position = tile.position;
			coordinates[i] = Vector3Int.RoundToInt(position);
			i++;
		}

		return coordinates;
	}

	Transform[] GetPieceTilesTransform() {
		//return an array of all the coordinates of the tile of the current piece.
		// Coordinates are relatives to a specific tile in the piece.
		
		Transform[] tiles = new Transform[4];
		for (int i = 0; i < 4; ++i) {
			tiles[i] = transform.GetChild(0).GetChild(i);
		}
		return tiles;
	}

	public virtual void PieceRotation(float rotation) {
		gameObject.transform.GetChild(0).Rotate(new Vector3(0,0,rotation));
		Vector3Int[] tilesCoordinates = GetPieceTiles();
		if (! grid.GetComponent<GridScript>().IsTranslateOk(Vector3Int.zero, tilesCoordinates)) {
			if (!grid.GetComponent<GridScript>().IsTranslateOk(Vector3Int.right* (int)Mathf.Sign(rotation), tilesCoordinates)) {
				if (!grid.GetComponent<GridScript>().IsTranslateOk(Vector3Int.up, tilesCoordinates)) {
					gameObject.transform.GetChild(0).Rotate(new Vector3(0,0,-rotation));
				} else{
					gameObject.transform.Translate(Vector3.up);
				}
			} else {
				gameObject.transform.Translate(Vector3.right* Mathf.Sign(rotation));
			}
		}

	}


	void StoreTiles() {

		Destroy(gameObject);
	}


}
