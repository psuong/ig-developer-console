using Console;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Console.EditorTools {

    [CustomEditor(typeof(ConsoleCmdExecutor))]
    public class CmdExecutorInspector : Editor {
        
        private SerializedProperty useCustomRegexProp;
        private SerializedProperty regexProperty;
        private ReorderableList regexArray;

        private void OnEnable() {
            useCustomRegexProp = serializedObject.FindProperty("useCustomRegexPatterns");
            regexProperty = serializedObject.FindProperty("customRegexPatterns");
            regexArray = new ReorderableList(serializedObject, regexProperty, false, true, false, false);

            regexArray.drawElementCallback += DrawElement;
            regexArray.drawHeaderCallback += DrawHeader;
        }

        private void OnDisable() {
            regexArray.drawElementCallback -= DrawElement;
            regexArray.drawHeaderCallback -= DrawHeader;
        }

        private void DrawElement(Rect r, int index, bool isActive, bool isFocused) {
            var prop = regexProperty.GetArrayElementAtIndex(index);
            regexArray.elementHeight = EditorGUIUtility.singleLineHeight;
            switch(index) {
                case 0:
                    DrawPropertyInfo(r, prop, "Event Regex");
                    return;
                case 1:
                    DrawPropertyInfo(r, prop, "Bool Regex");
                    return;
                case 2:
                    DrawPropertyInfo(r, prop, "Char Regex");
                    return;
                case 3:
                    DrawPropertyInfo(r, prop, "Int Regex");
                    return;
                case 4:
                    DrawPropertyInfo(r, prop, "Float Regex");
                    return;
                case 5:
                    DrawPropertyInfo(r, prop, "String Regex");
                    return;
                default:
                    return;
            }
        }

        private void DrawHeader(Rect r) {
            EditorGUI.LabelField(r, new GUIContent("Custom Regexes", "Define custom regular expressions below"));
        }

        private void DrawPropertyInfo(Rect r, SerializedProperty prop, string label) {
            EditorGUI.PropertyField(r, prop, new GUIContent(label));
        }
        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            
            if (useCustomRegexProp.boolValue) {
                regexArray.DoLayoutList();
            }

            EditorGUI.BeginChangeCheck();
            serializedObject.Update();

            if (EditorGUI.EndChangeCheck()) {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
