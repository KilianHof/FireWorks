using Microsoft.VisualStudio.TestTools.UnitTesting;
using FireWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks.Tests
{
    [TestClass()]
    public class AuthenticatorTests
    {
        [TestMethod()]
        public void IsValidInputTestSucceed()
        {
            TUI _t = new TUI();
            FileIO _filer = new FileIO(_t);
            Authenticator auth = new Authenticator(_t, _filer);
            Assert.IsTrue(auth.IsValidInput("1234"));
        }
        [TestMethod()]
        public void IsValidInputTestInvalid()
        {
            TUI _t = new TUI();
            FileIO _filer = new FileIO(_t);
            Authenticator auth = new Authenticator(_t, _filer);
            Assert.IsFalse(auth.IsValidInput("1x#"));
        }
    }
}