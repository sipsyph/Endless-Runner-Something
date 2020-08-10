using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtommDocument : MonoBehaviour
{
    public string documentName;
    [TextArea(5, 10)]
    public string documentText;
}
