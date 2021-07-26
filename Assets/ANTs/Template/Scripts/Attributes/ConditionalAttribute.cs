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
            Enum,
            EnumMask
        }

        public ConditionType type;
        public string targetName;

        public bool boolValue;
        public int enumValue;
        public int[] enumMask;

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

        public ConditionalAttribute(string targetName, params object[] enumMask)
        {
            type = ConditionType.EnumMask;
            this.targetName = targetName;
            this.enumMask = enumMask.Cast<int>().ToArray();
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ConditionalAttribute))]
    public class ConditionalAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (CheckEqual(GetAttribute(), GetTargetProperty(property)))
            {
                EditorGUI.PropertyField(position, property);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return CheckEqual(GetAttribute(), GetTargetProperty(property)) ? 18 : 0;
        }

        private bool CheckEqual(ConditionalAttribute myAttribute, SerializedProperty targetProperty)
        {
            switch (myAttribute.type)
            {
                case ConditionalAttribute.ConditionType.Boolean:
                    return targetProperty.boolValue == myAttribute.boolValue;
                case ConditionalAttribute.ConditionType.Enum:
                    return targetProperty.enumValueIndex == myAttribute.enumValue;
                case ConditionalAttribute.ConditionType.EnumMask:
                    return myAttribute.enumMask.Contains(targetProperty.enumValueIndex);
                default:
                    throw new UnityException("Invalid parameter type");
            }
        }

        private SerializedProperty GetTargetProperty(SerializedProperty property)
        {
            SerializedProperty result;
            string targetName = GetTargetName();
            if (GetParentPath(property.propertyPath) == "")
            {
                result = property.serializedObject.FindProperty(GetTargetName());
            }
            else
            {
                SerializedProperty myProperty = GetPropertyRelative(property.serializedObject, GetParentPath(property.propertyPath));
                result = myProperty.FindPropertyRelative(GetTargetName());
            }

            return result;
        }

        private string GetTargetName()
        {
            return GetAttribute().targetName;
        }

        private ConditionalAttribute GetAttribute()
        {
            return (ConditionalAttribute)attribute;
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