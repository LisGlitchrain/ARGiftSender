using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MusicPlayer;

[CustomEditor(typeof(MenuItemSelectionHelper))]
public class MenuItemSelectionHelperCE : Editor
{
    MenuItemSelectionHelper targetScript;

    void OnEnable()
    {
        targetScript = (MenuItemSelectionHelper)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("SelectItem")) targetScript.OnSelectionStart();
        if (GUILayout.Button("Deselect")) targetScript.OnDeselect();

    }
}