// Author: Ai Sun
//   Date: 2023, Feb 14
//   Platform: Rider (Mac)

// Revision History:
/*      - 2023, Feb 14 Ai Sun - Initial creation of the class.
 */

// Process:
/*      An ExecutablePlan object is created by the user, stores a dynamic list of Formula(s)
 *      The user can interact with the instance by adding, replacing formula(s),
 *      removing the last one, or making a deep copy of the ExecutablePlan instance.
 */

// Assumptions:
/*      1. The index for 'Replace' method should not be out of range of the Formula list
 *      2. There should be at least one formula in the list when 'Remove' method is invoked
 *      3. The Formulas could just apply for one round and one round only. No
 *      reset option.
 */

// Use and Validity (Error Processing):
/*      The ExecutablePlan class validates in several ways:
 *         - When replacing, if the index is out of range, an exception is thrown
 *         - When removing, if the list is empty, an exception is thrown
 *         - These checks ensure the validity of the ExecutablePlan object throughout its lifecycle
 */
namespace Formula;

public class ExecutablePlan(Formula[] list) : Plan(list)
{
    private int _current = Default;     // holds current step

    // Applies the Formula at the current step and advances to the next
    /*      ** Precondition:
     *          There are still Formulas left to apply in the '_sequences' array.
     *      ** Postcondition:
     *          The Formula at the current step has been applied and the '_current'
     * value has been incremented. */
    public override String Apply()
    {
        // Check if there are any formulas left to apply
        if (_current >= Sequences.Length)
            throw new InvalidOperationException
                ("Cannot advance beyond the end of the sequence.");
        _current++;
        
        return Sequences[_current - Index].Apply();
    }

    // Returns a string format of the current formula. 
    /* Precondition: '_current' points to a valid formula.
     * Postcondition: Returns "[current index]. [current formula]".
     */
    public override String Query()
    {
        return $"({_current + Index}). {Sequences[_current]}";
    }

    // Replaces a Formula at a specific step in the sequence
    /*      ** Precondition:
     *          The step (index) is not yet completed (i.e. '_current'
     * is not past the index) and the new Formula is valid.
     *      ** Postcondition:
     *          The Formula at the given index has been replaced with a
     * deep copy of the new Formula. */
    public override void Replace(int index, Formula newFormula)
    {
        if (index < _current)
            throw new InvalidOperationException
                ("Cannot edit a completed step.");

        base.Replace(index, newFormula);
    }
    
    // Removes the last Formula in the sequence
    /*      ** Precondition:
     *          The last Formula in the sequence has not been completed yet
     * (i.e. '_current' is not past the last index).
     *      ** Postcondition:
     *          The last Formula in '_sequences' array is removed. */
    public override void Remove()
    {
        if (Sequences.Length - Index < _current)
            throw new InvalidOperationException
                ("Cannot remove a completed step.");

        base.Remove();
    }

    // Makes a deep copy of the ExecutablePlan object
    /*      ** Precondition: None.
     *      ** Postcondition:
     *          A new ExecutablePlan object which is a deep copy of the current
     * object is returned. */
    public override ExecutablePlan Clone()
    {
        ExecutablePlan newPlan = new ExecutablePlan(Sequences);

        newPlan._current = _current;
        
        return newPlan;
    }

}

/*
  Implementation Invariants:
      1. The '_sequences' array in the ExecutablePlan class always contains
       Formula(s) that are deep copied.
      2. The size of the '_sequences' array can be resized according to Add or Remove
      operations in the ExecutablePlan class.
      3. Appropriate exceptions are thrown from the methods of the ExecutablePlan 
      class if invalid operations are performed.
  */