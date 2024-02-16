// Author: Ai Sun
//   Date: 2023, Feb 14
//   Platform: Rider (Mac)

// Revision History:
/*      - 2023, Feb 14 Ai Sun - Initial creation of the class.
 */

// Process:
/*      A Plan object is created by the user, stores a dynamic list of Formula(s)
 *      The user can interact with the instance by adding, replacing formula(s)
 *      removing the last one, or making a deep copy of the Plan instance
 */

// Assumptions:
/*      1. The index for 'Replace' method should not be out of range of the Formula list
 *      2. There should be at least one formula in the list when 'Remove' method is invoked
 */

// Use and Validity (Error Processing):
/*      The Plan class validates in several ways:
 *         - When replacing, if the index is out of range, an exception is thrown
 *         - When removing, if the list is empty, an exception is thrown
 *         - These checks ensure the validity of the Plan object throughout its lifecycle
 */

namespace Formula;

using System;

/// <summary>
// /// Class representing a plan of formulas.
// /// Class Invariant: The '_sequences' list must always contain
// deep copied formulas.
// This class have a child class ExecutablePlan.
// Apply, Clone, Query functions are implemented. 
// Complete apply formula's remove/replace is not allowed in ExecutablePlan.
// /// </summary>
public class Plan
{
    public Formula[] Sequences; // holds Formula list
    protected const int Index = 1; // constant index value
    protected const int Default = 0; // constant default value

    // - Default Constructor
    /*      ** Precondition:
     *          N/A
     *      ** Postcondition:
     *          A Plan instance is created with an empty list of formulas
     *      initialized.
     */
    public Plan()
    {
        Sequences = Array.Empty<Formula>();
    }

    // - Overloaded Constructor
    /*      ** Precondition:
     *          The passed in list is not null.
     *      ** Postcondition:
     *          A Plan instance is created with each formula in the list
     *          deep copied into the '_sequences' array.
     */
    public Plan(Formula[] list)
    {
        // If is not empty list
        if (list.Length != Default)
        {
            Sequences = new Formula[list.Length];

            for (int i = Default; i < list.Length; i++)
            {
                Sequences[i] = new Formula(list[i]);
            }

        }
        else
            Sequences = Array.Empty<Formula>();

    }

    // - Method to add a new formula to the Plan
    /*      ** Precondition:
     *          The new formula is not null.
     *      ** Postcondition:
     *          A deep copy of the new formula is added to the end of the
     *      '_sequences' array.
     */
    public void Add(Formula? newFormula)
    {
        // Resize the _sequences array
        Sequences = Resize(Sequences, Sequences.Length + Index);
        Sequences[^Index] =
            newFormula ?? throw new ArgumentNullException(nameof(newFormula),
                "New formula cannot be null.");
    }

    // - Method to create a deep copy of the plan
    /*      ** Precondition:
     *          None.
     *      ** Postcondition:
     *          A Plan instance is created with deep copied formulas from the
     *      current Plan instance.
     */
    public Plan DeepCopy()
    {
        Plan newPlan = new Plan();

        foreach (Formula formula in Sequences)
        {
            // Add a deep copy of each Formula in _sequences to the new Plan
            newPlan.Add(new Formula(formula));
        }

        return newPlan;
    }

    // - Method to replace a formula in the Plan
    /*      ** Precondition:
     *          The index is not out of the '_sequences' array bounds, and the
     *      new formula is not null.
     *      ** Postcondition:
     *          The formula at the given index in the '_sequences' array is
     *      replaced with a deep copy of the new formula.
     */
    public virtual void Replace(int index, Formula newFormula)
    {
        if (newFormula == null)
        {
            throw new ArgumentNullException(nameof(newFormula),
                "New formula cannot be null.");
        }

        // Might want to add some boundary checks before attempting to replace
        if (index < Default || index >= Sequences.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(index),
                "Index out of range.");
        }

        // Deep copy of newFormula replaced at the specified index
        Sequences[index] = new Formula(newFormula);
    }

    // - Method to remove the last formula in the Plan
    /*      ** Precondition:
     *          The '_sequences' array is not empty.
     *      ** Postcondition:
     *          The last formula in the '_sequences' array is removed.
     */
    public virtual void Remove()
    {
        if (Sequences.Length == Default)
        {
            throw new InvalidOperationException
                ("No formulas to remove.");
        }

        Sequences = Resize(Sequences, Sequences.Length - Index);
    }

    // - Method to resize the '_sequences' array based on a new size
    /*      ** Precondition:
     *          The '_sequences' array is not null, and the new size is not negative.
     *
     *      ** Postcondition:
     *          A new array is returned, which is a copy of the old '_sequences'
     *          array rescaled to the provided new size.
     */
    private Formula[] Resize(Formula[] array, int newSize)
    {
        Formula[] newArray = new Formula[newSize];

        for (int i = Default; i < Math.Min(array.Length, newSize); i++)
        {
            newArray[i] = array[i];
        }

        return newArray;
    }

    // - Method to represent the Plan as a string
    /*      ** Precondition: None
     *
     *      ** Postcondition:
     *          A string representation of the Plan object is returned.
     */
    public override string ToString()
    {
        string result = "";
        for (int i = Default; i < Sequences.Length; i++)
        {
            result += "(" + (i + Index) + ") " + Sequences[i] + "\n";
        }

        return result;
    }

    // - Method to query the current formula
    /*      ** Precondition: None
     *
     *      ** Postcondition:
     *          A string representation of the current formula is returned,
     *          or null if not available.
     */
    public virtual String Query()
    {
        return null;
    }

    // - Method to clone the current plan
    /*      ** Precondition: None
     *
     *      ** Postcondition:
     *          A clone of the Plan object is returned,
     *          or null if not available.
     */
    public virtual Plan Clone()
    {
        return null;
    }

    // - Method to apply the current formula
    /*      ** Precondition: None
     *
     *      ** Postcondition:
     *          A string response to the apply action is returned,
     *          or null if not available.
     */
    public virtual String Apply()
    {
        return null;
    }

}

/*
  Implementation Invariants:
        1. The '_sequences' array always contains Formula(s) that are deep copied.
        2. The '_sequences' array size can be resized according to Add or Remove 
        operations.
        3. Appropriate exceptions are thrown if invalid operations are performed.
  */