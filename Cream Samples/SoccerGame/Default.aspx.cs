using System;
using Cream.AllenTemporal;
using Cream;

namespace SoccerGame
{
    public partial class _Default : System.Web.UI.Page
    {
        Network net;
        AllenVariable John;
        AllenVariable Mary;
        AllenVariable Wendy;
        AllenVariable Soccer;
        Solution solution;
        Solver solver;
        private int counter = 0;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private void doItOnce()
        {
            Session.Clear();
            net = new Network();
            John = new AllenVariable(net, 0, 40, 30, "John");
            Mary = new AllenVariable(net, 35, 60, 20, "Mary");
            Wendy = new AllenVariable(net, 0, 60, 50, "Wendy");
            Soccer = new AllenVariable(net, 30, 135, 105, "Soccer");
            John.Equals(Mary);
            John.Starts(Mary);
            John.StartedBy(Mary);
            John.Meets(Mary);
            John.Equals(Wendy);
            John.Starts(Wendy);
            John.StartedBy(Wendy);
            John.Meets(Wendy);
            John.Overlaps(Soccer);
            Mary.Finishes(Wendy);
            Mary.FinishedBy(Wendy);
            Mary.During(Soccer);
            Mary.Contains(Soccer);
            AllenDomain ad0 = (AllenDomain)(John.Domain);
            AllenDomain ad1 = (AllenDomain)(Mary.Domain);
            AllenDomain ad2 = (AllenDomain)(Wendy.Domain);
            AllenDomain ad3 = (AllenDomain)(Soccer.Domain);
            solver = new AllenSolver(net);
            for (solver.Start(); solver.WaitNext(); solver.Resume())
            {
                solution = solver.Solution;
                Session["jd"] = ad0.Duration;
                Session["md"] = ad1.Duration;
                Session["wd"] = ad2.Duration;
                Session["sd"] = ad3.Duration;
                Session["js" + Convert.ToInt16(counter)] = solution.GetIntValue(John);
                Session["ms" + Convert.ToInt16(counter)] = solution.GetIntValue(Mary);
                Session["ws" + Convert.ToInt16(counter)] = solution.GetIntValue(Wendy);
                Session["ss" + Convert.ToInt16(counter)] = solution.GetIntValue(Soccer);
                Session["jp" + Convert.ToInt16(counter)] = ad0.Duration;
                Session["mp" + Convert.ToInt16(counter)] = ad1.Duration;
                Session["wp" + Convert.ToInt16(counter)] = ad2.Duration;
                Session["sp" + Convert.ToInt16(counter)] = ad3.Duration;
                Session["jn" + Convert.ToInt16(counter)] = solution.GetIntValue(John).ToString();
                Session["mn" + Convert.ToInt16(counter)] = solution.GetIntValue(Mary).ToString();
                Session["wn" + Convert.ToInt16(counter)] = solution.GetIntValue(Wendy).ToString();
                Session["sn" + Convert.ToInt16(counter)] = solution.GetIntValue(Soccer).ToString();
                counter += 1;
            }
            Session["done"] = true;
            Session["total"] = counter;
            Session["counter"] = 0;
            total.Text = "There are " + counter + " solutions";
            counter = 0;
            Display();
            Button1.Enabled = true;
            Button2.Enabled = true;
            Button3.Enabled = true;
            Button4.Enabled = true;
            if (Convert.ToInt16(Session["counter"]) == 0)
            {
                Button1.Enabled = false;
                Button2.Enabled = false;
            }
            if (Convert.ToInt16(Session["counter"]) == Convert.ToInt16(Session["total"]) - 1)
            {
                Button3.Enabled = false;
                Button4.Enabled = false;
            }
        }

        private void Display()
        {
            if (Session.Count != 0)
            {
                JohnStart.Width = Convert.ToInt16(Session["js" + counter])*3;
                MaryStart.Width = Convert.ToInt16(Session["ms" + counter])*3;
                WendyStart.Width = Convert.ToInt16(Session["ws" + counter])*3;
                SoccerStart.Width = Convert.ToInt16(Session["ss" + counter])*3;
                JohnPanel.Width = Convert.ToInt16(Session["jp" + counter])*3;
                MaryPanel.Width = Convert.ToInt16(Session["mp" + counter])*3;
                WendyPanel.Width = Convert.ToInt16(Session["wp" + counter])*3;
                SoccerPanel.Width = Convert.ToInt16(Session["sp" + counter])*3;
                JohnPanelN.Width = Convert.ToInt16(Session["js" + counter])*3;
                MaryPanelN.Width = Convert.ToInt16(Session["ms" + counter])*3;
                WendyPanelN.Width = Convert.ToInt16(Session["ws" + counter])*3;
                SoccerPanelN.Width = Convert.ToInt16(Session["ss" + counter])*3;
                string sJohn = Convert.ToString(Session["jn" + counter]);
                string sMary = Convert.ToString(Session["mn" + counter]);
                string sWendy = Convert.ToString(Session["wn" + counter]);
                string sSoccer = Convert.ToString(Session["sn" + counter]);
                DateTime j =
                    new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7,
                                 Convert.ToInt16(Session["jn" + counter]), 0);
                DateTime m =
                    new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7,
                                 Convert.ToInt16(Session["mn" + counter]), 0);
                DateTime w =
                    new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7,
                                 Convert.ToInt16(Session["wn" + counter]), 0);
                DateTime s =
                    new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7,
                                 Convert.ToInt16(Session["sn" + counter]), 0);
                int jd = Convert.ToInt16(Session["jd"]);
                int md = Convert.ToInt16(Session["md"]);
                int wd = Convert.ToInt16(Session["wd"]);
                int sd = Convert.ToInt16(Session["sd"]);
                DateTime jf = j.AddMinutes(jd);
                DateTime mf = m.AddMinutes(md);
                DateTime wf = w.AddMinutes(wd);
                DateTime sf = s.AddMinutes(sd);
                JohnNumber.Text = "7:" + (sJohn.Length < 2 ? "0" + sJohn : sJohn);
                MaryNumber.Text = "7:" + (sMary.Length < 2 ? "0" + sMary : sMary);
                WendyNumber.Text = "7:" + (sWendy.Length < 2 ? "0" + sWendy : sWendy);
                SoccerNumber.Text = "7:" + (sSoccer.Length < 2 ? "0" + sSoccer : sSoccer);
                JohnF.Text = jf.Hour + ":" + (jf.Minute < 10 ? "0" + jf.Minute : jf.Minute.ToString());
                MaryF.Text = mf.Hour + ":" + (mf.Minute < 10 ? "0" + mf.Minute : mf.Minute.ToString());
                WendyF.Text = wf.Hour + ":" + (wf.Minute < 10 ? "0" + wf.Minute : wf.Minute.ToString());
                SoccerF.Text = sf.Hour + ":" + (sf.Minute < 10 ? "0" + sf.Minute : sf.Minute.ToString());
                soln.Text = " Solution # " + (counter + 1);
            }
            else
            {
                soln.Text = " Session expired ... You should find solutions again!!!";
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            counter = 0;
            Display();
            Session["counter"] = 0;
            Button1.Enabled = true;
            Button2.Enabled = true;
            Button3.Enabled = true;
            Button4.Enabled = true;
            if (Convert.ToInt16(Session["counter"]) == 0)
            {
                Button1.Enabled = false;
                Button2.Enabled = false;
            }
            if (Convert.ToInt16(Session["counter"]) == Convert.ToInt16(Session["total"]) - 1)
            {
                Button3.Enabled = false;
                Button4.Enabled = false;
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            counter = Convert.ToInt16(Session["counter"])-1;
            Display();
            Session["counter"] = Convert.ToInt16(Session["counter"]) - 1;
            Button1.Enabled = true;
            Button2.Enabled = true;
            Button3.Enabled = true;
            Button4.Enabled = true;
            if (Convert.ToInt16(Session["counter"]) == 0)
            {
                Button1.Enabled = false;
                Button2.Enabled = false;
            }
            if (Convert.ToInt16(Session["counter"]) == Convert.ToInt16(Session["total"]) - 1)
            {
                Button3.Enabled = false;
                Button4.Enabled = false;
            }


        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            counter = Convert.ToInt16(Session["counter"])+1;
            Display();
            Session["counter"] = Convert.ToInt16(Session["counter"]) + 1;
            Button1.Enabled = true;
            Button2.Enabled = true;
            Button3.Enabled = true;
            Button4.Enabled = true;
            if (Convert.ToInt16(Session["counter"]) == 0)
            {
                Button1.Enabled = false;
                Button2.Enabled = false;
            }
            if (Convert.ToInt16(Session["counter"]) == Convert.ToInt16(Session["total"]) - 1)
            {
                Button3.Enabled = false;
                Button4.Enabled = false;
            }

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            counter = Convert.ToInt16(Session["total"])-1;
            Display();
            Session["counter"] = Convert.ToInt16(Session["total"]) - 1;
            Button1.Enabled = true;
            Button2.Enabled = true;
            Button3.Enabled = true;
            Button4.Enabled = true;
            if (Convert.ToInt16(Session["counter"]) == 0)
            {
                Button1.Enabled = false;
                Button2.Enabled = false;
            }
            if (Convert.ToInt16(Session["counter"]) == Convert.ToInt16(Session["total"]) - 1)
            {
                Button3.Enabled = false;
                Button4.Enabled = false;
            }

        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            doItOnce();
            mainPanel.Visible = true;
        }
    }
}
