using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

	public GameObject[] pieceArray;

	void Start() {
		SpawnNewPiece();
	}

	public void SpawnNewPiece() {
		int id = Random.Range(0, 7);
		
		GameObject.Instantiate(pieceArray[id], gameObject.transform.position, Quaternion.identity);

	}


}
