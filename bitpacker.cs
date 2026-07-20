using UnityEngine;
using System.IO;

public class bitpacker : MonoBehaviour{
	
	// each stat increase/ decrease gets 4 bits to define and goes from start which is right
	/////////////////////////////
	//		Social	         /
	//	elegance = 0000 = 0    /
	//	grace = 0010 = 2	    / 
	//	glamor = 0100	= 4	    / 
	//	negotiation = 0110 = 6  /
	//					    /
	//		physical	         /
	//	agality = 1000	= 8	    / 
	//	athletics = 1010 =10   / 
	//	strength = 1100 = 12   /  
	//	craftsmanship = 1110 = 14 / 
	//					    /
	//		intelligance      /
	//	stategy = 0001 = 1     /  
	//	science = 0011 = 3	    /  
	//	history = 0101 = 5	    /  
	//	math = 0111 = 7	    /  
	//	morality = 1001 = 9    /
	//	theology = 1011 = 11   /
	//	sin = 1101 = 13	    /
	//	peity = 1111 = 15	    /
	//					    /
	/////////////////////////////
	

	public byte unpacker(ushort a, jobs.Activity job) {
		uint temp = job.PackedStatIdentifier;
		a = (ushort)(a * 4);
		return (byte)((temp >> a) & 0xF);
	}
	
	//this is for packedStats for bit-slice interpreter for compound stat modifiers
	public byte statsUnpacker(byte whatToPick/*age=0 or no-1*/, byte upperlower/*0 or 1, this is needed to switch for each stat*/, ushort a/*current*/, jobs.Activity job) {
		uint temp = job.PackedStats;
		if(whatToPick == 0) {
			a = (ushort)(a + 4);
			return (byte)((temp >> 28) & 0xF);
		} else{
			a = (ushort)(a * 2);
			if (upperlower == 0) {
				return (byte)((temp >> a) & 0x3);
			} else {
				return (byte)(((temp >> a) & 0xC) >> 2);
			}
		}
	}
	
	public short moneyUnpacker(jobs.Activity job) {
		uint temp = job.PackedStats;
		return (short)((temp >> 20) & 0xFF);
	}
	
	public void savepacker(BinaryWriter writer, ushort stat1, ushort stat2) {/*this is only ran with ushort stats*/
		byte b0 = (byte)(stat1 & 0xFF);
		byte b1 = (byte)(((stat1 >> 8) & 0x0F) | ((stat2 & 0x0F) << 4));
		byte b2 = (byte)((stat2 >> 4) & 0xFF);
		
		writer.Write(b0);
		writer.Write(b1);
		writer.Write(b2);
	}
	
	public void loadupacker() {
		
	}
	
}
