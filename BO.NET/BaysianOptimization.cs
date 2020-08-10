using BOnet.Kernels;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Random;
using System;
using System.Collections.Generic;

namespace BOnet
{
    public class BaysianOptimization
    {
        private Matrix<double> K;
        private readonly KernelBase kernel;
        private Func<IList<double>, double> function;
        private Matrix<double> bounds;
        private int dim;
        private readonly Xorshift rng;
        private readonly List<Vector<double>> x;
        private readonly List<double> y;

        public BaysianOptimization(Func<IList<double>, double> function, Matrix<double> bounds, KernelType kernelType)
        {
            this.function = function;
            this.bounds = bounds;
            kernel = new GaussianKernel(0.01);
            dim = bounds.RowCount;
            rng = new Xorshift(114514);
            x = new List<Vector<double>>();
            y = new List<double>();
        }

        public void Init()
        {
            var x1 = Vector<double>.Build.Dense(dim);
            var x2 = Vector<double>.Build.Dense(dim);
            for (int i = 0; i < dim; i++)
            {
                x1[i] = (bounds[i, 1] - bounds[i, 0]) * rng.NextDouble() + bounds[i, 0];
                x2[i] = (bounds[i, 1] - bounds[i, 0]) * rng.NextDouble() + bounds[i, 0];
            }
            x.Add(x1);
            x.Add(x2);
            y.Add(function(x1));
            y.Add(function(x2));

            CalcMatrixK();
        }

        private void CalcMatrixK()
        {
            if (K == null)
            {
                K = Matrix<double>.Build.DenseDiagonal(y.Count, 1);
                for (int i = 0; i < y.Count; i++)
                {
                    for (int j = i + 1; j < y.Count; j++)
                    {
                        var temp = kernel.GetKernelValue(x[i], x[j]);
                        K[i, j] = temp;
                        K[j, i] = temp;
                    }
                }
                return;
            }
        }


        public void Optimize()
        {

        }
    }
}
