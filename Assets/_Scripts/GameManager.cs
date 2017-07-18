using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private const int RIGHT = 1;
	private const int LEFT  = -1;

	public Matrix matrixController;
	// Custom var
	public int row;
	public int col;
	public float speedStart;
	public float speed;
	public float speedIncPerLevel;

	private Rectangle rect;
	private float timePerMove;
	private int lastDirect = 1;

	private float timeDelta = 0.0f;

	private int scores;
	private int levels;

	// Text
	public GameObject canvas;
	public Text resultText;
	public Text scoreText;
	public Text levelText;

	private bool isGameEnd = false;

	void Start() {
		setup();
	}

	public void setup()
	{
		levels = 0;
		scores = 0;
		canvas.SetActive(false);

		matrixController.newMatrix(row, col, new Vector3(0,0,0));
		rect = new Rectangle(new Vector2(0,0), 3);
		isGameEnd = false;
		lastDirect = RIGHT;
		
		speed = speedStart;
		timePerMove = 1f / speed;
	}

	void ContinueLevel()
	{
		matrixController.ScrollDown(row - 8, rect);
	}

	void ReTurnOnSquares() 
	{
		TurnOffAll();

		// re-turn on all light square
		for (int x = 0; x < col; x++)
		{
			for (int y = 0; y < row; y++)
			{
				if (matrixController.matrix[x, y].squareStatus)
					matrixController.matrix[x, y].TurnOn(true);
				else
					matrixController.matrix[x, y].TurnOn(false);
			}
		}

		for (int x = (int)rect.startPosition.x; x < (int)(rect.startPosition.x + rect.length); x++) 
		{
			int y = (int) rect.startPosition.y;
			if ( (x >= 0 && x < col) && (y >= 0 && y < row) )
				matrixController.matrix[x, y].TurnOn(true);
		}
	}

	void Update()
	{
		if (!isGameEnd) {
			
			if (Input.GetKeyDown(KeyCode.Space)) {

				// Save Squared
				CheckRect();
				for (int x = (int)rect.startPosition.x; x < (int)(rect.startPosition.x + rect.length); x++) 
				{
					int y = (int) rect.startPosition.y;
					if ( (x >= 0 && x < col) && (y >= 0 && y < row) )
						matrixController.matrix[x, y].squareStatus = true;
				}
				rect.moveVertical(1);
				speed += speedIncPerLevel;
				timePerMove = 1f / speed;

				if (rect.length > 0) {
					levels++;
					scores += rect.length + levels;
				}

			}

			scoreText.text = "Score: " + scores;
			levelText.text = "Level: " + levels;
			
			if (!(rect.length > 0) || (rect.startPosition.y >= row)) {

				if (rect.startPosition.y >= row) {
					ContinueLevel ();
				}
				else {
					canvas.SetActive(true);
					isGameEnd = true;
				}
			}

			AutoMove();

		} else { // Game is not end
			if (Input.GetKeyDown(KeyCode.R)) 
				setup ();
		}
			
		ReTurnOnSquares ();
	}

	void TurnOffAll()
	{
		for (int x = 0; x < col; x++)
		{
			for (int y = 0; y < row; y++)
			{
				matrixController.matrix[x, y].TurnOn(false);
			}
		}
	}

	void AutoMove()
	{
		Debug.Log("AutoMove:" + Time.deltaTime);
		timeDelta += Time.deltaTime;

		if (timeDelta >= timePerMove) {
			timeDelta -= timePerMove;

			int xStart = (int)rect.startPosition.x;
			int xEnd = xStart + rect.length;

			if (lastDirect > 0 && xEnd >= col) 
				lastDirect = -lastDirect;
			else 
			if (lastDirect < 0 && xStart <= 0) 
				lastDirect = -lastDirect;

			rect.moveHorizontal(lastDirect);
		}
	}

	void CheckRect()
	{
		int count = 0;
		string where = "Tail";

		for (int x = (int)rect.startPosition.x; x < (int)(rect.startPosition.x + rect.length); x++) 
		{
			int y = (int) rect.startPosition.y;
			if (y > 0 && matrixController.matrix[x, y - 1].squareStatus != true) {
				if (count == 0 && x == (int)rect.startPosition.x)
					where = "Head";
				count ++;
			}
		}

		rect.CutRect(where, count);
	}
}
