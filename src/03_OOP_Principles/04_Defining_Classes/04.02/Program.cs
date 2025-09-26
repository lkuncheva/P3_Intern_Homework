using System.Reflection;

namespace _04._02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Type classType = typeof(SampleClass);
            Console.WriteLine($"--- Reflection Demo for: {classType.Name} ---");
            Console.WriteLine();

            // Retrieving the attribute applied to the CLASS
            VersionAttribute classVersionAttr = classType.GetCustomAttribute<VersionAttribute>();

            if (classVersionAttr != null)
            {
                Console.WriteLine($"[Class Level]");
                Console.WriteLine($"Found Version: {classVersionAttr.Version}");
                Console.WriteLine("-----------------------------------");
            }
            else
            {
                Console.WriteLine($"[Class Level] No VersionAttribute found.");
            }

            Console.WriteLine();

            // Retrieving the attribute applied to the METHOD
            MethodInfo runProcessMethod = classType.GetMethod(nameof(SampleClass.RunProcess));

            if (runProcessMethod != null)
            {
                VersionAttribute methodVersionAttr = runProcessMethod.GetCustomAttribute<VersionAttribute>();

                if (methodVersionAttr != null)
                {
                    Console.WriteLine($"[Method Level: {runProcessMethod.Name}]");
                    Console.WriteLine($"Found Version: {methodVersionAttr.Version}");
                    Console.WriteLine("-----------------------------------");
                }
                else
                {
                    Console.WriteLine($"[Method Level: {runProcessMethod.Name}] No VersionAttribute found.");
                }
            }

            Console.WriteLine();

            // --- Setup Matrices ---

            // Matrix A (2x2)
            Matrix<int> A = new Matrix<int>(2, 2);
            A[0, 0] = 1; A[0, 1] = 2;
            A[1, 0] = 3; A[1, 1] = 4;

            // Matrix B (2x2)
            Matrix<int> B = new Matrix<int>(2, 2);
            B[0, 0] = 5; B[0, 1] = 6;
            B[1, 0] = 7; B[1, 1] = 8;

            // Matrix C (2x3) for multiplication
            Matrix<int> C = new Matrix<int>(2, 3);
            C[0, 0] = 1; C[0, 1] = 0; C[0, 2] = 2;
            C[1, 0] = -1; C[1, 1] = 3; C[1, 2] = 1;

            // Matrix D (3x2) for multiplication (A * D is incompatible, C * D is compatible)
            Matrix<int> D = new Matrix<int>(3, 2);
            D[0, 0] = 3; D[0, 1] = 1;
            D[1, 0] = 2; D[1, 1] = 1;
            D[2, 0] = 1; D[2, 1] = 0;

            // Zero Matrix (2x2)
            Matrix<int> Z = new Matrix<int>(2, 2);

            Console.WriteLine("--- Operator Overload Tests ---");

            // --- 1. Addition Test (A + B) ---
            Console.WriteLine("\n[1. Addition: A + B]");
            Console.WriteLine(A);
            Console.WriteLine(B);
            Matrix<int> Sum = A + B;
            Console.WriteLine("Result (A + B):");
            Console.WriteLine(Sum);

            // --- 2. Subtraction Test (B - A) ---
            Console.WriteLine("\n[2. Subtraction: B - A]");
            Matrix<int> Diff = B - A;
            Console.WriteLine("Result (B - A):");
            Console.WriteLine(Diff);

            // --- 3. Multiplication Test (C * D) ---
            Console.WriteLine("\n[3. Multiplication: C (2x3) * D (3x2)]");
            Console.WriteLine("Matrix C:");
            Console.WriteLine(C);
            Console.WriteLine("Matrix D:");
            Console.WriteLine(D);
            Matrix<int> Product = C * D;
            Console.WriteLine("Result (C * D) (2x2):");
            Console.WriteLine(Product);

            // --- 4. Truthiness Test ---
            Console.WriteLine("\n[4. Truthiness Test (using 'if' statement)]");

            Console.WriteLine("Matrix A:");
            if (A)
            {
                Console.WriteLine("A is true (contains non-zero elements).");
            }
            else
            {
                Console.WriteLine("A is false (all zeros).");
            }

            Console.WriteLine("Zero Matrix Z:");
            if (Z)
            {
                Console.WriteLine("Z is true (contains non-zero elements).");
            }
            else
            {
                Console.WriteLine("Z is false (all zeros).");
            }

            // --- 5. Exception Test (Incompatible Addition) ---
            Console.WriteLine("\n[5. Exception Test: Incompatible Addition (A + C)]");
            try
            {
                Matrix<int> BadSum = A + C;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Caught expected exception: {ex.Message}");
            }

            // --- 6. Exception Test (Incompatible Multiplication) ---
            Console.WriteLine("\n[6. Exception Test: Incompatible Multiplication (A * C)]");
            try
            {
                Matrix<int> BadProduct = A * C;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Caught expected exception: {ex.Message}");
            }
        }
    }
}
