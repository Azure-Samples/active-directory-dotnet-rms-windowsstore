---
services: active-directory
platforms: dotnet
author: yaircohen
---

# Sample application for Microsoft RMS SDK v4.1 for Windows Store Applications ##

The Sample for Microsoft RMS SDK v4.1 for Windows Store Applications provides a basic document consumption example for the platform. 

## Features

This Application can do the following:

 - Consume pjpg, ptxt, pfile protected to the cloud.


##How to use this sample application

Prerequisites 

You must have installed the following software 

 - Git for Windows 
 - RMS UI SDK for Windows Store Applications([UI SDK git](https://github.com/AzureAD/rms-sdk-ui-for-windowsstore))
 - RMS SDK for Windows Store Applications([SDK download site](http://go.microsoft.com/fwlink/?LinkId=526163)) 
 - ADAL for Windows Store Applications ([ADAL download site](https://www.nuget.org/packages/Microsoft.IdentityModel.Clients.ActiveDirectory)) 
 - Visual studio 12.0 and above with Windows Store Developer SDK 
 - 
##Setting up development environment

 - Create a new windows store application, with Microsoft Visual C++ Runtime package for Windows. 
 - Download directly, or via nugget package, the ADAL SDK from [here](https://www.nuget.org/packages/Microsoft.IdentityModel.Clients.ActiveDirectory).
 - Download the RMS SDK v4.1 for Windows Store Applications from [here](http://go.microsoft.com/fwlink/?LinkId=526163)
 - Get the latest UI Library drop: "git clone git@github.com:AzureAD/rms-sdk-ui-for-windowsstore.git"
 - Add references to libraries in your project, (if you used the nugget command to get the ADAL SDK you will need to add 

##Additional information

Client Id and Redirection Uri must be set according to the ADAL library's specifications [here](https://github.com/azureadsamples/nativeclient-dotnet) - For domain joined onprem scenarios should be registered in the local ADFS. 
