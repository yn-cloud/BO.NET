using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BOnet.Kernels
{
    internal class GaussianKernel : KernelBase
    {
        private readonly double alpha;

        internal GaussianKernel(double alpha)
        {
            this.alpha = alpha;
        }

        internal override double GetKernelValue(double x1, double x2)
        {
            return alpha * Math.Exp(-Math.Pow(x1 - x2, 2));
        }

        internal override double GetKernelValue(IList<double> x1, IList<double> x2)
        {
            if (x1.Count != x2.Count)
            {
                throw new ArgumentException("Length of " + nameof(x1) + " and " + nameof(x2) + " must be same.");
            }

            double expValue = 0;
            Parallel.For(0, x1.Count, i =>
            {
                expValue += Math.Pow(x1[i] - x2[i], 2);
            });

            return alpha * Math.Exp(-expValue);
        }


    }
}