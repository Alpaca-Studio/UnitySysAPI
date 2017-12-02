import System.Collections.Generic;

@HeaderAttribute("Directory Info")
	@Tooltip("This is the filename for exported Sys.Log data")
		var logFileName : String = "SysLog.txt";
	@Tooltip("This is the filename for exported System Information data")
		var sysFileName : String = "SystemInformation.txt";
	var dir : String;
	@Tooltip("This is the default path to save files. Read Only!")
		var dataPath : String;
	@Space(15)
		//var Texture2D capture;
	@HeaderAttribute("Sys Method Switches")
	@Tooltip("Click this to capture a screenshot!")
		var CaptureScreenView : boolean;
	@Tooltip("Click this to save 'saveArray' to a text file")
		var SaveArrayToFile : boolean;
	@Tooltip("Click this to load 'saveArray' data from a text file")
		var GetArrayFromFile : boolean;
	@Tooltip("Click this save the current system's information to a text file")
		var SaveSystemInfo : boolean;
	@Tooltip("Click this read the current system's information and cast to 'SystemInfo' list variable")
		var GetSystemInfo : boolean;
	@Tooltip("Click this save the Sys.Log to a text file")
		var SaveSystemLog : boolean;
	@Tooltip("Click this to clear the Sys.Log cache")
		var ClearSystemLog : boolean;
	@Space(15)
	
	@HeaderAttribute("Sys Logger Imported Data")
	@Tooltip("This is the raw Sys.Log messages without time stamps")
		var logRaw : List.<String> = new List.<String>();
	@Tooltip("This is the Sys.Log messages WITH time stamps")
		var logData : List.<String> = new List.<String>();
	@Space(12)
	
	@HeaderAttribute("File Save/Load Example Arrays")
	@Tooltip("This is the example String array to save to a file using 'Sys.SaveFile()'")
		var saveArray : String[] = ["a","b","c","d","e","f","g","h","i","j"];
	@Tooltip("This array reads all the data saved from the 'saveArray' variable when activated by 'GetArrayFromFile' boolean")
		var loadArray : String[];
	@Space(12)
	
	@HeaderAttribute("Imported System Info")
	@Tooltip("This is current system's information cast to a list of strings. Activated by 'GetSystemInfo' boolean")
		var SystemInfo : List.<String> = new List.<String>();
	@Space(12)
	
	@HeaderAttribute("Hash Code Generation [Alpha]")
	@Tooltip("Click this generate a random hash code")
		var GenerateUniqueID : boolean;
	@Tooltip("This is current system's information cast to a list of strings. Activated by 'GetSystemInfo' boolean")
		var uIDLength : int;
		var uniqueID : String;
	
	public function Awake () {
		dir = Sys.DeviceExternalStorage();
		dataPath = dir + "/" + Application.productName + "/Logs2";
		if(Sys.DirectoryCheck(dataPath,true)){
			Sys.FileCheck(dataPath+"/", logFileName, true);
		}
	}
	function Start () {
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
	}
	
	// Update is called once per frame
	function Update () {
		logRaw = Sys.logRaw;
		logData = Sys.logData;

		if(CaptureScreenView){
			var getDateTime : String = System.DateTime.Now.ToString("MMddyyyy_hhmmss");
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
			SystemInfo = new List.<String>(Info.GetSystemInfo());
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
