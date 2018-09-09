using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Console.EditorTools {

    using Console;

    [CustomEditor(typeof(IdCache))]
    public class IdCacheInspector : Editor {

        private const string CacheName = "cache";

        private IDictionary<int, object> cacheTable;

        private void OnEnable() {
            FieldInfo cacheField = typeof(IdCache).GetField(CacheName, BindingFlags.NonPublic | BindingFlags.Instance);
            cacheTable = cacheField.GetValue(target as IdCache) as IDictionary<int, object>;
        }

        private void DrawCacheTable() {
            try {
                EditorGUILayout.LabelField("Cache Table", EditorStyles.boldLabel);
                using (var group = new EditorGUILayout.HorizontalScope()) {
                    using (var idColumn = new EditorGUILayout.VerticalScope()) {
                        EditorGUILayout.LabelField("Id", EditorStyles.boldLabel);
                        foreach(var entry in cacheTable) {
                            EditorGUILayout.LabelField(entry.Key.ToString());
                        }
                    }

                    using (var objectColumn = new EditorGUILayout.VerticalScope()) {
                        EditorGUILayout.LabelField("Object Instance", EditorStyles.boldLabel);
                        foreach(var entry in cacheTable) {
                            EditorGUILayout.LabelField(entry.Value.ToString());
                        }
                    }
                }
            } catch (System.NullReferenceException) {}
        }

        public override bool RequiresConstantRepaint() {
            return Application.isPlaying;
        }

        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            DrawCacheTable();
        }
    }
}
