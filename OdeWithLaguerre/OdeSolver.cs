using System;
using mathlib;
using mathlib.Polynomials;
using static System.Math;

namespace OdeWithLaguerre
{
    public class OdeSolver
    {
        private Func<double, double> _right;
        private double[] _coeffs;
        private double[] _initialConditions;

        /// <summary>
        /// y^{(r)}(x) + a_{r-1} y^{(r-1)}(x) +... a_0 y(x) = h(x)
        /// </summary>
        /// <param name="right">h(x)</param>
        /// <param name="initialConditions"></param>
        /// <param name="coeffs">coeffs[i] corresponds to a_i</param>
        public OdeSolver(Func<double, double> right, double[] initialConditions, double[] coeffs)
        {
            _right = right;
            if (initialConditions.Length != coeffs.Length)
                throw new ArgumentException("initialConditions must have the same length as coeffs");
            _initialConditions = initialConditions;
            _coeffs = coeffs;

        }

        public Func<double, double> GetApproxSolution(int coeffsCount)
        {
            var coeffs = CalcCoeffs(coeffsCount);
            return x =>
            {
                var r = _initialConditions.Length;
                var val = 0d;
                for (int k = 0; k < coeffsCount; k++)
                {
                    val += coeffs[k]*LaguerreSobolev.Get(r, k)(x);
                }

                return val;
            };

        }

        public double[] CalcCoeffs(int count)
        {
            var r = _initialConditions.Length;
            var coeffs = new double[count];
            Array.Copy(_initialConditions, coeffs, Min(r, count));

            var mul = 1 / (QCoeffs(0) + 1);
            for (int k = 0; r + k < count; k++)
            {
                coeffs[r + k] = PCoeffs(k);
                for (int i = 1; i <= k; i++)
                {
                    coeffs[r + k] -= (QCoeffs(i) - QCoeffs(i - 1)) * coeffs[r + k - i];
                }
                coeffs[r + k] *= mul;
            }
            return coeffs;
        }

        public double QCoeffs(int nu)
        {
            int r = _coeffs.Length;
            var sum = 0d;
            for (int j = 0; j < r; j++)
            {
                var sumj = 0d;
                for (int k = 0; k <= nu; k++)
                {
                    sumj += Pow(-1, k) * Binomial.Calc(nu, k) * Binomial.Calc(r + k - j - 1, k);
                }
                sum += _coeffs[j] * sumj;
            }
            return sum;
        }

        public double PCoeffs(int s)
        {
            return HCoeffs(s) - PSecondPart(s);

        }

        /// <summary>
        /// $\sum_{\nu=0}^{r-1} a_\nu \sum_{k=0}^{r-\nu-1} y^{(k+\nu)}(0) \sum_{j=0}^s (-1)^j \binom{s}{j} \binom{k+j}{j}
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        protected double PSecondPart(int s)
        {
            var sum = 0d;
            var r = _coeffs.Length;
            for (int nu = 0; nu <= r - 1; nu++)
            {
                var innerSum = 0d;
                for (int k = 0; k <= r - nu - 1; k++)
                {
                    innerSum += _initialConditions[k + nu] * BinomsProductSum(s, k);
                }
                sum += _coeffs[nu] * innerSum;
            }
            return sum;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="k"></param>
        /// <returns>Sum $\sum_{j=0}^s (-1)^j \binom{s}{j} \binom{k+j}{j} $</returns>
        protected static int BinomsProductSum(int s, int k)
        {
            var sum = 1;
            var sign = -1;
            for (int j = 1; j <= s; j++)
            {
                sum += sign * Binomial.Calc(s, j) * Binomial.Calc(k + j, j);
                sign *= -1;
            }
            return sum;
        }

        public virtual double HCoeffs(int s)
        {
            // TODO: use Gauss quadrature formula
            var coeffSym = mathlib.ScalarMul.LebesgueLaguerre(_right, Laguerre.Get(s));
            return Integrals.RectangularInfinite(coeffSym, 100000, 10);
        }

    }
}