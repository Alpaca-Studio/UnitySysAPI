//---SYS_MASTER.cs v1.0.4--- Created by Alpaca Studio [ http://alpaca.studio ]//
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

namespace UnityEngine
{
    public class Sys : MonoBehaviour
    {
        //Logging Methods//
        public static string temp;
        public static List<string> logRaw = new List<string>();
        public static List<string> logData = new List<string>();
        public static Dictionary<string, string> messages = new Dictionary<string, string>();
        public static string lastPath;

        public static void addMessage(string code, string message)
        {
            messages.Add(code, message);
        }
		public static void Log (string message, bool showInConsole) {
			if(message != null){
				temp = message;
				if(showInConsole){Debug.Log(message);}
				Handle(temp);
			} else {
				Debug.LogError("[Sys API] ERROR003: Sys.Log(string, bool) has Invalid Arguments: (string) message cannot be null. "+Sys.GetErrorStackTrace());
			}
		}

        public static void Log(string message)
        {
            Log(message, false);
        }

        public static void LogCode(string messageCode)
        {
            Log(messages[messageCode], false);
        }

        public static void LogCode(string messageCode, bool showInConsole)
        {
            Log(messages[messageCode], showInConsole);
        }

        public static void LogCode(string messageCode, MonoBehaviour M)
        {
            LogCode(messageCode, M, false);
        }

        public static void LogCode(string messageCode, MonoBehaviour M, bool showInConsole)
        {
            Log(messages[messageCode].ToString() + " from " + M.ToString() + " ID# " + M.GetInstanceID(), showInConsole);
        }

        private static void Handle(string message)
        {
            string output = "[" + System.DateTime.Now + "]: " + message;
            logData.Add(output);
            logRaw.Add(message);
        }

        // Deletes all entries in the log file
        public static void ClearLog()
        {
            if (lastPath == null) { lastPath = Application.persistentDataPath + "/" + Application.productName + "/Logs/SysLog.txt"; }
            if (!Directory.Exists(System.IO.Path.GetDirectoryName(lastPath)))
            {
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(lastPath));
                File.Create(lastPath).Dispose();
            }
            else
            {
                if (!File.Exists(lastPath))
                {
                    File.Create(lastPath).Dispose();
                }
                else
                {
                    File.WriteAllLines(lastPath, new string[] { "" });
                }
            }
            logData.Clear();
            logRaw.Clear();
        }

        // Save the log to its location
        public static void SaveLog()
        {
            //logData.Clear();
            string path = Application.persistentDataPath + "/" + Application.productName + "/Logs/SysLog.txt";

            foreach (string message in logData)
            {
                File.AppendAllText(path, string.Format("{0}{1}", message.ToString(), System.Environment.NewLine));
            }
            lastPath = path;
            Debug.Log("SysLog.txt saved to default location: " + (Application.persistentDataPath + "/" + Application.productName + "/Logs/SysLog.txt"));
        }

        // Save the log to the given file path
        public static void SaveLog(string path)
        {
			if(path != null){
            //logData.Clear();
            foreach (string message in logData)
            {
                File.AppendAllText(path, string.Format("{0}{1}", message.ToString(), System.Environment.NewLine));
            }
            lastPath = path;
            Debug.Log("SysLog.txt saved to location: " + path);
			} else {
				Debug.LogError("[Sys API] ERROR004: Path is null or empty. (EC-SYS-"+GetLine()+")");
			}
        }

        // Save the log to the given file path, specifying whether or not to open the directory
        public static void SaveLog(string path, bool openDir)
        {
			if(path != null){
				//logData.Clear();
				foreach (string message in logData)
				{
					File.AppendAllText(path, string.Format("{0}{1}", message.ToString(), System.Environment.NewLine));
				}
				if (openDir) { System.Diagnostics.Process.Start(path); }
				lastPath = path;
				Debug.Log("SysLog.txt saved to location: " + path);
				Debug.Log("Opening Directory: " + path);
			} else {
				Debug.LogError("[Sys API] ERROR004: Path is null or empty. (EC-SYS-"+GetLine()+")");
			}
        }
	//DIRECTORY CHECKING//
		//Creates a new directory
		private static void CreateDirectory(string path){ 
			Directory.CreateDirectory(path);
			Log("[Sys API] Directory: "+path+" sucessfully created. "+GetStackTrace());
		}
		//Checks if a directory exists
		public static bool DirectoryCheck (string path){
			if(Directory.Exists (path)){return true;} 
			else { return false;} 
		}
		//Checks if a directory exists, and creates the directory if not.
		public static bool DirectoryCheck (string path, bool createDirectory){
				if(Directory.Exists (path)){
					return true;
				} else { 
					if (createDirectory){ 
						CreateDirectory(path);
						if(Directory.Exists(path)){
							Log("[Sys API] Directory: "+path+" sucessfully created. "+GetStackTrace(),true);
						}
					}
					return true;
				} 
		} 
		//Creates an empty file at a given path.
		private static void CreateEmptyFile(string path){ 
				File.Create(path).Dispose();
		}
			private static void CreateEmptyFile(string path, string file){ 
					File.Create(path+file).Dispose();
			}
		//Checks if a file already exists
		public static bool FileCheck (string path){
			if(File.Exists (path)){return true;} 
			else { return false;} 
		}
			public static bool FileCheck (string path, string file){
				if(File.Exists (path+file)){return true;} 
				else { return false;} 
			}
		//Checks if a file already exists, if not an empty file will be created.
		public static bool FileCheck (string path, bool createDispose){
				if(File.Exists (path)){
					return true;
				} else { 
					if (createDispose){ 
						CreateEmptyFile(path);
						if(File.Exists(path)){
							Log("[Sys API] File: "+Path.GetFileName(path)+" sucessfully created. "+GetStackTrace());
						}
						return true;
					} else {
						return false;
					}
				} 
		}
			public static bool FileCheck (string path, string file, bool createDispose){
				string newPath = path+file;
					if(File.Exists (newPath)){
						return true;
					} else { 
						if (createDispose){ 
							CreateEmptyFile(newPath);
							if(File.Exists(newPath)){
								Log("[Sys API] File: "+Path.GetFileName(newPath)+" sucessfully created. "+GetStackTrace(),true);
							}
							return true;
						} else {
							return false;
						}
					} 
			}
		//Returns the external storage location per platform.
		public static string DeviceExternalStorage(){
			string dir = "";
			#if UNITY_EDITOR
				dir = Application.persistentDataPath;
			#endif
			#if UNITY_ANDROID && !UNITY_EDITOR
				dir = "/storage/emulated/0";
			#endif
			#if UNITY_IOS && !UNITY_EDITOR
				dir = Application.persistentDataPath;
			#endif
			return dir;
		}

        // File Saving Methods//
        public static void SaveDataToFile(string path, string[] data)
        {
			if(path != null){
				if (!File.Exists(path))
				{
					File.Create(path).Dispose();
					foreach (string d in data)
					{
						File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
					}
				}
				else
				{
					File.Create(path).Dispose();
					foreach (string d in data)
					{
						File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
					}
				}
			} else {
				Debug.LogError("[Sys API] ERROR004: Path is null or empty. (EC-SYS-"+GetLine()+")");
			}
        }

        public static void SaveFile(string path, string[] data)
        {
			if(path != null){
				if (!File.Exists(path))
				{
					File.Create(path).Dispose();
					foreach (string d in data)
					{
						File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
					}
				}
				else
				{
					File.Create(path).Dispose();
					foreach (string d in data)
					{
						File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
					}
				}
			} else {
				Debug.LogError("[Sys API] ERROR004: Path is null or empty. (EC-SYS-"+GetLine()+")");
			}
        }
        // Save the given data to the given file, specifying whether to append or overwrite
        public static void SaveDataToFile(string path, string[] data, bool clearOldFiles)
        {
			if(path != null){
				if (!File.Exists(path))
				{
					File.Create(path).Dispose();
					foreach (string d in data)
					{
						File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
					}
				}
				else
				{
					if (clearOldFiles)
					{
						File.Create(path).Dispose();
						foreach (string d in data)
						{
							File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
						}
					}
					else
					{
						foreach (string d in data)
						{
							File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
						}
					}
				}
			} else {
				Debug.LogError("[Sys API] ERROR004: Path is null or empty. (EC-SYS-"+GetLine()+")");
			}
        }
		
        //'SaveDataToFile' Alternate
        public static void SaveFile(string path, string[] data, bool clearOldFiles)
        {
			if(path != null){
				if (!File.Exists(path))
				{
					File.Create(path).Dispose();
					foreach (string d in data)
					{
						File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
					}
				}
				else
				{
					if (clearOldFiles)
					{
						File.Create(path).Dispose();
						foreach (string d in data)
						{
							File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
						}
					}
					else
					{
						foreach (string d in data)
						{
							File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
						}
					}
				}
			} else {
				Debug.LogError("[Sys API] ERROR004: Path is null or empty. (EC-SYS-"+GetLine()+")");
			}
        }

        // Load data from a text file to a string array.
        public static string[] LoadDataFromFile(string path)
        {
			if(path != null){
				string[] data = File.ReadAllLines(path);
				return data;
			} else {
				Debug.LogError("[Sys API] ERROR004: Path is null or empty. (EC-SYS-"+GetLine()+")");
				return null;
			}
        }
		//'LoadDataFromFile' Alternate
        public static string[] ReadFile(string path)
        {
			if(path != null){
				string[] data = File.ReadAllLines(path);
				return data;
			} else {
				Debug.LogError("[Sys API] ERROR004: Path is null or empty. (EC-SYS-"+GetLine()+")");
				return null;
			}
        }
		// Load data from a text file to a string list.
        public static List<string> LoadDataListFromFile(string path)
        {
			if(path != null){
				List<string> data = new List<string>(File.ReadAllLines(path));
				return data;
			} else {
				Debug.LogError("[Sys API] ERROR004: Path is null or empty. (EC-SYS-"+GetLine()+")");
				return null;
			}
        }

        //Screen Capture Methods//
        public static void CaptureScreenshot(MonoBehaviour instance)
        {
            string path;
            path = (Application.persistentDataPath + "/" + Application.productName + "/Screenshots/" + System.DateTime.Now.ToString("MMddyyyy - hhmmss") + ".png");
            instance.StartCoroutine(GetTexture2D(path, false));
        }

        public static void CaptureScreenshot(MonoBehaviour instance, string path)
        {
            instance.StartCoroutine(GetTexture2D(path, false));
        }

        public static void CaptureScreenshot(MonoBehaviour instance, string path, bool openDir)
        {
            instance.StartCoroutine(GetTexture2D(path, openDir));
        }

        public static IEnumerator GetTexture2D(string path, bool openDir)
        {
            yield return new WaitForEndOfFrame();

            Texture2D tex = new Texture2D(Screen.width, Screen.height);
            tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            tex.Apply();
            HandleScreenshot(tex, path, openDir);
        }

        private static void HandleScreenshot(Texture2D tex, string path, bool openDir)
        {
            File.WriteAllBytes(path, tex.EncodeToPNG());
            if (openDir) { System.Diagnostics.Process.Start(path); }
        }

        public static Texture2D ScreenToTexture2D()
        {
            Texture2D tex = new Texture2D(Screen.width, Screen.height);
            tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            tex.Apply();
            return tex;
        }

        public static Texture2D LoadImageAtPath(string path)
        {
            Texture2D tex;
            byte[] bytes;
            bytes = File.ReadAllBytes(path);
            tex = new Texture2D(1, 1);
            tex.LoadImage(bytes);
            return tex;
        }

        //Simple Math Methods//
        public static int Add(params int[] a)
        {
            int sum = 0;

            for (int k = 0; k < a.Length; k++)
            {
                sum += a[k];
            }
            return sum;
        }

        public static float Add(params float[] a)
        {
            float sum = 0;

            for (int k = 0; k < a.Length; k++)
            {
                sum += a[k];
            }
            return sum;
        }

        public static int Subtract(params int[] a)
        {
            int sum = a.Length > 0 ? a[0] : 0;

            for (int k = 1; k < a.Length; k++)
            {
                sum -= a[k];
            }
            return sum;
        }

        public static float Subtract(params float[] a)
        {
            float sum = a.Length > 0 ? a[0] : 0;

            for (int k = 1; k < a.Length; k++)
            {
                sum -= a[k];
            }
            return sum;
        }

        public static int Multiply(params int[] a)
        {
            int product = 1;

            for (int k = 0; k < a.Length; k++)
            {
                product *= a[k];
            }
            return product;
        }

        public static float Multiply(params float[] a)
        {
            float product = 1;

            for (int k = 0; k < a.Length; k++)
            {
                product *= a[k];
            }
            return product;
        }

        public static float Divide(float a, float b)
        {
            float sum = (a / b);
            return sum;
        }

        private static int get(int[] d, int index)
        {
            return index >= 0 && index < d.Length ? d[index] : 0;
        }

        public static int[] convolve(int[] a, int[] b)
        {
            int[] c = new int[] { a.Length > b.Length ? a.Length : b.Length };

            for (int x = 0; x < c.Length; x++)
            {
                int n = 0;

                for (int k = 0; k <= x; k++)
                {
                    n += get(a, k) * get(b, x - k);
                }
                c[x] = n;
            }
            return c;
        }

        public static int[] cProduct(int[] a, int[] b)
        {
            int[] c = new int[] { a.Length > b.Length ? a.Length : b.Length };

            for (int x = 0; x < c.Length; x++)
            {
                int n = 0;

                for (int k = 0; k <= x; k++)
                {
                    n += get(a, k) * get(b, x - k);
                }
                c[x] = n;
            }
            return c;
        }

        //System Information Methods//
        static string temporaryPath;
        static int itemCounter;
		
        public static float batteryLevel() { return SystemInfo.batteryLevel; }
        public static BatteryStatus batteryStatus() { return SystemInfo.batteryStatus; }
        public static Rendering.CopyTextureSupport copyTextureSupport() { return SystemInfo.copyTextureSupport; }
        public static string deviceModel() { return SystemInfo.deviceModel; }
        public static string deviceName() { return SystemInfo.deviceName; }
        public static DeviceType deviceType() { return SystemInfo.deviceType; }
        public static string deviceUniqueIdentifier() { return SystemInfo.deviceUniqueIdentifier; }
        public static int graphicsDeviceID() { return SystemInfo.graphicsDeviceID; }
        public static string graphicsDeviceName() { return SystemInfo.graphicsDeviceName; }
        public static Rendering.GraphicsDeviceType graphicsDeviceType() { return SystemInfo.graphicsDeviceType; }
        public static string graphicsDeviceVendor() { return SystemInfo.graphicsDeviceVendor; }
        public static int graphicsDeviceVendorID() { return SystemInfo.graphicsDeviceVendorID; }
        public static string graphicsDeviceVersion() { return SystemInfo.graphicsDeviceVersion; }
        public static int graphicsMemorySize() { return SystemInfo.graphicsMemorySize; }
        public static bool graphicsMultiThreaded() { return SystemInfo.graphicsMultiThreaded; }
        public static int graphicsShaderLevel() { return SystemInfo.graphicsShaderLevel; }
        public static bool graphicsUVStartsAtTop() { return SystemInfo.graphicsUVStartsAtTop; }
        public static int maxCubemapSize() { return SystemInfo.maxCubemapSize; }
        public static int maxTextureSize() { return SystemInfo.maxTextureSize; }
        public static NPOTSupport npotSupport() { return SystemInfo.npotSupport; }
        public static string operatingSystem() { return SystemInfo.operatingSystem; }
        public static OperatingSystemFamily operatingSystemFamily() { return SystemInfo.operatingSystemFamily; }
        public static int processorCount() { return SystemInfo.processorCount; }
        public static int processorFrequency() { return SystemInfo.processorFrequency; }
        public static string processorType() { return SystemInfo.processorType; }
        public static int supportedRenderTargetCount() { return SystemInfo.supportedRenderTargetCount; }
        public static bool supports2DArrayTextures() { return SystemInfo.supports2DArrayTextures; }
        public static bool supports3DRenderTextures() { return SystemInfo.supports3DRenderTextures; }
        public static bool supports3DTextures() { return SystemInfo.supports3DTextures; }
        public static bool supportsAccelerometer() { return SystemInfo.supportsAccelerometer; }
        public static bool supportsAudio() { return SystemInfo.supportsAudio; }
        public static bool supportsComputeShaders() { return SystemInfo.supportsComputeShaders; }
        public static bool supportsCubemapArrayTextures() { return SystemInfo.supportsCubemapArrayTextures; }
        public static bool supportsGyroscope() { return SystemInfo.supportsGyroscope; }
        public static bool supportsImageEffects() { return SystemInfo.supportsImageEffects; }
        public static bool supportsInstancing() { return SystemInfo.supportsInstancing; }
        public static bool supportsLocationService() { return SystemInfo.supportsLocationService; }
        public static bool supportsMotionVectors() { return SystemInfo.supportsMotionVectors; }
        public static bool supportsRawShadowDepthSampling() { return SystemInfo.supportsRawShadowDepthSampling; }
        public static bool supportsRenderToCubemap() { return SystemInfo.supportsRenderToCubemap; }
        public static bool supportsShadows() { return SystemInfo.supportsShadows; }
        public static bool supportsSparseTextures() { return SystemInfo.supportsSparseTextures; }
        public static bool supportsVibration() { return SystemInfo.supportsVibration; }
        public static int systemMemorySize() { return SystemInfo.systemMemorySize; }
        public static string unsupportedIdentifier() { return SystemInfo.unsupportedIdentifier; }
        public static bool usesReversedZBuffer() { return SystemInfo.usesReversedZBuffer; }

        public static void SaveSystemInfo(string path)
        {
           SaveSystemInfo(path, false);
        }

        public static void SaveSystemInfo(string path, bool openDir)
        {
			if(path != null){
				if (!File.Exists(path))
				{
					File.Create(path).Dispose();
					temporaryPath = path;
				}
				else
				{
					temporaryPath = path;
				}
				itemCounter = 0;
				WriteDataToFile(GetSystemInfo(), openDir);
			} else {
				Debug.LogError("[Sys API] ERROR004: Path is null or empty. (EC-SYS-"+GetLine()+")");
			}
        }

        static void WriteDataToFile(List<string> sysInfo, bool openDir)
        {
            foreach (string ab in sysInfo)
            {
                File.AppendAllText(temporaryPath, string.Format("{0} {1}", ab, System.Environment.NewLine));
                itemCounter++;
            }
            if (itemCounter >= (sysInfo.ToArray().Length)) { itemCounter = 0; sysInfo.Clear(); }
            if (openDir) { System.Diagnostics.Process.Start(temporaryPath); } //Debug.Log("[Sys]: Opening File Location...");}	
        }
		

        public static List<string> GetSystemInfo()
        {
            List<string> sysInfo = new List<string>();
            sysInfo.Add("Battery Level: " + (SystemInfo.batteryLevel * 100) + "%");
            sysInfo.Add("Battery Status: " + SystemInfo.batteryStatus.ToString());
            sysInfo.Add("Copy Texture Support: " + SystemInfo.copyTextureSupport.ToString());
            sysInfo.Add("Device Model: " + SystemInfo.deviceModel);
            sysInfo.Add("Device Name: " + SystemInfo.deviceName);
            sysInfo.Add("Device Type: " + SystemInfo.deviceType.ToString());
            sysInfo.Add("Unique Device ID: " + SystemInfo.deviceUniqueIdentifier);
            sysInfo.Add("Graphics Device ID: " + SystemInfo.graphicsDeviceID);
            sysInfo.Add("Graphics Device Name: " + SystemInfo.graphicsDeviceName);
            sysInfo.Add("Graphics Device Type: " + SystemInfo.graphicsDeviceType.ToString());
            sysInfo.Add("Graphics Device Vendor: " + SystemInfo.graphicsDeviceVendor);
            sysInfo.Add("Graphics Device VendorID: " + SystemInfo.graphicsDeviceVendorID);
            sysInfo.Add("Graphics Device Version: " + SystemInfo.graphicsDeviceVersion);
            sysInfo.Add("Graphics Memory Size: " + SystemInfo.graphicsMemorySize);//
            sysInfo.Add("Graphics Multithreaded: " + SystemInfo.graphicsMultiThreaded);
            sysInfo.Add("Graphics Shader Level: " + SystemInfo.graphicsShaderLevel);
            sysInfo.Add("Graphics UV Starts at Top?: " + SystemInfo.graphicsUVStartsAtTop);
            sysInfo.Add("Max Cubemap Size: " + SystemInfo.maxCubemapSize);
            sysInfo.Add("Max Texture Size: " + SystemInfo.maxTextureSize);
            sysInfo.Add("NPOT Support: " + SystemInfo.npotSupport.ToString());
            sysInfo.Add("OS: " + SystemInfo.operatingSystem);
            sysInfo.Add("OS Family: " + SystemInfo.operatingSystemFamily.ToString());
            sysInfo.Add("Processor Count: " + SystemInfo.processorCount);
            sysInfo.Add("Processor Frequency: " + (SystemInfo.processorFrequency * 0.001) + "MHz");
            sysInfo.Add("Processor Type: " + SystemInfo.processorType);
            sysInfo.Add("Supported Render Targer Count: " + SystemInfo.supportedRenderTargetCount);
            sysInfo.Add("Supports 2D Array Textures : " + (SystemInfo.supports2DArrayTextures ? "Yes" : "No"));
            sysInfo.Add("Supports 3D Render Textures : " + (SystemInfo.supports3DRenderTextures ? "Yes" : "No"));
            sysInfo.Add("Supports 3D Textures : " + (SystemInfo.supports3DTextures ? "Yes" : "No"));
            sysInfo.Add("Supports Accelerometer : " + (SystemInfo.supportsAccelerometer ? "Yes" : "No"));
            sysInfo.Add("Supports Audio : " + (SystemInfo.supportsAudio ? "Yes" : "No"));
            sysInfo.Add("Supports Compute Shaders : " + (SystemInfo.supportsComputeShaders ? "Yes" : "No"));
            sysInfo.Add("Supports Cubemap Array Textures : " + (SystemInfo.supportsCubemapArrayTextures ? "Yes" : "No"));
            sysInfo.Add("Supports Gyroscope : " + (SystemInfo.supportsGyroscope ? "Yes" : "No"));
            sysInfo.Add("Supports Image Effects : " + (SystemInfo.supportsImageEffects ? "Yes" : "No"));
            sysInfo.Add("Supports Instancing : " + (SystemInfo.supportsInstancing ? "Yes" : "No"));
            sysInfo.Add("Supports Location Services : " + (SystemInfo.supportsLocationService ? "Yes" : "No"));
            sysInfo.Add("Supports Motion Vectors : " + (SystemInfo.supportsMotionVectors ? "Yes" : "No"));
            sysInfo.Add("Supports Raw Shadow Depth Sampling : " + (SystemInfo.supportsRawShadowDepthSampling ? "Yes" : "No"));
            sysInfo.Add("Supports Render To Cubemap : " + (SystemInfo.supportsRenderToCubemap ? "Yes" : "No"));
            sysInfo.Add("Supports Shadows : " + (SystemInfo.supportsShadows ? "Yes" : "No"));
            sysInfo.Add("Supports Sparse Textures : " + (SystemInfo.supportsSparseTextures ? "Yes" : "No"));
            sysInfo.Add("Supports Vibration : " + (SystemInfo.supportsVibration ? "Yes" : "No"));
            sysInfo.Add("System Memory Size: " + SystemInfo.systemMemorySize);
            sysInfo.Add("Unsupported Identifier: " + SystemInfo.unsupportedIdentifier);
            sysInfo.Add("Supports Reversed Z Buffer : " + (SystemInfo.usesReversedZBuffer ? "Yes" : "No"));

            return sysInfo;
        }

        //Miscellanous Operators//
        public static string GenerateUniqueID(int length)
        {
            int a = 0;
            string hc = "";

            while (a < length)
            {
                float r = Random.Range(0.0f, 2);
                Debug.Log(r);
                if (r <= 0.999f)
                {
                    int v = GetValue();
                    hc += v.ToString();
                }
                if (r >= 1)
                {
                    string c = GetCharacter();
                    hc += c;
                }
                a++;
            }

            return hc;
        }

        private static int GetValue()
        {
            int val = Random.Range(0, 9);
            return val;
        }

        private static string GetCharacter()
        {
            string[] alp = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            string returnChar = alp[Random.Range(0, alp.Length)];
            return returnChar;
        }
		
		public static int GetLine(){
			System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(0,true);
			System.Diagnostics.StackFrame stackFrame = stackTrace.GetFrame(stackTrace.FrameCount -1);
			int line = stackFrame.GetFileLineNumber(); 
			return line;
		}
		
		public static string GetErrorStackTrace(){
			System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(0,true);
			System.Diagnostics.StackFrame stackFrame = stackTrace.GetFrame(stackTrace.FrameCount -1);
			string file = stackFrame.GetFileName();
			//string method = stackFrame.GetMethod().ToString();
			int line = stackFrame.GetFileLineNumber();
			string trace = string.Format("({0}-{1}-{2})","EC",Path.GetFileName(file),line);			
			return trace;
		}
		public static string GetStackTrace(){
			System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(0,true);
			System.Diagnostics.StackFrame stackFrame = stackTrace.GetFrame(stackTrace.FrameCount -1);
			string file = stackFrame.GetFileName();
			string method = stackFrame.GetMethod().ToString();
			int line = stackFrame.GetFileLineNumber();
			string trace = string.Format("{0}({1}:{2})",method,Path.GetFileName(file),line);			
			return trace;
		}
		public static string FormatStackTrace(string format){
			System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(0,true);
			System.Diagnostics.StackFrame stackFrame = stackTrace.GetFrame(stackTrace.FrameCount -1);
			string file = stackFrame.GetFileName();
			string method = stackFrame.GetMethod().ToString();
			int line = stackFrame.GetFileLineNumber();
			string trace = string.Format(format,method,Path.GetFileName(file),line);			
			return trace;
		}
		
    }
}
