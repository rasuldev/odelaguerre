using System;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using OdeWithLaguerre;
using static System.Math;
using static System.Linq.Enumerable;

namespace Tests
{
    [TestFixture]
    public class OdeSolverTests
    {
        [Test]
        public void QTest()
        {
            var solver = new OdeSolver(x => x, new[] { 1d, 2, 3 }, new[] { 1d, 2, 3 });
            // Q[0] should be equal to sum of coeffs a_i, i=0, 1,..., r-1
            Assert.AreEqual(1 + 2 + 3, solver.QCoeffs(0));
        }

        [Test]
        public void Q2Test()
        {
            var a = new[] { 1d, 2, 3 };
            Func<double, double> q = x =>
            {
                var sum = 0d;
                var r = a.Length;
                for (int k = 0; k < a.Length; k++)
                {
                    sum += a[k] * Pow(x, r - k - 1) / fact(r - k - 1);
                }
                return sum;
            };
            var solver = new OdeSolver(x => x, a, a);

            var qCoeffs = Range(0, 3).Select(solver.QCoeffs).ToArray();
            var Laguerre = Range(0, 3).Select(mathlib.Polynomials.Laguerre.Get).ToArray();
            Func<double, double> qLaguerreExpansion = x =>
            {
                var sum = 0d;
                for (int k = 0; k < qCoeffs.Length; k++)
                {
                    sum += qCoeffs[k] * Laguerre[k](x);
                }
                return sum;
            };

            var t = 0d;
            while (t < 100)
            {
                Assert.That(q(t), Is.EqualTo(qLaguerreExpansion(t)).Within(0.0001));
                t += 0.1;
            }
        }

        private int fact(int n)
        {
            var prod = 1;
            for (int i = 2; i <= n; i++)
            {
                prod *= i;
            }
            return prod;
        }

        [Test]
        public void HCoeffsTest()
        {
            var solver = new OdeSolver(t => Pow(t, 3), new []{1d}, new []{1d});
            var h = Range(0,5).Select(solver.HCoeffs).ToArray();
        }


        private class OdeSolverStub : OdeSolver
        {
            public OdeSolverStub(Func<double, double> right, double[] initialConditions, double[] coeffs) : base(right, initialConditions, coeffs)
            {
            }

            public static int BinomsProductSum(int s, int k)
            {
                return OdeSolver.BinomsProductSum(s, k);
            }

            public double PSecondPart(int s)
            {
                return base.PSecondPart(s);
            }
        }
    }
}