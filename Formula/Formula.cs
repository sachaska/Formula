// Author: Ai Sun
//   Date: 2023, Jan 18
//   Platform: Rider (Mac)

// Revision History:
/*      - 2023, Jan 18 Ai Sun - Initial creation of the class.
        - 2023, Jan 30 Ai Sun - Add comments and minimize the number of
        constructor.
        - 2023, Jan 31 Ai Sun - change Clients should be able to increase 
                                proficiency up to some maximum.
                                add empty exception.
                                delete variable convert count.
 */

// Process:
/*      A Formula object is created by the user inputting material names and
 *      quantities (both input and output materials) through the constructor,
 *      which initializes the _inputMaterials and _outputMaterials.
 * 
 *      For each material, the constructor checks if the provided quantity is
 *      greater than zero, and creates a new Material instance.
 * 
 *      Upon stored relationship (_inputMaterials and _outputMaterials),
 *      it calculates the conversion probabilities and potential output
 *      materials based on the stored manufacturing probabilities
 *      (_probability array) and efficiencies (_produceRate array).
 * 
 *      The user can interact further with the instance of Formula by calling
 *      the other methods to check or set the current proficiency level,
 *      or retrieve the list of materials for conversion.
 */

// Assumptions:
/*      1. The input and output material names provided by the user should not
 *      be null or empty.
 * 
 *      2. The quantities of input and output materials provided should always
 *      be positive.
 * 
 *      3. The _produceRate and _probability arrays are always of length 4.
 *
 *      4. Proficiency should always be integer,
 *      in the range of 0 to 4 (include 4).
 */

// Use and Validity (Error Processing):
/*      The Formula class validates its input in several ways:
 *          - If input name is empty, an exception is thrown.
 *
 *          - If input quantity is empty, an exception is thrown.
 * 
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
    private const int Default = 0;                  // default integer value (0)
    
    private const int Level = 0;                   // starting proficiency level
    private const int MaxLevel = 4;        // the maximum proficiency level
    private const int MinLevel = 0;        // the minimum proficiency level
    private const int UpdateValue = 5;     // for update probability
    /// <summary>
    /// Class representing a material.
    /// Class Invariant: Name and quantity shouldn't be null.
    /// quantity must always be non-negative. 
    /// </summary>
    private class Material
    {
        private readonly string _name;      // holds material name
        private readonly int _quantity;     // holds material quantity

        private const int NonPositive = 0;
        
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
            // error checking for constructor null inputs or output names 
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Create " +
                                                "Material instance " + 
                                                $"{name} " + "failed. "
                                                + "No name provide" );
            }
            
            // error checking for constructor invalid numbers 
            if (quantity <= NonPositive)
            {
                throw new InvalidDataException("Create Material " +
                                               "instance " + $"{name} failed. " +
                                               $"Quantity should greater than " +
                                               "zero.");
            }
            
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

        public string Name => _name;        // getter of name
        public int Quantity => _quantity;   // getter of quantity
    }

    // default produce rate
    private readonly double[] _produceRate = [0, 0.75, 1, 1.1];

    // default probability
    private readonly double[] _probability = [25, 20, 50, 5];
    
    // index of each probability
    const int FailProb = 0, PartProb = 1, FullProb = 2, ExtraProb = 3;

    // holds current probability
    private readonly double[] _currentProbability;

    // holds formula input materials
    private readonly Material[] _inputMaterials;

    // holds formula output materials
    private readonly Material[] _outputMaterials;

    // holds current proficiency
    private int _proficiency;
    
    // set and get proficiency
    public int Proficiency
    {
        set
        {
            int lastProf = _proficiency;
            
            if (value is < MinLevel or > MaxLevel)
            {
                throw new InvalidDataException("Proficiency " +
                                               "valid range [0, 4].");
            }
            
            _proficiency = value;

            if (_proficiency != lastProf)
            {
                int change = _proficiency - lastProf;
                
            }
        }

        get => _proficiency;
    }
    
    

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
        if (inNames.Length == Default || outNames.Length == Default)
        {
            throw new InvalidDataException("Initialize" +
                                           " Formula instance failed." +
                                           " Names should not be null.");
        }
        
        if (inQuantity.Length == Default || outQuantity.Length == Default)
        {
            throw new InvalidDataException("Initialize" +
                                           " Formula instance failed." +
                                           " No data in quantity.");
        }
        
        if (inNames.Length == inQuantity.Length && 
            outNames.Length == outQuantity.Length)
        {
            _inputMaterials = new Material[inNames.Length];
            _outputMaterials = new Material[outNames.Length];
        }
        else
            throw new Exception("Initialize Formula instance failed."
                                + "Names and quantity not pair.");
        
        for (int i = Default; i < inNames.Length; i++)
            _inputMaterials[i] = new Material(inNames[i], inQuantity[i]);
        
        for (int i = Default; i < outNames.Length; i++)
            _outputMaterials[i] = new Material(outNames[i], outQuantity[i]);
        
        // Initialize produce probability
        _currentProbability = _probability;

        // Initialize produce proficiency
        _proficiency = Level;

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

        return rate;
    }

    // -‘UpdateProbability’
    /*      Adjusts the production probabilities accordingly.
     *      ** Precondition:
     *          proficiency level has changed.
     *      ** Postcondition:
     *         The probabilities are adjusted.
     */
    private void UpdateProbability(int val)
    {
        int change = UpdateValue;

        if (val < Default)
        {
            change = -change;
            val = -val;
        }

        for (int i = Default; i < val; i++)
        {
            _currentProbability[FailProb] -= change;
            _currentProbability[PartProb] -= change;
            _currentProbability[FullProb] += change;
            _currentProbability[ExtraProb] += change;
        }
        
    }

    // -‘Apply’ 
    /*      Applies the formula and returns the simulation result.
     *      ** Precondition:
     *          The input arrays should match in length, and quantities should
     *          be positive.
     *      ** Postcondition:
     *          The simulation result is returned as a string.
     */
    public string Apply()
    {
        StringBuilder stringBuilder = new StringBuilder();
        
        double rate = Produce();

        stringBuilder.Append("PRODUCE RATE: ").Append(rate)
            .Append(" NORMAL RESOURCES\n")
            .Append("SIMULATION RESULT: ");
            
        if (rate != _produceRate[FailProb])
        {
            for (int i = Default; i < _outputMaterials.Length; i++)
            {
                if (i != Default)
                    stringBuilder.Append(", ");
                stringBuilder.Append($"{(int)(_outputMaterials[i].Quantity * 
                            rate)}" +
                                     $" {_outputMaterials[i].Name}");
            }
        }
            
        else
            stringBuilder.Append("N/A");

        return stringBuilder.ToString();
    
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

        for (int i = Default; i < _inputMaterials.Length; i++)
        {
            if (i == first)
                stringBuilder.Append(_inputMaterials[i]);
            
            else
                stringBuilder.Append(", ").Append(_inputMaterials[i]);
            
        }

        stringBuilder.Append(" -> ");
        
        for (int i = Default; i < _outputMaterials.Length; i++)
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
        2. _produceRate and _probability are always of length 4 (4 states) and
        should be unsigned.
        3. _proficiency is always a positive value within the range specified by
         Level and MaxLevel constants.
        4. maximum _proficiency is 5.
  */