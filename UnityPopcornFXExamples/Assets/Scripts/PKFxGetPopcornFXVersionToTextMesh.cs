//--------------------------------------------------------------------------------------------------------
// Copyright Persistant Studios, SARL. All Rights Reserved. https://www.popcornfx.com/terms-and-conditions/
//--------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TextMesh))]
public class PKFxGetPopcornFXVersionToTextMesh : MonoBehaviour
{
	void Awake()
	{
		gameObject.GetComponent<TextMesh>().text = PopcornFX.PKFxManager.PopcornFXVersion();
	}
}
