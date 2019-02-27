using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour {
	int gridWitdh;
	int gridHeight;

	int[][] gridStatus;
	Transform[][] gridView;

	GameObject spawner;

	// Use this for initialization
	void Start () {

		gridHeight = 24;
		gridWitdh = 10;

		gridStatus = new int[gridHeight][];
		gridView = new Transform[gridHeight][];
		for (int i = 0; i < gridHeight; i++)
		{
			gridStatus[i] = new int[gridWitdh];
			gridView[i] = new Transform[gridWitdh];
			for (int j = 0; j < gridWitdh; j++)
			{
				gridStatus[i][j] = 0;
			}
		}

		spawner = GameObject.Find("Spawner");
	}
	
	public bool IsTranslateOk (Vector3Int move, Vector3Int[] tiles) {
		for (int i = 0; i < 4; i++)
		{
			int tilePositionHorizontal = move.x + tiles[i].x;
			int tilePositionVertical = move.y+ tiles[i].y;

			// Debug.Log("origin: " + origin);
			// Debug.Log("x: "+tilePositionHorizontal);
			// Debug.Log("y: "+tilePositionVertical);

			if (tilePositionHorizontal >= gridWitdh || tilePositionHorizontal < 0 || tilePositionVertical < 0) {
				Debug.Log("out of bounds");
				return false;
			}
			if (gridStatus[tilePositionVertical][tilePositionHorizontal] == 1) {
				Debug.Log("obstruction");
				return false;
			}
		}
		// Debug.Log("everything is clear");
		return true;
	}

	public void PutPiece(Vector3Int origin, Vector3Int[] tilesPositions, Transform[] tilesObjects) {
		for (int i = 0; i < 4; i++)
		{
			int tilePositionHorizontal = tilesPositions[i].x;
			int tilePositionVertical = tilesPositions[i].y;

			gridStatus[tilePositionVertical][tilePositionHorizontal] = 1;
			gridView[tilePositionVertical][tilePositionHorizontal] = tilesObjects[i];
			tilesObjects[i].GetComponent<TileScript>().ChangeParent();
		}
		CheckLine();
		spawner.GetComponent<SpawnerScript>().SpawnNewPiece();
	}

	void CheckLine() {
		//TODO : check only the lines changed during last piece

		int total;
		int start = -1;
		int end = -1;
		for (int i = 0; i < gridHeight; i++)
		{
			total = 0;
			for (int j = 0; j < gridWitdh; j++)
			{
				total += gridStatus[i][j];
			}
			if (total == 10) {
				// There is a line !
				Debug.Log("LINE");
				if (start < 0) {
					Debug.Log("Start="+i);
					start = i;
				}
				end = i;
			}
		}
		if (start >= 0) {
			EraseLine(start,end);
		}
	}

	void EraseLine(int start, int end){
		//erase lines from start index to end index, then move all the upper lines down.
		for (int i = start; i < end+1; i++)
		{
			for (int j = 0; j < gridWitdh; j++)
			{
				gridStatus[i][j] = 0;
				gridView[i][j].GetComponent<TileScript>().DeleteTile();
			}
		}

		int range = end - start + 1;

		for (int k = 0; k < range; k++)
		{
			for (int i = start; i < gridHeight-1; i++)
			{
				gridStatus[i] = gridStatus[i+1];
				gridView[i] = gridView[i+1];

				for (int j = 0; j < gridWitdh; j++)
				{
					if(gridView[i][j] != null) {
						gridView[i][j].GetComponent<TileScript>().GoDown(1);
					}
				}
			}
			
		}
	}


}
