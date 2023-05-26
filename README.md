<br />

<p align="center">
     <img src="https://raw.githubusercontent.com/loic-lopez/UMVC/master/Docs/logo_transparent.png" alt="UMVC">
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
	<div align="center">
	  <h4 align="center">UMVC.Editor Metrics</h4>
	  <a href="https://codecov.io/gh/loic-lopez/UMVC">
           <img src="https://codecov.io/gh/loic-lopez/UMVC/branch/master/graph/badge.svg" />
          </a>
           <a href="https://github.com/loic-lopez/UMVC/actions?query=workflow%3AUMVC.Editor">
	    <img src="https://github.com/loic-lopez/UMVC/workflows/UMVC.Editor/badge.svg">
	   </a> 
        </div>
	<div align="center">
		<h4 align="center">UMVC.Core Metrics</h4>
		<a href="https://sonarcloud.io/summary/overall?id=UMVC.Core">
		   <img src="https://sonarcloud.io/api/project_badges/measure?project=UMVC.Core&metric=alert_status" />
		</a>
		<a href="https://sonarcloud.io/summary/overall?id=UMVC.Core">
		   <img src="https://sonarcloud.io/api/project_badges/measure?project=UMVC.Core&metric=code_smells" />
		</a>
		<a href="https://sonarcloud.io/summary/overall?id=UMVC.Core">
		   <img src="https://sonarcloud.io/api/project_badges/measure?project=UMVC.Core&metric=bugs" />
		</a>
		<a href="https://github.com/loic-lopez/UMVC/actions?query=workflow%3AUMVC.Core">
		    <img src="https://github.com/loic-lopez/UMVC/workflows/UMVC.Core/badge.svg">
		</a>
	</div>
</p>
<hr>

# Requirements

- Unity 2021.x
- com.solidalloy.type-references (as version v0.5.0)

# Install com.solidalloy.type-references (as version v0.5.0)

- Edit the `Packages/manifest.json` file in your Unity project.
- Add the following near the top of the file:

```json
"scopedRegistries": [
   {
      "name":"package.openupm.com",
      "url":"https://package.openupm.com",
      "scopes":[
         "com.solidalloy.util",
         "com.solidalloy.unity-dropdown",
         "com.solidalloy.type-references",
         "com.openupm",
         "org.nuget"
      ]
   }
],
```

- Add the following to the `dependencies` section of the file:

```json
  "com.solidalloy.type-references": "2.16.0",
```

# Features

### Overall

✅ Customizable T4 Templates

✅ Customizable Base Templates

✅ Generate your architecture directly in the Unity Editor

### Model

✅ Model Events
```csharp
// triggered before the update of the field
OnFieldWillUpdate(Model model, object newValue, object oldValue, PropertyChangedEventArgs eventArgs)
```
```csharp
// triggered after the update of the field
OnFieldDidUpdate(Model model, object newValue, PropertyChangedEventArgs eventArgs)
```

✅ Generate field when creating a Model

✅ Disable/Enable Model Events on demand

# The library is following the MVC pattern

<br />
<p align="center">	
	<img src="https://raw.githubusercontent.com/loic-lopez/UMVC/master/Docs/MVC_Pattern.png" alt="MVC Pattern">
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
|----------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|    
| **With Unity assets store (this version uses pre built UMVC.Core dlls)**                     | [UMVC - Model-View-Controller Generator built for Unity Unity Asset Store](https://u3d.as/1WKL)                                                                                                                                                                                                                                                                                                      |  
| **For users who need basic usage:** Pre built UMVC.Core dlls, fast setup | <a href="https://github.com/loic-lopez/UMVC/releases/download/v0.5.3/UMVC.Editor.PreBuiltDlls.0.5.3.unitypackage" target="_blank"><img src="https://img.shields.io/badge/Download-UMVC.Editor.PreBuiltDlls-blue"></a>
| **For users who need advanced usage:** customs Base Components, custom templates and more... | [![Download UMVC.Editor.MsBuildForUnity](https://img.shields.io/badge/Download-UMVC.Editor.MsBuildForUnity-blue)](https://github.com/loic-lopez/UMVC/releases/download/v0.5.3/UMVC.Editor.MsBuildForUnity.0.5.3.unitypackage)[![Download UMVC.Core Sources](https://img.shields.io/badge/Download-UMVC.Core-blue)](https://github.com/loic-lopez/UMVC/releases/download/v0.5.3/UMVC.Core.0.5.3.zip) 

## Basic usage using UMVC.Editor.PreBuiltDlls UnityPackage

* Download UMVC.Editor.PreBuiltDlls UnityPackage from above

* Import UMVC.Editor.PreBuiltDlls UnityPackage in Unity Editor Assets > Import Package > Custom Package

## Advanced usage using UMVC.Editor.MsBuildForUnity UnityPackage and UMVC.Core sources

### Download and unzip sources

* Download UMVC.Editor.MsBuildForUnity UnityPackage from above

* Download UMVC.Core sources from above

* Import UMVC.Editor.MsBuildForUnity UnityPackage in Unity Editor Assets > Import Package > Custom Package

* Unzip UMVC.Core sources to your project next to your Assets folder

### Add MSBuildForUnity to your manifest.json
Add the `com.microsoft.msbuildforunity` UPM (Unity Package Manager) package.

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
  "com.microsoft.msbuildforunity": "0.9.2-20211109.1",
```


# FAQ

**See the associated wiki: [UMVC Wiki](https://github.com/loic-lopez/UMVC/wiki)**


