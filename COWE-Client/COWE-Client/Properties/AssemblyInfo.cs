﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Virtual Machine Co-Residency Probe")]
[assembly: AssemblyDescription("The Virtual Machine Co-Residency Probe is a tool " +
"that can be used to detect two virtual machines " +
"(VMs) whichreside on the same physical host.  The " +
"Client communicates with a Flooder via socket " +
"connection.  The Flooder cycles UDP packet " + 
"streaming.  The Client contacts the Server, and  " +
"captures network traffic from the Server.  If the  " +
"Flooder and the Server are co-resident, the network " +
"traffic pattern at the Client will be affected by the  " +
"UDP streaming, and co-residency will be detected.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("TSU Department of Engineering, CISE (MS) Program")]
[assembly: AssemblyProduct("Virtual Machine Co-Residency Probe")]
[assembly: AssemblyCopyright("Copyright © James A. Savage 2015")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("8b7cb2f6-852b-48e0-b445-118ad254a1d3")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
