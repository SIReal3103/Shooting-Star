using System.Linq;
using UnityEngine;
using UnityEditor;

namespace ANTs.Template
{
    [CustomEditor(typeof(ActionScheduler))]
    public class ActionSchedulerEditor : Editor
    {
        public const int MAX_MASK = 5;
        private const float VERTICAL_DISTANCE = 10f;
        private const float HORIZONTAL_DISTANCE = 300f;
        private const float TOGGLES_DISTANCE = 16f;
        private const float HORIZONTAL_TEXT_OFFSET = 20f;

        private SerializableMask mask;
        private SerializedProperty property;
        private string[] labelNames;

        private void OnEnable()
        {
            property = serializedObject.FindProperty("maskTable");
            mask = property.objectReferenceValue as SerializableMask;
            if (mask == null)
            {
                mask = SerializableMask.CreateNew(Vector2Int.one * MAX_MASK);
                property.objectReferenceValue = mask;
            }

            labelNames = GetLabelNames();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            base.OnInspectorGUI();

            WriteVerticalText(GetVertcalArea());
            WriteHorizontalText();

            serializedObject.ApplyModifiedProperties();
        }

        private void WriteHorizontalText()
        {
            for (int i = 0; i < labelNames.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(labelNames[i], GUILayout.Width(HORIZONTAL_DISTANCE));
                for (int j = 0; j < labelNames.Length; j++)
                {
                    if (i == j)
                    {
                        EditorGUILayout.LabelField("", GUILayout.Width(TOGGLES_DISTANCE));
                    }
                    else
                    {
                        mask[i, j] = EditorGUILayout.Toggle(mask[i, j], GUILayout.Width(TOGGLES_DISTANCE));
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

        private string[] GetLabelNames()
        {
            GameObject go = Selection.activeGameObject;
            ActionBase[] actions = go.GetComponents<ActionBase>();
            return actions.Select(action => action.GetType().Name).ToArray();
        }

        Vector2 GetTextSize(string text)
        {
            return GUI.skin.label.CalcSize(new GUIContent(text + "_"));
        }
    }
}