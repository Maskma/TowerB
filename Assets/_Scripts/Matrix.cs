using UnityEngine;
using System;
using System.Collections.Generic;

public class Matrix : MonoBehaviour {

	public GameObject square;

	private Transform matrixHolder = null;
	public Square[,] matrix;

	public void newMatrix (int row, int col, Vector3 pos) 
	{
		Clear();

		matrix = new Square[col, row];
		matrixHolder = new GameObject("Matrix").transform;
		matrixHolder.transform.position = new Vector2(0, 0);

		for (int x = 0; x < col; x++) 
		{
			for (int y = 0; y < row; y++) 
			{
				GameObject instance = Instantiate(square, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
				instance.transform.SetParent(matrixHolder);
				matrix[x, y] = instance.GetComponent<Square>();
			}
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
