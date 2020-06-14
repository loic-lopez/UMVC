<br />

<p align="center">
     <img src="/Docs/logo_transparent.png" alt="UMVC">
</p>

<h3 align="center" style="text-align:center;">
	Model-View-Controller Generator built for Unity
</h3>
<p align="center">
	Fast, Simple and intuitive
</p>

<hr>
<p align="center">
	<a href="https://github.com/loic-lopez/UMVC/blob/master/LICENSE">
           <img src="https://img.shields.io/github/license/loic-lopez/UMVC" />
        </a>
	<a href="https://github.com/loic-lopez/UMVC/releases">
	    <img src="https://img.shields.io/github/v/release/loic-lopez/UMVC">
	</a>
	<br/>
	<a href="https://codecov.io/gh/loic-lopez/UMVC">
           <img src="https://codecov.io/gh/loic-lopez/UMVC/branch/master/graph/badge.svg" />
        </a>
	<a href="https://github.com/loic-lopez/UMVC/actions?query=workflow%3AUMVC.Editor">
	    <img src="https://github.com/loic-lopez/UMVC/workflows/UMVC.Editor/badge.svg">
	</a>
	<a href="https://github.com/loic-lopez/UMVC/actions?query=workflow%3AUMVC.Core">
	    <img src="https://github.com/loic-lopez/UMVC/workflows/UMVC.Core/badge.svg">
	</a>
	
</p>
<hr>

# Features

### Overall

:white_check_mark: Customizable T4 Templates

:white_check_mark: Customizable Base Templates

:white_check_mark: Generate your architecture directly in the Unity Editor

### Model

:white_check_mark: Automated Events bound on model fields
- :white_check_mark: OnFieldWillUpdate(string field, object newObject, object oldObject)
- :white_check_mark: OnFieldDidUpdate(string field, object value)

:white_check_mark: Generate field when creating a Model

:white_check_mark: Disable/Enable Model Events on demand

# The library is following the MVC pattern

<br />
<p align="center">	
	<img src="/Docs/MVC_Pattern.png" alt="Download">
</p>

## The View

Any representation of information such as a chart, diagram or table. Multiple views of the same information are possible, such as a bar chart for management and a tabular view for accountants.

## The Model

The central component of the pattern. It is the application's dynamic data structure, independent of the user interface. It directly manages the data, logic and rules of the application.

## The Controller

Accepts input and converts it to commands for the model or view.

## Resume

In addition to dividing the application into these components, the model–view–controller design defines the interactions between them.

- The model is responsible for managing the data of the application. It receives user input from the controller.
- The view means presentation of the model in a particular format.
- The controller responds to the user input and performs interactions on the data model objects. The controller receives the input, optionally validates it and then passes the input to the model.

# How to get started

## Choose your installation method

| Requirements and use cases | Release Archive(s) |  
| -------------------------------------------------- | -------- |  
| **For users who need basic usage:** Pre built UMVC.Core dlls, fast setup | <a href="https://github.com/loic-lopez/UMVC/releases/download/v$VERSION$/UMVC.Editor.PreBuiltDlls.$VERSION$.unitypackage" target="_blank"><img src="https://img.shields.io/badge/Download-UMVC.Editor.PreBuiltDlls-blue"></a>
| **For users who need advanced usage:** customs Base Components, custom templates and more... | [![Download UMVC.Editor.MsBuildForUnity](https://img.shields.io/badge/Download-UMVC.Editor.MsBuildForUnity-blue)](https://github.com/loic-lopez/UMVC/releases/download/v$VERSION$/UMVC.Editor.MsBuildForUnity.$VERSION$.unitypackage)[![Download UMVC.Core Sources](https://img.shields.io/badge/Download-UMVC.Core-blue)](https://github.com/loic-lopez/UMVC/releases/download/v$VERSION$/UMVC.Core.$VERSION$.zip)  |  

## Basic usage using UMVC.Editor.PreBuiltDlls UnityPackage

* Download UMVC.Editor.PreBuiltDlls UnityPackage from from above

* Import UMVC.Editor.PreBuiltDlls UnityPackage in Unity Editor Assets > Import Package > Custom Package

## Advanced usage using UMVC.Editor.MsBuildForUnity UnityPackage and UMVC.Core sources

### Download and unzip sources

* Download UMVC.Editor.MsBuildForUnity UnityPackage from from above

* Download UMVC.Core sources from from above

* Import UMVC.Editor.MsBuildForUnity UnityPackage in Unity Editor Assets > Import Package > Custom Package

* Unzip UMVC.Core sources to your project next to your Assets folder

### Add MSBuildForUnity to your manifest.json
Add the `com.microsoft.msbuildforunity` UPM (Unity Package Manager) package.

- Edit the `Packages/manifest.json` file in your Unity project.
- Add the following near the top of the file:

```json
"scopedRegistries": [
    {
      "name": "Microsoft",
      "url": "https://pkgs.dev.azure.com/UnityDeveloperTools/MSBuildForUnity/_packaging/UnityDeveloperTools/npm/registry/",
      "scopes": [
        "com.microsoft"
      ]
    }
],
```

- Add the following to the `dependencies` section of the file:

```json
  "com.microsoft.msbuildforunity": "0.9.2-20200131.11",
```


# FAQ

**See the associated wiki: [UMVC Wiki](https://github.com/loic-lopez/UMVC/wiki)**
