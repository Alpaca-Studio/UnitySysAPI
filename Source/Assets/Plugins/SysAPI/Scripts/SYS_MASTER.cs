//---SYS_MASTER.cs v1.0.3--- (c)2017 mcproj. Not for redistribution.///
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

namespace UnityEngine {
	public class Sys : MonoBehaviour {
	
	//Logging Methods//
		public static string temp;
		public static List<string> logRaw = new List<string>();
		public static List<string> logData = new List<string>();
		public static string lastPath;
		
		public static void Log (string message, bool showInConsole) {
			temp = message;
			if(showInConsole){Debug.Log(message);}
			Handle(temp);
		}
		
		public static void Log (string message) {
			temp = message;
			Handle(temp);
		}
		
		private static void Handle (string message) {
			string output = "[" + System.DateTime.Now + "]: " + message;
			logData.Add(output);
			logRaw.Add(message);
		}
		
		public static void ClearLog() {
			if(lastPath == null){lastPath = Application.persistentDataPath + "/" + Application.productName + "/Logs/SysLog.txt";}
			if(!Directory.Exists(System.IO.Path.GetDirectoryName(lastPath))){
				Directory.CreateDirectory(System.IO.Path.GetDirectoryName(lastPath));
				File.Create(lastPath).Dispose();
			} else {
				if(!File.Exists(lastPath)){
					File.Create(lastPath).Dispose();
				} else {
					File.WriteAllLines(lastPath, new string[]{""});
				}
			}
			logData.Clear();
			logRaw.Clear();
		}
		
		public static void SaveLog(){
			//logData.Clear();
			string path = Application.persistentDataPath + "/" + Application.productName + "/Logs/SysLog.txt";
				foreach(string message in logData){
					File.AppendAllText(path, string.Format("{0}{1}", message.ToString(), System.Environment.NewLine));
				}
					lastPath = path;
				Debug.Log("SysLog.txt saved to default location: " + (Application.persistentDataPath + "/" + Application.productName + "/Logs/SysLog.txt"));
		}
		
		public static void SaveLog(string path){
			//logData.Clear();
				foreach(string message in logData){
					File.AppendAllText(path, string.Format("{0}{1}", message.ToString(), System.Environment.NewLine));
				}
					lastPath = path;
				Debug.Log("SysLog.txt saved to location: " + path);
		}
		
		public static void SaveLog(string path, bool openDir){
			//logData.Clear();
				foreach(string message in logData){
					File.AppendAllText(path, string.Format("{0}{1}", message.ToString(), System.Environment.NewLine));
				}
					if(openDir){System.Diagnostics.Process.Start(path);}
						lastPath = path;
				Debug.Log("SysLog.txt saved to location: " + path);
				Debug.Log("Opening Directory: " + path);
		}
		
		
	// File Saving Methods//
		public static void SaveDataToFile(string path, string[] data){
			if(!File.Exists(path)){
				File.Create(path).Dispose();
				foreach(string d in data){
					File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
				}
			} else {
				File.Create(path).Dispose();
				foreach(string d in data){
					File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
				}
			}
			
		}
		
		public static void SaveFile(string path, string[] data){
			if(!File.Exists(path)){
				File.Create(path).Dispose();
				foreach(string d in data){
					File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
				}
			} else {
				File.Create(path).Dispose();
				foreach(string d in data){
					File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
				}
			}
			
		}
		
		public static void SaveDataToFile(string path, string[] data, bool clearOldFiles){
			if(!File.Exists(path)){
				File.Create(path).Dispose();
				foreach(string d in data){
					File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
				}
			} else {
				if(clearOldFiles){
					File.Create(path).Dispose();
					foreach(string d in data){
						File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
					}
				} else {
					foreach(string d in data){
						File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
					}
				}
			}
			
		}
		
		public static void SaveFile(string path, string[] data, bool clearOldFiles){
			if(!File.Exists(path)){
				File.Create(path).Dispose();
				foreach(string d in data){
					File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
				}
			} else {
				if(clearOldFiles){
					File.Create(path).Dispose();
					foreach(string d in data){
						File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
					}
				} else {
					foreach(string d in data){
						File.AppendAllText(path, string.Format("{0}{1}", d.ToString(), System.Environment.NewLine));
					}
				}
			}
			
		}
		
		public static string[] LoadDataFromFile (string path) {
			string[] data = File.ReadAllLines(path);
			return data;
		}
		
		public static string[] ReadFile (string path) {
			string[] data = File.ReadAllLines(path);
			return data;
		}
		
		public static List<string> LoadDataListFromFile (string path) {
			List<string> data = new List<string>(File.ReadAllLines(path));
			return data;
		}
		
		
	//Screen Capture Methods//
		public static void CaptureScreenshot (MonoBehaviour instance) {
			string path;
			path = (Application.persistentDataPath + "/" + Application.productName+ "/Screenshots/" + System.DateTime.Now.ToString("MMddyyyy - hhmmss") + ".png");
			instance.StartCoroutine(GetTexture2D(path, false));
		}
		
		public static void CaptureScreenshot (MonoBehaviour instance, string path) {
			instance.StartCoroutine(GetTexture2D(path, false));
		}
		
		public static void CaptureScreenshot (MonoBehaviour instance, string path, bool openDir) {
			instance.StartCoroutine(GetTexture2D(path, openDir));
		}
		
		public static IEnumerator GetTexture2D (string path, bool openDir) {
			yield return new WaitForEndOfFrame();
			Texture2D tex = new Texture2D(Screen.width, Screen.height);
			tex.ReadPixels(new Rect(0,0,Screen.width,Screen.height),0,0);
			tex.Apply();
			HandleScreenshot(tex, path, openDir);
		}
		
		private static void HandleScreenshot (Texture2D tex, string path, bool openDir){
			File.WriteAllBytes(path, tex.EncodeToPNG());
			if(openDir){System.Diagnostics.Process.Start(path);}
			/*byte[] bytes;
			bytes = tex.EncodeToPNG();
			FileStream fileStream;
			fileStream = new FileStream(path, FileMode.Create);
			BinaryWriter bin;
			bin = new BinaryWriter(fileStream);
			bin.Write(bytes);
			fileStream.Close();*/
		}

		public static Texture2D ScreenToTexture2D () {
			Texture2D tex = new Texture2D(Screen.width, Screen.height);
			tex.ReadPixels(new Rect(0,0,Screen.width,Screen.height),0,0);
			tex.Apply();
			return tex;
		}
		public static Texture2D LoadImageAtPath(string path){
			Texture2D tex;
			byte[] bytes;
			bytes = File.ReadAllBytes(path);
			tex = new Texture2D(1,1);
			tex.LoadImage(bytes);
			return tex;
		}
		
	//Simple Math Methods//
		public static int Add(int a, int b){
			int sum = (a + b);
			return sum;
		}
		public static int Add(int a, int b, int c){
			int sum = (a + b + c);
			return sum;
		}
		public static int Add(int a, int b, int c, int d){
			int sum = (a + b + c + d);
			return sum;
		}
		
		public static float Add(float a, float b){
			float sum = (a + b);
			return sum;
		}
		public static float Add(float a, float b, float c){
			float sum = (a + b + c);
			return sum;
		}
		public static float Add(float a, float b, float c, float d){
			float sum = (a + b + c + d);
			return sum;
		}
		
		public static int Subtract(int a, int b){
			int sum = (a - b);
			return sum;
		}
		public static int Subtract(int a, int b, int c){
			int sum = (a - b - c);
			return sum;
		}
		public static int Subtract(int a, int b, int c, int d){
			int sum = (a - b - c - d);
			return sum;
		}
		
		public static float Subtract(float a, float b){
			float sum = (a - b);
			return sum;
		}
		public static float Subtract(float a, float b, float c){
			float sum = (a - b - c);
			return sum;
		}
		public static float Subtract(float a, float b, float c, float d){
			float sum = (a - b - c - d);
			return sum;
		}
		
		public static int Multiply(int a, int b){
			int sum = (a * b);
			return sum;
		}
		public static int Multiply(int a, int b, int c){
			int sum = (a * b * c);
			return sum;
		}
		
		public static float Multiply(float a, float b){
			float sum = (a * b);
			return sum;
		}
		public static float Multiply(float a, float b, float c){
			float sum = (a * b * c);
			return sum;
		}
		
		public static float Divide(float a, float b){
			float sum = (a / b);
			return sum;
		}
		
		private static int get(int[] d, int index){
			return index >= 0 && index < d.Length ? d[index] : 0; 
		}

		public static int[] convolve(int[] a, int[] b){
			int[] c = new int[]{a.Length > b.Length ? a.Length : b.Length};
			for(int x = 0; x < c.Length; x++){
				int n = 0;
				for(int k = 0; k <= x; k++){
					n += get(a, k) * get(b, x-k);
				}
				c[x] = n;
			}
		 return c;
		}
		
		public static int[] cProduct(int[] a, int[] b){
			int[] c = new int[]{a.Length > b.Length ? a.Length : b.Length};
			for(int x = 0; x < c.Length; x++){
				int n = 0;
				for(int k = 0; k <= x; k++){
					n += get(a, k) * get(b, x-k);
				}
				c[x] = n;
			}
		 return c;
		}

	//System Information Methods//
		public static List<string> sysInfo = new List<string>();
		static string temporaryPath;
		static int i;
		public static float batteryLevel(){return SystemInfo.batteryLevel;}
		public static BatteryStatus batteryStatus(){return SystemInfo.batteryStatus;}
		public static Rendering.CopyTextureSupport copyTextureSupport(){return SystemInfo.copyTextureSupport;}
		public static string deviceModel(){return SystemInfo.deviceModel;}
		public static string deviceName(){return SystemInfo.deviceName;}
		public static DeviceType deviceType(){return SystemInfo.deviceType;}
		public static string deviceUniqueIdentifier(){return SystemInfo.deviceUniqueIdentifier;}
		public static int graphicsDeviceID(){return SystemInfo.graphicsDeviceID;}
		public static string graphicsDeviceName(){return SystemInfo.graphicsDeviceName;}
		public static Rendering.GraphicsDeviceType graphicsDeviceType(){return SystemInfo.graphicsDeviceType;}
		public static string graphicsDeviceVendor(){return SystemInfo.graphicsDeviceVendor;}
		public static int graphicsDeviceVendorID(){return SystemInfo.graphicsDeviceVendorID;}
		public static string graphicsDeviceVersion(){return SystemInfo.graphicsDeviceVersion;}
		public static int graphicsMemorySize(){return SystemInfo.graphicsMemorySize;}
		public static bool graphicsMultiThreaded(){return SystemInfo.graphicsMultiThreaded;}
		public static int graphicsShaderLevel(){return SystemInfo.graphicsShaderLevel;}
		public static bool graphicsUVStartsAtTop(){return SystemInfo.graphicsUVStartsAtTop;}
		public static int maxCubemapSize(){return SystemInfo.maxCubemapSize;}
		public static int maxTextureSize(){return SystemInfo.maxTextureSize;}
		public static NPOTSupport npotSupport(){return SystemInfo.npotSupport;}
		public static string operatingSystem(){return SystemInfo.operatingSystem;}
		public static OperatingSystemFamily operatingSystemFamily(){return SystemInfo.operatingSystemFamily;}
		public static int processorCount(){return SystemInfo.processorCount;}
		public static int processorFrequency(){return SystemInfo.processorFrequency;}
		public static string processorType(){return SystemInfo.processorType;}
		public static int supportedRenderTargetCount(){return SystemInfo.supportedRenderTargetCount;}
		public static bool supports2DArrayTextures(){return SystemInfo.supports2DArrayTextures;}
		public static bool supports3DRenderTextures(){return SystemInfo.supports3DRenderTextures;}
		public static bool supports3DTextures(){return SystemInfo.supports3DTextures;}
		public static bool supportsAccelerometer(){return SystemInfo.supportsAccelerometer;}
		public static bool supportsAudio(){return SystemInfo.supportsAudio;}
		public static bool supportsComputeShaders(){return SystemInfo.supportsComputeShaders;}
		public static bool supportsCubemapArrayTextures(){return SystemInfo.supportsCubemapArrayTextures;}
		public static bool supportsGyroscope(){return SystemInfo.supportsGyroscope;}
		public static bool supportsImageEffects(){return SystemInfo.supportsImageEffects;}
		public static bool supportsInstancing(){return SystemInfo.supportsInstancing;}
		public static bool supportsLocationService(){return SystemInfo.supportsLocationService;}
		public static bool supportsMotionVectors(){return SystemInfo.supportsMotionVectors;}
		public static bool supportsRawShadowDepthSampling(){return SystemInfo.supportsRawShadowDepthSampling;}
		public static bool supportsRenderToCubemap(){return SystemInfo.supportsRenderToCubemap;}
		public static bool supportsShadows(){return SystemInfo.supportsShadows;}
		public static bool supportsSparseTextures(){return SystemInfo.supportsSparseTextures;}
		public static bool supportsVibration(){return SystemInfo.supportsVibration;}
		public static int systemMemorySize(){return SystemInfo.systemMemorySize;}
		public static string unsupportedIdentifier(){return SystemInfo.unsupportedIdentifier;}
		public static bool usesReversedZBuffer(){return SystemInfo.usesReversedZBuffer;}
		

		public static void SaveSystemInfo (string path) {
			if(!File.Exists(path)){
				File.Create(path).Dispose();
				temporaryPath = path;
			} else {
				temporaryPath = path;
			}
			i = 0;
				sysInfo.Add("--- SystemInfo --- ");
				sysInfo.Add("Battery Level: " + (SystemInfo.batteryLevel *100) + "%");
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
				sysInfo.Add("Processor Frequency: " + SystemInfo.processorFrequency + "MHz");
				sysInfo.Add("Processor Type: " + SystemInfo.processorType);
				sysInfo.Add("Supported Render Targer Count: " + SystemInfo.supportedRenderTargetCount);
				if(SystemInfo.supports2DArrayTextures){sysInfo.Add("Supports 2D Array Textures : Yes");} else { sysInfo.Add("Supports 2D Array Textures : No");}
				if(SystemInfo.supports3DRenderTextures){sysInfo.Add("Supports 3D Render Textures : Yes");} else { sysInfo.Add("Supports 3D Render Textures : No");}
				if(SystemInfo.supports3DTextures){sysInfo.Add("Supports 3D Textures : Yes");} else { sysInfo.Add("Supports 3D Textures : No");}
				if(SystemInfo.supportsAccelerometer){sysInfo.Add("Supports Accelerometer : Yes");} else { sysInfo.Add("Supports Accelerometer : No");}
				if(SystemInfo.supportsAudio){sysInfo.Add("Supports Audio : Yes");} else { sysInfo.Add("Supports Audio : No");}
				if(SystemInfo.supportsComputeShaders){sysInfo.Add("Supports Compute Shaders : Yes");} else { sysInfo.Add("Supports Compute Shaders : No");}
				if(SystemInfo.supportsCubemapArrayTextures){sysInfo.Add("Supports Cubemap Array Textures : Yes");} else { sysInfo.Add("Supports Cubemap Array Textures : No");}
				if(SystemInfo.supportsGyroscope){sysInfo.Add("Supports Gyroscope : Yes");} else { sysInfo.Add("Supports Gyroscope : No");}
				if(SystemInfo.supportsImageEffects){sysInfo.Add("Supports Image Effects : Yes");} else { sysInfo.Add("Supports Image Effects : No");}
				if(SystemInfo.supportsInstancing){sysInfo.Add("Supports Instancing : Yes");} else { sysInfo.Add("Supports Instancing : No");}
				if(SystemInfo.supportsLocationService){sysInfo.Add("Supports Location Services : Yes");} else { sysInfo.Add("Supports Location Services : No");}
				if(SystemInfo.supportsMotionVectors){sysInfo.Add("Supports Motion Vectors : Yes");} else { sysInfo.Add("Supports Motion Vectors : No");}
				if(SystemInfo.supportsRawShadowDepthSampling){sysInfo.Add("Supports Raw Shadow Depth Sampling : Yes");} else { sysInfo.Add("Supports Raw Shadow Depth Sampling : No");}
				if(SystemInfo.supportsRenderToCubemap){sysInfo.Add("Supports Render To Cubemap : Yes");} else { sysInfo.Add("Supports Render To Cubemap : No");}
				if(SystemInfo.supportsShadows){sysInfo.Add("Supports Shadows : Yes");} else { sysInfo.Add("Supports Shadows : No");}
				if(SystemInfo.supportsSparseTextures){sysInfo.Add("Supports Sparse Textures : Yes");} else { sysInfo.Add("Supports Sparse Textures : No");}
				if(SystemInfo.supportsVibration){sysInfo.Add("Supports Vibration : Yes");} else { sysInfo.Add("Supports Vibration : No");}
				sysInfo.Add("System Memory Size: " + SystemInfo.systemMemorySize);
				sysInfo.Add("Unsupported Identifier: " + SystemInfo.unsupportedIdentifier);
				if(SystemInfo.usesReversedZBuffer){sysInfo.Add("Uses Reversed Z Buffer : Yes");} else { sysInfo.Add("Uses Reversed Z Buffer : No");}
				sysInfo.Add(string.Format("{0} {1}", "", System.Environment.NewLine));
				sysInfo.Add("--- " + System.DateTime.Now.ToString()+" ---" );
				WriteDataToFile(false);
		}
		
		public static void SaveSystemInfo (string path, bool openDir) {
			if(!File.Exists(path)){
				File.Create(path).Dispose();
				temporaryPath = path;
			} else {
				temporaryPath = path;
			}
			i = 0;
				sysInfo.Add("--- SystemInfo --- ");
				sysInfo.Add("Battery Level: " + (SystemInfo.batteryLevel *100) + "%");
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
				sysInfo.Add("Processor Frequency: " + SystemInfo.processorFrequency + "MHz");
				sysInfo.Add("Processor Type: " + SystemInfo.processorType);
				sysInfo.Add("Supported Render Targer Count: " + SystemInfo.supportedRenderTargetCount);
				if(SystemInfo.supports2DArrayTextures){sysInfo.Add("Supports 2D Array Textures : Yes");} else { sysInfo.Add("Supports 2D Array Textures : No");}
				if(SystemInfo.supports3DRenderTextures){sysInfo.Add("Supports 3D Render Textures : Yes");} else { sysInfo.Add("Supports 3D Render Textures : No");}
				if(SystemInfo.supports3DTextures){sysInfo.Add("Supports 3D Textures : Yes");} else { sysInfo.Add("Supports 3D Textures : No");}
				if(SystemInfo.supportsAccelerometer){sysInfo.Add("Supports Accelerometer : Yes");} else { sysInfo.Add("Supports Accelerometer : No");}
				if(SystemInfo.supportsAudio){sysInfo.Add("Supports Audio : Yes");} else { sysInfo.Add("Supports Audio : No");}
				if(SystemInfo.supportsComputeShaders){sysInfo.Add("Supports Compute Shaders : Yes");} else { sysInfo.Add("Supports Compute Shaders : No");}
				if(SystemInfo.supportsCubemapArrayTextures){sysInfo.Add("Supports Cubemap Array Textures : Yes");} else { sysInfo.Add("Supports Cubemap Array Textures : No");}
				if(SystemInfo.supportsGyroscope){sysInfo.Add("Supports Gyroscope : Yes");} else { sysInfo.Add("Supports Gyroscope : No");}
				if(SystemInfo.supportsImageEffects){sysInfo.Add("Supports Image Effects : Yes");} else { sysInfo.Add("Supports Image Effects : No");}
				if(SystemInfo.supportsInstancing){sysInfo.Add("Supports Instancing : Yes");} else { sysInfo.Add("Supports Instancing : No");}
				if(SystemInfo.supportsLocationService){sysInfo.Add("Supports Location Services : Yes");} else { sysInfo.Add("Supports Location Services : No");}
				if(SystemInfo.supportsMotionVectors){sysInfo.Add("Supports Motion Vectors : Yes");} else { sysInfo.Add("Supports Motion Vectors : No");}
				if(SystemInfo.supportsRawShadowDepthSampling){sysInfo.Add("Supports Raw Shadow Depth Sampling : Yes");} else { sysInfo.Add("Supports Raw Shadow Depth Sampling : No");}
				if(SystemInfo.supportsRenderToCubemap){sysInfo.Add("Supports Render To Cubemap : Yes");} else { sysInfo.Add("Supports Render To Cubemap : No");}
				if(SystemInfo.supportsShadows){sysInfo.Add("Supports Shadows : Yes");} else { sysInfo.Add("Supports Shadows : No");}
				if(SystemInfo.supportsSparseTextures){sysInfo.Add("Supports Sparse Textures : Yes");} else { sysInfo.Add("Supports Sparse Textures : No");}
				if(SystemInfo.supportsVibration){sysInfo.Add("Supports Vibration : Yes");} else { sysInfo.Add("Supports Vibration : No");}
				sysInfo.Add("System Memory Size: " + SystemInfo.systemMemorySize);
				sysInfo.Add("Unsupported Identifier: " + SystemInfo.unsupportedIdentifier);
				if(SystemInfo.usesReversedZBuffer){sysInfo.Add("Uses Reversed Z Buffer : Yes");} else { sysInfo.Add("Uses Reversed Z Buffer : No");}
				sysInfo.Add("--- " + System.DateTime.Now.ToString()+" ---" );
				sysInfo.Add(string.Format("{0} {1}", "", System.Environment.NewLine));
				WriteDataToFile(openDir);
		}
		
		static void WriteDataToFile (bool openDir) {
			foreach(string ab in sysInfo){
					File.AppendAllText(temporaryPath, string.Format("{0} {1}", ab, System.Environment.NewLine));
					i++;
				}
			if(i >= (sysInfo.ToArray().Length)){ i = 0; sysInfo.Clear(); }
			if(openDir){System.Diagnostics.Process.Start(temporaryPath);} //Debug.Log("[Sys]: Opening File Location...");}	
		}
		
		public static List<string> GetSystemInfo () {
			List<string> newSysInfo = new List<string>();
				newSysInfo.Add("Battery Level: " + (SystemInfo.batteryLevel *100) + "%");
				newSysInfo.Add("Battery Status: " + SystemInfo.batteryStatus.ToString());
				newSysInfo.Add("Copy Texture Support: " + SystemInfo.copyTextureSupport.ToString());
				newSysInfo.Add("Device Model: " + SystemInfo.deviceModel);
				newSysInfo.Add("Device Name: " + SystemInfo.deviceName);
				newSysInfo.Add("Device Type: " + SystemInfo.deviceType.ToString());
				newSysInfo.Add("Unique Device ID: " + SystemInfo.deviceUniqueIdentifier);
				newSysInfo.Add("Graphics Device ID: " + SystemInfo.graphicsDeviceID);
				newSysInfo.Add("Graphics Device Name: " + SystemInfo.graphicsDeviceName);
				newSysInfo.Add("Graphics Device Type: " + SystemInfo.graphicsDeviceType.ToString());
				newSysInfo.Add("Graphics Device Vendor: " + SystemInfo.graphicsDeviceVendor);
				newSysInfo.Add("Graphics Device VendorID: " + SystemInfo.graphicsDeviceVendorID);
				newSysInfo.Add("Graphics Device Version: " + SystemInfo.graphicsDeviceVersion);
				newSysInfo.Add("Graphics Memory Size: " + SystemInfo.graphicsMemorySize);//
				newSysInfo.Add("Graphics Multithreaded: " + SystemInfo.graphicsMultiThreaded);
				newSysInfo.Add("Graphics Shader Level: " + SystemInfo.graphicsShaderLevel);
				newSysInfo.Add("Graphics UV Starts at Top?: " + SystemInfo.graphicsUVStartsAtTop);
				newSysInfo.Add("Max Cubemap Size: " + SystemInfo.maxCubemapSize);
				newSysInfo.Add("Max Texture Size: " + SystemInfo.maxTextureSize);
				newSysInfo.Add("NPOT Support: " + SystemInfo.npotSupport.ToString());
				newSysInfo.Add("OS: " + SystemInfo.operatingSystem);
				newSysInfo.Add("OS Family: " + SystemInfo.operatingSystemFamily.ToString());
				newSysInfo.Add("Processor Count: " + SystemInfo.processorCount);
				newSysInfo.Add("Processor Frequency: " + SystemInfo.processorFrequency + "MHz");
				newSysInfo.Add("Processor Type: " + SystemInfo.processorType);
				newSysInfo.Add("Supported Render Targer Count: " + SystemInfo.supportedRenderTargetCount);
				if(SystemInfo.supports2DArrayTextures){newSysInfo.Add("Supports 2D Array Textures : Yes");} else { newSysInfo.Add("Supports 2D Array Textures : No");}
				if(SystemInfo.supports3DRenderTextures){newSysInfo.Add("Supports 3D Render Textures : Yes");} else { newSysInfo.Add("Supports 3D Render Textures : No");}
				if(SystemInfo.supports3DTextures){newSysInfo.Add("Supports 3D Textures : Yes");} else { newSysInfo.Add("Supports 3D Textures : No");}
				if(SystemInfo.supportsAccelerometer){newSysInfo.Add("Supports Accelerometer : Yes");} else { newSysInfo.Add("Supports Accelerometer : No");}
				if(SystemInfo.supportsAudio){newSysInfo.Add("Supports Audio : Yes");} else { newSysInfo.Add("Supports Audio : No");}
				if(SystemInfo.supportsComputeShaders){newSysInfo.Add("Supports Compute Shaders : Yes");} else { newSysInfo.Add("Supports Compute Shaders : No");}
				if(SystemInfo.supportsCubemapArrayTextures){newSysInfo.Add("Supports Cubemap Array Textures : Yes");} else { newSysInfo.Add("Supports Cubemap Array Textures : No");}
				if(SystemInfo.supportsGyroscope){newSysInfo.Add("Supports Gyroscope : Yes");} else { newSysInfo.Add("Supports Gyroscope : No");}
				if(SystemInfo.supportsImageEffects){newSysInfo.Add("Supports Image Effects : Yes");} else { newSysInfo.Add("Supports Image Effects : No");}
				if(SystemInfo.supportsInstancing){newSysInfo.Add("Supports Instancing : Yes");} else { newSysInfo.Add("Supports Instancing : No");}
				if(SystemInfo.supportsLocationService){newSysInfo.Add("Supports Location Services : Yes");} else { newSysInfo.Add("Supports Location Services : No");}
				if(SystemInfo.supportsMotionVectors){newSysInfo.Add("Supports Motion Vectors : Yes");} else { newSysInfo.Add("Supports Motion Vectors : No");}
				if(SystemInfo.supportsRawShadowDepthSampling){newSysInfo.Add("Supports Raw Shadow Depth Sampling : Yes");} else { newSysInfo.Add("Supports Raw Shadow Depth Sampling : No");}
				if(SystemInfo.supportsRenderToCubemap){newSysInfo.Add("Supports Render To Cubemap : Yes");} else { newSysInfo.Add("Supports Render To Cubemap : No");}
				if(SystemInfo.supportsShadows){newSysInfo.Add("Supports Shadows : Yes");} else { newSysInfo.Add("Supports Shadows : No");}
				if(SystemInfo.supportsSparseTextures){newSysInfo.Add("Supports Sparse Textures : Yes");} else { newSysInfo.Add("Supports Sparse Textures : No");}
				if(SystemInfo.supportsVibration){newSysInfo.Add("Supports Vibration : Yes");} else { newSysInfo.Add("Supports Vibration : No");}
				newSysInfo.Add("System Memory Size: " + SystemInfo.systemMemorySize);
				newSysInfo.Add("Unsupported Identifier: " + SystemInfo.unsupportedIdentifier);
				if(SystemInfo.usesReversedZBuffer){newSysInfo.Add("Uses Reversed Z Buffer : Yes");} else { newSysInfo.Add("Uses Reversed Z Buffer : No");}
				
				return newSysInfo;
		}
	
	//Miscellanous Operators//
		public static string GenerateHashCode(int length) {
			int a = 0;
			string hc = "";
			while(a<length){
				float r = Random.Range(0.0f,2);
				Debug.Log(r);
				if(r <= 0.999f){
					int v = GetValue();
					hc += v.ToString();
				}
				if(r >= 1){
					string c = GetCharacter();
					hc += c;
				}
				a++;
			}
			
			return hc;
		}
		
		private static int GetValue () {
			int val = Random.Range(0,9);
			return val;
		}
		private static string GetCharacter () {
			string[] alp = new string[]{"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"};
			//int[] val = new int[]{0,1,2,3,4,5,6,7,8,9};
			string returnChar = alp[Random.Range(0, alp.Length)];
			return returnChar;
		}
	}
}