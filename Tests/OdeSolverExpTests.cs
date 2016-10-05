using System;
using NUnit.Framework;
using OdeWithLaguerre;
using static System.Linq.Enumerable;
using static System.Math;

namespace Tests
{
    [TestFixture]
    public class OdeSolverExpTests
    {
        private const double Tolerance = 0.000001;

        [Test]
        public void Param_c_is_zero_Test()
        {
            // r = 2, a_0 = a_1 = 1, y(t) = e^{0*t} = 1
            var solver = new OdeSolverExp(0, 1, 1);
            var pCoeffs = Range(0, 10).Select(solver.PCoeffs).ToArray();
            Assert.That(pCoeffs, Is.EqualTo(Repeat(0d, 10).ToArray()).Within(Tolerance));

            var yCoeffs = solver.CalcCoeffs(2);
            Assert.That(yCoeffs, Is.EqualTo(new[] { 1d, 0 }).Within(Tolerance));

            yCoeffs = solver.CalcCoeffs(6);
            Assert.That(yCoeffs, Is.EqualTo(new[] { 1d, 0, 0, 0, 0, 0 }).Within(Tolerance));
        }

        [Test]
        public void Param_c_is_1_divby_4_Test()
        {
            // r = 2, a_0 = a_1 = 1, y(t) = e^{t/4}
            var solver = new OdeSolverExpStub(0.25, 1, 1);

            var yCoeffs = solver.CalcCoeffs(2);
            Assert.That(yCoeffs, Is.EqualTo(new[] { 1d, 0.25 }).Within(Tolerance));

            Assert.That(solver.RightCoeff, Is.EqualTo(21.0 / 16).Within(Tolerance));

            Assert.That(solver.HCoeffs(0), Is.EqualTo(7 / 4d).Within(Tolerance));
            Assert.That(solver.HCoeffs(1), Is.EqualTo(7 / 4d - 7 / 3d).Within(Tolerance));
            Assert.That(solver.HCoeffs(2), Is.EqualTo(21d / 32 * (128d / 27 - 64d / 9 + 8d / 3)).Within(Tolerance));
            Assert.That(solver.HCoeffs(3), Is.EqualTo(21d / (16 * 6) * (-6d * 256 / 81 + 2 * 64d / 3 - 24)).Within(Tolerance));

            Assert.That(solver.PSecondPart(0), Is.EqualTo(1.5).Within(Tolerance));
            Assert.That(solver.PSecondPart(1), Is.EqualTo(-0.25).Within(Tolerance));
            Assert.That(solver.PSecondPart(2), Is.EqualTo(0).Within(Tolerance));
            Assert.That(solver.PSecondPart(3), Is.EqualTo(0).Within(Tolerance));
            Assert.That(solver.PSecondPart(4), Is.EqualTo(0).Within(Tolerance));
            Assert.That(solver.PSecondPart(5), Is.EqualTo(0).Within(Tolerance));

            Assert.That(solver.QCoeffs(0), Is.EqualTo(2).Within(Tolerance));
            Assert.That(solver.QCoeffs(1), Is.EqualTo(-1).Within(Tolerance));

            yCoeffs = solver.CalcCoeffs(4);
            Assert.That(yCoeffs, Is.EqualTo(new[] { 1d, 0.25, 1d/12, -1d/36 }).Within(Tolerance));
        }

        [Test]
        public void PCoeffTwoTest()
        {
            var solver = new OdeSolverExp(0, 1, 1);
            Assert.That(solver.PCoeffs(2), Is.EqualTo(0).Within(Tolerance));
        }
    }

    class OdeSolverExpStub : OdeSolverExp
    {
        public double RightCoeff => _rightCoeff;
        public OdeSolverExpStub(double c, params double[] coeffs) : base(c, coeffs)
        {
        }

        public new double PSecondPart(int s)
        {
            return base.PSecondPart(s);
        }
    }
}