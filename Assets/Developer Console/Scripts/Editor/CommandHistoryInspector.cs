using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

namespace Console {

    /// <summary>
    /// This is used to see the ConsolesHistory within the inspector of the
    /// scriptable object.
    /// </summary>
    [CustomEditor (typeof (CommandHistory))]
    public class CommandHistoryInspector : Editor {

        private const string CommandHistoryName = "commandHistory";
        private const string CurrentIndexName = "currentIndex";

        private CommandHistory commandHistory;
        private System.Type type;
        private FieldInfo commandHistoryField;
        private FieldInfo currentIndexField;

        private void OnEnable () {
            commandHistory = target as CommandHistory;
            type = commandHistory.GetType ();

            commandHistoryField = type.GetField (CommandHistoryName, BindingFlags.NonPublic | BindingFlags.Instance);
            currentIndexField = type.GetField (CurrentIndexName, BindingFlags.NonPublic | BindingFlags.Instance);
        }

        private void DrawCommandHistory () {
            EditorGUILayout.LabelField ("Console History", EditorStyles.boldLabel);
            var list = (IList<string>) commandHistoryField.GetValue (commandHistory);

            if (list != null) {
                for (int i = 0; i < list.Count; i++) {
                    var entry = list[i];
                    EditorGUILayout.LabelField (string.Format ("Command at {0}: {1}", i + 1, entry));
                }
            }
        }

        private void DrawIndexValue () {
            var list = (IList<string>) commandHistoryField.GetValue (commandHistory);
            string value;
            var index = currentIndexField.GetValue (commandHistory);
            try {
                value = list[(int) index];
            } catch (System.ArgumentOutOfRangeException) {
                value = "N/A";
            }
            EditorGUILayout.LabelField (string.Format ("Current Index: {0}, Value: {1}", index, value));
        }

        public override bool RequiresConstantRepaint () {
            return true;
        }

        public override void OnInspectorGUI () {
            DrawDefaultInspector ();
            DrawCommandHistory ();
            DrawIndexValue ();
        }

    }
}
