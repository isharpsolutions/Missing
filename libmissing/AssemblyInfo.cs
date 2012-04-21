using System.Reflection;
using System.Runtime.CompilerServices;

// Information about this assembly is defined by the following attributes. 
// Change them to the values specific to your project.

[assembly: AssemblyTitle("Missing")]
[assembly: AssemblyDescription("Contains methods and classes missing from .NET/Mono")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.


/**
 * Releases will be numbered with the follow format:
 * <major>.<minor>.<patch>
 *
 * And constructed with the following guidelines:
 *
 * Addition of new namespaces bumps the major
 * Additions in existing namespaces bumps the minor
 * Bug fixes and misc that does not change the public API bump the patch
 */
[assembly: AssemblyVersion("0.8.5")]

[assembly: InternalsVisibleTo("libmissing-tests")]

// The following attributes are used to specify the signing key for the assembly, 
// if desired. See the Mono documentation for more information about signing.

//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]

