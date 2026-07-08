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
	
	void Awake() {
		//put into function that checks if it an alt pathway has been loaded in and bases off that what to set pathway as on start
		savefolder = Path.Combine(myDocuments,"Velvetsoft\\ProjectTwilight\\Saves");

		if (!Directory.Exists(savefolder))
            Directory.CreateDirectory(savefolder);
		
		binFiles = Directory.GetFiles(savefolder, "*.bin");
		fileCount = binFiles.Length;
		
		createNewSave(true);
    }
	
	private void createNewSave(bool awakerun) {
		if(awakerun && fileCount == 0) {
			savefile = Path.Combine(savefolder, "Save1.bin");
			using (File.Create(savefile)) {};
		} else if(!awakerun) {
			fileCount++;
			savefile = Path.Combine(savefolder, "Save" + fileCount + ".bin");
			using (File.Create(savefile)) {};
		}
	}
	
	public void saveGame() {
		
	}
	
	public void loadGame() {
		
	}
}
