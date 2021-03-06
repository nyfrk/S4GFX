﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S4GFX.FileReader
{
	class DilFileReader : FileReaderBase
	{
		Int32[] offsetTable;

		public int ReverseLookupOffset(int gilIndex) {
			int offset = gilIndex * 4 + HeaderSize;

			int lastGood = 0;

			for (int i = 0; i < offsetTable.Length; i++) {
				if (offsetTable[i] == 0)
					continue;

				if(offsetTable[i] > offset) {
					//Console.WriteLine($"DIL {gilIndex} --> {lastGood}");
					return lastGood;
				}

				lastGood = i;
			}

			//Console.WriteLine($"Unable to find offset gilIndex: {gilIndex}");
			return lastGood;
		}

		public DilFileReader(BinaryReader reader) {
			ReadResource(reader);

			int imageCount = GetImageCount(reader);
			//Console.WriteLine($"Image count: {imageCount}");

			offsetTable = new Int32[imageCount];

			for(int i = 0; i < imageCount; i++) {
				offsetTable[i] = reader.ReadInt32();
			}
		}
	}
}
