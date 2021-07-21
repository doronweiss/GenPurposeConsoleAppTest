using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatLib {
  class DMatrix {
    // actual data
    private double[,] mat;
    private readonly int rows, cols;

    public int Cols => cols;
    public int Rows => rows;
    // constructors
    public DMatrix(int rows, int cols, double? initVal = null) {
      this.rows = rows;
      this.cols = cols;
      this.mat = new double[rows, cols];
      if (initVal is double d) {
        for (int i = 0; i < rows; i++)
          for (int j = 0; j < cols; j++)
            mat[i, j] = (double)initVal;
      }
    }
    public DMatrix(DMatrix src) {
      this.rows = src.rows;
      this.cols = src.cols;
      this.mat = new double[rows, cols];
      for (int idx = 0; idx < rows; idx++)
        for (int jdx = 0; jdx < cols; jdx++)
          mat[idx, jdx] = src.mat[idx, jdx];
    }
    // accessor
    public double this[int idx, int jdx] {
      get => mat[idx, jdx];
      set => mat[idx, jdx] = value;
    }
    public bool IsRow() => rows == 1 && cols >= 1;
    public bool IsCol() => cols == 1 && rows >= 1;
    public bool IsVector() => IsRow() || IsCol();
    public DMatWrapper D => new DMatWrapper(this);

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Rows/cols => {rows}/{cols}");
      for (int i = 0; i < rows; i++) {
        List<string> ls = new List<string>();
        for (int j = 0; j < cols; j++)
          ls.Add(mat[i, j].ToString());
        sb.AppendLine(string.Join(",", ls));
      }
      return sb.ToString();
    }

    // math operations
    public static DMatrix operator +(DMatrix me, DMatrix other) {
      DMatrix result = new DMatrix(me.rows, me.cols);
      for (int i = 0; i < me.rows; i++)
        for (int j = 0; j < me.cols; j++)
          result[i, j] = me[i, j] + other[i, j];
      return result;
    }

    public static DMatrix operator -(DMatrix me, DMatrix other) {
      DMatrix result = new DMatrix(me.rows, me.cols);
      for (int i = 0; i < me.rows; i++)
        for (int j = 0; j < me.cols; j++)
          result[i, j] = me[i, j] - other[i, j];
      return result;
    }

    public static DMatrix operator *(DMatrix me, DMatrix other) {
      int rows = me.rows;
      int cols = other.cols;
      DMatrix result = new DMatrix(rows, cols);
      for (int i = 0; i < rows; i++) {
        for (int j = 0; j < cols; j++) {
          result[i, j] = 0.0;
          for (int k = 0; k < me.cols; k++)
            result[i, j] += me[i, k] * other[k, j];
        }
      }
      return result;
    }

  }

  class DMatWrapper {
    private DMatrix mat;
    public DMatWrapper(DMatrix src) {
      mat = src;
    }

    public override string ToString() => mat.ToString();


    public static DMatrix operator *(DMatWrapper me, DMatrix other) {
      int rows = me.mat.Rows;
      int cols = me.mat.Cols;
      DMatrix result = new DMatrix(rows, cols);
      for (int i = 0; i < rows; i++) {
        for (int j = 0; j < cols; j++) {
          Console.WriteLine($"looping i/j : {i}/{j}");
            result[i, j] = me.mat[i, j] * other[i,j];
        }
      }
      return result;
    }

  }
}
