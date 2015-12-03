using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MorseLib;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class MorseTests
    {
        [Test]
        public static void Morse_roundtrip_should_work()
        {
            string test = "Hello world!";

            var morse = MorseConverter.ToMorse(test);

            var back = MorseConverter.FromMorse(morse);

            Assert.IsTrue(String.Equals(test, back, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
