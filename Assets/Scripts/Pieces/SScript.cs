using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SScript : PieceScript {

	public override void PieceRotation(float rotation) {
		gameObject.transform.GetChild(0).Rotate(new Vector3(0,0,rotation));
		return;
	}
}
