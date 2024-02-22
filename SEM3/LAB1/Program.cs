using System.Diagnostics.CodeAnalysis;
using Vector;

namespace ConsoleApp
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        private const string Path = "..\\..\\..\\ConsoleContent.txt";
        static void Main(string[] args)
        {
            Vector3D vector1 = GetVectorFromUser("Enter the first vector:\n");
            Vector3D vector2 = GetVectorFromUser("Enter the second vector:\n");

            Vector3D result = new Vector3D();
                
            while (true)
            {
                PrintFileContents(Path);

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine($"Magnitude of vector 1: {vector1.Length}");
                            break;
                        case 2:
                            Console.WriteLine($"Magnitude of vector 2: {vector2.Length}");
                            break;
                        case 3:
                            result = vector1 + vector2;
                            Console.WriteLine($"Sum of vectors: ({result.ToString()})");
                            break;
                        case 4:
                            result = vector1 - vector2;
                            Console.WriteLine($"Difference of vectors: ({result.ToString()})");
                            break;
                        case 5:
                            Console.WriteLine($"Dot product of vectors: {vector1 * vector2}");
                            break;
                        case 6:
                            result = Vector3D.CrossProduct(vector1, vector2);
                            Console.WriteLine($"Cross product of vectors: ({result.ToString()})");
                            break;
                        case 7:
                            Console.Write("Enter a scalar value: ");
                            if (double.TryParse(Console.ReadLine(), out double scalar))
                            {
                                Vector3D result1 = vector1 * scalar;
                                Vector3D result2 = vector2 * scalar;
                                Console.WriteLine($"Vector 1 multiplied by scalar: ({result1.ToString()})");
                                Console.WriteLine($"Vector 2 multiplied by scalar: ({result2.ToString()})");
                            }
                            else
                            {
                                Console.WriteLine("Invalid scalar input.");
                            }
                            break;
                        case 8:
                            Console.Write("Enter a scalar value: ");
                            if (double.TryParse(Console.ReadLine(), out scalar))
                            {
                                Vector3D result1 = vector1 * scalar;
                                Vector3D result2 = vector2 * scalar;
                                Console.WriteLine($"Vector 1 divided by scalar: ({result1.ToString()})");
                                Console.WriteLine($"Vector 2 divided by scalar: ({result2.ToString()})");
                            }
                            else
                            {
                                Console.WriteLine("Invalid scalar input.");
                            }
                            break;
                        case 9:
                            Console.WriteLine($"Vectors are equal?: {vector1 == vector2}");
                            break;
                        case 10:
                            Console.WriteLine($"Vector 1 is greater than vector 2?: {vector1 > vector2}");
                            break;
                        case 11:
                            Console.WriteLine($"Vector 1 is less than vector 2?: {vector1 < vector2}");
                            break;
                        case 12:
                            Console.WriteLine($"Vector 1 is less than vector 2?: {vector1 ^ vector2}");
                            break;
                        case 13:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a valid option.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid option.");
                }
            }
        }

        public static Vector3D GetVectorFromUser(string prompt)
        {
            Console.WriteLine(prompt);
            Console.Write("x = ");
            double x = double.Parse(Console.ReadLine());
            Console.Write("y = ");
            double y = double.Parse(Console.ReadLine());
            Console.Write("z = ");
            double z = double.Parse(Console.ReadLine());

            return new Vector3D(x, y, z);
        }

        static void PrintFileContents(string filePath)
        {
            try
            {
                string fileContents = File.ReadAllText(filePath);
                Console.WriteLine(fileContents);
            }
            catch (FileNotFoundException)   {Console.WriteLine("File not found.");}
            catch (IOException)     {Console.WriteLine("An error occurred while reading the file.");}
        }
    }
}
