using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

namespace DeveloperConsole {

    /// <summary>
    /// This is used to see the ConsolesHistory within the inspector of the
    /// scriptable object.
    /// </summary>
    [CustomEditor(typeof(ConsoleHistory))]
    public class ConsoleHistoryInspector : Editor {

        private const string ConsoleHistoryName = "consoleHistory";
        private const string CurrentIndexName = "currentIndex";

        private ConsoleHistory consoleHistory;
        private System.Type type;
        private FieldInfo consoleHistoryField;
        private FieldInfo currentIndexField;

        private void OnEnable() {
            consoleHistory = target as ConsoleHistory;
            type = consoleHistory.GetType();

            consoleHistoryField = type.GetField(ConsoleHistoryName, BindingFlags.NonPublic | BindingFlags.Instance);
            currentIndexField = type.GetField(CurrentIndexName, BindingFlags.NonPublic | BindingFlags.Instance);
        }

        private void DrawConsoleHistory() {
            EditorGUILayout.LabelField("Console History", EditorStyles.boldLabel);
            var list =(IList<string>) consoleHistoryField.GetValue(consoleHistory);

            if(list != null) {
                for(int i = 0; i < list.Count; i++) {
                    var entry = list[i];
                    EditorGUILayout.LabelField(string.Format("Element {0}: {1}", i + 1, entry));
                }
            }
        }

        private void DrawCurrentIndex() {
            EditorGUILayout.LabelField(string.Format("Current Index: {0}", currentIndexField.GetValue(consoleHistory)));
        }

        public override bool RequiresConstantRepaint() {
            return true;
        }

        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            DrawConsoleHistory();
            DrawCurrentIndex();
        }

    }
}

