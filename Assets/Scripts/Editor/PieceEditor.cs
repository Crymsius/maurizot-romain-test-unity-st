using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceEditor : EditorWindow {

	public Object tile;
	string pieceName;

	int height = 5;
	int width = 5;
	int previousHeight;
	int previousWidth;

	string newGridButton = "New Grid";
	string createButton = "Create";

	public bool [][] cells;


	[MenuItem ("Custom/Piece Editor")]

	public static void ShowWindow () {
		EditorWindow.GetWindow(typeof(PieceEditor));
		
	}
	void OnGUI () {
		
		height = EditorGUILayout.IntField("height of the grid:", height);
		width = EditorGUILayout.IntField("width of the grid:", width);

		if (GUILayout.Button(newGridButton))
		{
			InitializeGrid();
		}

		if (height == previousHeight && width == previousWidth) {
			ShowGrid(previousHeight, previousWidth);
		}


		pieceName = EditorGUILayout.TextField("Name of the Piece :", pieceName);

		tile = EditorGUILayout.ObjectField("Tile Object", tile, typeof(Object), false);

		if (GUILayout.Button(createButton))
		{
			CreatePiece();
		}
	}

	void InitializeGrid() {
		cells= new bool[height][];
		for (int i = 0; i < height; i++)
		{
			cells[i] = new bool[width];
		}
		previousHeight = height;
		previousWidth = width;
	}

	void ShowGrid(int h, int w) {

		for (int i = 0; i < h; i++)
		{
			EditorGUILayout.BeginHorizontal();
			for (int j = 0; j < w; j++)
			{
				cells[i][j] =EditorGUILayout.Toggle(cells[i][j]);
			}
			EditorGUILayout.EndHorizontal();
		}
	}


	void CreatePiece() {
		Object prefab = PrefabUtility.CreateEmptyPrefab("Assets/Temporary/"+pieceName+".prefab");

		GameObject piece = new GameObject("piece");
		for (int i = 0; i < previousHeight; i++)
		{
			for (int j = 0; j < previousWidth; j++)
			{
				if (cells[i][j]) {
					GameObject.Instantiate(tile, new Vector3(j, -i), Quaternion.identity, piece.transform);
				}
			}
		}

		PrefabUtility.ReplacePrefab(piece.gameObject, prefab, ReplacePrefabOptions.ConnectToPrefab);
	}

}
