using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PKFxDontDestroyOnLoad : MonoBehaviour
{
	void Start()
	{
		PKFxDontDestroyOnLoad[] dontDestroyOnLoad = FindObjectsOfType<PKFxDontDestroyOnLoad>();
		if (dontDestroyOnLoad.Length != 1)
		{
			DestroyImmediate(gameObject);
			return;
		}
		GameObject.DontDestroyOnLoad(gameObject);
	}

	void Update()
	{
		
	}
}
