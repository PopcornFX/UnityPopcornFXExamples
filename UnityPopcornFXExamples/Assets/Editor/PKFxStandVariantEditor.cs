//--------------------------------------------------------------------------------------------------------
// Copyright Persistant Studios, SARL. All Rights Reserved. https://www.popcornfx.com/terms-and-conditions/
//--------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PKFxStand))]
public class PKFxStandVariantEditor : Editor
{
	bool m_ShowBase = false;


	public void OnEnable()
	{
	}
	public override void OnInspectorGUI()
	{
		PKFxStand asset = target as PKFxStand;

		asset.Current = EditorGUILayout.Popup(asset.Current, asset.m_StringVariations.ToArray());


		if (GUILayout.Button("Add Emitter"))
		{
			GameObject go = new GameObject("Fx");
			go.transform.position = Vector3.zero;
			go.transform.SetParent(asset.transform);
			go.transform.position = Vector3.zero;
			go.transform.localPosition = Vector3.zero;
			go.AddComponent<PopcornFX.PKFxEmitter>();
			Selection.objects = new GameObject[] { go };
		}
		if (GUILayout.Button("Add 3D Text Title"))
		{
			GameObject go = new GameObject("Text Title");
			go.transform.position = Vector3.zero;
			go.transform.SetParent(asset.transform); 
			go.transform.localPosition = new Vector3(2.25f, 3.75f, -3.3f);
			go.transform.localEulerAngles = new Vector3(0, 180f, 0f);
			
			TextMesh	TextMeshComp = go.AddComponent<TextMesh>();
			TextMeshComp.text = "Lorem";
			TextMeshComp.characterSize = 0.04f;
			TextMeshComp.fontSize = 100;
			TextMeshComp.font = asset.Font;
			TextMeshComp.GetComponent<MeshRenderer>().material = new Material(Shader.Find("PopcornFX/Text"));
			Selection.objects = new GameObject[] { go };
		}
		if (GUILayout.Button("Add 3D Text Corps du Text"))
		{
			GameObject go = new GameObject("Text");
			go.transform.position = Vector3.zero;
			go.transform.SetParent(asset.transform);
			go.transform.localPosition = new Vector3(2.25f, 3f, -3.3f);
			go.transform.localEulerAngles = new Vector3(0, 180f, 0f);

			TextMesh TextMeshComp = go.AddComponent<TextMesh>();
			TextMeshComp.text = "Lorem";
			TextMeshComp.characterSize = 0.02f;
			TextMeshComp.fontSize = 100;
			TextMeshComp.font = asset.Font;
			TextMeshComp.GetComponent<MeshRenderer>().material = new Material(Shader.Find("PopcornFX/Text"));
			Selection.objects = new GameObject[] { go };
		}
		if (GUILayout.Button("Add 3D Text footer"))
		{
			GameObject go = new GameObject("Text Footer");
			go.transform.position = Vector3.zero;
			go.transform.SetParent(asset.transform);
			go.transform.localPosition = new Vector3(0.05f, 0.35f, 1.585f);
			go.transform.localEulerAngles = new Vector3(30, 180f, 0f);

			TextMesh TextMeshComp = go.AddComponent<TextMesh>();
			TextMeshComp.text = "Lorem";
			TextMeshComp.characterSize = 0.015f;
			TextMeshComp.fontSize = 100;
			TextMeshComp.font = asset.Font;
			TextMeshComp.anchor = TextAnchor.MiddleCenter;
			TextMeshComp.alignment = TextAlignment.Center;
			TextMeshComp.GetComponent<MeshRenderer>().material = new Material(Shader.Find("PopcornFX/Text"));
			Selection.objects = new GameObject[] { go };
		}
		if (GUILayout.Button("Add Image"))
		{
			GameObject go = new GameObject("Image");
			go.transform.position = Vector3.zero;
			go.transform.SetParent(asset.transform);
			go.transform.localPosition = new Vector3(0, 2.5f, -3.3f);
			go.transform.localEulerAngles = new Vector3(0, 180f, 0f);
			MeshRenderer	meshRenderer = go.AddComponent<MeshRenderer>();
			MeshFilter		meshFilter = go.AddComponent<MeshFilter>();

			Mesh			mesh = new Mesh();

			Vector3[]		vertices = new Vector3[4] { new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0) };
			Vector3[]		normals = new Vector3[4] { -Vector3.forward, -Vector3.forward, -Vector3.forward, -Vector3.forward };
			Vector2[]		uv = new Vector2[4] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };
			int[]			tris = new int[6] { 0, 2, 1, 2, 3, 1 };

			mesh.vertices = vertices;
			mesh.triangles = tris;
			mesh.normals = normals;
			mesh.uv = uv;
			meshFilter.mesh = mesh;

			meshRenderer.material = new Material(asset.BaseImageMaterial);

			Selection.objects = new GameObject[] { go };
		}

		bool namePlate = EditorGUILayout.Toggle("NamePlate", asset.HasNamePlate);
		if (namePlate != asset.HasNamePlate)
		{
			asset.HasNamePlate = namePlate;
			EditorUtility.SetDirty(asset);
		}
		asset.HasTriggerButton = EditorGUILayout.Toggle("Proximity trigger button", asset.HasTriggerButton);
		if (asset.HasTriggerButton)
		{
			SerializedObject triggerObj = new SerializedObject(asset.m_TriggerButtonInstance.GetComponent<PKFxTriggerPillar>());
			SerializedProperty OnEnterCB = triggerObj.FindProperty("OnPublicTriggerEnter");
			SerializedProperty OnStayCB = triggerObj.FindProperty("OnPublicTriggerStay");
			SerializedProperty OnExitCB = triggerObj.FindProperty("OnPublicTriggerExit");

			if (OnEnterCB != null)
			{
				EditorGUILayout.PropertyField(OnEnterCB, new GUIContent("OnEnter Cbs"));
				EditorGUILayout.PropertyField(OnStayCB, new GUIContent("OnStay Cbs"));
				EditorGUILayout.PropertyField(OnExitCB, new GUIContent("OnExit Cbs"));
			}
			triggerObj.ApplyModifiedProperties();
		}

		m_ShowBase = EditorGUILayout.Foldout(m_ShowBase, "Base editor");
		if (m_ShowBase)
		{
			base.OnInspectorGUI();
		}
	}
}
