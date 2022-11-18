﻿using System;
using System.Runtime.Loader;
using System.Reflection;

namespace ASP.NETCoreWithEFCore.Api.Net.Extra
{
    public class CustomAssemblyLoadContext : AssemblyLoadContext
    {

        public IntPtr LoadUnmanagedLibrary(string absolutePath)
        {
            return LoadUnmanagedDll(absolutePath);
        }
        protected override IntPtr LoadUnmanagedDll(String unmanagedDllName)
        {
            return LoadUnmanagedDllFromPath(unmanagedDllName);
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            throw new NotImplementedException();
        }
    }
}
