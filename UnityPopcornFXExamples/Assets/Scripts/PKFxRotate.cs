//--------------------------------------------------------------------------------------------------------
// Copyright Persistant Studios, SARL. All Rights Reserved. https://www.popcornfx.com/terms-and-conditions/
//--------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PKFxRotate : MonoBehaviour
{
	public float rotationSpeed = 90.0f;
	public Vector3 axis = Vector3.up;

	void Update()
	{
		transform.RotateAround(transform.position, axis, Time.deltaTime * rotationSpeed);
	}
}
