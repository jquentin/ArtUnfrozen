using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpriteRenderer))]
public class SpriteRendererInspector : Editor
{
	public override void OnInspectorGUI()
	{
		SpriteRenderer renderer = target as SpriteRenderer;
		if (GUILayout.Button("Test Instancing"))
		{
			renderer.materials = new Material[1]; 
			renderer.materials[0] = new Material(renderer.sharedMaterial);
			renderer.materials[0].name = renderer.sharedMaterial.name + " (Instance)";
		}
		DrawDefaultInspector();
	}
}
