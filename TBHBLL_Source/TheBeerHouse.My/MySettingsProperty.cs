namespace TheBeerHouse.My
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel.Design;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [DebuggerNonUserCode, CompilerGenerated, HideModuleName, StandardModule]
    internal sealed class MySettingsProperty
    {
        [HelpKeyword("My.Settings")]
        internal static MySettings Settings
        {
            get
            {
                return MySettings.Default;
            }
        }
    }
}

