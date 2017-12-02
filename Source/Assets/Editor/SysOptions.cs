using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
public class SysOptions : EditorWindow {
	[MenuItem("Tools/Sys API/Options %_w")]
	public static void ShowWindow () {
		EditorWindow.GetWindow(typeof(SysOptions));
	}
	[MenuItem("Tools/Sys API/Examples")]
	public static void GenerateExample () {
		GameObject newObj = new GameObject("SYS");
		newObj.AddComponent<Sys_API_CSharp_Example>();
		newObj.AddComponent<Sys_API_JS_Example>();
	}
	[MenuItem("Tools/Sys API/Check For Updates")]
	public static void CheckForUpdates () {
		EditorApplication.isPlaying = true;
		GameObject newObj = new GameObject("SysOptions");
		newObj.AddComponent<SysUpdateDownloader>();
	}
	[MenuItem("Tools/Sys API/Wiki Documentation")]
	public static void OpenWebpage () {
		Application.OpenURL("https://github.com/Alpaca-Studio/UnitySysAPI/wiki");
	}
	
	string _path;
	string _docPath;
	//List<string> VC = new List<string>();
	string _currentVersion;
	string _documentation;

	[SerializeField]
	bool _checkForUpdates;
	Vector2 _scrollPosition;
	
	GameObject obj;
	
	void OnEnable () {
		_path = Application.dataPath + "/Plugins/SysAPI/";
		_docPath = Application.dataPath + "/Sys_API/";
		if(!Directory.Exists(_path)){
			Directory.CreateDirectory(_path);
			File.Create(_path+"vc.dxt").Dispose();
			_currentVersion = "1.0.2";
			File.WriteAllText(_path+"vc.dxt", _currentVersion);
			//System.Diagnostics.Process.Start(_path);
		} else {
			if(File.Exists(_path+"vc.dxt")){
				_currentVersion = File.ReadAllLines(_path+"vc.dxt")[0];
			} else {
				File.Create(_path+"vc.dxt").Dispose();
				_currentVersion = "1.0.2";
				File.WriteAllText(_path+"vc.dxt", _currentVersion);
			}
		}
		
		_documentation = "";
		List<string> docu = new List<string>(File.ReadAllLines(_docPath+"Documentation.txt"));
		for(int i = 0; i < docu.Count; i++){
			if(i==0){
				_documentation += "<b>"+docu[i]+"</b> \n";
			}
			if(i!=0){
				_documentation += docu[i]+"\n";
			}
		}
	}
	void OnFocus () {
		if(EditorPrefs.HasKey("VC")){
			_currentVersion = EditorPrefs.GetString("VC");
			if(EditorApplication.isPlaying == false){
				foreach( var gObj in FindObjectsOfType(typeof(GameObject)) as GameObject[]){
					if(gObj.name == "SysOptions"){
						DestroyImmediate(gObj);
					}
				}
			}
		}	
	}
	void OnLostFocus () {
		EditorPrefs.SetString("VC",_currentVersion);
		
		_documentation = "";
		List<string> docu = new List<string>(File.ReadAllLines(_docPath+"Documentation.txt"));
		for(int i = 0; i < docu.Count; i++){
			if(i==0){
				_documentation += "<b>"+docu[i]+"</b> \n";
			}
			if(i!=0){
				_documentation += docu[i]+"\n";
			}
		}
		
		if(EditorApplication.isPlaying == false){
				foreach( var gObj in FindObjectsOfType(typeof(GameObject)) as GameObject[]){
					if(gObj.name == "SysOptions"){
						DestroyImmediate(gObj);
					}
				}
		}
	}
	void OnDestroy () {
		EditorPrefs.SetString("VC",_currentVersion);
		DestroyImmediate(GameObject.Find("SysOptions"));
	}
	void OnGUI () {
		GUIStyle helpBox = GUI.skin.GetStyle("HelpBox");
		helpBox.richText = true;
		
		EditorGUILayout.Space();
		GUILayout.Label("Sys API v" + _currentVersion+ " Editor Options", EditorStyles.boldLabel);
		
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("Check For Updates", EditorStyles.miniButton)){
			EditorPrefs.SetString("VC",_currentVersion);
			EditorApplication.isPlaying = true;
			GameObject newObj = new GameObject("SysOptions");
			newObj.AddComponent<SysUpdateDownloader>();
		}
		if(GUILayout.Button("Force Update", EditorStyles.miniButton)){
			EditorPrefs.SetString("VC",_currentVersion);
			EditorApplication.isPlaying = true;
			GameObject newObj = new GameObject("SysOptions");
			newObj.AddComponent<SysUpdateDownloader>();
			newObj.GetComponent<SysUpdateDownloader>().forceUpdate = true;
		}
		if(GUILayout.Button("Examples", EditorStyles.miniButton)){
			GameObject newObj = new GameObject("SYS");
			newObj.AddComponent<Sys_API_CSharp_Example>();
			newObj.AddComponent<Sys_API_JS_Example>();
		}
		GUILayout.EndHorizontal();
		
		EditorGUILayout.Space(); 
			EditorGUILayout.Space();
				EditorGUILayout.Space();
		
		_scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition,false,false);
		EditorGUILayout.LabelField(_documentation, helpBox);
		EditorGUILayout.EndScrollView();
		EditorGUILayout.Space();		
	}
	
}
#endif
