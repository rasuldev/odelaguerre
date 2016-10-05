using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiscreteFunctions;
using DiscreteFunctionsPlots;
using GraphBuilders;

namespace OdeWithLaguerre
{
    public partial class Form1 : GraphBuilder2DForm
    {
        Plot2D plotExact = new Plot2D("Exact");
        Plot2D plotApprox = new Plot2D("Approx");
        public Form1()
        {
            InitializeComponent();
            GraphBuilder.DrawPlots(plotExact, plotApprox);
        }

        private void Draw(int coeffsCount)
        {
            var solver = new OdeSolverExp(0.25, 1, 1);
            plotExact.DiscreteFunction = new DiscreteFunction2D(solver.ExactSolution, 0, 10, 1000);
            plotApprox.DiscreteFunction = new DiscreteFunction2D(solver.GetApproxSolution(coeffsCount), 0, 10, 1000);

            plotExact.Refresh();
            plotApprox.Refresh();
        }

        private void nupCoeffsCount_ValueChanged(object sender, EventArgs e)
        {
            Draw((int)nupCoeffsCount.Value);
        }
    }
}
