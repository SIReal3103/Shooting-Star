using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace ANTs.Game
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class ConditionalAttribute : PropertyAttribute
    {
        public enum ConditionType
        {
            Boolean,
            Enum
        }

        public ConditionType type;
        public string targetName;

        public bool boolValue;
        public int enumValue;

        public ConditionalAttribute(string targetName, bool boolValue)
        {
            type = ConditionType.Boolean;
            this.targetName = targetName;
            this.boolValue = boolValue;
        }

        public ConditionalAttribute(string targetName, object enumValue)
        {
            type = ConditionType.Enum;
            this.targetName = targetName;
            this.enumValue = (int)enumValue;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ConditionalAttribute))]
    public class ConditionalAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ConditionalAttribute myAttribute = (ConditionalAttribute)attribute;
            SerializedProperty targetProperty = GetTargetProperty(property, myAttribute);

            if (targetProperty == null)
            {
                throw new UnityException("there is no " + myAttribute.targetName + " in " + property.serializedObject);
            }

            if (CheckEqual(myAttribute, targetProperty))
            {
                EditorGUI.PropertyField(position, property);
            }
        }

        private bool CheckEqual(ConditionalAttribute myAttribute, SerializedProperty targetProperty)
        {
            switch (myAttribute.type)
            {
                case ConditionalAttribute.ConditionType.Boolean:
                    return targetProperty.boolValue == myAttribute.boolValue;
                case ConditionalAttribute.ConditionType.Enum:
                    return targetProperty.enumValueIndex == myAttribute.enumValue;
                default:
                    throw new UnityException("Invalid parameter type");
            }
        }

        private SerializedProperty GetTargetProperty(SerializedProperty property, ConditionalAttribute myAttribute)
        {
            SerializedProperty result;
            if (GetParentPath(property.propertyPath) == "")
            {
                result = property.serializedObject.FindProperty(myAttribute.targetName);
            }
            else
            {
                SerializedProperty myProperty = GetPropertyRelative(property.serializedObject, GetParentPath(property.propertyPath));
                result = myProperty.FindPropertyRelative(myAttribute.targetName);
            }

            return result;
        }

        public SerializedProperty GetPropertyRelative(SerializedObject myObject, string path)
        {
            string[] propertyPaths = path.Split('.');
            Assert.AreNotEqual(propertyPaths.Length, 0);
            SerializedProperty result = myObject.FindProperty(propertyPaths[0]);

            foreach (string propertyPath in propertyPaths.Skip(1))
            {
                result = result.FindPropertyRelative(propertyPath);
            }
            return result;
        }

        private string GetParentPath(string path)
        {
            string[] properties = path.Split('.');
            RemoveLastElement(ref properties);
            string result = string.Join(".", properties);
            return result;
        }

        private void RemoveLastElement(ref string[] array)
        {
            if (array.Length == 0) return;
            Array.Resize(ref array, array.Length - 1);
        }
    }
#endif
}