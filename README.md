# Sys API for Unity
##### Version: v1.0.5 | Requirements: Unity5+

## About
The **_Sys API_** is essentially an API layer for the _UnityEngine_. The Sys API adds an array of new methods as well as alternatives some older ones. The _Sys_ class lies within the _UnityEngine_ namespace, so it will already be imported into your scripts. You could also choose to import the Sys class via `using UnityEngine.Sys;` in C#, or `import UnityEngine.Sys;` in Javascript.

For example, saving a screenshot via `Application.CaptureScreenshot(“Screenshot.png”)` works pretty well and is very easy to use. However if you wanna dictate where you save that screenshot, you will have to jump through a few hoops.
But with the Sys API you can simply use:
```Sys.CaptureScreenshot(this, “Path/To/Screenshot.png”);```
And the Sys API will handle all of the texture encoding and file saving. Continue to _Using the Sys API_ to get started.

__*If you are looking for information about contributing to the UnitySysAPI, check out [this guide](https://github.com/Alpaca-Studio/UnitySysAPI/wiki/Contributing-to-the-Sys-API).*__

## Using the Sys API
The Sys API handles a variety of methods, such as: Logging/Debugging; Data Handling; Screen Capturing/Loading; Arithmetic; and System Information. We will cover a few of these below. If this is your first time using the Sys API, we recommend looking at [Getting Started](https://github.com/Alpaca-Studio/UnitySysAPI/wiki/Getting-Started) in the wiki.
### Logging and Debugging
The Sys API logging and debugging methods are relatively simple and very similar to using Debug.Log. One way we elaborated upon the basic Debug.Log was to create a “silent log” that captures and timestamps log messages and then allows them to be saved to any desired location. This way developers access their log via txt file for more in depth debugging. But only user declared messages appear in the log. It does not add Engine or System logging.
So to add a message to the silent log:

```Sys.Log(“This is a test!”);```

If you did however want to display the message in the Console you would:

```Sys.Log(“This is a test!”, true);```

This will add the message to the log file and display it in the console window.

You also have the ability to clear the log at anytime with the `Sys.ClearLog();` function.
To save a log file you would use:

```Sys.SaveLog("Path_To_Log_File/SysLog.txt");```

Calling ‘Sys.SaveLog();’ will save to the default location of ‘[Application.persistentDataPath]/[Application.productName]/Logs/SysLog.txt’

### Data Handling 

Saving and Loading data is confined into two methods:

```Sys.SaveDataToFile(“Path/To/File.txt”);```

And ```Sys.LoadDataFromFile(“Path/To/File.txt”);```

Also with the `SaveDataToFile` method you can open the saved file’s directory in file explorer via:
```Sys.SaveDataToFile(“Path/To/File.txt”, true);```
It's recommended for use during development, untested in build.

The `LoadDataFromFile` method makes it simple to cast a text file a string array or list. This can be done by using:

```string[] data = Sys.LoadDataFromFile(“Path/To/File.txt”);```
Or
```List <string> data = new List <string>(Sys.LoadDataFromFile(“Path/To/File.txt”));```

Now let’s look a bit more into Screen Capturing.

### Screen Capturing/ Image Loading
As stated above, capturing screenshots with the Sys API is a little different from using ```Application.CaptureScreenshot();```

Anytime you use Sys screen capturing you must declare the MonoBehaviour instance as the first parameter of the method. 
Using `Sys.CaptureScreenshot(this);` will save a screenshot to the default location: _‘[Application.persistentDataPath]/[Application.productName]/Screenshots/MMddyyyy - hhmmss.png’._

To define a path and name of a screenshot use: `Sys.CaptureScreenshot(this, “Path/To/Screenshot.png”);` as stated above.

Much like `SaveDataToFile`, the `CaptureScreenshot` method will open the png directory using:
```Sys.CaptureScreenshot(this, “Path/To/Screenshot.png”, true);``` 

Image loading is much like loading data in the section above. At this time the Sys API only allows png images to be loaded into _Texture2D_ objects. To load a png image into a texture, you would use:
```Texture2D tex = Sys.LoadImageAtPath("Path_To_Image/SomeImage.png");```

Lastly we will look at System Information logging methods.

### System Information
> As of v1.0.5+ SystemInfomation methods now are within the **Info** class of the [SYS_MASTER.cs](https://github.com/Alpaca-Studio/UnitySysAPI/blob/master/Source/Assets/Plugins/SysAPI/Scripts/SYS_MASTER.cs).
The Sys API’s System Information methods are very useful for logging users system information. This section of the API essentially layers the _UnityEngine.SystemInfo_ variables into the Sys API. For example instead of calling `SystemInfo.batteryLevel`, you could use  `Info.batteryLevel` to get the same result. 
You can export system info into a neatly formatted text file using:
```Info.SaveSystemInfo(“Path/To/Save/SystemInfo.txt”);```
And of course open the file directory by using:
```Info.SaveSystemInfo(“Path/To/Save/SystemInfo.txt”, true);```

The formatted system info can also be cast into a list by using:
```List <string> sysInfo = new List <string>(Info.GetSystemInfo());```
[See Example](UnitySysAPI/Documentation/Images/5.png)

## Updating the Sys API via Unity Editor
In the _Sys API_ source package is two c# scripts that handle updating the API via the unity editor.
To update the Sys API navigate to _‘Tools/SysAPI/Options’_ or alternatively press __Control+W__ to open the Sys Options editor window.
![1.gif](/Documentation/Images/1.gif)

You will see a new window appear that looks something like this:
![2.png](/Documentation/Images/2.png)

The options window contains three buttons: __Check For Updates; Force Update;__ and __Example__. Pressing _‘Check for Updates’_ will check your version versus the latest version of the Sys API. If you have the latest version you will receive this message:
![3.png](/Documentation/Images/3.png)

If there is an update available the newest _SYS_MASTER.cs_ will be downloaded and saved to _‘/Assets/Plugins/SysAPI/SYS_MASTER.cs’_. After updating this message will display in the console:
![4.png](/DocumentationImages/4.png)

If somehow _SYS_MASTER.cs_ becomes corrupted you can press the _‘Force Update’_ button to grab the latest update from the server.

Lastly, pressing _‘Example’_ will generate a new game object named **_SYS_** and attach _Sys_API_CSharp_Example.cs_ and _Sys_API_JS_Example.js_ scripts (same as the _'SYS'_ object in the _Example.unity_ scene).

For more information try checking out the [wiki](https://github.com/Alpaca-Studio/UnitySysAPI/wiki). There’s also a few other functions not covered in this readme. And for a more “hands-on” example, a look at the .cs and .js [examples](UnitySysAPI/Source/Assets/Sys_API/Examples).

_The Sys API is looking for contributors. If you would like to contribute a useful method or a shortcut, please send a message to_ @Alpaca-Studio _so we can test and implement it into the api._
