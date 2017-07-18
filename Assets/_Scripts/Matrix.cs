using UnityEngine;
using System;
using System.Collections.Generic;

public class Matrix : MonoBehaviour {

	public GameObject square;

	private Transform matrixHolder = null;
	public Square[,] matrix;
	private int rows;
	private int cols;

	public void newMatrix (int row, int col, Vector3 pos) 
	{
		Clear();

		matrix = new Square[col, row];
		rows = row;
		cols = col;
		matrixHolder = new GameObject("Matrix").transform;
		matrixHolder.transform.position = new Vector2(0, 0);

		for (int x = 0; x < col; x++) 
		{
			for (int y = 0; y < row; y++) 
			{
				GameObject instance = Instantiate(square, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
				instance.transform.SetParent (matrixHolder);
				matrix[x, y] = instance.GetComponent<Square>();
			}
		}
	}

	public void ScrollDown (int step, Rectangle rect) 
	{
		if (step >= rows) {
			newMatrix (rows, cols, matrixHolder.position);
			rect.startPosition.y = 0;
		}
		// 0 < step < rows
		else if (step > 0) {
			for (int y = step; y < rows; y++)
				for (int x = 0; x < cols; x++)
					matrix[x, y-step].Copy(matrix[x, y].squareStatus);

			for (int y = rows-step; y < rows; y++)
				for (int x = 0; x < cols; x++)
					matrix[x, y].Clear ();

			rect.startPosition.y -= step;
		}

	}

	public void Clear()
	{
		if (matrixHolder != null) {
			Destroy(matrixHolder.gameObject);
			matrixHolder = null;
		}
		//Destroy(GameObject.Find("Matrix"));
	}
}
