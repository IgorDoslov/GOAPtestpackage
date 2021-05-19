using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GOAP
{

    public class AgentDebugger : ScriptableWizard
    {
        Vector2 scrollPos = Vector2.zero;

        [MenuItem("Window/GOAP Debugger")]
        public static void ShowWindow()
        {
            GetWindow<AgentDebugger>();
        }

        private void OnGUI()
        {
            GameObject agent = Selection.activeGameObject;

            if (Selection.activeGameObject == null || agent.GetComponent<Agent>() == null)
                return;


            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandHeight(true));

            EditorGUILayout.LabelField("Agent Name: ", agent.name);


            if (agent.gameObject.GetComponent<Agent>().currentAction != null)
                EditorGUILayout.LabelField("Current Action: ", agent.gameObject.GetComponent<Agent>().currentAction.ToString());


            EditorGUILayout.EndScrollView();

        }
    }
}
