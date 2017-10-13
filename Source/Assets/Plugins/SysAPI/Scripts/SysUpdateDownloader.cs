using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SysUpdateDownloader : MonoBehaviour {
#if UNITY_EDITOR
	string _versionURL = "https://pastebin.com/raw/yynXy2ij"; //"https://pastebin.com/raw/fcfvqrv3";
	string _sourceURL = "https://pastebin.com/raw/Cs37xqMZ"; //"https://pastebin.com/raw/Rq8ZMZYU";
	string _docuURL = "https://pastebin.com/raw/7qacsN6i";
	string _sysCSURL = "https://pastebin.com/raw/VJ9GcLpd";
	string _sysJSURL = "https://pastebin.com/raw/LGhfm1GK";
	string _changelogURL = "https://pastebin.com/raw/qHJHdt8e";
	string _silURL = "https://pastebin.com/raw/RbDeJA2A";
	string _path;
	//List<string> VC = new List<string>();
	string _currentVersion;
	public bool forceUpdate = false;
	int dlCount = 0;
	
	void Start () {
		dlCount = 0;
		_path = Application.dataPath + "/Plugins/SysAPI/";
		_currentVersion = UnityEditor.EditorPrefs.GetString("VC");
		if(!forceUpdate){
			StartCoroutine(DownloadVC(_versionURL));
		} else {
			File.Create(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_MASTER.cs").Dispose();
			InitDataFiles();
		}
	}
	
	IEnumerator DownloadVC(string url) {
		WWW www = new WWW(url);
		yield return www;
		string file = www.text;
		if(www.error != null){Debug.LogError("[Sys API] ERROR001: "+www.error+" (EC-SUD-037)");}
		if(file.Length == 0 || file == null){Debug.LogError("[Sys API] ERROR000: Unable to establish connection. (EC-SUD-036)");} else {
			File.Create(_path+"vc.dxt").Dispose();
			File.WriteAllText(_path+"vc.dxt", file); 
			InitDataFiles();
		}
		
	}
	
	void InitDataFiles () {
		if(File.ReadAllLines(_path+"vc.dxt")[0] != null){
			string newVC = File.ReadAllLines(_path+"vc.dxt")[0];
			if(newVC != _currentVersion || forceUpdate){
				File.Create(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_MASTER.cs").Dispose();
				
				//Debug.LogWarning("[Sys API]: Downloading Sys API v" + newVC );
				//StartCoroutine(DownloadPluginFiles(_sourceURL,"SYS_MASTER.cs"));
				//Debug.LogWarning("[Sys API]: Downloading SYS_MASTER.cs");
				
				Debug.LogWarning("[Sys API]: Downloading Sys API v" + newVC );
				StartCoroutine(DownloadDataFiles(_sourceURL, Application.dataPath + "/Plugins/SysAPI/Scripts/","SYS_MASTER.cs"));
				Debug.LogWarning("[Sys API]: Downloading SYS_MASTER.cs");
				
				File.Create(Application.dataPath + "/Sys_API/Documentation.txt").Dispose();
				StartCoroutine(DownloadDataFiles(_docuURL,Application.dataPath + "/Sys_API/","Documentation.txt"));
				File.Create(Application.dataPath + "/Plugins/SysAPI/Documentation.txt").Dispose();
				StartCoroutine(DownloadDataFiles(_docuURL,Application.dataPath + "/Plugins/SysAPI/","Documentation.txt"));
				Debug.LogWarning("[Sys API]: Downloading 'Documentation.txt'");
				
				File.Create(Application.dataPath + "/Sys_API/change.log").Dispose();
				StartCoroutine(DownloadDataFiles(_changelogURL,Application.dataPath + "/Sys_API/","change.log"));
				Debug.LogWarning("[Sys API]: Downloading 'change.log'");
				
				File.Create(Application.dataPath + "/Editor/SystemInformationLogger.cs").Dispose();
				StartCoroutine(DownloadDataFiles(_changelogURL,Application.dataPath + "/Editor/","SystemInformationLogger.cs"));
				Debug.LogWarning("[Sys API]: Downloading 'SystemInformationLogger.cs'");
				
				File.Create(Application.dataPath + "/Sys_API/Examples/Sys_API_CSharp_Example.cs").Dispose();
				StartCoroutine(DownloadDataFiles(_sysCSURL,Application.dataPath + "/Sys_API/Examples/","Sys_API_CSharp_Example.cs"));
				Debug.LogWarning("[Sys API]: Downloading 'Sys_API_CSharp_Example.cs'");
				
				File.Create(Application.dataPath + "/Sys_API/Examples/Sys_API_JS_Example.js").Dispose();
				StartCoroutine(DownloadDataFiles(_sysJSURL,Application.dataPath + "/Sys_API/Examples/","Sys_API_JS_Example.js"));
				Debug.LogWarning("[Sys API]: Downloading 'Sys_API_JS_Example.js'");
				
				_currentVersion = newVC;
				UnityEditor.EditorPrefs.SetString("VC",_currentVersion);
			} else {
				Debug.LogWarning("[Sys API]: You already have the latest version." + " Sys API " + _currentVersion + ".");
				UnityEditor.EditorPrefs.SetString("VC",_currentVersion);
				Destroy(this.gameObject);
				UnityEditor.EditorApplication.isPlaying = false;
				Destroy(this.gameObject);
			}
		}
	}
	
	IEnumerator DownloadPluginFiles(string url, string fileName) {
		
		WWW www = new WWW(url);
		yield return www;
		string file = www.text;
		if(www.isDone){
			if(file.Length == 0 || file == null){UnityEditor.EditorApplication.isPlaying = false; Debug.LogError("[Sys API] ERROR000: Unable to establish connection. (EC-SUD-089)");} else {
				File.AppendAllText(Application.dataPath + "/Plugins/SysAPI/Scripts/" + fileName, file);
				Debug.LogWarning("[Sys API]: " + fileName+ " successfully updated.");
				dlCount++;
				if(dlCount >= 7){
					UnityEditor.EditorPrefs.SetString("VC",_currentVersion);
					Destroy(this.gameObject);
					UnityEditor.EditorApplication.isPlaying = false;
					Destroy(this.gameObject);
				}
			}
		}
	}
	
	IEnumerator DownloadDataFiles(string url, string path, string fileName) {
		WWW www = new WWW(url);
		yield return www;
		string file = www.text;
		if(www.isDone){
			if(file.Length == 0 || file == null){UnityEditor.EditorApplication.isPlaying = false; Debug.LogError("[Sys API] ERROR000: Unable to establish connection. (EC-SUD-108)");} else {
				File.AppendAllText(path + fileName, file);
				Debug.LogWarning("[Sys API]: " + fileName + " successfully updated.");
				dlCount++;
				//Debug.Log(dlCount);
				if(dlCount >= 7){
					UnityEditor.EditorPrefs.SetString("VC",_currentVersion);
					Destroy(this.gameObject);
					UnityEditor.EditorApplication.isPlaying = false;
					Debug.LogWarning("[Sys API]: Updated to version " + _currentVersion);
					Destroy(this.gameObject);
				}
			}
		}
	}
	
	
	
	
#endif
}
