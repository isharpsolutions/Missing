using System;
using NUnit.Framework;
using Missing.Security.Authentication;
using System.Collections.Generic;

namespace Missing
{
	[TestFixture]
	public class RoleCollectionTests
	{
		[Test]
		public void HasPrivilege_True()
		{
			RoleCollection roles = new RoleCollection() {
				new Role() {
					Name = "NotInThis",
					Privileges = new List<string>() {
						"NotThis",
						"NotThisEither"
					}
				},
				new Role() {
					Name = "InThis",
					Privileges = new List<string>() {
						"ButNotThis",
						"ThisIsWhatWeWant"
					}
				}
			};
			
			Assert.IsTrue(roles.HasPrivilege("ThisIsWhatWeWant"));
		}
		
		[Test]
		public void HasPrivilege_False()
		{
			RoleCollection roles = new RoleCollection() {
				new Role() {
					Name = "NotInThis",
					Privileges = new List<string>() {
						"NotThis",
						"NotThisEither"
					}
				},
				new Role() {
					Name = "InThis",
					Privileges = new List<string>() {
						"ButNotThis",
						"ThisIsWhatWeWant"
					}
				}
			};
			
			Assert.IsFalse(roles.HasPrivilege("ThisPrivilegeDoesNotExistInAnyOfTheRoles"));
		}
		
		[Test]
		public void HasPrivilege_Empty()
		{
			RoleCollection roles = new RoleCollection();
			
			Assert.IsFalse(roles.HasPrivilege("ThisPrivilegeDoesNotExistInAnyOfTheRoles"));
		}
		
		[Test]
		public void HasPrivilege_RolesWithNoPrivileges()
		{
			RoleCollection roles = new RoleCollection() {
				new Role() {
					Name = "NotInThis"
				},
				new Role() {
					Name = "InThis"
				}
			};
			
			Assert.IsFalse(roles.HasPrivilege("ThisPrivilegeDoesNotExistInAnyOfTheRoles"));
		}
	}
}