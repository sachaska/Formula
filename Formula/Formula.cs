﻿// Author: Ai Sun
//   Date: 2023, Jan 18
//   Platform: Rider (Mac)

// Revision History:
/*      - 2023, Jan 18 Ai Sun - Initial creation of the class.
 */

// Process:
/*      A Formula object is created by the user inputting material names and
 *      quantities (both input and output materials) through the constructor,
 *      which initializes the _inputMaterials and _outputMaterials.
 * 
 *      For each material, the constructor checks if the provided quantity is
 *      greater than zero, and creates a new Material instance.
 * 
 *      Upon user providing materials to the Formula, it calculates the
 *      conversion probabilities and potential output materials based on
 *      the stored manufacturing probabilities (_probability array)
 *      and efficiencies (_produceRate array).
 * 
 *      The user can interact further with the instance of Formula by calling
 *      the other methods to check the current proficiency level, retrieve
 *      the list of materials for conversion.
 */

// Assumptions:
/*      1. The input and output material names provided by the user should not
 *      be null or empty.
 * 
 *      2. The quantities of input and output materials provided should always
 *      be positive.
 * 
 *      3. The _produceRate and _probability arrays are always of length 4.
 */

// Use and Validity (Error Processing):
/*      The Formula class validates its input in several ways:
 *          - If the input names' length does not match their respective
 *          quantities, an exception is thrown.
 *
 *          - If the input name does not match as in the format,
 *          an exception is thrown.
 * 
 *          - If any input or output quantity is less than zero,
 *          an exception is thrown.
 * 
 *          - These checks ensure the validity of the Formula object
 *          throughout its lifecycle.
 */

using System.Text;
using Exception = System.Exception;

namespace Formula;

/// <summary>
/// Class representing a formula.
/// Class Invariant: The quantities of input and output materials must always
/// be non-negative,
///                   and there must be at least one output material.
/// </summary>
public class Formula
{
    private const char Space = ' ';
    private const string ProduceMark = " -> ";
    private const string DivideMark = ", ";
    
    private const int Default = 0;
    private const int Error = -1;
    
    private const int Level = 1;
    private const int Range = 5;
    private const int MaxLevel = 5;
    
    private class Material
    {
        private readonly string _name;
        private readonly int _quantity;

            
        // - Constructor:
        /*     ** Precondition:
        *          The input string should be in the format.
        *      ** Postcondition:
        *          A Material instance is created with provided name
        *         and quantity.
        */
        public Material(string input)
        {
            int index = input.IndexOf(Space);
            
            if (index != Error)
            {
                string numStr = input.Substring(Default, index);
                string name = input.Substring(index + 1);
                _name = name;
                _quantity = int.Parse(numStr);
            }
            
            else
                throw new Exception("Create Material instance failed." +
                                    " Invalid input format.");
            
        }
        
        // - Constructor
        /*     ** Precondition:
        *          The input name should not be null or empty,
        *          quantity should be positive.
        *      ** Postcondition:
        *          A Material instance is created with
        *         provided name and quantity.
        */
        public Material(string name, int quantity)
        {
            _name = name;
            _quantity = quantity;

        }

        // -‘ToString’
        /*      ** Short Description:
        *          Returns a string representation of the Material instance.
        *      ** Precondition:
        *          None.
        *      ** Postcondition:
        *          Returns a string in the format "quantity name".
        */
        public override string ToString()
        {
            return $"{Quantity} {Name}";
        }

        public string Name => _name;
        public int Quantity => _quantity;
    }

    private readonly double[] _produceRate = [0, 0.75, 1, 1.1];

    private readonly double[] _probability = [25, 20, 50, 5];
    
    const int FailProb = 0, PartProb = 1, FullProb = 2, ExtraProb = 3;

    private double[] _currentProbability;

    private readonly Material[] _inputMaterials;

    private readonly Material[] _outputMaterials;

    private Material[]? _userInputMaterials;

    private Material[]? _formulaOutputMaterials;

    private static int _convertCount;

    private int _proficiency;

    public string Proficiency => 
        $"CURRENT PROFICIENCY: LEVEL {_proficiency.ToString()}";

    // - Constructor
    /*      ** Precondition:
     *          The lengths of input and output material names and quantities
     *          should match.
     *      ** Postcondition:
     *          A Formula instance is created with initialized input and output
     *          materials.
     */
    public Formula(string[] inNames, int[] inQuantity, 
        string[] outNames, int[] outQuantity)
    {
        if (inNames.Length == inQuantity.Length && 
            outNames.Length == outQuantity.Length)
        {
            _inputMaterials = new Material[inNames.Length];
            _outputMaterials = new Material[outNames.Length];
        }
        else
            throw new Exception("Initialize Formula instance failed."
                                + "Names and quantity not pair.");
        
        for (int i = 0; i < inNames.Length; i++)
        {
            if (inQuantity[i] > 0)
            {
                _inputMaterials[i] = new Material(inNames[i], inQuantity[i]);
            }
            else if (inQuantity[i] < 0)
            {
                throw new Exception($"Initialize Formula instance " +
                                    $"failed." + $" Invalid {inNames[i]} " +
                                    $"quantity.");
            }
        }
        
        for (int i = 0; i < outNames.Length; i++)
        {
            if (outQuantity[i] > 0)
            {
                _outputMaterials[i] = new Material(outNames[i], outQuantity[i]);
            }
            else if (outQuantity[i] < 0)
            {
                throw new Exception($"Initialize Formula instance " +
                                    $"failed." + $" Invalid {outNames[i]} " +
                                    $"quantity.");
            }
        }
        
        OrderList(_inputMaterials);
        OrderList(_outputMaterials);
        
        // Initialize convert count
        _convertCount = Default;
        
        // Initialize produce probability
        _currentProbability = _probability;

        // Initialize produce proficiency
        _proficiency = Level;

    }

    // - Constructor
    /*      ** Precondition:
     *          The input string should be in the format "
     *          inputMaterials -> outputMaterials".
     *      ** Postcondition:
     *          A Formula instance is created with initialized input and output
     *          materials.
     */
    public Formula(string input)
    {
        // Process user input
        string[] collection = input.ToLowerInvariant()
            .Split(new string[]{ProduceMark}, StringSplitOptions.None);
        
        // Initialize formula input/output materials names and numbers
        if (collection.Length == 2)
        {
            _inputMaterials = Normalize(collection[0]);
            _outputMaterials = Normalize(collection[1]);
        }
        
        else
            throw new Exception("Initialize Formula instance failed.");

        // Initialize convert count
        _convertCount = Default;
        
        // Initialize produce probability
        _currentProbability = _probability;
        
        // Initialize produce proficiency
        _proficiency = Level;
    }
    
    // -‘OrderList’
    /*      Sorts the given list of materials alphabetically.
     *      ** Precondition:
     *          The input list should not be null.
     *      ** Postcondition:
     *          The input list is sorted alphabetically.
     */
    private void OrderList(Material[] list)
    {
        // Sort by alphabet order
        Array.Sort(list, (m1, m2) => 
            string.Compare(m1.Name, m2.Name, StringComparison.Ordinal));
    }

    // -‘Normalize’
    /*      Converts the input string into an array of Material instances.
     *      ** Precondition:
     *          The input string should be in the format
     *          "quantity1 name1, quantity2 name2, ...".
     *      ** Postcondition:
     *          Returns an array of Material instances based on the input
     *          string.
     */
    private Material[] Normalize(string input)
    {
        string[] inputList = input.Split(new string[] { DivideMark }, 
            StringSplitOptions.None);
        
        Material[] materials = new Material[inputList.Length];
        
        for (int i = 0; i < inputList.Length; i++)
        {
            materials[i] = new Material(inputList[i]);
        }

        OrderList(materials);

        return materials;
    }

    
    // -‘Produce’
    /*      Simulates the production process and returns the produce rate.
     *      ** Precondition:
     *          None.
     *      ** Postcondition:
     *          The production process is simulated, and the produce rate is
     *          returned.
     */
    private double Produce()
    {
        const int min = 0, max = 100;

        double rate;
        
        // Generate an random number from 0~99
        Random random = new Random();

        int randomNumber = random.Next(min, max);

        if (randomNumber < _currentProbability[FailProb])
            // Produce failed
            return _produceRate[FailProb];
        
        if (randomNumber < _currentProbability[FailProb] +
            _currentProbability[PartProb])
            // Produce 3/4
            rate = _produceRate[PartProb];

        else if (randomNumber < _currentProbability[FailProb] + 
            _currentProbability[PartProb] +
            _currentProbability[FullProb])
            // Produce 100%
            rate = _produceRate[FullProb];
        
        else
            // Produce 110%
            rate = _produceRate[ExtraProb];

        // Add success convert count
        _convertCount++; 
        
        // If reached, level up
        if (_convertCount % Range == Default)
            LevelUp();

        return rate;
    }

    // -‘LevelUp’
    /*      Increases the proficiency level and adjusts the production
     *      probabilities accordingly.
     *      ** Precondition:
     *          None.
     *      ** Postcondition:
     *          The proficiency level is increased, and probabilities are
     *          adjusted.
     */
    private void LevelUp()
    {
        const int change = 5;
        
        // When user successful produce 5 times (not include failed result),
        // level up. Maximum proficiency level is 5 (12 times)
        int lastProficiency = _proficiency;
        _proficiency = (Level + _convertCount / Range) >= MaxLevel ? 
            MaxLevel : Level + _convertCount / Range;
        
        // If proficiency change, update _probability
        if (lastProficiency < _proficiency)
        {
            _currentProbability[FailProb] -= change;
            _currentProbability[PartProb] -= change;
            _currentProbability[FullProb] += change;
            _currentProbability[ExtraProb] += change;
        }
    }

    // -‘Apply’ 
    /*      Applies the formula to user-provided input materials and returns the
     *      simulation result.
     *      ** Precondition:
     *          The input arrays should match in length, and quantities should
     *          be positive.
     *      ** Postcondition:
     *          The simulation result is returned as a string.
     */
    public string Apply(string[] inputNames, int[] inputQuantity)
    {
        
        if (inputNames.Length == inputQuantity.Length)
            _userInputMaterials = new Material[inputNames.Length];
        
        else
            throw new Exception("SIMULATION FAILED. " +
                                "names and numbers not pair.");

        for (int i = 0; i < inputNames.Length; i++)
        {
            if (inputQuantity[i] > 0)
            {
                _userInputMaterials[i] = new Material(inputNames[i],
                    inputQuantity[i]);
            }
            else if (inputQuantity[i] < 0)
            {
                throw new Exception("SIMULATION FAILED. " 
                                    + $" Invalid {inputNames[i]} " +
                                    $"quantity.");
            }
            
        }
        
        OrderList(_userInputMaterials);
        
        StringBuilder stringBuilder = new StringBuilder();

        // Show current Proficiency with result
        stringBuilder.Append(Proficiency).Append('\n');

        // Compare to formula 'input materials'
        if (Compare())
            stringBuilder.Append(Simulation());
        
        return stringBuilder.ToString();
    
    }
    
    // -‘Apply’
    /*      Applies the formula to user-provided input materials and returns the
     * simulation result.
     *      ** Precondition:
     *          The input string should be in the format
     * "inputMaterials -> outputMaterials".
     *      ** Postcondition:
     *          The user-provided input materials are applied to the formula,
     * and the simulation result is returned as a string.
     */
    public string Apply(string input)
    {
        StringBuilder stringBuilder = new StringBuilder();

        // Show current Proficiency with result
        stringBuilder.Append(Proficiency).Append('\n');
        
        // Get user input
        _userInputMaterials = Normalize(input);
        
        // Compare to formula 'input materials'
        if (Compare())
            stringBuilder.Append(Simulation());
        
        return stringBuilder.ToString();
    }

    // -‘Simulation’
    /*      Simulates the production process and returns the simulation result.
     *      ** Precondition:
     *          None.
     *      ** Postcondition:
     *          The production process is simulated, and the result is returned
     *          as a string.
     */
    private string Simulation()
    {
        StringBuilder stringBuilder = new StringBuilder();
        
        double rate = Produce();

        stringBuilder.Append("PRODUCE RATE: ").Append(rate)
            .Append(" NORMAL RESOURCES\n")
            .Append("SIMULATION RESULT: ");
            
        if (rate != _produceRate[FailProb])
        {
            // Find the smallest quotient.
            int smallest = _userInputMaterials[Default].Quantity /
                           _inputMaterials[Default].Quantity;
            
            for (int i = 1; i < _inputMaterials.Length; i++)
            {
                int current = _userInputMaterials[i].Quantity / 
                              _inputMaterials[i].Quantity;
                if (current < smallest)
                    smallest = current;
            }

            if (smallest > 0)
            {
                double multiple = smallest * rate;
                
                _formulaOutputMaterials = 
                    new Material[_outputMaterials.Length];
                
                for (int i = 0; i < _outputMaterials.Length; i++)
                {
                    _formulaOutputMaterials[i] = 
                        new Material(_outputMaterials[i].Name, 
                            (int) 
                            (_outputMaterials[i].Quantity * multiple));
                }
                    
                foreach (Material mate in _formulaOutputMaterials)
                {
                    stringBuilder.Append(mate);
                }
            }
        }
            
        else
            stringBuilder.Append("N/A");

        return stringBuilder.ToString();
    }

    // -‘Compare’
    /*      Compares user-provided input materials with formula input materials.
     *      ** Precondition:
     *          The user input and formula input materials should match in
     *          type and quantity.
     *      ** Postcondition:
     *          Returns true if the comparison is successful.
     */
    private bool Compare()
    {
        const bool equal = true;

        if (_userInputMaterials == null)
            throw new Exception("CANNOT SIMULATE. " +
                                "User input resource null.");

        if (_inputMaterials.Length > _userInputMaterials.Length)
            throw new Exception("CANNOT SIMULATE. " +
                                "Too much type of input resources.");

        if (_inputMaterials.Length < _userInputMaterials.Length)
            throw new Exception("CANNOT SIMULATE. " +
                                "Not enough type of input resources.");
        
        for (int i = 0; i < _inputMaterials.Length; i++)
        {
            if (_inputMaterials[i].Name != _userInputMaterials[i].Name)
                throw new Exception("CANNOT SIMULATE. " +
                                    "Incorrect resource name: " +
                                    $"**{_userInputMaterials[i].Name}**");
        }

        return equal;
    }

    // -‘ToString’
    /*      Returns a string representation of the Formula instance.
     *      ** Precondition:
     *          None.
     *      ** Postcondition:
     *          Returns a string in the format
     *          "inputMaterials -> outputMaterials".
     */
    public override string ToString()
    {
        const int first = 0;
        
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < _inputMaterials.Length; i++)
        {
            if (i == first)
                stringBuilder.Append(_inputMaterials[i]);
            
            else
                stringBuilder.Append(", ").Append(_inputMaterials[i]);
            
        }

        stringBuilder.Append(" -> ");
        
        for (int i = 0; i < _outputMaterials.Length; i++)
        {
            if (i == first)
                stringBuilder.Append(_outputMaterials[i]);
            
            else
                stringBuilder.Append(", ").Append(_outputMaterials[i]);
            
        }

        return stringBuilder.ToString();

    }
    
}

/*
  Implementation Invariants:
        1. All Material quantities in _inputMaterials and _outputMaterials are 
         positive.
        2. _produceRate and _probability are always of length 4.
        3. _proficiency is always a positive value within the range specified by
         Level and MaxLevel constants.
  */