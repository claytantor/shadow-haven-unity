using RuleEngine.Base;

namespace Rules
{
	internal class PlayerInventoryAllStateCondition : BaseCondition<InventoryState>
	{
		#region Constructors
		
		/// <summary>
		/// Initializes a new instance of the <see cref="EqualsCondition{T}"/> class.
		/// </summary>
		/// <param name="threshold">The threshold value.</param>
		/// <param name="actual">The actual value.</param>
		public PlayerInventoryAllStateCondition(InventoryState threshold)
		: base(threshold) { }
		
		#endregion
		
		#region ICondition<int> methods
		
		/// <summary>
		/// Determines whether this instance is satisfied.
		/// </summary>
		/// <returns></returns>
		public override bool IsSatisfied
		{
			get {			
				return Value.ContainsAll(Threshold); 
			}
		}
		
		#endregion
	}
	
	internal class PlayerInventoryAllStateRule : BaseRule<InventoryState>
	{
		public string stateResult;
		
		#region Constructors
		
		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerInventoryStateRule"/> class.
		/// </summary>
		/// <param name="threshold">The threshold.</param>
		public PlayerInventoryAllStateRule(InventoryState threshold, string stateResult)
			: base(threshold)
		{
			this.stateResult = stateResult;
			Initialize();
		}
		
		#endregion
		
		#region Methods
		
		public string GetState(string baseState){
			if(stateResult.Contains(baseState)){
				return stateResult;
			} else {
				return baseState+"0";
			}
		}
		
		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public override void Initialize()
		{
			// Clear any existing conditions
			Conditions.Clear();
			
			// Create our conditions
			var condition1 = new PlayerInventoryAllStateCondition(Threshold);
			
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

