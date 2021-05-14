using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using GOAP;


[CustomEditor(typeof(GAgentVisual))]
[CanEditMultipleObjects]
public class GAgentVisualEditor : Editor 
{


    void OnEnable()
    {

    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();
        GAgentVisual agent = (GAgentVisual) target;
        GUILayout.Label("Name: " + agent.name);
        GUILayout.Label("Current Action: " + agent.gameObject.GetComponent<Agent>().currentAction);
        GUILayout.Label("Actions: ");
        foreach (Action a in agent.gameObject.GetComponent<Agent>().actions)
        {
            string pre = "";
            string eff = "";

            foreach (KeyValuePair<string, int> p in a.preconditionsDic)
                pre += p.Key + ", ";
            foreach (KeyValuePair<string, int> e in a.effectsDic)
                eff += e.Key + ", ";

            GUILayout.Label("====  " + a.actionName + "(" + pre + ")(" + eff + ")");
        }
        GUILayout.Label("Goals: ");
        foreach (KeyValuePair<SubGoal, int> g in agent.gameObject.GetComponent<Agent>().goalsDic)
        {
            GUILayout.Label("---: ");
            foreach (KeyValuePair<string, int> sg in g.Key.subGoals)
                GUILayout.Label("=====  " + sg.Key);
        }
        GUILayout.Label("agentInternalState: ");
        foreach (KeyValuePair<string, int> sg in agent.gameObject.GetComponent<Agent>().agentInternalState.GetStateDictionary())
        {
                GUILayout.Label("=====  " + sg.Key);
        }

        GUILayout.Label("Inventory: ");
        foreach (GameObject g in agent.gameObject.GetComponent<Agent>().inventory.items)
        {
            GUILayout.Label("====  " + g.tag);
        }


        serializedObject.ApplyModifiedProperties();
    }
}