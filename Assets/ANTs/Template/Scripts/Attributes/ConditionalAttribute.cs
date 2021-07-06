using System;
using UnityEditor;
using UnityEngine;

namespace ANTs.Core
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class ConditionalAttribute : PropertyAttribute
    {
        public string targetField;
        public ConditionalAttribute(string booleanField)
        {
            this.targetField = booleanField;
        }
    }

    [CustomPropertyDrawer(typeof(ConditionalAttribute))]
    public class ConditionalAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ConditionalAttribute myAttribute = (ConditionalAttribute)attribute;
            SerializedProperty targetProperty = property.serializedObject.FindProperty(myAttribute.targetField);

            if (targetProperty == null)
            {
                throw new UnityException("there is no " + myAttribute.targetField + " in " + property.serializedObject);
            }

            if (targetProperty.boolValue)
            {
                EditorGUILayout.PropertyField(property);
            }
        }
    }
}