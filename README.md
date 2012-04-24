Missing
=======

This provides a library that encapsulates common tasks that we (iSharp Solutions) use in almost all of our projects.

It is based on an internal library that we now wish to share with the world and our clients.

Our goal is to make a lightweight library that is easy to use and understand and does not get in your way.


Platform support
----------

The library works equally well on both Microsoft .NET and Mono.


Versioning
----------

Releases will be numbered with the follow format:

`<major>.<minor>.<patch>`

And constructed with the following guidelines:

* Addition of new namespaces bumps the major
* Additions in existing namespaces bumps the minor
* Bug fixes and misc that does not change the public API bump the patch

We strive to never break backwards-compatibility. Once the API is public it must not change.

Some APIs might need to be replaced over time. This will be done by adding new APIs with different names
and marking the old APIs as "deprecated" and later as "obsolete", and only removing them from the code if
we learn that no one is using them.

**The APIs should be considered unstable until we reach version 1.0.0**

Until we reach version 1.0.0 namespace additions will bump the minor.


Missing.Data.Persistance
----------
All of the code in here has been ported from the S#arp lite project. We have "replaced" the dependency on 
System.Web.Mvc with a dependency on Common Service Locator. All credits go the original developers of the
S#arp lite project!