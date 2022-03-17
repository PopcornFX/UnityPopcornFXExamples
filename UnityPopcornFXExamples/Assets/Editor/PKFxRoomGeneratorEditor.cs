//--------------------------------------------------------------------------------------------------------
// Copyright Persistant Studios, SARL. All Rights Reserved. https://www.popcornfx.com/terms-and-conditions/
//--------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PKFxRoomGenerator))]
public class PKFxRoomGeneratorEditor : Editor
{
	bool m_ShowBase = false;

	public override void OnInspectorGUI()
	{
		PKFxRoomGenerator asset = target as PKFxRoomGenerator;

		asset.Rooms = EditorGUILayout.IntField("Room number", asset.Rooms);
		asset.Segments = EditorGUILayout.IntField("Segments per Room", asset.Segments);

		asset.DoubleHeight = EditorGUILayout.Toggle("Double Height", asset.DoubleHeight);
		asset.Terrasse = EditorGUILayout.Toggle("Terrasse", asset.Terrasse);


		asset.m_TargetObject = EditorGUILayout.ObjectField("Transient objects", asset.m_TargetObject, typeof(GameObject), true) as GameObject;
		asset.m_PersitentTargetObject = EditorGUILayout.ObjectField("Persistent objects", asset.m_PersitentTargetObject, typeof(GameObject), true) as GameObject;


		if (GUILayout.Button("Generate"))
		{
			asset.GenerateRooms();
		}
		if (GUILayout.Button("Clean"))
		{
			asset.CleanRooms();
		}

		m_ShowBase = EditorGUILayout.Foldout(m_ShowBase, "Base editor");
		if (m_ShowBase)
		{
			base.OnInspectorGUI();
		}
	}
}
