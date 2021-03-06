﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guyAnim : MonoBehaviour {
	
	public Sprite[] sprites = new Sprite[3];
	public Sprite[] dollaSprites = new Sprite[4];

	public SpriteRenderer ren;
	public float animTime;

	public bool walking, singing;
	public float animTimer = 0;
	public SpriteRenderer dollas;
	public int budget = 0;
	

	// Use this for initialization
	void Start ()
	{
		ren = GetComponent<SpriteRenderer>();
		dollas = transform.GetChild(0).GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(Camera.main.transform.position, Vector3.up);
		if (walking)
		{
			if (animTimer < animTime)
			{
				animTimer += Time.deltaTime;
			}
			else
			{
				animTimer = 0;
				if (ren.sprite == sprites[0])
				{
					ren.sprite = sprites[1];
				}
				else
				{
					ren.sprite = sprites[0];
				}
			}
				
		} else if (singing)
		{
			ren.sprite = sprites[2];
		}
		else
		{
			ren.sprite = sprites[0];
		}

		dollas.sprite = dollaSprites[budget];
	}
	
	
}
