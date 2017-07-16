using UnityEngine;
using System;
using System.Collections;

public class Square : MonoBehaviour 
{

	[Serializable]
	public class SquareStatus
	{
		public Sprite blank;
		public Sprite light;
	}

	public SquareStatus squareStatusSprite;

	[HideInInspector]
	public bool squareStatus = false;
	private SpriteRenderer spriteRenderer;

	void Awake()
	{
		spriteRenderer = GetComponent <SpriteRenderer> ();
	}

	/// <summary>
	/// Turns on the light.
	/// </summary>
	/// <param name="para">If set to <c>true</c></param>
	public void TurnOn(bool para)
	{
		if (para) 
			this.spriteRenderer.sprite = this.squareStatusSprite.light;
		else 
			this.spriteRenderer.sprite = this.squareStatusSprite.blank;
	}
}
