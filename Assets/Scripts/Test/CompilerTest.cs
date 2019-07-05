using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using Microsoft.CSharp;
using UnityEngine;
using UnityEngine.UI;

public class CompilerTest : MonoBehaviour {

    public Text resultUI;
    public Text inputUI;

    public void Run () {
        var compileTimer = new System.Diagnostics.Stopwatch ();
        var totalTimer = new System.Diagnostics.Stopwatch ();
        compileTimer.Start ();
        totalTimer.Start ();
        string userCode = inputUI.text;

        CSharpCodeProvider provider = new CSharpCodeProvider ();
        CompilerResults results = provider.CompileAssemblyFromSource (new CompilerParameters (), userCode);

        if (results.Errors.HasErrors) {
            string errorString = "";
            foreach (CompilerError error in results.Errors) {
                errorString += error.ErrorNumber + " " + error.ErrorText + "\n";
            }
            resultUI.text = errorString;
        } else {
            System.Type classType = results.CompiledAssembly.GetType ("MyClass");
            System.Reflection.MethodInfo method = classType.GetMethod ("MyFunction");

            object result = method.Invoke (null, null);
            resultUI.text = result.ToString ();
        }

        compileTimer.Stop ();
        resultUI.text += "\nCompile time: " + compileTimer.ElapsedMilliseconds + " ms; Total time: " + totalTimer.ElapsedMilliseconds + " ms.";
    }

    public object RunCode (string userCode) {
        CSharpCodeProvider provider = new CSharpCodeProvider ();
        CompilerResults results = provider.CompileAssemblyFromSource (new CompilerParameters (), userCode);

        System.Type classType = results.CompiledAssembly.GetType ("MyClass");
        System.Reflection.MethodInfo method = classType.GetMethod ("MyFunction");

        object result = method.Invoke (null, null);
        return result;
    }

}