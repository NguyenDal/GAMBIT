using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Python.Runtime;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.WSA;

public class GambitInstallation
{
    private String localAppData;
    private String gambitPath;
    private String pythonPath;
    private String OSFPath;

    public GambitInstallation()
    {
        switch (UnityEngine.Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
                this.localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                this.gambitPath = this.localAppData + "\\Gambit";
                this.pythonPath = gambitPath + "\\Python379\\python.exe";
                this.OSFPath = gambitPath + "\\OpenSeeFace\\OpenSeeFace\\facetracker.py";
                break;
            case RuntimePlatform.WindowsPlayer:
                this.localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                this.gambitPath = this.localAppData + "\\Gambit";
                this.pythonPath = this.gambitPath + "\\Python379\\python.exe";
                this.OSFPath = this.gambitPath + "\\OpenSeeFace\\OpenSeeFace\\facetracker.py";
                break;
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:
                throw new DirectoryNotFoundException();
            case RuntimePlatform.LinuxEditor:
            case RuntimePlatform.LinuxPlayer:
                throw new DirectoryNotFoundException();
            default:
                throw new DirectoryNotFoundException();
        }
    }

    public String getGambitPath()
    {
        return gambitPath;
    }

    public String getPythonPath()
    { 
        return pythonPath; 
    }

    public String getOSFPath()
    {
        return OSFPath;
    }
}

public class PythonInstaller : MonoBehaviour
{
    PythonEngine engine;
    public static IntPtr threadPtr;
    public static dynamic pyFile;
    public static dynamic pySys;
    private static Process proc;


    // Please see Python.Included for more documentation
    // https://github.com/henon/Python.Included
    // The gist of why we're integrating Python into our project is because our project ideally should be OS-neutral
    // We'll be running all of the core dependencies of OpenSeeFace, and if future teams require another python dependency 
    // they can optionally include it within their project using this runtime.


    // This task should only run once. This task will install Python into the User's home directory


    static async Task StartInstall()
    {
        UnityEngine.Debug.Log("Checking for Python installation....");
        UnityEngine.Debug.Log("Running test");


        Runtime.PythonDLL = "C:\\Users\\r-exa\\AppData\\Local\\Programs\\Python\\Python37" + "\\python37.dll";
        UnityEngine.Debug.Log(Runtime.PythonDLL);


        PythonEngine.Initialize();
        //threadPtr = PythonEngine.BeginAllowThreads();

        UnityEngine.Debug.Log("Initialized Python Engine");
        using (Py.GIL())
        {
            UnityEngine.Debug.Log(threadPtr);
            dynamic os = Py.Import("os");
            dynamic sys = Py.Import("sys");
            sys.path.append(os.path.dirname(os.path.expanduser("C:\\Users\\r-exa\\GitCLones\\gambit\\Native\\facetracker.py")));
            var fromFile = Py.Import(Path.GetFileNameWithoutExtension("C:\\Users\\r-exa\\GitCLones\\gambit\\Native\\facetracker.py"));
            pyFile = fromFile;
            pySys = sys;
            fromFile.InvokeMethod("replace");

        }

        //Debug.Log(sys); 
        //Debug.Log("Version" + sys.version);
    }

    static bool isWindowsDepsInstalled()
    {
        GambitInstallation currentInstall = new GambitInstallation();

        if (File.Exists(currentInstall.getGambitPath()) && File.Exists(currentInstall.getPythonPath()) && File.Exists(currentInstall.getOSFPath())) {
            return true;
        }
        return false;
    }

    static String getWinInstaller()
    {
        String dataPath = UnityEngine.Application.dataPath;
        String ps1Installer = dataPath + "\\..\\DependancyInstaller\\win\\installscript.ps1";
        return ps1Installer;
    }

    static void startShellExecuteHalting(String executablePath, String arguments)
    {
        ProcessStartInfo processStartInfo = new ProcessStartInfo();
        processStartInfo.FileName = executablePath;
        processStartInfo.Arguments = arguments;
        processStartInfo.UseShellExecute = true;
        String dataPath = UnityEngine.Application.dataPath;
        String ps1WD = dataPath + "\\..\\DependancyInstaller\\win\\";
        processStartInfo.WorkingDirectory = ps1WD;
        processStartInfo.RedirectStandardOutput = false;
        
        Process process = Process.Start(processStartInfo);
        proc = process;
        //process.WaitForExit();
    }

    static bool installWindowsDeps()
    {
        if (isWindowsDepsInstalled())
        {
            // return true;
        }

        String installer = getWinInstaller();

        try
        {
            startShellExecuteHalting("C:\\Windows\\System32\\WindowsPowerShell\\v1.0\\powershell.exe", installer);
            return true;
        }
        catch (Exception e)
        {
            {
                UnityEngine.Debug.Log(e);
                UnityEngine.Debug.Log(Environment.SpecialFolder.Windows);
                return false;
            }
        }
    }

    static bool areDepsInstalled()
    {
        switch(UnityEngine.Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.WindowsPlayer:
                return isWindowsDepsInstalled();
            case RuntimePlatform.OSXEditor: 
            case RuntimePlatform.OSXPlayer:
                throw new DirectoryNotFoundException();
            case RuntimePlatform.LinuxEditor:
            case RuntimePlatform.LinuxPlayer:
                throw new DirectoryNotFoundException();
            default:
                throw new DirectoryNotFoundException();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //if (!areDepsInstalled() && UnityEngine.Application.platform == RuntimePlatform.WindowsEditor)
        //{
        //        installWindowsDeps();
        //}
            
        GambitInstallation installation = new GambitInstallation();

        startShellExecuteHalting(installation.getPythonPath(), installation.getOSFPath());
        
        // Initialize the Python engine for our test.

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationQuit()
    {
        proc.Kill();
    }
}
