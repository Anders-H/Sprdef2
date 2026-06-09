using System;
using System.IO;
using System.Text;

namespace Sprdef2.Export.ExportLogic.D64Logic;

public class D64DiskCreator
{
    private const int D64ImageSize = 174848; // Standardstorlek för en 1541 D64-fil (35 spår)

    public static void CreateD64WithPrg(string d64Path, string prgFileName, byte[] prgData, string diskName = "SPRDEF2 DISK")
    {
        var d64Buffer = new byte[D64ImageSize];
        FormatD64Structure(d64Buffer, diskName);
        const int startTrack = 1;
        const int startSector = 0;
        var fileSectorCount = WriteFileData(d64Buffer, prgData, startTrack, startSector);
        AddFileToDirectory(d64Buffer, prgFileName, startTrack, startSector, fileSectorCount);
        File.WriteAllBytes(d64Path, d64Buffer);
    }

    private static void FormatD64Structure(byte[] buffer, string diskName)
    {
        var bamOffset = GetSectorOffset(18, 0);
        buffer[bamOffset + 0x00] = 18; // Internt länkspår till katalog (Spår 18)
        buffer[bamOffset + 0x01] = 1;  // Intern länksektor till katalog (Sektor 1)
        buffer[bamOffset + 0x02] = 0x41; // DOS-version 'A'
        var nameBytes = ToPetsciiPadded(diskName, 16);
        Array.Copy(nameBytes, 0, buffer, bamOffset + 0x90, 16);
        buffer[bamOffset + 0xA0] = 0xA0;  // Skiftat mellanslag (fast)
        buffer[bamOffset + 0xA1] = 0xA0;  // Skiftat mellanslag (fast)
        buffer[bamOffset + 0xA2] = 0x31;  // Disk-ID byte 1, t.ex. '1'
        buffer[bamOffset + 0xA3] = 0x41;  // Disk-ID byte 2, t.ex. 'A'
        buffer[bamOffset + 0xA4] = 0xA0;  // Skiftat mellanslag (fast)
        buffer[bamOffset + 0xA5] = 0x32;  // DOS-typ '2'
        buffer[bamOffset + 0xA6] = 0x41;  // DOS-typ 'A'

        for (var track = 1; track <= 35; track++)
        {
            var entryOffset = bamOffset + 4 + (track - 1) * 4;
            var sectorsInTrack = GetSectorsInTrack(track);
            buffer[entryOffset] = (byte)sectorsInTrack; // Antal lediga sektorer
            buffer[entryOffset + 1] = 0xFF; // Bitmask för lediga sektorer
            buffer[entryOffset + 2] = 0xFF;
            buffer[entryOffset + 3] = 0xFF;
        }
    }

    private static int WriteFileData(byte[] buffer, byte[] prgData, int startTrack, int startSector)
    {
        var currentTrack = startTrack;
        var currentSector = startSector;
        var dataOffset = 0;
        var sectorsUsed = 0;

        while (dataOffset < prgData.Length)
        {
            sectorsUsed++;
            var sectorOffset = GetSectorOffset(currentTrack, currentSector);
            var remainingData = prgData.Length - dataOffset;

            if (remainingData <= 254)
            {
                buffer[sectorOffset + 0] = 0x00; // 0x00 i spårlänk betyder "sista blocket"
                buffer[sectorOffset + 1] = (byte)(remainingData + 1); // Hur många bytes som användes i sektorn (+1 för länkpekaren)
                Array.Copy(prgData, dataOffset, buffer, sectorOffset + 2, remainingData);
                break;
            }

            var nextTrack = currentTrack;
            var nextSector = currentSector + 1;

            if (nextSector >= GetSectorsInTrack(currentTrack))
            {
                nextSector = 0;
                nextTrack++;
            }

            buffer[sectorOffset + 0] = (byte)nextTrack;
            buffer[sectorOffset + 1] = (byte)nextSector;
            Array.Copy(prgData, dataOffset, buffer, sectorOffset + 2, 254);
            dataOffset += 254;
            currentTrack = nextTrack;
            currentSector = nextSector;
        }

        return sectorsUsed;
    }

    private static void AddFileToDirectory(byte[] buffer, string fileName, int fileStartTrack, int fileStartSector, int sectorCount)
    {
        var dirOffset = GetSectorOffset(18, 1);
        var nameBytes = ToPetsciiPadded(fileName, 16);
        buffer[dirOffset + 0x02] = 0x82;
        buffer[dirOffset + 0x03] = (byte)fileStartTrack;
        buffer[dirOffset + 0x04] = (byte)fileStartSector;
        Array.Copy(nameBytes, 0, buffer, dirOffset + 0x05, 16);
        buffer[dirOffset + 0x1E] = (byte)(sectorCount & 0xFF);
        buffer[dirOffset + 0x1F] = (byte)((sectorCount >> 8) & 0xFF);
    }

    private static int GetSectorOffset(int track, int sector)
    {
        var sectorIndex = 0;

        for (var t = 1; t < track; t++)
        {
            sectorIndex += GetSectorsInTrack(t);
        }

        sectorIndex += sector;
        return sectorIndex * 256;
    }

    private static int GetSectorsInTrack(int track)
    {
        if (track is >= 1 and <= 17)
            return 21;

        if (track is >= 18 and <= 24)
            return 19;

        if (track is >= 25 and <= 30)
            return 18;

        if (track is >= 31 and <= 35)
            return 17;

        throw new ArgumentOutOfRangeException(nameof(track));
    }

    private static byte[] ToPetsciiPadded(string text, int length)
    {
        var result = new byte[length];
        // Fyll med 0xA0 (skiftat mellanslag) från start
        for (int i = 0; i < length; i++)
            result[i] = 0xA0;

        for (int i = 0; i < Math.Min(text.Length, length); i++)
        {
            char c = char.ToUpper(text[i]);
            // Enkel ASCII->PETSCII-mappning för vanliga tecken
            result[i] = (byte)c;
        }

        return result;
    }
}