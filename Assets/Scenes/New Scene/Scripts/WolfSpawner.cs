using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class WolfSpawner : MonoBehaviour
{
    public GameObject wolf;
    private ChaseWolf cw;

    // Start is called before the first frame update
    void Start()
    {

        cw = FindObjectOfType<ChaseWolf>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cw.wolfCaught)
            Instantiate(wolf, transform.position, Quaternion.identity);

    }
}
