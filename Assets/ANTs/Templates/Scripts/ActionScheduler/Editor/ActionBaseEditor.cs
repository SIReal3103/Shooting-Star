using System;
using UnityEditor;
using UnityEngine;

namespace ANTs.Template
{
    [CustomEditor(typeof(ActionBase), true)]
    public class ActionBaseEditor : Editor
    {
        SerializedProperty isTransitionTrigger;
        SerializedProperty syncWithAnimation;
        SerializedProperty actionStartOnPlay;
        SerializedProperty isActionStart;
        SerializedProperty typeOfAction;

        public void OnEnable()
        {
            isTransitionTrigger = serializedObject.FindProperty("isTransitionTrigger");
            syncWithAnimation = serializedObject.FindProperty("syncWithAnimation");
            actionStartOnPlay = serializedObject.FindProperty("actionStartOnPlay");
            isActionStart = serializedObject.FindProperty("isActionActive");
            typeOfAction = serializedObject.FindProperty("typeOfAction");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            base.OnInspectorGUI();

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("ActionBase", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(typeOfAction);
            AssignValue((ActionType)typeOfAction.enumValueIndex);

            if (typeOfAction.enumValueIndex == (int)ActionType.Custom)
            {
                DisplayProperty();
            }
            else
            {
                ReadOnly(DisplayProperty);
            }
            ReadOnly(DisplayActive);

            serializedObject.ApplyModifiedProperties();
        }

        private void DisplayActive()
        {
            GUILayout.TextField("isActionActive", isActionStart.boolValue ? GetTextStyle(Color.green) : GetTextStyle(Color.gray));
        }

        private void DisplayProperty()
        {
            EditorGUILayout.PropertyField(isTransitionTrigger);
            EditorGUILayout.PropertyField(syncWithAnimation);
            EditorGUILayout.PropertyField(actionStartOnPlay);
        }

        private void ReadOnly(Action func)
        {
            bool wasEnabled = GUI.enabled;
            GUI.enabled = false;
            func();
            GUI.enabled = wasEnabled;
        }

        private GUIStyle GetTextStyle(Color color)
        {
            GUIStyle s = new GUIStyle(EditorStyles.label);
            s.normal.textColor = color;
            return s;
        }

        private void AssignValue(ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.TriggerWithSync:
                    isTransitionTrigger.boolValue = true;
                    syncWithAnimation.boolValue = true;
                    actionStartOnPlay.boolValue = false;
                    break;
                case ActionType.TriggerButNotSync:
                    isTransitionTrigger.boolValue = true;
                    syncWithAnimation.boolValue = false;
                    actionStartOnPlay.boolValue = false;
                    break;
                case ActionType.BooleanStartOnPlay:
                    isTransitionTrigger.boolValue = false;
                    syncWithAnimation.boolValue = false;
                    actionStartOnPlay.boolValue = true;
                    break;
                case ActionType.BooleanNotStartOnPlay:
                    isTransitionTrigger.boolValue = false;
                    syncWithAnimation.boolValue = false;
                    actionStartOnPlay.boolValue = false;
                    break;
                default: // Custom
                    break;
            }
        }
    }
}