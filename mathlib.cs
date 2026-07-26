using UnityEngine;

public static class mathlib {
	
	// Must never be 0.
    public static ushort Seed = 0xACE1;
	
	/* the idea is that normal numbers will be proccessed normally but decials
	are set to always be xxxx.xx so they will always be two decimal places
	this means all decimals will have to be writing as normal numbers then
	the last two(or 10-20 and then 0-9) are actually the tenth and hundrith
	and not the arabic numbers or tens
	*/
	
	//(int)mathlib.Remainder((uint) , )
	//(int)mathlib.Divider((uint) , )

    // 16 bit
	// precomputed: round(65536 / i)
	private static readonly uint[] ReciprocalTable = new uint[129] {
		0,
		65536, 32768, 21845, 16384, 13107, 10923, 9362, 8192,
		7282, 6554, 5958, 5461, 5041, 4681, 4369, 4096,
		3855, 3641, 3449, 3277, 3121, 2979, 2849, 2731,
		2621, 2521, 2427, 2341, 2260, 2185, 2114, 2048,
		1986, 1928, 1872, 1820, 1771, 1725, 1680, 1638,
		1601, 1560, 1525, 1490, 1456, 1425, 1395, 1365,
		1337, 1311, 1285, 1260, 1236, 1214, 1192, 1170,
		1150, 1129, 1111, 1092, 1074, 1057, 1040, 1024,
		1008, 993, 978, 963, 949, 936, 922, 910,
		897, 885, 873, 862, 851, 840, 829, 819,
		809, 799, 790, 780, 771, 762, 753, 745,
		736, 728, 720, 712, 705, 697, 690, 683,
		676, 670, 663, 657, 650, 644, 638, 632,
		626, 621, 615, 610, 604, 599, 594, 589,
		584, 579, 574, 570, 565, 560, 556, 551,
		547, 543, 538, 534, 530, 526, 522, 518
	};
	
	// Bit length lookup for 8-bit values (0 = 0 bits)
	private static readonly byte[] BitLengthTable = new byte[256] {
		0,1,2,2,3,3,3,3,4,4,4,4,4,4,4,4,
		5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,
		6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,
		6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,
		7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,
		7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,
		7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,
		7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,
		8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,
		8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,
		8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,
		8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,
		8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,
		8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,
		8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,
		8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8
	};
	
	public static uint Remainder(uint numerator, uint divider) {
		uint quotient = Divider(numerator, divider);
        uint remainder = numerator - (quotient * divider);
		return remainder;
	}
    
	//this is for dividing decimals particulary those that that have larger dividers then numerators 
    public static uint DecimalFixedPointDivider(uint numerator, uint divider) {
        if (divider == 0)
            return 0;

        uint quotient = Divider(numerator, divider);
        uint remainder = numerator - (quotient * divider);
        uint temp = quotient;

        remainder = (remainder << 3) + (remainder << 1);
        uint digit = Divider(remainder, divider);
        temp = (temp << 3) + (temp << 1) + digit; 
        remainder = remainder - (digit * divider);

        remainder = (remainder << 3) + (remainder << 1);
        digit = Divider(remainder, divider);
        temp = (temp << 3) + (temp << 1) + digit;

        return temp;
    }
	
    //this is for basic division and not for larger dividers, it will error out and return 0
    public static uint Divider(uint numerator, uint divider) {
        if (divider == 0) {
            return 65535;
        }
        
        uint scale;
        if (divider <= 129) {
            scale = ReciprocalTable[divider];
        } else if (divider < 16384) {
            uint index = divider >> 7;
            scale = ReciprocalTable[index];
        } else {
            uint leading = (uint)BitLength(divider) - 1;
            uint shift = (leading + 1) >> 1;
            scale = 1U << (int)(15 - shift);
            scale = scale + (scale >> 2);
            for (int i = 0; i < 2; i++) {
                uint prod = (divider * scale) >> 16;
                uint term = 0x20000 - prod;
                scale = (scale * term) >> 16;
            }
        }
        if (scale > 65535) 
            scale = 65535;
        if (scale < 1) 
            scale = 1;
            
        ulong temp = ((ulong)numerator * scale) + 32768;
        return (uint)(temp >> 16);
    }
    
	// Quick square root
    public static uint Quicksqrt(uint baseVal) {
        if (baseVal == 0)
            return 0;
        
        int leading = BitLength(baseVal) - 1;
        uint shift = (uint)((leading + 1) >> 1);
        uint guess = 1U << (int)shift;
        if (guess == 0)
            guess = 1;

        for (int x = 0; x < 2; x++) { 
            guess = (guess + Divider(baseVal, guess)) >> 1;
        }
        return guess;
    }
    
    // Quick exponent
    public static uint Exponent(uint baseVal, byte power) {
        uint temp;
        if (power < 1) {
            return Quicksqrt(baseVal);
        }
        switch (power) {
            case 1:
                return baseVal;
            case 2:
                return baseVal * baseVal;
            case 3:
                return baseVal * baseVal * baseVal;
            default:
                temp = baseVal * baseVal;
                if ((power & 1) != 0)
                    return baseVal * Exponent(temp, (byte)(power >> 1));
                else
                    return Exponent(temp, (byte)(power >> 1));
        }
    }
	
	// Generates a random byte (0-255)
    public static byte byteRNG() {
	        for (int i = 0; i < 8; i++)
        {
            bool carry = (Seed & 0x8000) != 0;

            Seed <<= 1;

            if (carry)
                Seed ^= 0x0039;
        }

        return (byte)Seed;
    }


    // Helper implementation for finding leading bit length (De Bruijn or simple loop)
	private static int BitLength(uint value) {
		if (value == 0) 
			return 0;

		if (value >= 0x10000) {
			if (value >= 0x1000000) {
				return 24 + BitLengthTable[(byte)(value >> 24)];
			} else {
				return 16 + BitLengthTable[(byte)(value >> 16)];
			}
		} else {
			if (value >= 0x100) {
				return 8 + BitLengthTable[(byte)(value >> 8)];
			} else {
				return BitLengthTable[(byte)value];
			}
		}
	}
}
