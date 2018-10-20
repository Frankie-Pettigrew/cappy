using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickReactionImage : MonoBehaviour
{


	public List<Sprite> sprites = new List<Sprite>();

	public void displaySprite(int index)
	{
		GetComponent<SpriteRenderer>().sprite = sprites[index];
	}
}
