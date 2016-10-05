using System;
using static System.Math;

namespace OdeWithLaguerre
{
    public class OdeSolverExp : OdeSolver
    {
        private readonly double _c;
        protected readonly double _rightCoeff; // c^r + a_{r-1}c^{r-1}+...+a_0 c
        /// <summary>
        /// It is supposed that y(t)=e^{ct}. In this case right side would be equal to e^{cx}*(c^r + a_{r-1}c^{r-1}+...+a_0 c) and 
        /// initial conditions are y^{(k)}(0)=c^k 
        /// </summary>
        /// <param name="c">It must be less than 1/2</param>
        /// <param name="initialConditions"></param>
        /// <param name="coeffs"></param>
        public OdeSolverExp(double c, params double[] coeffs) : base(null, ConstructInitialConditions(c, coeffs.Length), coeffs)
        {
            _c = c;
            var r = coeffs.Length;
            _rightCoeff = Pow(c, r);
            for (int i = 0; i < r; i++)
            {
                _rightCoeff += coeffs[i] * Pow(c, i);
            }
        }

        private static double[] ConstructInitialConditions(double c, int r)
        {
            var initConds = new double[r];
            for (int k = 0; k < r; k++)
            {
                initConds[k] = Pow(c, k);
            }
            return initConds;
        }

        public override double HCoeffs(int s)
        {
            if (s == 0) return _rightCoeff / (1 - _c);
            return _rightCoeff * Pow(_c / (_c - 1), s) / (1 - _c);
        }

        public Func<double, double> ExactSolution => t => Pow(E, _c * t);
    }
}