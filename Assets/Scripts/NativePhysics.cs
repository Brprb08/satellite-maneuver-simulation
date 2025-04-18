using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public static class NativePhysics
{
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr LoadLibrary(string dllToLoad);

    static NativePhysics()
    {
        string unityPluginsPath = Path.Combine(Application.dataPath, "Plugins/x86_64/PhysicsPlugin.dll");
        Debug.Log($"Checking for DLL at: {unityPluginsPath}");

        if (File.Exists(unityPluginsPath))
        {
            Debug.Log("DLL exists at expected path!");
        }
        else
        {
            Debug.LogError("DLL NOT FOUND! Check file path.");
        }

        IntPtr handle = LoadLibrary(unityPluginsPath);
        if (handle == IntPtr.Zero)
        {
            Debug.LogError($"DLL load failed! Error Code: {Marshal.GetLastWin32Error()}");
        }
        else
        {
            Debug.Log("DLL loaded successfully");
        }
    }


    [DllImport("PhysicsPluginTest", EntryPoint = "RungeKuttaSingle", CallingConvention = CallingConvention.Cdecl)]
    public static extern void RungeKuttaSingle(
        ref Vector3 position,
        ref Vector3 velocity,
        float mass,
        Vector3[] bodies,
        float[] masses,
        int numBodies,
        float deltaTime,
        ref Vector3 thrustImpulse
    );
}
