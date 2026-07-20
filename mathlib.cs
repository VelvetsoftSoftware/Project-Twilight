using UnityEngine;

public static class mathlib {
	
	/* the idea is that normal numbers will be proccessed normally but decials
	are set to always be xxxx.xx so they will always be two decimal places
	this means all decimals will have to be writing as normal numbers then
	the last two(or 10-20 and then 0-9) are actually the tenth and hundrith
	and not the arabic numbers or tens
	*/

    // 16 bit
    // precomputed: round(65536 / ((i << 7) + 64))
    private static readonly uint[] ReciprocalTable = new uint[128] {
        1024, 1014, 1004, 994,  985,  975,  966,  957,  948,  940,  931,  923,  914,  906,  898,  890,
        883,  875,  868,  860,  853,  846,  839,  832,  825,  819,  812,  805,  799,  793,  786,  780,
        774,  768,  762,  756,  750,  745,  739,  734,  728,  723,  717,  712,  707,  702,  697,  692,
        687,  682,  677,  673,  668,  663,  659,  654,  650,  646,  641,  637,  633,  629,  625,  621,
        617,  613,  609,  605,  601,  598,  594,  590,  587,  583,  580,  576,  573,  569,  566,  563,
        559,  556,  553,  550,  546,  543,  540,  537,  534,  531,  529,  526,  523,  520,  517,  515,
        512,  509,  506,  504,  501,  499,  496,  494,  491,  489,  486,  484,  482,  479,  477,  475,
        472,  470,  468,  466,  463,  461,  459,  457,  455,  453,  451,  449,  447,  445,  443,  441
    };
    
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
        if (divider <= 128) {
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

    // Helper implementation for finding leading bit length (De Bruijn or simple loop)
    private static int BitLength(uint value) {
        if (value == 0) return 0;
        int length = 0;
        while (value > 0) {
            length++;
            value >>= 1;
        }
        return length;
    }
}
