///---SystemInformationLogger Editor Standalone v1.0.0---/// 
///Created by Alpaca Studio [ http://alpaca.studio ]///
///Provided as part of the UnitySysAPI. Check out the SysAPI here: https://github.com/Alpaca-Studio/UnitySysAPI ///
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
public class SystemInformationLogger : EditorWindow {
	[MenuItem("Tools/System Info Logger")]
	public static void ShowWindow () {
		EditorWindow.GetWindow(typeof(SystemInformationLogger));
	}
	Vector2 _scrollPosition;
	string _path;
	public static List<string> sysInfo = new List<string>();
	List<string> richTextSysInfo = new List<string>();
	string sysText;
	void OnEnable () {
			sysInfo.Clear();
			richTextSysInfo.Clear();
				sysInfo.Add("     --- SystemInfo ---    ");
				sysInfo.Add("----- " + System.DateTime.Now.ToString()+" -----" );
				sysInfo.Add("Battery Level  :  " + (SystemInfo.batteryLevel *100) + "%");
				sysInfo.Add("Battery Status  :  " + SystemInfo.batteryStatus.ToString());
				sysInfo.Add("Copy Texture Support  :  " + SystemInfo.copyTextureSupport.ToString());
				sysInfo.Add("Device Model  :  " + SystemInfo.deviceModel);
				sysInfo.Add("Device Name  :  " + SystemInfo.deviceName);
				sysInfo.Add("Device Type  :  " + SystemInfo.deviceType.ToString());
				sysInfo.Add("Unique Device ID  :  " + SystemInfo.deviceUniqueIdentifier);
				sysInfo.Add("Graphics Device ID  :  " + SystemInfo.graphicsDeviceID);
				sysInfo.Add("Graphics Device Name  :  " + SystemInfo.graphicsDeviceName);
				sysInfo.Add("Graphics Device Type  :  " + SystemInfo.graphicsDeviceType.ToString());
				sysInfo.Add("Graphics Device Vendor  :  " + SystemInfo.graphicsDeviceVendor);
				sysInfo.Add("Graphics Device VendorID  :  " + SystemInfo.graphicsDeviceVendorID);
				sysInfo.Add("Graphics Device Version  :  " + SystemInfo.graphicsDeviceVersion);
				sysInfo.Add("Graphics Memory Size  :  " + SystemInfo.graphicsMemorySize);//
				sysInfo.Add("Graphics Multithreaded  :  " + SystemInfo.graphicsMultiThreaded);
				sysInfo.Add("Graphics Shader Level  :  " + SystemInfo.graphicsShaderLevel);
				sysInfo.Add("Graphics UV Starts at Top?  :  " + SystemInfo.graphicsUVStartsAtTop);
				sysInfo.Add("Max Cubemap Size  :  " + SystemInfo.maxCubemapSize);
				sysInfo.Add("Max Texture Size  :  " + SystemInfo.maxTextureSize);
				sysInfo.Add("NPOT Support  :  " + SystemInfo.npotSupport.ToString());
				sysInfo.Add("OS  :  " + SystemInfo.operatingSystem);
				sysInfo.Add("OS Family  :  " + SystemInfo.operatingSystemFamily.ToString());
				sysInfo.Add("Processor Count  :  " + SystemInfo.processorCount);
				sysInfo.Add("Processor Frequency  :  " + (SystemInfo.processorFrequency*0.001) + "MHz");
				sysInfo.Add("Processor Type  :  " + SystemInfo.processorType);
				sysInfo.Add("Supported Render Targer Count  :  " + SystemInfo.supportedRenderTargetCount);
				if(SystemInfo.supports2DArrayTextures){sysInfo.Add("Supports 2D Array Textures  :  Yes");} else { sysInfo.Add("Supports 2D Array Textures  :  No");}
				if(SystemInfo.supports3DRenderTextures){sysInfo.Add("Supports 3D Render Textures  :  Yes");} else { sysInfo.Add("Supports 3D Render Textures  :  No");}
				if(SystemInfo.supports3DTextures){sysInfo.Add("Supports 3D Textures  :  Yes");} else { sysInfo.Add("Supports 3D Textures  :  No");}
				if(SystemInfo.supportsAccelerometer){sysInfo.Add("Supports Accelerometer  :  Yes");} else { sysInfo.Add("Supports Accelerometer  :  No");}
				if(SystemInfo.supportsAudio){sysInfo.Add("Supports Audio  :  Yes");} else { sysInfo.Add("Supports Audio  :  No");}
				if(SystemInfo.supportsComputeShaders){sysInfo.Add("Supports Compute Shaders  :  Yes");} else { sysInfo.Add("Supports Compute Shaders  :  No");}
				if(SystemInfo.supportsCubemapArrayTextures){sysInfo.Add("Supports Cubemap Array Textures  :  Yes");} else { sysInfo.Add("Supports Cubemap Array Textures  :  No");}
				if(SystemInfo.supportsGyroscope){sysInfo.Add("Supports Gyroscope  :  Yes");} else { sysInfo.Add("Supports Gyroscope  :  No");}
				if(SystemInfo.supportsImageEffects){sysInfo.Add("Supports Image Effects  :  Yes");} else { sysInfo.Add("Supports Image Effects  :  No");}
				if(SystemInfo.supportsInstancing){sysInfo.Add("Supports Instancing  :  Yes");} else { sysInfo.Add("Supports Instancing  :  No");}
				if(SystemInfo.supportsLocationService){sysInfo.Add("Supports Location Services  :  Yes");} else { sysInfo.Add("Supports Location Services  :  No");}
				if(SystemInfo.supportsMotionVectors){sysInfo.Add("Supports Motion Vectors  :  Yes");} else { sysInfo.Add("Supports Motion Vectors  :  No");}
				if(SystemInfo.supportsRawShadowDepthSampling){sysInfo.Add("Supports Raw Shadow Depth Sampling  :  Yes");} else { sysInfo.Add("Supports Raw Shadow Depth Sampling  :  No");}
				if(SystemInfo.supportsRenderToCubemap){sysInfo.Add("Supports Render To Cubemap  :  Yes");} else { sysInfo.Add("Supports Render To Cubemap  :  No");}
				if(SystemInfo.supportsShadows){sysInfo.Add("Supports Shadows  :  Yes");} else { sysInfo.Add("Supports Shadows  :  No");}
				if(SystemInfo.supportsSparseTextures){sysInfo.Add("Supports Sparse Textures  :  Yes");} else { sysInfo.Add("Supports Sparse Textures  :  No");}
				if(SystemInfo.supportsVibration){sysInfo.Add("Supports Vibration  :  Yes");} else { sysInfo.Add("Supports Vibration  :  No");}
				sysInfo.Add("System Memory Size  :  " + SystemInfo.systemMemorySize);
				sysInfo.Add("Unsupported Identifier  :  " + SystemInfo.unsupportedIdentifier);
				if(SystemInfo.usesReversedZBuffer){sysInfo.Add("Uses Reversed Z Buffer  :  Yes");} else { sysInfo.Add("Uses Reversed Z Buffer  :  No");}
				sysInfo.Add(string.Format("{0} {1}", "", System.Environment.NewLine));
				sysInfo.Add("'System Information Logger' is part of the Sys API by Alpaca Studio" );
				sysInfo.Add(string.Format("{0} {1}", "", System.Environment.NewLine));
				GetRichText();
		}
		
	void GetRichText () {
		int i = 0;
		sysText = string.Empty;
			richTextSysInfo.Add("     --- SystemInfo ---    ");
			richTextSysInfo.Add("----- " + System.DateTime.Now.ToString()+" -----" );
			richTextSysInfo.Add("Battery Level  <b>:</b>  " + "<i>"+(SystemInfo.batteryLevel *100) + "%"+"</i>");
			richTextSysInfo.Add("Battery Status  <b>:</b>  " + "<i>"+SystemInfo.batteryStatus.ToString()+"</i>");
			richTextSysInfo.Add("Copy Texture Support  <b>:</b>  " + "<i>"+SystemInfo.copyTextureSupport.ToString()+"</i>");
			richTextSysInfo.Add("Device Model  <b>:</b>  " + "<i>"+SystemInfo.deviceModel+"</i>");
			richTextSysInfo.Add("Device Name  <b>:</b>  " + "<i>"+SystemInfo.deviceName+"</i>");
			richTextSysInfo.Add("Device Type  <b>:</b>  " + "<i>"+SystemInfo.deviceType.ToString()+"</i>");
			richTextSysInfo.Add("Unique Device ID  <b>:</b>  " + "<i>"+SystemInfo.deviceUniqueIdentifier+"</i>");
			richTextSysInfo.Add("Graphics Device ID  <b>:</b>  " + "<i>"+SystemInfo.graphicsDeviceID+"</i>");
			richTextSysInfo.Add("Graphics Device Name  <b>:</b>  " + "<i>"+SystemInfo.graphicsDeviceName+"</i>");
			richTextSysInfo.Add("Graphics Device Type  <b>:</b>  " + "<i>"+SystemInfo.graphicsDeviceType.ToString()+"</i>");
			richTextSysInfo.Add("Graphics Device Vendor  <b>:</b>  " + "<i>"+SystemInfo.graphicsDeviceVendor+"</i>");
			richTextSysInfo.Add("Graphics Device VendorID  <b>:</b>  " + "<i>"+SystemInfo.graphicsDeviceVendorID+"</i>");
			richTextSysInfo.Add("Graphics Device Version  <b>:</b>  " + "<i>"+SystemInfo.graphicsDeviceVersion+"</i>");
			richTextSysInfo.Add("Graphics Memory Size  <b>:</b>  " + "<i>"+SystemInfo.graphicsMemorySize+"</i>");//
			richTextSysInfo.Add("Graphics Multithreaded  <b>:</b>  " + "<i>"+SystemInfo.graphicsMultiThreaded+"</i>");
			richTextSysInfo.Add("Graphics Shader Level  <b>:</b>  " + "<i>"+SystemInfo.graphicsShaderLevel+"</i>");
			richTextSysInfo.Add("Graphics UV Starts at Top?  <b>:</b>  " + "<i>"+SystemInfo.graphicsUVStartsAtTop+"</i>");
			richTextSysInfo.Add("Max Cubemap Size  <b>:</b>  " + "<i>"+SystemInfo.maxCubemapSize+"</i>");
			richTextSysInfo.Add("Max Texture Size  <b>:</b>  " + "<i>"+SystemInfo.maxTextureSize+"</i>");
			richTextSysInfo.Add("NPOT Support  <b>:</b>  " + "<i>"+SystemInfo.npotSupport.ToString()+"</i>");
			richTextSysInfo.Add("OS  <b>:</b>  " + "<i>"+SystemInfo.operatingSystem+"</i>");
			richTextSysInfo.Add("OS Family  <b>:</b>  " + "<i>"+SystemInfo.operatingSystemFamily.ToString()+"</i>");
			richTextSysInfo.Add("Processor Count  <b>:</b>  " + "<i>"+SystemInfo.processorCount+"</i>");
			richTextSysInfo.Add("Processor Frequency  <b>:</b>  " + "<i>"+(SystemInfo.processorFrequency*0.001) + "MHz"+"</i>");
			richTextSysInfo.Add("Processor Type  <b>:</b>  " + "<i>"+SystemInfo.processorType+"</i>");
			richTextSysInfo.Add("Supported Render Targer Count  <b>:</b>  " + "<i>"+SystemInfo.supportedRenderTargetCount+"</i>");
				if(SystemInfo.supports2DArrayTextures){richTextSysInfo.Add("Supports 2D Array Textures  <b>:</b>  <i>Yes</i>");} else { richTextSysInfo.Add("Supports 2D Array Textures  <b>:</b>  <i>No</i>");}
				if(SystemInfo.supports3DRenderTextures){richTextSysInfo.Add("Supports 3D Render Textures  <b>:</b>  <i>Yes</i>");} else { richTextSysInfo.Add("Supports 3D Render Textures  <b>:</b>  <i>No</i>");}
				if(SystemInfo.supports3DTextures){richTextSysInfo.Add("Supports 3D Textures  <b>:</b>  <i>Yes</i>");} else { richTextSysInfo.Add("Supports 3D Textures  <b>:</b>  <i>No</i>");}
				if(SystemInfo.supportsAccelerometer){richTextSysInfo.Add("Supports Accelerometer  <b>:</b>  <i>Yes</i>");} else { richTextSysInfo.Add("Supports Accelerometer  <b>:</b>  <i>No</i>");}
				if(SystemInfo.supportsAudio){richTextSysInfo.Add("Supports Audio  <b>:</b>  <i>Yes</i>");} else { richTextSysInfo.Add("Supports Audio  <b>:</b>  <i>No</i>");}
				if(SystemInfo.supportsComputeShaders){richTextSysInfo.Add("Supports Compute Shaders  <b>:</b>  <i>Yes</i>");} else { richTextSysInfo.Add("Supports Compute Shaders  <b>:</b>  <i>No</i>");}
				if(SystemInfo.supportsCubemapArrayTextures){richTextSysInfo.Add("Supports Cubemap Array Textures  <b>:</b>  <i>Yes</i>");} else { richTextSysInfo.Add("Supports Cubemap Array Textures  <b>:</b>  <i>No</i>");}
				if(SystemInfo.supportsGyroscope){richTextSysInfo.Add("Supports Gyroscope  <b>:</b>  <i>Yes</i>");} else { richTextSysInfo.Add("Supports Gyroscope  <b>:</b>  <i>No</i>");}
				if(SystemInfo.supportsImageEffects){richTextSysInfo.Add("Supports Image Effects  <b>:</b>  <i>Yes</i>");} else { richTextSysInfo.Add("Supports Image Effects  <b>:</b>  <i>No</i>");}
				if(SystemInfo.supportsInstancing){richTextSysInfo.Add("Supports Instancing  <b>:</b>  <i>Yes</i>");} else { richTextSysInfo.Add("Supports Instancing  <b>:</b>  <i>No</i>");}
				if(SystemInfo.supportsLocationService){richTextSysInfo.Add("Supports Location Services  <b>:</b>  <i>Yes</i>");} else { richTextSysInfo.Add("Supports Location Services  <b>:</b>  <i>No</i>");}
				if(SystemInfo.supportsMotionVectors){richTextSysInfo.Add("Supports Motion Vectors  <b>:</b>  <i>Yes</i>");} else { richTextSysInfo.Add("Supports Motion Vectors  <b>:</b>  <i>No</i>");}
				if(SystemInfo.supportsRawShadowDepthSampling){richTextSysInfo.Add("Supports Raw Shadow Depth Sampling  <b>:</b>  <i>Yes</i>");} else { richTextSysInfo.Add("Supports Raw Shadow Depth Sampling  <b>:</b>  <i>No</i>");}
				if(SystemInfo.supportsRenderToCubemap){richTextSysInfo.Add("Supports Render To Cubemap  <b>:</b>  <i>Yes</i>");} else { richTextSysInfo.Add("Supports Render To Cubemap  <b>:</b>  <i>No</i>");}
				if(SystemInfo.supportsShadows){richTextSysInfo.Add("Supports Shadows  <b>:</b>  <i>Yes</i>");} else { richTextSysInfo.Add("Supports Shadows  <b>:</b>  <i>No</i>");}
				if(SystemInfo.supportsSparseTextures){richTextSysInfo.Add("Supports Sparse Textures  <b>:</b>  <i>Yes</i>");} else { richTextSysInfo.Add("Supports Sparse Textures  <b>:</b>  <i>No</i>");}
				if(SystemInfo.supportsVibration){richTextSysInfo.Add("Supports Vibration  <b>:</b>  <i>Yes</i>");} else { richTextSysInfo.Add("Supports Vibration  <b>:</b>  <i>No</i>");}
				richTextSysInfo.Add("System Memory Size  <b>:</b>  " + "<i>"+SystemInfo.systemMemorySize+"</i>");
				richTextSysInfo.Add("Unsupported Identifier  <b>:</b>  " + "<i>"+SystemInfo.unsupportedIdentifier+"</i>");
				if(SystemInfo.usesReversedZBuffer){richTextSysInfo.Add("Uses Reversed Z Buffer  <b>:</b>  Yes");} else { richTextSysInfo.Add("Uses Reversed Z Buffer  <b>:</b>  <i>No</i>");}
				richTextSysInfo.Add(string.Format("{0} {1}", "", System.Environment.NewLine));
				richTextSysInfo.Add("<i>**'System Information Logger' is part of the Unity Sys API by Alpaca Studio**</i>" );
				richTextSysInfo.Add(string.Format("{0} {1}", "", System.Environment.NewLine));
				foreach(string ab in richTextSysInfo){
					if(i <=1){
						string cd = "<b>"+ab+"</b>";
						sysText += string.Format("{0}{1}", cd.ToString(), System.Environment.NewLine);
					}else{
						sysText += string.Format("{0}{1}", ab.ToString(), System.Environment.NewLine);
					}
					i++;
				}
		
		
	}
	void OnGUI () {
		GUIStyle helpBox = GUI.skin.GetStyle("HelpBox");
		helpBox.richText = true;
		
		EditorGUILayout.Space();
		GUILayout.Label("Sys API System Information Logger", EditorStyles.boldLabel);
		
		GUILayout.BeginHorizontal();
		EditorGUILayout.Space();
		if(GUILayout.Button("Export System Information", EditorStyles.miniButton)){
			//string fileName = "SystemInformation[" + System.DateTime.Now.ToString("MMddyyyy_hhmmss") +"].txt";
			string fileName = "SystemInformation.txt";
			if(!File.Exists(fileName))File.Create(fileName).Dispose();
			string _path = EditorUtility.SaveFilePanel("Export System Information File",Application.dataPath,fileName,"txt");
			//int i = 0;
			if(_path != string.Empty){
				File.Create(_path).Dispose();
				foreach(string ab in sysInfo){
						File.AppendAllText(_path, string.Format("{0} {1}", ab, System.Environment.NewLine));
						//i++;
				}
				System.Diagnostics.Process.Start(_path);
			} else {
				Debug.LogError("[Sys API] ERROR002: Path or file not specified.(EC-SIL-162)");
			}
		}
		EditorGUILayout.Space();
		GUILayout.EndHorizontal();
		
		EditorGUILayout.Space(); 
			EditorGUILayout.Space();
				EditorGUILayout.Space();
		GUILayout.Label("System Information", EditorStyles.boldLabel);
		_scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition,false,false);
		EditorGUILayout.LabelField(sysText, helpBox);
		EditorGUILayout.EndScrollView();
		EditorGUILayout.Space();		
	}
	void OnDestroy(){
		sysInfo.Clear();
			richTextSysInfo.Clear();
				sysText = "";
	}
}
#endif
