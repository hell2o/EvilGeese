﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Grid))]
[CanEditMultipleObjects]
public class GridManager : Editor {
	Vector2 mousePos = new Vector2 ();
	int tempx = 0;
	int tempy = 0;
	GridPosition pos;
	Grid myGrid;
	public override void OnInspectorGUI(){
		myGrid = (Grid)target;

		myGrid.OnAfterDeserialize ();
		//DrawDefaultInspector ();

		EditorGUILayout.BeginHorizontal (null);
		EditorGUILayout.PrefixLabel(new GUIContent("x:"));
		tempx = EditorGUILayout.IntField (tempx, null);
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal (null);
		EditorGUILayout.PrefixLabel(new GUIContent("y:"));
		tempy = EditorGUILayout.IntField (tempy, null);
		EditorGUILayout.EndHorizontal ();

		if (GUILayout.Button ("add")) {
			myGrid.setPosition (new GridPosition (tempx, tempy, blocked : true));
		}
		if (GUILayout.Button ("remove")) {
			myGrid.clearPosition (tempx, tempy);
		}
		if (GUILayout.Button ("search")) {
			pos = myGrid.getPosition (tempx, tempy);
		}
		if (pos != null) {
			EditorGUILayout.BeginHorizontal (null);
			EditorGUILayout.PrefixLabel(new GUIContent("x:"));
			pos.x = EditorGUILayout.IntField (pos.x, null);
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.BeginHorizontal (null);
			EditorGUILayout.PrefixLabel(new GUIContent("y:"));
			pos.y = EditorGUILayout.IntField (pos.y, null);
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.BeginHorizontal (null);
			EditorGUILayout.PrefixLabel(new GUIContent("blocked:"));
			pos.blocked = EditorGUILayout.Toggle (pos.blocked, null);
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.BeginHorizontal (null);
			EditorGUILayout.PrefixLabel(new GUIContent("interaction:"));
			pos.interactionObject = (GameObject) EditorGUILayout.ObjectField (pos.interactionObject, null, allowSceneObjects: true);
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.BeginHorizontal (null);
			EditorGUILayout.PrefixLabel(new GUIContent("step on:"));
			pos.stepOn = (GameObject) EditorGUILayout.ObjectField (pos.stepOn, null, allowSceneObjects: true);
			EditorGUILayout.EndHorizontal ();

		}
		myGrid.OnBeforeSerialize ();
		//Debug.Log (Event.current.mousePosition);
	}

	void OnSceneGUI()
	{
		mousePos = Event.current.mousePosition;
		Vector3 worldPos = Camera.current.ScreenToWorldPoint (mousePos);
		int localX = Mathf.RoundToInt (worldPos.x);
		int localY = -Mathf.RoundToInt (worldPos.y);
		worldPos.z = 0f;
		if (Event.current.type == EventType.KeyDown) {
			if (Event.current.keyCode == KeyCode.Q) {
				myGrid.setPosition(new GridPosition (localX, localY, blocked : true));
			}
			if (Event.current.keyCode == KeyCode.S) {
				pos = myGrid.getPosition (localX, localY);
			}
			if (Event.current.keyCode == KeyCode.C) {
				myGrid.clearPosition (localX, localY);
			}
		}
		
	}
}