using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sys_API_CSharp_Example : MonoBehaviour {
	
	[HeaderAttribute("Directory Info")]
	[Tooltip("This is the filename for exported Sys.Log data")]
		public string logFileName = "SysLog.txt";
	[Tooltip("This is the filename for exported System Information data")]
		public string sysFileName = "SystemInformation.txt";
	string dir;
	[Tooltip("This is the default path to save files. Read Only!")]
		public string dataPath;
	[Space(15)]
		//public Texture2D capture;
	[HeaderAttribute("Sys Method Switches")]
	[Tooltip("Click this to capture a screenshot!")]
		public bool CaptureScreenView;
	[Tooltip("Click this to save 'saveArray' to a text file")]
		public bool SaveArrayToFile;
	[Tooltip("Click this to load 'saveArray' data from a text file")]
		public bool GetArrayFromFile;
	[Tooltip("Click this save the current system's information to a text file")]
		public bool SaveSystemInfo;
	[Tooltip("Click this read the current system's information and cast to 'SystemInfo' list variable")]
		public bool GetSystemInfo;
	[Tooltip("Click this save the Sys.Log to a text file")]
		public bool SaveSystemLog;
	[Tooltip("Click this to clear the Sys.Log cache")]
		public bool ClearSystemLog;
	[Space(15)]
	
	[HeaderAttribute("Sys Logger Imported Data")]
	[Tooltip("This is the raw Sys.Log messages without time stamps")]
		public List<string> logRaw = new List<string>();
	[Tooltip("This is the Sys.Log messages WITH time stamps")]
		public List<string> logData = new List<string>();
	[Space(12)]
	
	[HeaderAttribute("File Save/Load Example Arrays")]
	[Tooltip("This is the example string array to save to a file using 'Sys.SaveFile()'")]
		public string[] saveArray = new string[]{"a","b","c","d","e","f","g","h","i","j"};
	[Tooltip("This array reads all the data saved from the 'saveArray' variable when activated by 'GetArrayFromFile' boolean")]
		public string[] loadArray;
	[Space(12)]
	
	[HeaderAttribute("Imported System Info")]
	[Tooltip("This is current system's information cast to a list of strings. Activated by 'GetSystemInfo' boolean")]
		public List<string> SystemInfo = new List<string>();
	[Space(12)]
	
	[HeaderAttribute("Hash Code Generation [Alpha]")]
	[Tooltip("Click this generate a random hash code")]
		public bool GenerateUniqueID;
	[Tooltip("This is current system's information cast to a list of strings. Activated by 'GetSystemInfo' boolean")]
	public int uIDLength;
	public string uniqueID;
	
	[Space(12)]
	
	[HeaderAttribute("Arithmetic Testing [Unstable]")]
	[Tooltip("The Cartesian Product of sets 'a' and 'b'.")]
	public int[] cProduct;
	public int[] a = new int[]{1,2,3,4};
	public int[] b = new int[]{1,1,2,3};
	
	public void Awake () {
		/*#if UNITY_EDITOR
		dir = Application.persistentDataPath;
		#endif
		#if UNITY_ANDROID && !UNITY_EDITOR
			dir = "/storage/emulated/0";
		#endif
		#if UNITY_IOS && !UNITY_EDITOR
			dir = Application.persistentDataPath;
		#endif
		dataPath = dir + "/" + Application.productName + "/Logs";
		if(!Directory.Exists(dataPath)){
			Directory.CreateDirectory(dataPath);
			File.Create(dataPath + "/" + logFileName).Dispose();
			dataPath = dir + "/"+ Application.productName + "/Logs";
		} else {
			if(!File.Exists(dataPath + "/" + logFileName)){
				File.Create(dataPath + "/" + logFileName).Dispose();
				dataPath = dir + "/"+ Application.productName + "/Logs";
			} else {
				dataPath = dir + "/"+ Application.productName + "/Logs";
			}
		}*/

		dir = Sys.DeviceExternalStorage();
		dataPath = dir + "/" + Application.productName + "/Logs2";
		if(Sys.DirectoryCheck(dataPath,true)){
			Sys.FileCheck(dataPath+"/", logFileName, true);
		}
	}
	void Start () {
		Sys.Log("This is a test..");
		Sys.Log("This is a test...",true);
		Sys.Log("This is a test....");
		Sys.Log("This is a test.....", true);
		Sys.Log("This is a test......");
		Sys.Log("This is a test.......",true);
		Sys.Log("This is a test........");
		Sys.Log("This is a test.........", true);
		Sys.Log("This is a test..........");
		Sys.Log("This is a test...........",true);
		Sys.Log("This is a test............");
		Sys.Log("This is a test.............", true);
		Sys.Log("This is a test..............");
		Sys.Log("This is a test...............", true);
		Sys.Log("This is a test................", false);
		logRaw = Sys.logRaw;
		logData = Sys.logData;
		//Sys.SaveLog(dataPath + "/" + logFileName, true);
		//Sys.SaveSystemInfo(dataPath + "/" + sysFileName);
		//uniqueID = Sys.GenerateUniqueID(21);
		cProduct = Sys.cProduct(a,b);
		
	}
	
	// Update is called once per frame
	void Update () {
		logRaw = Sys.logRaw;
		logData = Sys.logData;

		if(CaptureScreenView){
			string getDateTime = System.DateTime.Now.ToString("MMddyyyy_hhmmss");
			Sys.CaptureScreenshot(this, (dataPath + "/" + getDateTime + ".png"), true);
			//capture = Sys.ScreenToTexture2D();
			//capture = Sys.LoadImageAtPath(dataPath + "/" + getDateTime + ".png");
			CaptureScreenView = false;
		}
		
		if(SaveArrayToFile){
			//Sys.SaveDataToFile(dataPath + "/" + "abc.txt", saveArray);
			Sys.SaveFile(dataPath + "/" + "abc.txt", saveArray);
			SaveArrayToFile = false;
		}
		if(GetArrayFromFile){
			//loadArray = Sys.LoadDataFromFile(dataPath + "/" + "abc.txt");
			loadArray = Sys.ReadFile(dataPath + "/" + "abc.txt");
			GetArrayFromFile = false;
		}
		
		if(SaveSystemInfo){
			Info.SaveSystemInfo(dataPath + "/" + sysFileName, true);
			SaveSystemInfo = false;
		}
		
		if(GetSystemInfo){
			SystemInfo = new List<string>(Info.GetSystemInfo());
			GetSystemInfo = false;
		}
		
		if(SaveSystemLog){
			Sys.SaveLog(dataPath + "/" + logFileName, true);
			SaveSystemLog = false;
		}
		
		if(ClearSystemLog){
			Sys.ClearLog();
			ClearSystemLog = false;
		}
		
		if(GenerateUniqueID){
			uniqueID = Sys.GenerateUniqueID(uIDLength);
			GenerateUniqueID = false;
		}
	}
}
