using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Formula.Tests;

[TestFixture]
[TestOf(typeof(Formula))]
public class FormulaTest
{
    [Test]
    public void Test1()
{
    // Arrange
    string[] inNames = { "butter", "egg", "sugar" };
    int[] inQuantity = { 2, 3, 1 };
    string[] outNames = { "cookies" };
    int[] outQuantity = { 36 };
    Formula formula = new Formula(inNames, inQuantity, outNames, outQuantity);
    // Act
    string result;
    for (int i = 0; i < 100; i++)
    {
        result = formula.Apply(inNames, inQuantity);
        Console.WriteLine(result);
    }
}

    // [Test]
    // public void Formula_Constructor_ValidInput_Test()
    // { Formula formula =
    //         new Formula("2 butter, 3 egg, 1 sugar, 2 flour," +
    //                     " 2 baking soda -> 36 cookies");
    //     ClassicAssert.NotNull(formula);
    // }
    //
    // [Test]
    // public void Formula_Constructor_InvalidInput_Test()
    // {
    //     Assert.Throws<Exception>(() => new Formula("invalid input"));
    // }
    //
    // [Test]
    // public void Formula_Apply_ValidInput_Test()
    // {
    //     var formula =
    //         new Formula(
    //             "2 butter, 3 egg, 1 sugar, 2 flour, 2 baking soda ->" +
    //             " 36 cookies");
    //     string result =
    //         formula.Apply("2 butter, 3 egg, 1 sugar, 2 flour," +
    //                       " 2 baking soda");
    //     ClassicAssert.True(result.StartsWith("PRODUCE RATE:"));
    // }
    //
    // [Test]
    // public void Formula_Apply_InvalidInput_Test()
    // {
    //     var formula =
    //         new Formula(
    //             "1000 water -> 999 hydrogen, 1 deuterium");
    //     Assert.Throws<Exception>(() => formula.Apply("invalid " +
    //         "input"));
    // }
    //
    // [Test]
    // public void Formula_ToString_Test()
    // {
    //     var formula =
    //         new Formula(
    //             "2 butter, 3 egg, 1 sugar, 2 flour, 2 baking soda" +
    //             " -> 36 cookies");
    //     string result = formula.ToString();
    //     ClassicAssert.Equals(
    //         "2 butter, 3 egg, 1 sugar, 2 flour, 2 baking soda -> 36 cookies",
    //         result);
    // }
}