//--------------------------------------------------------------------------------------------------------
// Copyright Persistant Studios, SARL. All Rights Reserved. https://www.popcornfx.com/terms-and-conditions/
//--------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PKFxSwitchScene : MonoBehaviour
{
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab) == true)
		{
			Scene scene = SceneManager.GetActiveScene();
			PopcornFX.PKFxManager.ResetAllEffects();
			SceneManager.LoadSceneAsync((scene.buildIndex+1) % SceneManager.sceneCountInBuildSettings);
		}
	}
}
