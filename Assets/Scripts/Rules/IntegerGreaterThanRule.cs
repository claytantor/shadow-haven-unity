using RuleEngine.Base;

namespace Rules
{
	internal class IntegerGreaterThanCondition : BaseCondition<int>
	{
		#region Constructors
		
		/// <summary>
		/// Initializes a new instance of the <see cref="EqualsCondition{T}"/> class.
		/// </summary>
		/// <param name="threshold">The threshold value.</param>
		/// <param name="actual">The actual value.</param>
		public IntegerGreaterThanCondition(int threshold)
		: base(threshold) { }
		
		#endregion
		
		#region ICondition<int> methods
		
		/// <summary>
		/// Determines whether this instance is satisfied.
		/// </summary>
		/// <returns></returns>
		public override bool IsSatisfied
		{
			get { return Value > Threshold; }
		}
		
		#endregion
	}
	
	internal class IntegerGreaterThanRule : BaseRule<int>
	{
		#region Constructors
		
		/// <summary>
		/// Initializes a new instance of the <see cref="IntegerGreaterThanRule"/> class.
		/// </summary>
		/// <param name="threshold">The threshold.</param>
		public IntegerGreaterThanRule(int threshold)
			: base(threshold)
		{
			Initialize();
		}
		
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public override void Initialize()
		{
			// Clear any existing conditions
			Conditions.Clear();
			
			// Create our conditions
			var condition1 = new IntegerGreaterThanCondition(Threshold);
			
			// ...and add them to our collection of conditions
			Conditions.Add(condition1);
		}
		
		/// <summary>
		/// Matches the conditions.
		/// </summary>
		/// <returns></returns>
		public override bool MatchConditions()
		{
			return base.MatchesAnyCondition();
		}
		
		#endregion
	}
}

