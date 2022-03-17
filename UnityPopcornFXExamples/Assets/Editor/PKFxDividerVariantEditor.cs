//--------------------------------------------------------------------------------------------------------
// Copyright Persistant Studios, SARL. All Rights Reserved. https://www.popcornfx.com/terms-and-conditions/
//--------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PKFxDivider))]
public class PKFxDividerVariantEditor : Editor
{
	bool m_ShowBase = false;

	public void OnEnable()
	{
	}
	public override void OnInspectorGUI()
	{
		PKFxDivider asset = target as PKFxDivider;

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
		if (GUILayout.Button("Add 3D Text"))
		{
			GameObject go = new GameObject("Text");
			go.transform.position = Vector3.zero;
			go.transform.SetParent(asset.transform);
			go.transform.localPosition = new Vector3(0, 2.5f, 0.1f);
			go.transform.localEulerAngles = new Vector3(0, 180f, 0f);
			TextMesh TextMeshComp = go.AddComponent<TextMesh>();
			TextMeshComp.text = "Lorem";
			TextMeshComp.characterSize = 0.1f;
			TextMeshComp.fontSize = 200;
			TextMeshComp.font = asset.Font;
			TextMeshComp.GetComponent<MeshRenderer>().material = new Material(Shader.Find("PopcornFX/Text"));
			Selection.objects = new GameObject[] { go };
		}
		if (GUILayout.Button("Add Image"))
		{
			GameObject go = new GameObject("Image");
			go.transform.position = Vector3.zero;
			go.transform.SetParent(asset.transform);
			go.transform.localPosition = new Vector3(asset.m_XOffset, 5f, 0.1f);
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

			meshRenderer.material = asset.BaseImageMaterial;

			Selection.objects = new GameObject[] { go };
		}
		m_ShowBase = EditorGUILayout.Foldout(m_ShowBase, "Base editor");
		if (m_ShowBase)
		{
			base.OnInspectorGUI();
		}
	}
}
