using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DeleteTile() {
		Destroy(gameObject);
	}

	public void GoDown(int range) {
		transform.SetPositionAndRotation(transform.position+Vector3.down*range, Quaternion.identity);
	}

	public void ChangeParent() {
		transform.SetParent(GameObject.Find("Tiles").transform);
	}
}
