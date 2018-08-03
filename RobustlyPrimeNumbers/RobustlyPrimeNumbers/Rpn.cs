using System;

namespace RobustlyPrimeNumbers
{
    public class RPN
    {
        private readonly int _length;
        private int rpnIndex = 0;
        private int[] rpns;
        //private List<int> primes = new List<int>();
        private int[] primes = new int[203280220];
        private int primeIndex = 0;
        public RPN(int length)
        {
            //validate for 32 bit length array RPN prime numbers
            if (length <= 0 && length > 2209) throw new ArgumentOutOfRangeException("The length of the array must be greater than 0");

            _length = length;
            rpns = new int[length];
            rpns[rpnIndex++] = 2;

            GeneretRPNs();
        }

        public bool ContainsZero(int number)
        {
            return number.ToString().Contains("0");
        }

        public bool IsRpn(int number, bool checkThis)
        {
            if (number < 10) return AreLastDigitsRpn(number);

            var t = GetLowAndHighRangeOfRpn(rpnIndex - 1, rpns, number);
            for (int i = t[1]; i >= t[0] && rpns[i] >= number; i--)
            {
                if (rpns[i] == number) return true;
            }
            return false;

        }

        //public bool IsPrime(int number)
        //{
        //    for (int i = 2; i < number / 2; i++)
        //    {
        //        if (number % i == 0)
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}
        public bool AreLastDigitsRpn(int number)
        {
            if (number == 2) return true;

            var lastDigit = number % 10;

            return lastDigit == 3 || lastDigit == 5 || lastDigit == 7;

        }

        public bool IsPrime(int i)
        {
            for (int j = 0; j < primeIndex && primes[j] <= (i / 2); j++)
            {
                if (i % primes[j] == 0) return false;
            }
            return true;
        }

        int[] GetLowAndHighRangeOfRpn(int numElems, int[] arr, int target)
        {
            int low = 4, high = numElems; // numElems is the size of the array i.e arr.size()
            while (low != high)
            {
                int mid = (low + high) / 2; // Or a fancy way to avoid int overflow
                if (arr[mid] < target)
                {
                    /* This rpnIndex, and everything below it, must not be the first element
                     * greater than what we're looking for because this element is no greater
                     * than the element.
                     */
                    low = mid + 1;
                }
                else
                {
                    /* This element is at least as large as the element, so anything after it can't
                     * be the first element that's at least as large.
                     */
                    high = mid;
                }
            }
            return new[] { low, high };
            /* Now, low and high both point to the element in question. */
        }

        public void GeneretRPNs()
        {
            for (int i = 3; i <= Int32.MaxValue && rpnIndex < _length; i += 2)
            {
                var isPrime = IsPrime(i);
                if (isPrime)
                {
                    primes[primeIndex++] = i;
                }
                if (isPrime && !ContainsZero(i) && IsRpn(i < 10 ? i : int.Parse(i.ToString().Substring(1)), true))
                {
                    rpns[rpnIndex++] = i;
                }
            }
        }

        public void PrintLengthElement()
        {
            Console.WriteLine("Element is: " + rpns[_length - 1]);
            Console.WriteLine($"Total Prime numer {primeIndex} and the last one is: { primes[primeIndex - 1]}");
        }
        public void PrintArray()
        {
            for (var i = 0; i < rpns.Length; i++)
            {
                Console.Write(rpns[i] + ", ");
            }
            Console.WriteLine();
        }
    }
}