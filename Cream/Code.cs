using System;
using System.Linq;

namespace  Cream
{
	
	public class Code : ICloneable
	{
		virtual public Network To
		{
			set
			{
				for (var i = 0; i < conditions.Length; i++)
				{
					if (conditions[i] == null)
					{
						value.GetConstraint(i).ClearCondition();
					}
					else
					{
						conditions[i].To = value;
					}
				}
			}
			
		}
		public Condition[] conditions;
		
		private Code()
		{
		}
		
		public Code(Network network)
		{
			System.Collections.IList constraints = network.Constraints;
			conditions = new Condition[constraints.Count];
			for (var i = 0; i < conditions.Length; i++)
			{
				var c = network.GetConstraint(i);
				conditions[i] = c.ExtractCondition();
			}
		}
		
		public virtual Object Clone()
		{
			var code = new Code {conditions = new Condition[conditions.Length]};
		    conditions.CopyTo(code.conditions, 0);
			return code;
		}
		
		public virtual System.Collections.IList Operations()
		{
			System.Collections.IList operations = new System.Collections.ArrayList();
			foreach (var t in conditions.Where(t => t != null))
			{
				SupportClass.CollectionSupport.AddAll(operations, t.Operations());
			}
			return operations;
		}
	}
}