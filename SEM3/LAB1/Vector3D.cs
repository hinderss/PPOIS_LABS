///<summary>
///Representation of 3D vectors.</summary>
///This class is used to represent 3D positions and directions around.
///It also contains methods for doing common vector operations
namespace Vector
{
    public class Vector3D
    {
        /// <summary>
        ///     x - double.  Default value is 0.
        /// </summary>
        /// <remarks>The value of x-coordinate</remarks>
        /// 
        private double _x = 0;
        public double X{
            get
            {
                return _x;
            }

            set
            { 
                _x = value; 
            }
        }

        /// <summary>
        ///     y - double.  Default value is 0.
        /// </summary>
        ///<remarks>The value of y-coordinate</remarks>
        private double _y = 0;
        public double Y 
        {
            get
            {
                return _y;
            }

            set
            {
                _y = value;
            }
        }

        /// <summary>
        ///     z - double.  Default value is 0.
        /// </summary>
        /// <remarks>The value of z-coordinate</remarks>
        private double _z = 0;
        public double Z
        {
            get
            {
                return _z;
            }

            set
            {
                _z = value;
            }
        }


        /// <summary>
        /// Default constructor with no params
        /// </summary>
        public Vector3D()
        {
        }

        /// <summary>
        /// Default constructor with three parameters - coordinates of the end of the vector.
        /// </summary>
        /// <param name="x1">- abcissa: The value of x-coordinate</param>
        /// <param name="y1">- ordinate: The value of y-coordinate</param>
        /// <param name="z1">- applicate: The value of z-coordinate</param>
        public Vector3D(double x1, double y1, double z1)
        {
            X = x1;
            Y = y1;
            Z = z1;
        }

        /// <summary>
        /// Default constructor with three parameters - coordinates of the end of the vector in a 2D Cartesian Plane.
        /// </summary>
        /// <param name="x1">- abcissa: The value of x-coordinate</param>
        /// <param name="y1">- ordinate: The value of y-coordinate</param>
        public Vector3D(double x1, double y1)
        {
            X = x1;
            Y = y1;
        }

        /// <summary>
        /// Default constructor which creates a copy of an existing vector.
        /// </summary>
        /// <param name="vector1">- An existing vector from which coordinates are copied</param>
        public Vector3D(Vector3D vector1)
        {
            X = vector1.X;
            Y = vector1.Y;
            Z = vector1.Z;
        }

        /// <summary>
        /// Default constructor with six parameters - coordinates of the start and the end of the vector.
        /// </summary>
        /// <param name="x0">- abcissa: The value of x-coordinate of the start of the vector.</param>
        /// <param name="y0">- ordinate: The value of y-coordinate of the start of the vector.</param>
        /// <param name="z0">- applicate: The value of z-coordinate of the start of the vector.</param>
        /// <param name="x1">- abcissa: The value of x-coordinate of the end of the vector.</param>
        /// <param name="y1">- ordinate: The value of y-coordinate of the end of the vector.</param>
        /// <param name="z1">- applicate: The value of z-coordinate of the end of the vector.</param>
        public Vector3D(double x0, double y0, double z0, double x1, double y1, double z1)
        {
            X = x1 - x0;
            Y = y1 - y0;
            Z = z1 - z0;
        }

        /// <summary>
        /// The length of this Vector3D .
        /// </summary>
        /// <remarks>Gets the length of this Vector3D .
        /// When you set this parameter, the vector coordinates are recalculated to match the set length without changing its direction.
        /// </remarks>
        /// The length of a Vector3D is sometimes referred to as the magnitude.</remarks>
        /// 
        public double _length = 0;
        public double Length
        {
            get
            {
                _length = (double)Math.Sqrt(X * X + Y * Y + Z * Z);
                return _length;
            }
        }

        /// <summary>
        /// Adds two Vector3D and returns the result as a Vector3D.
        /// </summary>
        /// <param name="vector1">The first Vector3D to add.</param>
        /// <param name="vector2">The second Vector3D to add.</param>
        /// <returns>The sum of vector1 and vector2.</returns>
        public static Vector3D operator +(Vector3D vector1, Vector3D vector2)
        {
            return new Vector3D(vector1.X + vector2.X, vector1.Y + vector2.Y, vector1.Z + vector2.Z);
        }

        /// <summary>
        /// Subtracts two Vector3D and returns the result as a Vector3D.
        /// </summary>
        /// <param name="vector1">The Vector3D to be subtracted from.</param>
        /// <param name="vector2">The Vector3D to subtract from vector1.</param>
        /// <returns>The result of subtracting vector2 from vector1.</returns>
        public static Vector3D operator -(Vector3D vector1, Vector3D vector2)
        {
            return new Vector3D(vector1.X - vector2.X, vector1.Y - vector2.Y, vector1.Z - vector2.Z);
        }

        /// <summary>
        /// Calculates the dot product of two Vector3D
        /// </summary>
        /// <param name="vector1">The first Vector3D to evaluate.</param>
        /// <param name="vector2">The second Vector3D to evaluate.</param>
        /// <returns>Double<br>The dot product of vector1 and vector2.</returns>
        public static double operator *(Vector3D vector1, Vector3D vector2)
        {
            return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
        }

        /// <summary>
        /// Calculates the cross product of two Vector3D
        /// </summary>
        /// <param name="vector1">The first Vector3D to evaluate.</param>
        /// <param name="vector2">The second Vector3D to evaluate.</param>
        /// <returns>Vecror3D<br>The cross product of vector1 and vector2.</returns>
        public static Vector3D CrossProduct(Vector3D vector1, Vector3D vector2)
        {
            return new Vector3D
            {
                X = vector1.Y * vector2.Z - vector1.Z * vector2.Y,
                Y = vector1.Z * vector2.X - vector1.X * vector2.Z,
                Z = vector1.X * vector2.Y - vector1.Y * vector2.X
            };
        }

        /// <summary>
        /// Multiplies the specified Vector3D by the specified scalar and returns the result as a Vector3D.
        /// </summary>
        /// <param name="vector1">The Vector3D to multiply.</param>
        /// <param name="value">The scalar to multiply.</param>
        /// <returns>Vecror3D<br>The result of multiplying vector and value.</returns>
        public static Vector3D operator *(Vector3D vector, double value)
        {
            return new Vector3D(vector.X * value, vector.Y * value, vector.Z * value);
        }

        /// <summary>
        /// Divides the specified Vector3D by the specified scalar and returns the result as a Vector3D.
        /// </summary>
        /// <param name="vector1">The Vector3D to be devided.</param>
        /// <param name="value">The scalar to divide vector by.</param>
        /// <returns>Vecror3D<br>The result of dividing vector and value.</returns>
        /// <exception cref="DivideByZeroException()">Thrown when value = 0</exception>
        public static Vector3D operator /(Vector3D vector, double value)
        {
            if (value == 0)
                throw new DivideByZeroException();

            return new Vector3D(vector.X / value, vector.Y / value, vector.Z / value);
        }

        /// <summary>
        /// Calculates the devision of two Vector3D
        /// </summary>
        /// <remarks>
        /// Divides two Vector3D by each other. <br>It is said that the operation of dividing a scalar by a vector is not unambiguous.
        /// In this case, the corresponding Vector3D coordinates are divided by each other and written as the coordinates of the new Vector3D.
        /// </remarks>
        /// <param name="vector1">The first Vector3D to evaluate.</param>
        /// <param name="vector2">The second Vector3D to evaluate.</param>
        /// <returns>Vecror3D<br>The "division" product of vector1 and vector2.</returns>
        public static Vector3D operator /(Vector3D vector1, Vector3D vector2)
        {
            return new Vector3D(vector1.X / vector2.X, vector1.Y / vector2.Y, vector1.Z / vector2.Z);
        }

        /// <summary>
        /// Retrieves cosine of the angle between two Vector3D.
        /// </summary>
        /// <param name="vector1">The first Vector3D to evaluate.</param>
        /// <param name="vector2">The second Vector3D to evaluate.</param>
        /// <returns>Double<br>The cosine of the angle between vector1 and vector2.</returns>
        public static double operator ^(Vector3D vector1, Vector3D vector2)
        {
            return (vector1 * vector2) / (vector1.Length * vector2.Length);
        }

        /// <summary>
        /// Compares two Vector3D instances for exact equality.
        /// </summary>
        /// Note that double values can acquire error when operated upon,
        /// such that an exact comparison between two values which are logically equal may fail.
        /// </remarks>
        /// <returns>
        /// bool - true if the two Vector3D instances are exactly equal, false otherwise
        /// </returns>
        /// <param name='vector1'>The first Vector3D to compare</param>
        /// <param name='vector2'>The second Vector3D to compare</param>
        public static bool operator ==(Vector3D vector1, Vector3D vector2)
        {
            return vector1.X == vector2.X && vector1.Y == vector2.Y && vector1.Z == vector2.Z;
        }

        /// <summary>
        /// Greater. Compares two Vector3D instances.
        /// </summary>
        /// <remarks>Rather than simply comparing their magnitudes, the comparison is lexicographically defined.<br>
        /// The two vectors are compared coordinate by coordinate. The first mismatching coordinate defines which Vector3D is lexicographically less or greater than the other.
        /// </remarks>
        /// <returns>
        /// bool - true if the two vector1 less than vector2 lexicographically
        /// </returns>
        /// <param name='vector1'>The first Vector3D to compare</param>
        /// <param name='vector2'>The second Vector3D to compare</param>
        public static bool operator <(Vector3D vector1, Vector3D vector2)
        {
            if (vector1.X < vector2.X) return true;
            if (vector1.X > vector2.X) return false;

            if (vector1.Y < vector2.Y) return true;
            if (vector1.Y > vector2.Y) return false;

            if (vector1.Z < vector2.Z) return true;
            if (vector1.Z > vector2.Z) return false;

            return false;
        }

        /// <summary>
        /// Less. Compares two Vector3D instances.
        /// </summary>
        /// <remarks>Rather than simply comparing their magnitudes, the comparison is lexicographically defined.<br>
        /// The two vectors are compared coordinate by coordinate. The first mismatching coordinate defines which Vector3D is lexicographically less or greater than the other.
        /// </remarks>
        /// <returns>
        /// bool - true if the two vector1 greater than vector2 lexicographically
        /// </returns>
        /// <param name='vector1'>The first Vector3D to compare</param>
        /// <param name='vector2'>The second Vector3D to compare</param>
        public static bool operator >(Vector3D vector1, Vector3D vector2)
        {
            if (vector1 == vector2)
                return false;

            return !(vector1 < vector2);
        }

        /// <summary>
        /// Compares two Vector3D instances for exact inequality.
        /// </summary>
        /// <remarks>
        /// Note that double values can acquire error when operated upon,
        /// such that an exact comparison between two values which are logically equal may fail.
        /// </remarks>
        /// <returns>
        /// bool - true if the two Vector3D instances are exactly unequal, false otherwise
        /// </returns>
        /// <param name='vector1'>The first Vector3D to compare</param>
        /// <param name='vector2'>The second Vector3D to compare</param>
        public static bool operator !=(Vector3D vector1, Vector3D vector2)
        {
            return !(vector1 == vector2);
        }

        /// <summary>
        /// Creates a string representation of this object.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public override string ToString()
        {
            return $"{X}, {Y}, {Z}";
        }

        /// <summary>
        /// Prints the string representation of this object to the console.
        /// </summary>
        public void Print()
        {
            Console.WriteLine($"({ToString()})");
        }
    }
}