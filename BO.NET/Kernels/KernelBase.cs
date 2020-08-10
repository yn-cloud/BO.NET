using System.Collections.Generic;

namespace BOnet.Kernels
{
    internal abstract class KernelBase
    {
        internal abstract double GetKernelValue(double x1, double x2);
        internal abstract double GetKernelValue(IList<double> x1, IList<double> x2);
    }
}