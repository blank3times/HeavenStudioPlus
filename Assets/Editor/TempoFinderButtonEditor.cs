using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using RhythmHeavenMania;

namespace RhythmHeavenMania.Editor
{
    [CustomEditor(typeof(TempoFinderButton))]
    public class TempoFinderButtonEditor : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            TempoFinderButton targetButton = (TempoFinderButton)target;

            targetButton.tempoFinder = (TempoFinder)EditorGUILayout.ObjectField("Tempo Finder", targetButton.tempoFinder, typeof(TempoFinder), true);

            base.OnInspectorGUI();
        }
    }
}