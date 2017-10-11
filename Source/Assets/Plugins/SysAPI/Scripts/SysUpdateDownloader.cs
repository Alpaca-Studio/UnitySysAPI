using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SysUpdateDownloader : MonoBehaviour {
#if UNITY_EDITOR
	string _versionURL = "https://pastebin.com/raw/yynXy2ij"; //"https://pastebin.com/raw/fcfvqrv3";
	string _sourceURL = "https://pastebin.com/raw/Rq8ZMZYU";
	string _path;
	//List<string> VC = new List<string>();
	string _currentVersion;
	public bool forceUpdate = false;
	
	void Start () {
		_path = Application.dataPath + "/Plugins/SysAPI/";
		_currentVersion = UnityEditor.EditorPrefs.GetString("VC");
		if(!forceUpdate){
			StartCoroutine(DownloadVC(_versionURL));
		} else {
			File.Create(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_MASTER.cs").Dispose();
			StartCoroutine(DownloadDataFiles(_sourceURL,"SYS_MASTER.cs"));
		}
	}
	
	IEnumerator DownloadVC(string url) {
		WWW www = new WWW(url);
		yield return www;
		string file = www.text;
		if(www.error != null){Debug.LogError(www.error);}
		File.Create(_path+"vc.dxt").Dispose();
		File.WriteAllText(_path+"vc.dxt", file); 
		InitDataFiles();
		
	}
	
	void InitDataFiles () {
		if(File.ReadAllLines(_path+"vc.dxt")[0] != null){
			string newVC = File.ReadAllLines(_path+"vc.dxt")[0];
			if(newVC != _currentVersion){
				File.Create(Application.dataPath + "/Plugins/SysAPI/Scripts/SYS_MASTER.cs").Dispose();
				StartCoroutine(DownloadDataFiles(_sourceURL,"SYS_MASTER.cs"));
				Debug.LogWarning("[Sys API]: Downloading Sys API v" + newVC);
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
	
	IEnumerator DownloadDataFiles(string url, string fileName) {
		WWW www = new WWW(url);
		yield return www;
		string file = www.text;
		if(www.isDone){
			File.AppendAllText(Application.dataPath + "/Plugins/SysAPI/Scripts/" + fileName, file);
			Debug.LogWarning("[Sys API]: Updated to version " + _currentVersion);
			UnityEditor.EditorPrefs.SetString("VC",_currentVersion);
			Destroy(this.gameObject);
			UnityEditor.EditorApplication.isPlaying = false;
			Destroy(this.gameObject);
		}
	}
	
	
	
	
#endif
}
