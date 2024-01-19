// Author: Ai Sun
//   Date: 2023, Jan 18
//   Platform: Rider (Mac)

// Revision History:
/*      - 2023, Jan 18 Ai Sun - Initial creation of the class.
 */

using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Formula.Tests;

/// <summary>
/// Represents a unit test class for the Formula class.
/// </summary>
[TestFixture]
[TestOf(typeof(Formula))]
public class FormulaTest
{
    /// TestConstructors -
    /// Method to test various constructors of the Formula class.
    /// /
    [Test]
    public void TestConstructors()
    {
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
        
        Console.WriteLine("\n2.    public Formula(string input)");
        
        // Arrange
        string input = "2 iron ore -> 1 iron bar";
        Console.WriteLine($"INPUT DATA: {input}");
        
        // Act
        TestDelegate act2 = () => 
            new Formula(input);
        
        // Assert
        Assert.DoesNotThrow(act2);
        
        Console.WriteLine("(TEST PASSED)");
        
        Console.WriteLine("***********************TEST " +
                          "END***********************");
    }

    /// Test the ToString method of the Formula class.
    /// /
    [Test]
    public void TestToString()
    {        
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

        ClassicAssert.AreEqual(
            "2 butter, 3 egg, 1 sugar -> 36 cookies", 
            formula1.ToString());
        
        Console.WriteLine("TO STRING METHOD RETURNED:\n" + formula1);

        string input = "2 iron ore -> 1 iron bar";
        Console.WriteLine($"INPUT DATA: {input}");
        
        Formula formula2 = new Formula(input);
        
        Console.WriteLine("TO STRING METHOD RETURNED:\n" + formula2);

        ClassicAssert.AreEqual("2 iron ore -> 1 iron bar", 
            formula2.ToString());
        
        Console.WriteLine("(TEST PASSED)");
        
        Console.WriteLine("***********************TEST " +
                          "END***********************");
    }

    /// Test constructor exceptions of the Formula class.
    /// /
    [Test]
    public void TestConstructorExceptions()
    {
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
        TestDelegate bad_act1 = () => 
            new Formula(inNames, inQuantity, outNames, outQuantity);

        // Assert
        Assert.Throws<Exception>(bad_act1, "Names and quantity not pair.");
        
        Console.WriteLine("1. (Names and quantity not pair PASSED)");

        // Arrange
        string bad_input = "1000 water, 999 hydrogen, 1 deuterium"; 
        // missing " -> " in the input string

        // Act
        TestDelegate bad_act2 = () => new Formula(bad_input);

        // Assert
        Assert.Throws<Exception>(bad_act2, 
            "Initialize Formula instance failed.");
        
        Console.WriteLine("INPUT DATA: " +
                          "1000 water, 999 hydrogen, 1 deuterium");
        
        Console.WriteLine("2. (Initialize Formula instance failed PASSED)");
        
        // Arrange
        string bad_number = "-1000 water, 999 hydrogen, 1 deuterium"; 
        
        Console.WriteLine("INPUT DATA: " +
                          $"{bad_number}");
        
        // Act
        TestDelegate bad_act3 = () => new Formula(bad_input);
        
        // Assert
        Assert.Throws<Exception>(bad_act3, 
            "Create Material instance " +
                                        "water " +
                                        "failed. Quantity should greater than" +
                                        "zero.");
        
        Console.WriteLine("3. (Quantity should greater than zero PASSED)");
        
        Console.WriteLine("***********************TEST " +
                          "END***********************");
    }

    /// <summary>
    /// Unit test for the method TestLevelUp.
    /// </summary>
    /// <remarks>
    /// This method tests the functionality of leveling up and proficiency in a
    /// formula.
    /// </remarks>
    /// <param name="formula">The formula to be tested.</param>
    [Test]
    public void TestLevelUp()
    {
        Console.WriteLine("***********************TEST " +
                          "LEVEL UP AND PROFICIENCY***********************");
        // Arrange
        var formula = new Formula("3 iron ore, 1 coal -> 1 steel bar");
        
        // Act
        string proficiency;
        
        proficiency = formula.Proficiency;
        Console.WriteLine(formula.Proficiency);
        
        for(int i=0; i<100; i++)
        {
            formula.Apply();

            if (formula.Proficiency != proficiency)
                Console.WriteLine("*****LEVEL UP!*****\n" + 
                                  formula.Proficiency);
            
            proficiency = formula.Proficiency;
        
            // Assert - check proficiency level
            StringAssert.StartsWith("CURRENT PROFICIENCY: LEVEL ", 
                proficiency);
            int proficiencyLevel = int.Parse(proficiency.Split(' ')[3]);
            ClassicAssert.GreaterOrEqual(proficiencyLevel, 1, 
                "Proficiency should not be less than 1");
            
            ClassicAssert.LessOrEqual(proficiencyLevel, 5, 
                "Proficiency should not be greater than 5");
            
        }
        Console.WriteLine("\n1. Proficiency should not be less than 1 PASSED");
        
        Console.WriteLine("2. Proficiency should not be greater than 5 " +
                          "PASSED");
        
        Console.WriteLine("***********************TEST " +
                          "END***********************");
    }

    /// <summary>
    /// Tests the Apply method of the Formula class.
    /// </summary>
    [Test]
    public void TestApplyOutput()
    {
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
}