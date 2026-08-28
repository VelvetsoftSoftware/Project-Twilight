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
	
	private string savefile, savefolder, filePath;
	private string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
	private string[] binFiles;
	private uint fileCount;
	private ushort height, width;
	private byte pallet, palletSet, palleteanddata;
	private Texture2D texture;
	private Color32[] pixels;
	
	private const ulong VelvetsoftHeart = 0x56454C534F4654D3UL;
	
		void Awake() {
		savefolder = Path.Combine(myDocuments,"Velvetsoft\\ProjectTwilight\\images");

    }
	
	private void loadfile() {
		palleteanddata = 0;
		
		using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
			using (BinaryReader reader = new BinaryReader(fs)) {
				palleteanddata = reader.ReadByte();
				if (!getmetadata(reader))
   					 return;
				
				uint totalPixels = width * height;
				pixels = new Color32[totalPixels];
				
				drawimage(reader);
				texture.SetPixels32(pixels);
				texture.Apply();
			}
		}
	}
	
	private bool getmetadata(BinaryReader reader) {
		if (VelvetsoftHeart != reader.ReadUInt64()) {
			return false;
		}
		height = reader.ReadUInt16();
		width = reader.ReadUInt16();
		byte temp = palleteanddata;
		palletSet = (byte)(temp >> 4);

		return true;
	}
	
	private void drawimage(BinaryReader reader) {
		uint totalPixels = (uint)width * height, pixel = 1;
		byte upperunder = 0, temp = 0;
		
		palleteanddata = (byte)(palleteanddata & 0x0F);
		
		pixels[0] =  new Color32(
			palleteTable[palletSet, 0, palleteanddata], 
			palleteTable[palletSet, 1, palleteanddata],
			palleteTable[palletSet, 2, palleteanddata],
			255
			);
		
		while(pixel < totalPixels) {
			
			if(upperunder == 0) {
				temp = reader.ReadByte();
			}
			
			pixels[pixel] =  new Color32(
				palleteTable[palletSet, 0, getPixelColor(upperunder, temp)], 
				palleteTable[palletSet,1, getPixelColor(upperunder, temp)],
				palleteTable[palletSet, 2, getPixelColor(upperunder, temp)],
				255
				);
			
			pixel++;
			if(upperunder == 1) {
				upperunder = 0;
	
			} else {
				upperunder++;
			}
		}
	}
		
	private byte getPixelColor(byte upper_under, byte temp) {
		if (upper_under == 0)
			return (byte)(temp >> 4);

		return (byte)(temp & 0x0F);
	}
}
