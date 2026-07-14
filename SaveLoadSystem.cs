using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveLoadSystem : MonoBehaviour{
	
	private string savefile, savefolder;
	private string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
	private string[] binFiles;
	private int fileCount;
	
	[SerializeField]private Stats stats;
	[SerializeField]private bitpacker Bitpacker;
	
	void Awake() {
		//put into function that checks if it an alt pathway has been loaded in and bases off that what to set pathway as on start
		savefolder = Path.Combine(myDocuments,"Velvetsoft\\ProjectTwilight\\Saves");

		if (!Directory.Exists(savefolder))
            Directory.CreateDirectory(savefolder);
		
		binFiles = Directory.GetFiles(savefolder, "*.vsoft");
		fileCount = binFiles.Length;
		
		createNewSave(true);
    }
	
	private void createNewSave(bool awakerun) {
		if(awakerun && fileCount == 0) {
			savefile = Path.Combine(savefolder, "Save1.vsoft");
			using (File.Create(savefile)) {};
		} else if(!awakerun) {
			fileCount++;
			savefile = Path.Combine(savefolder, "Save" + fileCount + ".vsoft");
			using (File.Create(savefile)) {};
		}
	}
	
	public void saveGame(ushort eventID, string filePath) {
		saveEvents(eventID, filePath);
		
		using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Write)) {
			fs.Seek(16, SeekOrigin.Begin); // Skip 16 bytes (8 for header/length, 8 for flags)
			using (BinaryWriter writer = new BinaryWriter(fs)) {
				writer.Write(stats.funds);
				writer.Write(stats.income);
			
				Bitpacker.savepacker(writer, stats.elegance, stats.grace);
				Bitpacker.savepacker(writer, stats.glamor, stats.negotiation);
				writer.Write(stats.reputation);
			
				Bitpacker.savepacker(writer, stats.agality, stats.athletics);
				Bitpacker.savepacker(writer, stats.strength, stats.fatigue);
			
				Bitpacker.savepacker(writer, stats.craftsmanship, stats.stategy);
				Bitpacker.savepacker(writer, stats.science, stats.history);
			
				Bitpacker.savepacker(writer, stats.math, stats.morality);
				Bitpacker.savepacker(writer, stats.theology, stats.sin);
				Bitpacker.savepacker(writer, stats.peity, 0);
			}
			
		}
		
	}
	
	public void saveEvents(ushort eventID, string filePath) {
		FileInfo fileInfo = new FileInfo(filePath);
		if (fileInfo.Length == 0) {
			using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Write)) {
				using (BinaryWriter writer = new BinaryWriter(fs)) {
					writer.Write(0x56454C534F4654D3UL/*this is VELSOFT♥ for velvetsoft in the commodore PET unshifted PETSCII, this is a header for da game and is 8 bytes*/);
					writer.Write(new byte[8]);
				}
			}
		} else {
			using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Write)) {
				fs.Seek(8 + (eventID / 8), SeekOrigin.Begin);
				int bitIndex = eventID % 8;
        
				// Read existing byte, flip bit, write back
				int b = fs.ReadByte();
				fs.Seek(-1, SeekOrigin.Current); // Go back one byte
				fs.WriteByte((byte)(b | (1 << bitIndex)));
			}
		}
	}
	
	public void loadGame() {
		
	}
	
	public void readEvents() {
		
	}
}
