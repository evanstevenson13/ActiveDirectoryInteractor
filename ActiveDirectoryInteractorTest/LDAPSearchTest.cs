

using ActiveDirectoryInteractor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ActiveDirectoryInteractorTest {
    [TestClass]
    public class LDAPSearchTest{

        //todo Make more tests
        
        /// <summary>
        /// Checks that the correct ldap uri is generated
        /// </summary>
        [TestMethod]
        public void CorrectLDAPURI(){
            LDAPSearch.Initialize("sub1.sub2.sub3.sub4.main", "", "");
            string expected = "LDAP://sub1.sub2.sub3.sub4.main/DC=sub1,DC=sub2,DC=sub3,DC=sub4,DC=main";
            Assert.AreEqual(expected, LDAPSearch.fullPath);
        }
    }
}
