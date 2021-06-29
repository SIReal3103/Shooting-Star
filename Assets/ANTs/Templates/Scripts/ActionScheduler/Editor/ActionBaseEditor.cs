using System;
using UnityEditor;
using UnityEngine;

namespace ANTs.Template
{
    [CustomEditor(typeof(ActionBase), true)]
    public class ActionBaseEditor : Editor
    {
        SerializedProperty isAttachWithAnimator;
        SerializedProperty typeOfAction;
        SerializedProperty isTransitionTrigger;
        SerializedProperty syncWithAnimation;
        SerializedProperty actionStartOnPlay;
        SerializedProperty isActionStart;

        public void OnEnable()
        {
            isAttachWithAnimator = serializedObject.FindProperty("isAttachWithAnimator");
            isTransitionTrigger = serializedObject.FindProperty("isTransitionTrigger");
            syncWithAnimation = serializedObject.FindProperty("syncWithAnimation");
            actionStartOnPlay = serializedObject.FindProperty("actionStartOnPlay");
            isActionStart = serializedObject.FindProperty("isActionActive");
            typeOfAction = serializedObject.FindProperty("typeOfAction");
        }

        public override void OnInspectorGUI()
        {
            // Header
            serializedObject.Update();

            EditorGUILayout.PropertyField(actionStartOnPlay);
            base.OnInspectorGUI();

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Animator Control", EditorStyles.boldLabel); // Header
            EditorGUILayout.PropertyField(isAttachWithAnimator);            
            if (isAttachWithAnimator.boolValue)
            {
                EditorGUILayout.PropertyField(typeOfAction);
                AssignValue((ActionType)typeOfAction.enumValueIndex);
                DisplayAnimatorControl();
            }

            // Debug
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Debug Info", EditorStyles.boldLabel); // Header
            ReadOnly(DisplayActive);

            serializedObject.ApplyModifiedProperties();
        }

        #region ========================================Displays
        private void DisplayAnimatorControl()
        {
            if (typeOfAction.enumValueIndex == (int)ActionType.Custom)
            {
                DisplayProperty();
            }
            else
            {
                ReadOnly(DisplayProperty);
            }
        }
        private void DisplayActive()
        {
            GUILayout.TextField(
                string.Format("isActionActive"), 
                isActionStart.boolValue ? GetTextStyle(Color.green) : GetTextStyle(Color.gray));
        }
        private void DisplayProperty()
        {
            EditorGUILayout.PropertyField(isTransitionTrigger);
            EditorGUILayout.PropertyField(syncWithAnimation);            
        }
        #endregion

        #region ========================================= Utils
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
                    break;
                case ActionType.TriggerButNotSync:
                    isTransitionTrigger.boolValue = true;
                    syncWithAnimation.boolValue = false;
                    break;
                case ActionType.Boolean:
                    isTransitionTrigger.boolValue = false;
                    syncWithAnimation.boolValue = false;
                    break;
                default: // Custom
                    break;
            }
        }
        #endregion
    }
}