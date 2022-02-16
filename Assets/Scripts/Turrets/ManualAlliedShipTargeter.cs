using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualAlliedShipTargeter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Identifiers.identifier.ReportAlliedShipTargeter(this.gameObject);
    }
}
