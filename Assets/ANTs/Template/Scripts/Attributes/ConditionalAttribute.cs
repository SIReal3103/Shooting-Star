using System;
using UnityEditor;
using UnityEngine;

namespace ANTs.Core
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class ConditionalAttribute : PropertyAttribute
    {
        public string booleanField;
        public bool value;
        public ConditionalAttribute(string booleanField, bool value)
        {
            this.booleanField = booleanField;
            this.value = value;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ConditionalAttribute))]
    public class ConditionalAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ConditionalAttribute myAttribute = (ConditionalAttribute)attribute;
            SerializedProperty targetProperty = property.serializedObject.FindProperty(myAttribute.booleanField);

            if (targetProperty == null)
            {
                throw new UnityException("there is no " + myAttribute.booleanField + " in " + property.serializedObject);
            }

            if (targetProperty.boolValue == myAttribute.value)
            {
                EditorGUI.PropertyField(position, property);
            }
        }
    }
#endif
}