using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/*
8 bytes header
2 bytes height
2 bytes width
4 bits pallet
4 bits first pixel 
1 byte 2 pixels
*/

public class imageLoader {
	
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
	};
	
	private string savefile, savefolder, filePath;
	private string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
	private string[] binFiles;
	private uint fileCount;
	private ushort height, width;
	private byte pallet, palletSet, palleteanddata;
	private Texture2D texture;
	private Color32[] pixels;
	
	private const ulong VelvetsoftHeart = 0x56454C534F4654D3UL;
	
	private void loadfile() {
		palleteanddata = 0;
		
		using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
			using (BinaryReader reader = new BinaryReader(fs)) {
				if (!getmetadata(reader))
   					 return;
				
				uint totalPixels = (uint)(width * height);
				pixels = new Color32[totalPixels];

				palleteanddata = reader.ReadByte();
				drawimage(reader);
				texture.SetPixels32(pixels);
				texture.Apply();
			}
		}
	}
	
	private bool getmetadata(BinaryReader reader) {
		if (VelvetsoftHeart != reader.ReadUInt64())
			return false;
		
		height = reader.ReadUInt16();
		width = reader.ReadUInt16();

		if (width == 0 || height == 0)
    		return false;
		
		byte temp = palleteanddata;
		palletSet = (byte)(temp >> 4);

		return true;
	}
	
	private void drawimage(BinaryReader reader) {
		uint totalPixels = (uint)width * height, pixel = 1;
		
		palleteanddata = (byte)(palleteanddata & 0x0F);
		
		pixels[0] = new Color32(
			palleteTable[palletSet, 0, palleteanddata], 
			palleteTable[palletSet, 1, palleteanddata],
			palleteTable[palletSet, 2, palleteanddata],
			255
		);
		
		while(pixel < totalPixels) {
			byte packedByte = reader.ReadByte();

			// 1st Pixel: High Nibble (Upper 4 bits)
			byte highNibble = (byte)(packedByte >> 4);
			pixels[pixel] = new Color32(
				palleteTable[palletSet, 0, highNibble],
				palleteTable[palletSet, 1, highNibble],
				palleteTable[palletSet, 2, highNibble],
				255
			);
			pixel++;
		
			// 2nd Pixel: Low Nibble (Lower 4 bits)
			if (pixel < totalPixels) {
				byte lowNibble = (byte)(packedByte & 0x0F);
				pixels[pixel] = new Color32(
					palleteTable[palletSet, 0, lowNibble],
					palleteTable[palletSet, 1, lowNibble],
					palleteTable[palletSet, 2, lowNibble],
					255
				);
				pixel++;
			}
		}
	}
}
