using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GOAP;

public class ClampName : MonoBehaviour
{
    public Text nameText;
    Agent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<Agent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 textPos = Camera.main.WorldToScreenPoint(transform.position);
        nameText.transform.position = textPos;
        if(agent.currentAction != null)
        nameText.text = agent.currentAction.ToString();
    }
}
