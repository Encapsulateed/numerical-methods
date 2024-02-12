import numpy as np

# File with matrix A
file_A = "matrix_a.txt"
# File with matrix F
file_B = "matrix_f.txt"
try:
    with open(file_A, "r") as f_A, open(file_B, "r") as f_B:
        lines_A = f_A.readlines()
        rows = len(lines_A)
        columns = len(lines_A[0].split())
        
        mA = np.zeros((rows, columns))
        mB = np.zeros(rows)

        for i, line in enumerate(lines_A):
            values = line.split()
            for j, val in enumerate(values):
                mA[i, j] = float(val)

        for i, line in enumerate(f_B):
            mB[i] = float(line)

        y = np.zeros(rows)
        a = np.zeros(rows)
        b = np.zeros(rows)

        y[0] = mA[0, 0]
        a[0] = -mA[0, 1] / y[0]
        b[0] = mB[0] / y[0]

        for i in range(1, rows - 1):
            y[i] = mA[i, i] + mA[i, i - 1] * a[i - 1]
            a[i] = -mA[i, i + 1] / y[i]
            b[i] = (mB[i] - (mA[i, i - 1] * b[i - 1])) / y[i]

        y[rows - 1] = mA[rows - 1, columns - 1] + (mA[rows - 1, columns - 2] * a[rows - 2])
        b[rows - 1] = (mB[rows - 1] - mA[rows - 1, columns - 2] * b[rows - 2]) / y[rows - 1]

        x = np.zeros(rows)
        x[rows - 1] = b[rows - 1]

        for i in range(rows - 2, -1, -1):
            x[i] = a[i] * x[i + 1] + b[i]

        for i in range(rows):
            print(f"x[{i}] = {x[i]}")

except Exception as e:
    print("Exception:", e)
finally:
    print("Executing finally block.")