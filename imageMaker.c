#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>
#include <stdint.h>

/*
this tool will only be for ppm, cause i dont want to do the fun act of 

 the file fomrat will be the same for many diffrent files
 so with that being said the name for the file will indicate
 what type of file it will be so, 
 first letter
 I means Image
 S means Save
 
 the second letter is what type it is, so for
 Image:
	M main menu graphics
	P puase menu graphics
	C charector graphics
		for C there will be two subsets
		V for vm char
		F for faces for main game
		C for main character
	B background graphics
	S shop graphics
	U ui graphics
	O is other graphics
	
	the last two are the number it is in the category so it would be xx
 */
	static unsigned char buffer[1036813];
	
	void GenWriteIMGFile(void) {
		//change later once i the ability to write in command prompt
		FILE *fptr = fopen("IM01.Vsoft", "wb");
		//change when i write more
		size_t written = fwrite(buffer, 1, 8, fptr);
		
	}
	
void read_ppm(const char *filename, unsigned char **pixels, int *w, int *h) {
    FILE *fp = fopen(filename, "rb");
    if (!fp) return;

    char magic[3];
    fscanf(fp, "%2s %d %d\n255\n", magic, w, h);

    *pixels = malloc((*w) * (*h) * 3);
    fread(*pixels, 1, (*w) * (*h) * 3, fp);
    fclose(fp);
}


	
	void makeBuffer(void) {
		unsigned char data[] = {0x56, 0x45, 0x4C, 0x53, 0x4F, 0x46, 0x54, 0xD3};
		memcpy(buffer, data, 8);
		
	}

	int main(void) {
		while(1) {
			
		}
		
		return 0;
	}