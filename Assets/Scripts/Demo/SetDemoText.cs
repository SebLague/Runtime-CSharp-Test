using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDemoText : MonoBehaviour {
    public InputField inputField;

    [Multiline (10)]
    public string defaultInput;

    void Start () {
        inputField.text = defaultInput;
    }
}