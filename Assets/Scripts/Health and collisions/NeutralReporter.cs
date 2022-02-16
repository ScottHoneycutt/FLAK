using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralReporter : MonoBehaviour
{
    private bool isReported = false;

    // Start is called before the first frame update
    void Update()
    {
        if (!isReported)
        {
            isReported = true;
            Identifiers.identifier.ReportNeutralTargeter(this.gameObject);
        }
    }

    // Update is called once per frame
    void OnDisable()
    {
        if (isReported)
        {
            isReported = false;
            Identifiers.identifier.RemoveNeutralTargeter(this.gameObject);
        }
    }
}
