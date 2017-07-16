using UnityEngine;
using System.Collections.Generic;

public class Rectangle : Object {

	public Vector2 startPosition;
	public int length;

	public Rectangle(Vector2 initPositon, int length)
	{
		this.startPosition = initPositon;
		this.length = length;
	}

	public void moveHorizontal (int step)
	{
		this.startPosition.x += step;
	}

	public void moveVertical (int step)
	{
		this.startPosition.y += step;
	}

	/// <summary>
	/// Cut the rectangle
	/// </summary>
	/// <param name="where">Cut "Tail" or "Head"?</param>
	/// <param name="length">Length of cutting rect</param>
	public void CutRect (string where, int length)
	{
		if (where == "Tail") {
			this.length -= length;
		} else if (where == "Head") {
			this.length -= length;
			this.startPosition.x += length;
		}
	}

}
