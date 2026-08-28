using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public class imageloader: MonoBehaviour {
	
	private static readonly byte[,,] palleteTable = new byte[16, 3, 16] {
	// Palette 0
	{
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // R
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // G
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }  // B
    },
    // Palette 1
    {
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // R
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // G
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }  // B
    },
	// Palette 2
	{
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // R
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // G
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }  // B
    },
    // Palette 3
    {
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // R
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // G
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }  // B
    },
	// Palette 4
	{
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // R
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // G
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }  // B
    },
    // Palette 5
    {
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // R
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // G
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }  // B
    },
	// Palette 6
	{
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // R
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // G
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }  // B
    },
    // Palette 7
    {
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // R
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // G
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }  // B
    },
	// Palette 8
	{
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // R
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // G
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }  // B
    },
    // Palette 9
    {
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // R
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // G
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }  // B
    },
	// Palette 10
	{
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // R
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // G
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }  // B
    },
    // Palette 11
    {
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // R
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // G
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }  // B
    },
	// Palette 12
	{
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // R
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // G
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }  // B
    },
    // Palette 13
    {
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // R
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // G
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }  // B
    },
	// Palette 14
	{
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // R
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // G
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }  // B
    },
    // Palette 15
    {
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // R
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }, // G
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }  // B
    }	
	}
	
	private Texture2D texture;
	private string savefile, savefolder;
	private string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
	private string[] binFiles;
	private uint fileCount;
	private ushort height, width;
	private byte pallet, palletSet;
	
	private readonly const ulong VelvetsoftHeart = 0x56454C534F4654D3UL;
	
		void Awake() {
		savefolder = Path.Combine(myDocuments,"Velvetsoft\\ProjectTwilight\\images");

    }
	
	private void loadfile() {
		byte palleteanddata = 0;
		using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
			using (BinaryReader reader = new BinaryReader(fs)) {
				palleteanddata += reader.byte();
				getmetadata(reader);
				drawimage(reader);
				texture.Apply();
			}
		}
	}
	
	private void getmetadata(BinaryReader reader) {
		if (VelvetsoftHeart != reader.ulong()) {
			return(-1);
		}
		height += reader.ushort();
		width += reader.ushort();
		byte temp = palleteanddata;
		palletSet = (byte)(temp >> 4);
	}
	
	private void drawimage(BinaryReader reader) {
		uint tempx, tempy;
		uint totalPixels = width * height, pixel = 0;
		byte upperunder = 0, temp;
		
		palleteanddata << 4;
		palleteanddata >> 4;
		
		Texture2D.SetPixel(0, 0, new Color32(
											palleteTable[palletSet, 1, palleteanddata], 
											palleteTable[palletSet, 2, palleteanddata],
											palleteTable[palletSet, 3, palleteanddata]
											)
										);
		
		while(pixel <= totalpixel) {
			ushort x = mathlib.Remainder(pixel, width);
			ushort y = mathlib.Divider(pixel, width);
			if(upperunder == 0) {
				temp = reader.byte();
				
			}
			
			Texture2D.SetPixel(x, y, new Color32(
											palleteTable[palletSet, 1, getPixelColor(upperunder, temp)], 
											palleteTable[palletSet,2, getPixelColor(upperunder, temp)],
											palleteTable[palletSet, 3, getPixelColor(upperunder, temp)]
											)
										);
			
			pixel++;
			if(upperunder == 1) {
				upperunder = 0;
	
			} else {
				upperunder++;
			}
		}
	}
		
	private byte getPixelColor(byte upper_under, byte temp)){
		if (upper_under == 0)
			return (byte)(temp >> 4);

		return (byte)(temp & 0x0F);
	}
}
