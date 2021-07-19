using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatLib {
  class DMatrix<T> {
    // actual data
    private T[,] mat;
    private readonly int rows, cols;
    // constructors
    public DMatrix(int rows, int cols) {
      this.rows = rows;
      this.cols = cols;
      this.mat = new T[rows, cols];
    }
    public DMatrix (DMatrix<T> src){
      this.rows = src.rows;
      this.cols = src.cols;
      this.mat = new T[rows, cols];
      for (int idx=0; idx<rows;idx++)
      for (int jdx = 0; jdx < cols; jdx++)
        mat[idx, jdx] = src.mat[idx, jdx];
    }
    // accessor
    public T this[int idx, int jdx] {
      get => mat[idx, jdx];
      set => mat[idx, jdx] = value;
    }
  }
}
