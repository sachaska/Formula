// Author: Ai Sun
// Date: 2023, Jan 18
// Platform: Rider (Mac)

// Revision History:
/* - 2024, Feb 15 Ai Sun - Initial creation of the test. */

using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Formula.Tests
{
    /// <summary>
    /// Represents a unit test class for the Plan class.
    /// </summary>
    [TestFixture]
    [TestOf(typeof(Plan))]
    public class PlanTest
    {
        /// <summary>
        /// Method to test various constructors of the Plan class.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            // Constructor test
            // Arrange
            Formula formulaA = new Formula(new string[]{"butter"}, new int[]{2}, new string[]{"cookies"}, new int[]{36});
            Formula formulaB = new Formula(new string[]{"sugar"}, new int[]{1}, new string[]{"candy"}, new int[]{50});
            Formula[] formulas = new Formula[]{formulaA, formulaB};

            // Act
            void Act1() => new Plan(formulas);

            // Assert
            Assert.DoesNotThrow(Act1);
        }

        /// <summary>
        /// Method to test the DeepCopy method of the Plan class.
        /// </summary>
        [Test]
        public void TestDeepCopy()
        {
            // Arrange
            Formula formulaA = new Formula(new string[]{"butter"}, new int[]{2}, new 
                string[]{"cookies"}, new int[]{36});
            Formula[] formulas = new Formula[]{formulaA};

            Plan originalPlan = new Plan(formulas);
            Plan deepcopyPlan = originalPlan.DeepCopy();

            // Assert
            Assert.That(deepcopyPlan, Is.Not.SameAs(originalPlan));
        }

        /// <summary>
        /// Method to test the Add method of the Plan class.
        /// </summary>
        [Test]
        public void TestAdd()
        {
            // Arrange
            Formula formulaA = new Formula(new string[]{"butter"}, new int[]{2}, new string[]{"cookies"}, new int[]{36});
            Formula[] formulas = new Formula[]{formulaA};

            Plan plan = new Plan(formulas);

            Formula formulaB = new Formula(new string[]{"sugar"}, new int[]{1}, new 
                string[]{"candy"}, new int[]{50});
            plan.Add(formulaB);

            // Assert
            StringAssert.Contains("sugar", plan.ToString());
        }

        /// <summary>
        /// Method to test the Replace method of the Plan class.
        /// </summary>
        [Test]
        public void TestReplace()
        {
            // Arrange
            Formula formulaA = new Formula(new string[]{"butter"}, new int[]{2}, new string[]{"cookies"}, new int[]{36});
            Formula[] formulas = new Formula[]{formulaA};

            Plan plan = new Plan(formulas);

            Formula formulaB = new Formula(new string[]{"sugar"}, new int[]{1}, new string[]{"candy"}, new int[]{50});
            plan.Replace(0, formulaB);

            // Assert
            StringAssert.Contains("sugar", plan.ToString());
        }

        /// <summary>
        /// Method to test adding a null formula to the Plan class.
        /// </summary>
        [Test]
        public void TestAddNullFormula()
        {
            // Arrange
            Plan plan = new Plan();

            // Act
            void Act() => plan.Add(null);

            // Assert
            Assert.Throws<ArgumentNullException>(Act);
        }

        /// <summary>
        /// Method to test replacing a formula with an index out of range in the Plan class.
        /// </summary>
        [Test]
        public void TestReplaceIndexOutOfRange()
        {
            // Arrange
            Plan plan = new Plan();
            Formula formula = new Formula(new string[] { "butter" }, new int[] { 2 }, new string[] { "cookies" }, new int[] { 36 });

            // Act
            void Act() => plan.Replace(1, formula);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(Act);
        }

        /// <summary>
        /// Method to test replacing a formula with a null formula in the Plan class.
        /// </summary>
        [Test]
        public void TestReplaceNullFormula()
        {
            // Arrange
            Plan plan = new Plan();
            Formula formula = new Formula(new string[] { "butter" }, new int[] { 2 }, new string[] { "cookies" }, new int[] { 36 });

            // Act
            void Act() => plan.Replace(0, null);

            // Assert
            Assert.Throws<ArgumentNullException>(Act);
        }

        /// <summary>
        /// Method to test removing from an empty list in the Plan class.
        /// </summary>
        [Test]
        public void TestRemoveEmptyList()
        {
            // Arrange
            Plan plan = new Plan();

            // Act
            void Act() => plan.Remove();

            // Assert
            Assert.Throws<InvalidOperationException>(Act);
        }

        /// <summary>
        /// Method to test the getter and setter for Sequences property in the Plan class.
        /// </summary>
        [Test]
        public void TestSequencesGetterSetter()
        {
            // Arrange
            Plan plan = new Plan();
            Formula[] formulas = new Formula[]
            {
                new Formula(new string[] { "butter" }, new int[] { 2 }, new string[] { "cookies" }, new int[] { 36 }),
                new Formula(new string[] { "sugar" }, new int[] { 1 }, new string[] { "candy" }, new int[] { 50 })
            };

            // Act
            plan.Sequences = formulas;
            Formula[] result = plan.Sequences;

            // Assert
            CollectionAssert.AreEqual(formulas, result);
        }

        /// <summary>
        /// Method to test removing the last formula in the Plan class.
        /// </summary>
        [Test]
        public void TestRemove()
        {
            // Arrange
            Formula formulaA = new Formula(new string[]{"butter"}, new int[]{2}, new string[]{"cookies"}, new int[]{36});
            Formula[] formulas = new Formula[]{formulaA};

            Plan plan = new Plan(formulas);
            plan.Remove();

            // Assert
            ClassicAssert.IsEmpty(plan.ToString());
        }

        /// <summary>
        /// Method to test converting the Plan to a string representation in the Plan class.
        /// </summary>
        [Test]
        public void TestToString()
        {
            // Arrange
            Formula formulaA = new Formula(new string[]{"butter"}, new int[]{2}, new string[]{"cookies"}, new int[]{36});
            Formula[] formulas = new Formula[]{formulaA};

            Plan plan = new Plan(formulas);
            string result = plan.ToString();

            // Assert
            StringAssert.Contains("butter", result);
        }
    }
}
