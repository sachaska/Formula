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
    /// Represents a unit test class for the ExecutablePlan class.
    /// </summary>
    [TestFixture]
    [TestOf(typeof(ExecutablePlan))]
    public class ExecutablePlanTest
    {
        /// <summary>
        /// Method to test constructor of the ExecutablePlan class.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            // Arrange
            Formula formulaA = new Formula(new string[] { "butter" }, new int[] { 2 }, new string[] { "cookies" }, new int[] { 36 });
            Formula formulaB = new Formula(new string[] { "sugar" }, new int[] { 1 }, new string[] { "candy" }, new int[] { 50 });
            Formula[] formulas = new Formula[] { formulaA, formulaB };

            // Act
            void Act() => new ExecutablePlan(formulas);

            // Assert
            Assert.DoesNotThrow(Act);
        }
        
        /// <summary>
        /// Method to test the Query method of the ExecutablePlan class.
        /// </summary>
        [Test]
        public void TestQuery()
        {
            // Arrange
            ExecutablePlan plan = new ExecutablePlan(new Formula[]
            {
                new Formula(new string[] { "butter" }, new int[] { 2 },
                    new string[] { "cookies" }, new int[] { 36 }),
                new Formula(new string[] { "sugar" }, new int[] { 1 },
                    new string[] { "candy" }, new int[] { 50 })
            });

            // Assert
            ClassicAssert.AreEqual(plan.Query(), "(1). 2 butter -> 36 cookies");
            plan.Apply();
            ClassicAssert.AreEqual(plan.Query(), "(2). 1 sugar -> 50 candy");
        }

        /// <summary>
        /// Method to test the Apply method of the ExecutablePlan class.
        /// </summary>
        [Test]
        public void TestApply()
        {
            // Arrange
            ExecutablePlan plan = new ExecutablePlan(new Formula[] {
                new Formula(new string[] { "butter" }, new int[] { 2 }, new string[] { "cookies" }, new int[] { 36 }),
                new Formula(new string[] { "sugar" }, new int[] { 1 }, new string[] { "candy" }, new int[] { 50 })
            });
            
            // Act & Assert
            void Act() => plan.Apply();
            Assert.DoesNotThrow(Act);
            Console.WriteLine(plan.Query());
        }

        /// <summary>
        /// Method to test the Replace method in ExecutablePlan class.
        /// </summary>
        [Test]
        public void TestReplace()
        {
            // Arrange
            ExecutablePlan plan = new ExecutablePlan(new Formula[] {
                new Formula(new string[] { "butter" }, new int[] { 2 }, new string[] { "cookies" }, new int[] { 36 }),
                new Formula(new string[] { "sugar" }, new int[] { 1 }, new string[] { "candy" }, new int[] { 50 })
            });

            // Act & Assert
            Assert.DoesNotThrow(() => plan.Replace(1, new Formula(new string[] { "chocolate" }, new int[] { 1 }, new string[] { "cake" }, new int[] { 20 })));
        }
        /// <summary>
        /// Method to test Replace completed formula in ExecutablePlan class.
        /// </summary>
        [Test]
        public void TestBadReplace()
        {
            // Arrange
            ExecutablePlan plan = new ExecutablePlan(new Formula[] {
                new Formula(new string[] { "butter" }, new int[] { 2 }, new string[] { "cookies" }, new int[] { 36 }),
                new Formula(new string[] { "sugar" }, new int[] { 1 }, new string[] { "candy" }, new int[] { 50 })
            });
            
            plan.Apply();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => plan.Replace(0, new Formula(new string[] { "chocolate" }, new int[] { 1 }, new string[] { "cake" }, new int[] { 20 })));
        }

        /// <summary>
        /// Method to test the Remove method in ExecutablePlan class.
        /// </summary>
        [Test]
        public void TestRemove()
        {
            // Arrange
            ExecutablePlan plan = new ExecutablePlan(new Formula[] {
                new Formula(new string[] { "butter" }, new int[] { 2 }, new string[] { "cookies" }, new int[] { 36 }),
                new Formula(new string[] { "sugar" }, new int[] { 1 }, new string[] { "candy" }, new int[] { 50 })
            });

            // Act & Assert
            Assert.DoesNotThrow(plan.Remove);
        }
        
        /// <summary>
        /// Method to test the Remove completed formula in ExecutablePlan class.
        /// </summary>
        [Test]
        public void TestBadRemove()
        {
            // Arrange
            ExecutablePlan plan = new ExecutablePlan(new Formula[] {
                new Formula(new string[] { "butter" }, new int[] { 2 }, new string[] { "cookies" }, new int[] { 36 }),
                new Formula(new string[] { "sugar" }, new int[] { 1 }, new string[] { "candy" }, new int[] { 50 })
            });

            plan.Apply();
            plan.Apply();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(plan.Remove);
        }

        /// <summary>
        /// Method to test the Clone method in the ExecutablePlan class.
        /// </summary>
        [Test]
        public void TestClone()
        {
            // Arrange
            ExecutablePlan plan = new ExecutablePlan(new Formula[] {
                new Formula(new string[] { "butter" }, new int[] { 2 }, new string[] { "cookies" }, new int[] { 36 }),
                new Formula(new string[] { "sugar" }, new int[] { 1 }, new string[] { "candy" }, new int[] { 50 })
            });

            // Act
              ExecutablePlan clone = plan.Clone();

            // Assert
           ClassicAssert.AreNotSame(plan, clone);
           ClassicAssert.AreEqual(plan.Sequences.Length, clone.Sequences.Length);
           ClassicAssert.AreEqual(plan.Query(), clone.Query());
        }
        
    }
}