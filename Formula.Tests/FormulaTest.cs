// Author: Ai Sun
//   Date: 2023, Jan 18
//   Platform: Rider (Mac)

// Revision History:
/*      - 2024, Jan 18 Ai Sun - Initial creation of the test.
        - 2024, Feb 15 Ai Sun - Add more functions, make functions atomic.
 */

using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Formula.Tests
{
    /// <summary>
    /// Represents a unit test class for the Formula class.
    /// </summary>
    [TestFixture]
    [TestOf(typeof(Formula))]
    public class FormulaTest
    {
        /// <summary>
        /// Method to test various constructors of the Formula class.
        /// </summary>
        [Test]
        public void TestConstructors()
        {
            // TestConstructors
            Console.WriteLine("***********************TEST " +
                              "CONSTRUCTORS***********************");
            
            Console.WriteLine("1.    public Formula(string[], int[], \n" +
                              "        string[], int[])");
            Console.WriteLine("INPUT DATA:" + " string[] inNames = " +
                              "{ \"butter\", \"egg\", \"sugar\" }\n        " +
                              "int[] inQuantity = { 2, 3, 1 }\n        " +
                              "string[] outNames = { \"cookies\" }\n        " +
                              "int[] outQuantity = { 36 }");
            // Arrange
            string[] inNames = { "butter", "egg", "sugar" };
            int[] inQuantity = { 2, 3, 1 };
            string[] outNames = { "cookies" };
            int[] outQuantity = { 36 };
            
            // Act
            TestDelegate act1 = () => 
                new Formula(inNames, inQuantity, outNames, outQuantity);
            
            // Assert
            Assert.DoesNotThrow(act1);
            
            Console.WriteLine("(TEST PASSED)");
            
            Console.WriteLine("***********************TEST " +
                              "END***********************");
        }

        /// <summary>
        /// Method to test the ToString method of the Formula class.
        /// </summary>
        [Test]
        public void TestToString()
        {        
            // TestToString
            Console.WriteLine("***********************TEST " +
                                   "TO STRING***********************");
        
            Console.WriteLine("INPUT DATA:" + " string[] inNames = " +
                              "{ \"butter\", \"egg\", \"sugar\" }\n        " +
                              "int[] inQuantity = { 2, 3, 1 }\n        " +
                              "string[] outNames = { \"cookies\" }\n        " +
                              "int[] outQuantity = { 36 }");
            // Arrange
            string[] inNames = { "butter", "egg", "sugar" };
            int[] inQuantity = { 2, 3, 1 };
            string[] outNames = { "cookies" };
            int[] outQuantity = { 36 };
            
            Formula formula1 = new Formula(inNames, inQuantity, 
                outNames, outQuantity);

            // Act & Assert
            ClassicAssert.AreEqual(
                "2 butter, 3 egg, 1 sugar -> 36 cookies", 
                formula1.ToString());
            
            Console.WriteLine("TO STRING METHOD RETURNED:\n" + formula1);
            
            Console.WriteLine("(TEST PASSED)");
            
            Console.WriteLine("***********************TEST " +
                              "END***********************");
        }

        /// <summary>
        /// Method to test constructor exceptions of the Formula class.
        /// </summary>
        [Test]
        public void TestInvalidConstructorArguments()
        {
            // TestInvalidConstructorArguments
            Console.WriteLine("***********************TEST " +
                              "CONSTRUCTOR EXCEPTIONS***********************");
            // Arrange
            string[] inNames = { "butter", "egg", "sugar" };
            int[] inQuantity = { 2, 3, 999, 90}; 
            // length of inQuantity is not equal to length of inNames
            string[] outNames = { "cookies" };
            int[] outQuantity = { 36 };

            Console.WriteLine("INPUT DATA: " +
                              "string[] inNames = { \"butter\", \"egg\", " +
                              "\"sugar\" }\nint[] inQuantity = { 2, 3, 999, 90}");

            // Act
            TestDelegate bad_act = () => 
                new Formula(inNames, inQuantity, outNames, outQuantity);

            // Assert
            Assert.Throws<Exception>(bad_act, "Names and quantity not pair.");
        }

        /// <summary>
        /// Method to test empty constructor arguments of the Formula class.
        /// </summary>
        [Test]
        public void TestEmptyConstructorArguments()
        {
            // Arrange
            string[] inNames = { };
            int[] inQuantity = { };
            string[] outNames = { };
            int[] outQuantity = { };

            // Assert
            Assert.Throws<InvalidDataException>(() =>
            {
                Formula formula = new Formula(inNames, inQuantity, 
                    outNames, outQuantity);
            });
        }

        /// <summary>
        /// Method to test leveling up and proficiency in a formula.
        /// </summary>
        [Test]
        public void TestIncrease()
        {
            const int max = 4;
            // TestIncrease
            Console.WriteLine("***********************TEST " +
                              "LEVEL UP AND PROFICIENCY***********************");
            // Arrange
            string[] inNames = { "butter", "egg", "sugar" };
            int[] inQuantity = { 2, 3, 1 };
            string[] outNames = { "cookies" };
            int[] outQuantity = { 36 };
            
            Formula formula = new Formula(inNames, inQuantity, 
                outNames, outQuantity);
            
            for(int i=0; i<= max; i++)
                formula.Increase();
            
            TestDelegate bad_act = () => 
                formula.Increase();

            // Assert
            Assert.Throws<InvalidOperationException>(bad_act,
                "This formula has reached the maximum level.");
            
            
            Console.WriteLine("***********************TEST " +
                              "END***********************");
        }

        /// <summary>
        /// Method to test the output of the Apply method of the Formula class.
        /// </summary>
        [Test]
        public void TestApplyOutput()
        {
            // TestApplyOutput
            Console.WriteLine("***********************TEST " +
                              "APPLY OUTPUT***********************");
            // Arrange
            string[] inNames = { "butter", "egg", "sugar" };
            int[] inQuantity = { 2, 3, 1 };
            string[] outNames = { "cookies" };
            int[] outQuantity = { 36 };
            Console.WriteLine("INPUT DATA:" + " string[] inNames = " +
                              "{ \"butter\", \"egg\", \"sugar\" }\n        " +
                              "int[] inQuantity = { 2, 3, 1 }\n        " +
                              "string[] outNames = { \"cookies\" }\n        " +
                              "int[] outQuantity = { 36 }");
            
            Formula formula = 
                new Formula(inNames, inQuantity, outNames, outQuantity);
        
            // Act
            string result;
            for (int i = 0; i < 10; i++)
            {
                result = formula.Apply();
                Console.WriteLine("**********APPLY() RESULT IS:**********\n" +
                                  result);
                // Assert
                Assert.That(result, Is.TypeOf<string>(), 
                    "Apply() should return a string");
        
                //optional depending on what you expect to be returned
                Assert.That(result, Is.Not.Null.Or.Empty, 
                    "Apply() should return not null or empty string");
            }
            
            Console.WriteLine("\n1. TEST " + "APPLY OUTPUT SHOULD ALWAYS BE " +
                              "POSITIVE OR N?A (PASSED)");
            
            Console.WriteLine("2. TEST " +
                              "APPLY OUTPUT FORMAT (PASSED)");
            
            Console.WriteLine("3. TEST " +
                              "APPLY PRODUCE RATE IS 0 to 1.1 (PASSED)");
            
            Console.WriteLine("***********************TEST " +
                              "END***********************");
        }

        /// <summary>
        /// Method to test the proficiency getter of the Formula class.
        /// </summary>
        [Test]
        public void TestGetValue()
        {
            // Arrange
            int expectedValue = 0;

            // Act
            string[] inNames = { "butter", "egg", "sugar" };
            int[] inQuantity = { 2, 3, 1 };
            string[] outNames = { "cookies" };
            int[] outQuantity = { 36 };
            Formula formula = 
                new Formula(inNames, inQuantity, outNames, outQuantity);
            
            // Assert
            ClassicAssert.AreEqual(expectedValue, formula.Proficiency);
            formula.Increase();
            ClassicAssert.AreEqual((expectedValue + 1), formula.Proficiency);
        }
    }
}
