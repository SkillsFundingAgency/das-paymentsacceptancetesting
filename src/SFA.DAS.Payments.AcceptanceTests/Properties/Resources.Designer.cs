﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SFA.DAS.Payments.AcceptanceTests.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SFA.DAS.Payments.AcceptanceTests.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to IF NOT EXISTS (SELECT [schema_id] FROM sys.schemas WHERE [name] = &apos;AT&apos;)
        ///	BEGIN
        ///		EXEC(&apos;CREATE SCHEMA AT&apos;)
        ///	END
        ///GO
        ///----------------------------------------------------------------------------------------------------------------------------
        ///-- TestRuns
        ///----------------------------------------------------------------------------------------------------------------------------
        ///IF NOT EXISTS (SELECT [object_id] FROM sys.tables WHERE [name] = &apos;TestRuns&apos; AND [schema_id] = SCHEMA_ID(&apos;AT&apos;))
        ///	BEGIN
        ///		CREATE TABLE [AT [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ddl_AT_deds_tables {
            get {
                return ResourceManager.GetString("ddl_AT_deds_tables", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to IF NOT EXISTS (SELECT [object_id] FROM sys.tables WHERE [name]=&apos;FileDetails&apos; AND [schema_id]=SCHEMA_ID(&apos;dbo&apos;))
        ///	BEGIN
        ///		CREATE TABLE [dbo].[FileDetails](
        ///			[ID] [int] IDENTITY(1,1) NOT NULL,
        ///			[UKPRN] [int] NOT NULL,
        ///			[Filename] [nvarchar](50) NULL,
        ///			[FileSizeKb] [bigint] NULL,
        ///			[TotalLearnersSubmitted] [int] NULL,
        ///			[TotalValidLearnersSubmitted] [int] NULL,
        ///			[TotalInvalidLearnersSubmitted] [int] NULL,
        ///			[TotalErrorCount] [int] NULL,
        ///			[TotalWarningCount] [int] NULL,
        ///			[SubmittedTi [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ilr_deds {
            get {
                return ResourceManager.GetString("ilr_deds", resourceCulture);
            }
        }
    }
}
