﻿#if UNITY_EDITOR

using System.Linq;
using UnityEngine;
using UnityEditor;

namespace ANTs.Template
{
    [CustomEditor(typeof(ActionScheduler))]
    public class ActionSchedulerEditor : Editor
    {
        private const float VERTICAL_DISTANCE = 10f;
        private const float HORIZONTAL_DISTANCE = 300f;
        private const float TOGGLES_DISTANCE = 16f;
        private const float HORIZONTAL_TEXT_OFFSET = 20f;

        private SerializedProperty mask;
        private int numLabel;
        private string[] labelNames;

        private void OnEnable()
        {
            mask = serializedObject.FindProperty("maskTable");
            labelNames = GetLabelNames();
            numLabel = labelNames.Length;
            mask.arraySize = numLabel * numLabel;

            serializedObject.ApplyModifiedProperties();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DisplayDefaultScriptLine();

            if (numLabel > 1)
            {
                WriteVerticalText(GetVertcalArea());
                WriteHorizontalText();
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void DisplayDefaultScriptLine()
        {
            using (new EditorGUI.DisabledScope(true))
                EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MonoBehaviour)target), GetType(), false);
        }

        private void WriteHorizontalText()
        {
            for (int i = 0; i < numLabel; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(labelNames[i], GUILayout.Width(HORIZONTAL_DISTANCE));
                for (int j = 0; j < numLabel; j++)
                {
                    if (i == j)
                    {
                        EditorGUILayout.LabelField("", GUILayout.Width(TOGGLES_DISTANCE));
                    }
                    else
                    {
                        mask.GetArrayElementAtIndex(i * numLabel + j).boolValue = 
                            EditorGUILayout.Toggle(mask.GetArrayElementAtIndex(i * numLabel + j).boolValue,
                                GUILayout.Width(TOGGLES_DISTANCE));
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        private void WriteVerticalText(Rect rect)
        {
            float offSetY = HORIZONTAL_DISTANCE + 10;
            for (int i = 0; i < labelNames.Length; i++)
            {
                string text = labelNames[i];
                Vector2 textSize = GetTextSize(text);

                // rotate to vertical
                GUIUtility.RotateAroundPivot(90, new Vector2(rect.x, rect.y + textSize.y / 2f));
                GUI.Label(new Rect(rect.x, rect.y - offSetY, textSize.x, textSize.y), text);
                GUIUtility.RotateAroundPivot(-90, new Vector2(rect.x, rect.y + textSize.y / 2f));

                offSetY += HORIZONTAL_TEXT_OFFSET;
            }
        }

        private Rect GetVertcalArea()
        {
            float maxHeight = labelNames.Max(name => GetTextSize(name).x) + VERTICAL_DISTANCE;
            Rect rect = GUILayoutUtility.GetRect(maxHeight, maxHeight);
            return rect;
        }

        Vector2 GetTextSize(string text)
        {
            return GUI.skin.label.CalcSize(new GUIContent(text + "_"));
        }

        private string[] GetLabelNames()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null)
            {
                Debug.Log("Seceted empty");
                return new string[] { }; //HACK: Protential bug
            }

            ActionBase[] actions = selected.GetComponents<ActionBase>();
            return actions.Select(action => action.GetType().Name).ToArray();
        }
    }
}

#endif