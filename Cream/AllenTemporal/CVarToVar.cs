using System;
using System.Collections;

namespace Cream.AllenTemporal
{
    public class CVarToVar
    {
    	public int ConstraintIndex { get; set; }

    	public AllenVariable Var1 { get; set; }

    	public AllenVariable Var2 { get; set; }

    	public override String ToString()
    	{
    		if ((Var1 != null) && (Var2 != null))
            {
                return Var1 + " " + AllenEvents.GetString(ConstraintIndex) + " " + Var2;
            }
    		return null;
    	}
    }

    public static class NetWorkConstraints
    {
        public static int[][][] GetConstraintNetwork(Network net)
        {
            var vNetowrk = new int[net.Variables.Count][][];
            IEnumerator vFrom = net.Variables.GetEnumerator();
            while (vFrom.MoveNext())
            {
                IEnumerator vTo = net.Variables.GetEnumerator();
                vNetowrk[((Variable)vFrom.Current).Index] = new int[net.Variables.Count][];
                while (vTo.MoveNext())
                {
                    if (vTo.Current != vFrom.Current)
                    {
                        vNetowrk[((Variable)vFrom.Current).Index][((Variable)vTo.Current).Index] = new int[14];
                        vNetowrk[((Variable)vFrom.Current).Index][((Variable)vTo.Current).Index]= 
                                  GetConstraints(vFrom.Current as Variable, vTo.Current as Variable);
                    }
                }
            }
            return vNetowrk;
        }

        private static int[] GetConstraints(Variable v1, Variable v2)
        {
            Network network = v1.Network;
            IEnumerator cs = network.Constraints.GetEnumerator();
            var cons = new int[14];
            bool hasbeenConstrainted = false;
            while (cs.MoveNext())
            {
                var c = (Constraint)cs.Current;
            	if (!(c is AllenConstraint)) continue;
            	var c1 = (AllenConstraint)c;
            	if ((c1.Vars[0] != v1) || (c1.Vars[1] != v2)) continue;
            	cons[c1.AllenEvent] = 1;
            	hasbeenConstrainted = true;
            }
            if (!hasbeenConstrainted)
            {
                for (int i = 0; i < 14; i++)
                {
                    cons[i] = 1;
                }
            }
            return cons;
        }
    }
   
}
