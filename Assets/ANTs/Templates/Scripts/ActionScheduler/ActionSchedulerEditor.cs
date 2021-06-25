using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ANTs.Template
{
    [CustomEditor(typeof(ActionScheduler))]
    public class ActionSchedulerEditor : Editor
    {
        bool[,] actionMask;
        int length;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ActionScheduler scheduler = target as ActionScheduler;
            actionMask = scheduler.actionMask;

            GameObject go = Selection.activeGameObject;
            ActionBase[] actions = go.GetComponents<ActionBase>();
            length = actions.Length;




            float maxHeight = 0;
            for(int i = 0; i < length; i++)
            {
                maxHeight = Mathf.Max(maxHeight, GetTextSize(GetName(actions[i])).x);
            }
            maxHeight += 10;
            Rect rect = GUILayoutUtility.GetRect(maxHeight, maxHeight);




            float offSetX = 310;
            for (int i = 0; i < length; i++)
            {
                string text = GetName(actions[i]);
                Vector2 textSize = GetTextSize(text);
                GUIUtility.RotateAroundPivot(90, new Vector2(rect.x, rect.y + textSize.y / 2f));
                GUI.Label(new Rect(rect.x, rect.y - offSetX, textSize.x, textSize.y), text);
                GUIUtility.RotateAroundPivot(-90, new Vector2(rect.x, rect.y + textSize.y / 2f));

                offSetX += textSize.y + 1;
            }



            
            for(int i = 0; i < length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                string name = GetName(actions[i]);
                EditorGUILayout.LabelField(name, GUILayout.Width(300));
                for(int j = 0; j < length; j++)
                {
                    if (i == j)
                    {
                        EditorGUILayout.LabelField("", GUILayout.Width(16));
                    }
                    else
                    {
                        actionMask[i, j] = EditorGUILayout.Toggle(actionMask[i, j], GUILayout.Width(16));
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        string GetName(MonoBehaviour type)
        {
            return type.GetType().Name;
        }

        Vector2 GetTextSize(string text)
        {
            return GUI.skin.label.CalcSize(new GUIContent(text + "_"));
        }
    }
}