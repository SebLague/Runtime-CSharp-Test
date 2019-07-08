using System;
using System.CodeDom.Compiler;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class CompileTest : MonoBehaviour {

    public Text input;

    public void Run () {
        var assembly = Compile (input.text);
        var method = assembly.GetType ("MyClass").GetMethod ("MyMethod");
        var del = (Action) Delegate.CreateDelegate (typeof (Action), method);
        del.Invoke ();
    }

    public static Assembly Compile (string source) {
        var options = new CompilerParameters ();
        options.GenerateExecutable = false;
        options.GenerateInMemory = true;

        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies ()) {
            if (!assembly.IsDynamic) {
                options.ReferencedAssemblies.Add (assembly.Location);
            }
        }

        var compiler = new CSharpCompiler.CodeCompiler ();
        var result = compiler.CompileAssemblyFromSource (options, source);

        foreach (var err in result.Errors) {
            Debug.Log (err);
        }

        return result.CompiledAssembly;
    }
}