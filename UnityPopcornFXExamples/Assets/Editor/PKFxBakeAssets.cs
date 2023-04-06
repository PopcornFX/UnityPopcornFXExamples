using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PopcornFX;
using AOT;
using System;
using System.Linq;

public class EditorCoRoutine
{
	private IEnumerator routine;

	private EditorCoRoutine(IEnumerator routine)
	{
		this.routine = routine;
	}

	public static EditorCoRoutine Start(IEnumerator routine)
	{
		EditorCoRoutine coroutine = new EditorCoRoutine(routine);
		coroutine.Start();
		return coroutine;
	}

	private void Start()
	{
		UnityEditor.EditorApplication.update += Update;
	}

	public void Stop()
	{
		UnityEditor.EditorApplication.update -= Update;
	}

	private void Update()
	{
		if (!routine.MoveNext())
		{
			Stop();
		}
	}

	static void PerformBuild()
	{
		string[] scenes = {
			"Assets/Scenes/00_WelcomeToPopcornFX.unity",
			"Assets/Scenes/01_Effects.unity",
			"Assets/Scenes/02_Emitters.unity",
			"Assets/Scenes/03_Attributes.unity",
			"Assets/Scenes/04_Samplers.unity",
			"Assets/Scenes/05_Physics.unity",
			"Assets/Scenes/06_Rendering.unity"
		};

		//TODO
		//BuildPipeline.BuildPlayer(scenes,);
	}
}


public static class AutomatedBaker
{
	public static int AssetCount = 0;

	[MonoPInvokeCallback(typeof(AssetChangeCallback))]
	public static void OnAssetChangeUnitTest(ref SAssetChangesDesc assetChange)
	{
		PKFxManager.PublicOnAssetChange(ref assetChange);
		AssetCount--;
	}

	// Invoke that one from the command line
	public static void ExecuteBake()
	{
		EditorCoRoutine.Start(BuildImpl());
	}

	private static System.Collections.IEnumerator BuildImpl()
	{
		Debug.Log(string.Format("ExecuteBake start"));
		string[] args = System.Environment.GetCommandLineArgs();

		List<string> effectsUsed = PKFxMenus.GetFxsOnAllScenesAndPrefabs("Assets/");

		if (effectsUsed == null)
		{
			Debug.LogError(string.Format("GetFxsOnAllScenesAndPrefabs failed"));
			EditorApplication.Exit(-1);
		}

		Debug.Log(string.Format("Trying to reimport PKFx Assets:\n {0}", string.Join("\n", effectsUsed)));

		effectsUsed = effectsUsed.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();

		AssetCount = effectsUsed.Count;
		int start = AssetCount;

		PKFxManager.PublicSetDelegateOnAssetChange(PKFxDelegateHandler.Instance.DelegateToFunctionPointer(new AssetChangeCallback(OnAssetChangeUnitTest)));

		//For now assume that all effects are contained in the same pack and pack is set correctly
		if (!PKFxSettings.ReimportAssets(effectsUsed, "Editor"))
		{
			Debug.LogError(string.Format("ReimportAssets failed with asset:\n {0}", string.Join("\n", effectsUsed)));
			EditorApplication.Exit(-1);
		}
		while (AssetCount > 0)
		{
			yield return new WaitForSeconds(1);
			Debug.Log("AssetCount left to bake s:" + start + " c:"+ AssetCount);
		}
		Debug.Log(string.Format("UpdateFxsOnAllScenesAndPrefabs start"));
		PKFxMenus.UpdateFxsOnAllScenesAndPrefabs("Assets/");
		Debug.Log(string.Format("ExecuteBake end"));

		yield return null;
		EditorApplication.Exit(0);
	}
}
