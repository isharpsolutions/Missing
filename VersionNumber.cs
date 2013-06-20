using System.Reflection;
using System.Runtime.CompilerServices;

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
[assembly: AssemblyVersion("0.19.0")]