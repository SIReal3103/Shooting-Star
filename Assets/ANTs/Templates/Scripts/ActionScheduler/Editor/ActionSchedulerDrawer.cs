//using System.Linq;
//using UnityEditor;
//using UnityEngine;

//namespace ANTs.Template
//{
//    [CustomPropertyDrawer(typeof(SerializableMask))]
//    public class ActionSchedulerDrawer : PropertyDrawer
//    {
//        private const float VERTICAL_DISTANCE = 10f;
//        private const float HORIZONTAL_DISTANCE = 300f;
//        private const float TOGGLES_DISTANCE = 16f;
//        private const float HORIZONTAL_TEXT_OFFSET = 20f;

//        SerializedProperty labels;
//        SerializedProperty mask;
//        SerializedProperty size;

//        string[] names;

//        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//        {
//            labels = property.FindPropertyRelative("labels");
//            mask = property.FindPropertyRelative("mask");
//            size = property.FindPropertyRelative("size");

//            names = GetLabelNames();

//            labels.arraySize = names.Length;
//            mask.arraySize = names.Length * names.Length;
//            size.vector2IntValue = new Vector2Int(names.Length, names.Length);

//            for(int i = 0; i < names.Length; i++)
//            {
//                labels.GetArrayElementAtIndex(i).stringValue = names[i];
//            }
//        }

//        private string[] GetLabelNames()
//        {
//            GameObject go = Selection.activeGameObject;
//            ActionBase[] actions = go.GetComponents<ActionBase>();
//            return actions.Select(action => action.GetType().Name).ToArray();
//        }

//        private void WriteHorizontalText()
//        {
//            for (int i = 0; i < names.Length; i++)
//            {
//                EditorGUILayout.BeginHorizontal();
//                EditorGUILayout.LabelField(names[i], GUILayout.Width(HORIZONTAL_DISTANCE));
//                for (int j = 0; j < names.Length; j++)
//                {
//                    if (i == j)
//                    {
//                        EditorGUILayout.LabelField("", GUILayout.Width(TOGGLES_DISTANCE));
//                    }
//                    else
//                    {
//                        mask.GetArrayElementAtIndex(i * names.Length + j).boolValue = 
//                            EditorGUILayout.Toggle(mask.GetArrayElementAtIndex(i * names.Length + j).boolValue,
//                                GUILayout.Width(TOGGLES_DISTANCE));
//                    }
//                }
//                EditorGUILayout.EndHorizontal();
//            }
//        }

//        private void WriteVerticalText(Rect rect)
//        {
//            float offSetY = HORIZONTAL_DISTANCE + 10;
//            for (int i = 0; i < names.Length; i++)
//            {
//                string text = names[i];
//                Vector2 textSize = GetTextSize(text);

//                // rotate to vertical
//                GUIUtility.RotateAroundPivot(90, new Vector2(rect.x, rect.y + textSize.y / 2f));
//                GUI.Label(new Rect(rect.x, rect.y - offSetY, textSize.x, textSize.y), text);
//                GUIUtility.RotateAroundPivot(-90, new Vector2(rect.x, rect.y + textSize.y / 2f));

//                offSetY += HORIZONTAL_TEXT_OFFSET;
//            }
//        }

//        private Rect GetVertcalArea()
//        {
//            float maxHeight = names.Max(name => GetTextSize(name).x) + VERTICAL_DISTANCE;
//            Rect rect = GUILayoutUtility.GetRect(maxHeight, maxHeight);
//            return rect;
//        }

//        Vector2 GetTextSize(string text)
//        {
//            return GUI.skin.label.CalcSize(new GUIContent(text + "_"));
//        }
//    }
//}