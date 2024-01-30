using System;

class HammingCode
{
    // Function to calculate the parity bits for a given data
    static int[] CalculateParityBits(int[] data)
    {
        int[] parityBits = new int[3];

        // Calculate parity bits
        parityBits[0] = data[0] ^ data[1] ^ data[3];
        parityBits[1] = data[0] ^ data[2] ^ data[3];
        parityBits[2] = data[1] ^ data[2] ^ data[3];

        return parityBits;
    }

    // Function to add parity bits to the data
    static int[] AddParityBits(int[] data, int[] parityBits)
    {
        int[] hammingCode = new int[7];

        // Copy data bits
        hammingCode[2] = data[0];
        hammingCode[4] = data[1];
        hammingCode[5] = data[2];
        hammingCode[6] = data[3];

        // Copy parity bits
        hammingCode[0] = parityBits[0];
        hammingCode[1] = parityBits[1];
        hammingCode[3] = parityBits[2];

        return hammingCode;
    }

    // Function to detect and correct errors in received code
    static int[] DetectAndCorrectErrors(int[] receivedCode)
    {
        int[] syndrome = new int[3];

        // Calculate syndrome
        syndrome[0] = receivedCode[0] ^ receivedCode[2] ^ receivedCode[4] ^ receivedCode[6];
        syndrome[1] = receivedCode[1] ^ receivedCode[2] ^ receivedCode[5] ^ receivedCode[6];
        syndrome[2] = receivedCode[3] ^ receivedCode[4] ^ receivedCode[5] ^ receivedCode[6];

        // Convert binary syndrome to decimal
        int errorPosition = syndrome[0] + 2 * syndrome[1] + 4 * syndrome[2];

        // If errorPosition is non-zero, correct the bit
        if (errorPosition != 0)
        {
            Console.WriteLine("Error detected at position: " + errorPosition);

            // Correct the bit
            receivedCode[errorPosition - 1] = receivedCode[errorPosition - 1] == 0 ? 1 : 0;
        }

        return receivedCode;
    }

    static void Main()
    {
        // Example data (4 bits)
        int[] data = { 1, 0, 1, 1 };

        // Calculate parity bits
        int[] parityBits = CalculateParityBits(data);

        // Add parity bits to data
        int[] hammingCode = AddParityBits(data, parityBits);

        Console.WriteLine("Original Data: " + string.Join("", data));
        Console.WriteLine("Hamming Code: " + string.Join("", hammingCode));

        // Simulate transmission by flipping a bit
        hammingCode[2] = hammingCode[2] == 0 ? 1 : 0;

        Console.WriteLine("Received Code with Error: " + string.Join("", hammingCode));

        // Detect and correct errors
        int[] correctedCode = DetectAndCorrectErrors(hammingCode);

        Console.WriteLine("Corrected Code: " + string.Join("", correctedCode));
    }
}