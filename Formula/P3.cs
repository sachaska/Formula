// Author: Ai Sun
//   Date: 2023, Feb 14
//   Platform: Rider (Mac)

// Revision History:
/*      - 2023, Feb 14 Ai Sun - Initial creation of the class.
 */

/*
 * Purpose: This file is used to test the functionality of the Plan class and the ExecutablePlan class.
 * 
 * Input: The input to this program is a set of Formula objects that are used to
 * create and manipulate a Plan object and an ExecutablePlan object.
 * Process:
 * - It first creates instances of Plan and ExecutablePlan classes with given formulas.
 * - It then uses these instances to perform a series of operations including
 * adding new formulas, removing the last formula, replacing an existing formula,
 * and in the case of the ExecutablePlan, applying the formulas.
 * Each operation is followed by outputting the current state of the Plan
 * or ExecutablePlan instance to the console.
 * Output: The output of this program is a series of messages that indicate
 * the operations performed and the current state of the Plan or ExecutablePlan
 * instances after each operation. This allows for verification of the behavior
 * of the Plan and ExecutablePlan classes and their methods.
 * Note: This program does not include any error handling or user input.
 * All data and operations are hardcoded for testing purposes.
 */

using System;
using System.Collections.Generic;
using Formula;

public class P3
{
    // Main method starts here.
    static void Main(string[] args)
    {
        List<Plan> plans = new List<Plan>();

        // Instantiate a variety of objects
        Plan parPlan = new Plan(new Formula.Formula[] {
            new Formula.Formula(new string[] { "butter", "milk" }, new int[] 
                { 2, 10 }, new string[] { "cookies" }, new int[] { 36 }),
            new Formula.Formula(new string[] { "sugar" }, new int[] { 1 }, new string[] { "candy" }, new int[] { 50 }),
            new Formula.Formula(new string[] { "apple", "sugar", "butter", "water" }, new int[] 
                { 2, 10, 30, 5 }, new string[] { "apple pie" }, new int[] { 10 }),
        });

        ExecutablePlan exePlan = new ExecutablePlan(new Formula.Formula[] {
            new Formula.Formula(new string[] { "flour", "yeast", "sugar" }, new int[] 
                { 10, 2, 1 }, new string[] { "bread" }, new int[] { 12 }),
            new Formula.Formula(new string[] { "chocolate" }, new int[] { 5 }, new string[] { "cake" }, new int[] { 24 }),
            new Formula.Formula(new string[] { "butter", "milk" }, new int[] 
                { 2, 10 }, new string[] { "cookies" }, new int[] { 36 }),
            new Formula.Formula(new string[] { "sugar" }, new int[] { 1 }, new string[] { "candy" }, new int[] { 50 }),
            new Formula.Formula(new string[] { "apple", "sugar", "butter", "water" }, new int[] 
                { 2, 10, 30, 5 }, new string[] { "apple pie" }, new int[] { 10 })
        });
        
        Plan emptyPlan = new Plan();
        
        // Add plans to list
        plans.Add(parPlan);
        plans.Add(exePlan);
        plans.Add(emptyPlan);

        // Common and variant behaviors
        foreach (Plan plan in plans)
        {
            Console.WriteLine($"***********Formula(s) in {plan}***********");
            Console.WriteLine(plan.ToString());

            // Add a new formula
            Console.WriteLine($"*******Add new formula to {plan}***********");
            plan.Add(new Formula.Formula(new string[] { "milk" }, new int[] { 2 }, new string[] { "pancakes" }, new int[] { 6 }));
            Console.WriteLine(plan.ToString());

            Console.WriteLine($"******Remove last formula from {plan}******");
            // Remove last formula
            plan.Remove();
            Console.WriteLine(plan.ToString());

            Console.WriteLine($"******Replace second formula from {plan}******");
            // Replace the formula at index 1 with a new formula
            plan.Replace(1, new Formula.Formula(new string[] { "chocolate", "sugar" }, new int[] { 3, 2 }, new string[] { "brownie" }, new int[] { 20 }));
            Console.WriteLine(plan.ToString());
            
            Console.WriteLine($"******Call Query function from {plan}******");
            Console.WriteLine($"******Call Apply function from {plan}******");
            // Query (should be available only for ExecutablePlan instances)
            if (plan is ExecutablePlan executablePlan)
            {
                Console.WriteLine(executablePlan.Query());

                // Apply (only applicable for ExecutablePlan)
                Console.WriteLine(executablePlan.Apply());
                Console.WriteLine(executablePlan.Query());

                // Additional tests for ExecutablePlan
                Console.WriteLine($"******Try replacing complete step in {plan}******");
                try
                {
                    executablePlan.Apply();
                    executablePlan.Replace(0, new Formula.Formula(new string[] { "sugar" }, new int[] { 1 }, new string[] { "candy" }, new int[] { 50 }));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }

                Console.WriteLine($"******Try removing complete current step in {plan}******");
                try
                {
                    for (int i = 1; i < executablePlan.Sequences.Length; i++)
                    {
                        executablePlan.Apply();
                    }
                    
                    executablePlan.Remove();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"****(SKIP)****{plan} is instance of Plan.");
            }

            Console.WriteLine();
        }
    }
}
