using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Console.Demo.EditorTools {

    [CustomEditor(typeof(ContextController))]
    public class ContextControllerInspector : Editor {

        private List<Patrol> activeAgents;
        private List<Patrol> inactiveAgents;

        private void OnEnable() {
            var type = target.GetType();

            var activeField = type.GetField("activeAIs", BindingFlags.NonPublic | BindingFlags.Instance);
            activeAgents = activeField.GetValue(target) as List<Patrol>;

            var inactiveField = type.GetField("inactiveAIs", BindingFlags.NonPublic | BindingFlags.Instance);
            inactiveAgents = inactiveField.GetValue(target) as List<Patrol>;
        }

        private void DrawAgentTable() {
            EditorGUILayout.LabelField("Patrol Agents", EditorStyles.boldLabel);

            using (var group = new EditorGUILayout.VerticalScope()) {
                EditorGUILayout.LabelField("Active", EditorStyles.boldLabel);
                try {
                    foreach(var agent in activeAgents) {
                        EditorGUILayout.LabelField(agent.name);
                    }
                } catch(System.NullReferenceException) {}
            }

            using (var group = new EditorGUILayout.VerticalScope()) {
                EditorGUILayout.LabelField("Active", EditorStyles.boldLabel);
                try {
                    foreach(var agent in inactiveAgents) {
                        EditorGUILayout.LabelField(agent.name);
                    }
                } catch(System.NullReferenceException) {}
            }
        }

        public override bool RequiresConstantRepaint() {
            return EditorApplication.isPlaying;
        }

        public override void OnInspectorGUI() {
            DrawDefaultInspector();
        }

    }
}
