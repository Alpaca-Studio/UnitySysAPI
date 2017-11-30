# Contributing to Development
_The Unity Sys API is hosted and operated on github. Any contributions to the API should be submitted as pull requests. After you submit a pull request please give us 3-5 days to review, test, and inform you of our decision._

Thank you for your interest in contributing to the Unity Sys API. We hope this project continues to grow and allows developers to create awesome stuff without time consuming, repetitive coding. The primary goal of the Unity Sys API is to simplify some of Unity’s methods as well as create more efficient ones. They Sys API is ever-expanding thanks to contributors like you. You're awesome!

#### Here you will learn:
* Creating a fork from the _Alpaca-Studio/UnitySysAPI_ repository 
* Properly editing the _Sys API_.
* Creating new documentation.
* Creating a pull request.

## Creating a New Fork
Go to the UnitySysAPI repository on Alpaca Studio’s github and look for a button that says “Fork” at the top right of the page. 
Click “Fork” and then “Create New Fork”. 
![Fork.png](/Documentation/Images/Fork.png)
You should now have a copy of the repository on your github.

## Contributing to the Sys API
After you have created a fork in your github you can now edit files in unity and/or directly on github. Be advised the following scripts are restricted and editing could result in your pull request being denied.
The files _**vc.dxt, SysUpdateDownloader.cs, SysOptions.cs,**_ and _**SystemInformationLogger.cs**_ are _Unity Editor_ scripts maintained by _Alpaca Studio_. If you wish to edit _SysOptions.cs_ or _SystemInformationLogger.cs_, be sure to extensively test for errors and thoroughly detail the changes in the _Create Pull Request_ description area.

Generally, most contributors will edit/update the **SYS_MASTER.cs** file. This file houses all the static methods available in the API. A good starting point would be to think of what methods you seem to find yourself coding over and over. Then translate that into a static function within the *SYS_MASTER.cs*. The main focus of the API is to create new, easier, more effiecent methods for Unity developers to use. 
Like a hypothetical question there are no wrong answers. Anything you think could be useful to your fellow developers is welcome and much appriciated. However be sure to document anything you edit/add in the Documentation.


### Editing the Sys_Master
Navigate to the [*Assets/Plugins/SysAPI/Scripts/SYS_MASTER.cs*](/Source/Assets/Plugins/SysAPI/Scripts/SYS_MASTER.cs)

Once you have the *Sys_Master* file open in a text editor, find the category best suited for your new method. If none seem to fit you can place it under “*Miscellaneous*” or create a new category.

Once you have chosen a category add your method **two** spaces under the last method in the category.
Test your method to ensure it functions as intended. If no errors or warnings appear, you should move on to *Documentation*.


### Creating New Documentation
After you are finished adding to/editing the *Sys_Master*, you should then document your new method in the **Documentation.txt** in the [*Assets/SysAPI*](UnitySysAPI/Source/Assets/SysAPI) directory.
In the **Documentation.txt**, find (or create) the category your method is classified under. 
Describe what your function does, detail any attributes, and give an example. Refer to the Documentation.txt to see some examples.
After you have edited the documentation, move on to *Submitting a Pull Request*.

### Error Coding and Formatting:
If a contribution is made to the Unity Sys API that defines a new error, the contributors must edit the __Sys Message Library__^ file accordingly.

The Sys Message Library was created to document errors, messages, and warnings for developers and end-users to easily debug their projects that use the Sys API.

Before you create your error message you should open up the Sys Message Library and navigate to Errors. Then find the latest error code and add *001* to it. The sum will be your new error code. Now that you have your error code you should learn how to format your error message.

Any error message should be formatted like the following example:
```[Sys API] ERROR002: Path or File not specified. (EC-SUD-037)```

##### Here’s a breakdown of the Sys API Error format:
![ErrorFormat.png](/Documentation/Images/ErrorFormat.png)
1. The API the error pertains to.
2. The error code.
3. The error message.
4. The API message code (EC = Error Called).
5. The script/class ID acronym. (The example used is from the *Sys Update Downloader.cs*.)
6. The line that the error is called from.

So the example states that the _Sys API_ threw a _“File or path not specified”_ error (aka Error 002) at _line 37_ in the _”Sys Update Downloader”_ class.

Alternatively, you can use ```Sys.GetLine()``` to automatically retrieve line number as opposed to editing manually (Recommended). To use *GetLine* method, you would format your message string like this:
```“[Sys API] ERROR002: Path or File not specified. (EC-SUD-” + Sys.GetLine() + ”)”```

>**UPDATE:** As of 11/30/2017 you can now use *'Sys.GetErrorStackTrace()'*.

>**Example:** `“[Sys API] ERROR002: Path or File not specified. " + Sys.GetErrorStackTrace()`

>**Output:** *[Sys API] ERROR002: Path or File not specified. (EC-SystemUpdateDownloader.cs-37)*

After you have created your error code you must add it to the [Sys Message Library spreadsheet file](/Documentation/Sys%20Message%20Library.xlsx). 

**^**_The 'Sys Message Library' is only available on the github and will NOT be downloaded alongside other update files at this time._


### Updating Repository Files
First, open the UnitySysAPI fork on your github. Next you will want to upload your new files to the exact directory displayed in the repository. If you created new directories and/or files be sure to add them accordingly.

So if you edited the **SYS_MASTER.cs** file at [*Assets/Plugins/SysAPI/Scripts*](/Source/Assets/Plugins/SysAPI/Scripts), you should delete the old **SYS_MASTER.cs** from your repository and then upload your new **SYS_MASTER.cs**.
Also do the same with [Documentation.txt](/Source/Assets/SysAPI/Documentation.txt) at *Assets/SysAPI/*.

Ideally we would like for you to edit files directly on the github. An easy way to do this would be:
1. Open file in github’s editor.
![Fig-1a](/Documentation/Images/EditFile.png)

2. Select all content (*CTRL+A*) and then delete (*BACKSPACE*) so the file is now blank.
3. Then goto your new file in your text editor, select all content, and then copy it to clipboard (*CTRL+C*).
4. Finally go back to your blank file opened in github’s editor and paste your copied text (*CTRL+V*).
5. Save the file on github, and you're done.
After all your changes are on your forked repository you can now create a pull request to submit the changes.

## Creating a Pull Request
At the homepage of your repository find the *Pull Requests* tab on the repository toolbar.
![PullReq.png](/Documentation/Images/PullReq.png)

On the right you will see a *Create Pull Request* button.
![NewPullReq.png](/Documentation/Images/NewPullReq.png)

This will take you to the create pull request screen as displayed here:
![CreatePullReq.png](/Documentation/Images/CreatePullReq.png)
Double check your files and make sure everything looks correct. In the title section briefly describe what you’ve changed. Then give a detailed summary in the description area below. After you are completed with the title and description you should then create your pull request.

After you have submitted the request give us a few days to check everything over and get back to you. Within a week you should receive a response from us regarding your pull request. Your contributions will be live with the next update bundle. Thanks for your interest in contributing :D
