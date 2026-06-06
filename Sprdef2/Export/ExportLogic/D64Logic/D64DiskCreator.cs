using System;
using System.IO;
using System.Text;

namespace Sprdef2.Export.ExportLogic.D64Logic;

public class D64DiskCreator
{
    private const int D64ImageSize = 174848; // Standardstorlek för en 1541 D64-fil (35 spår)

    /// <summary>
    /// Skapar en ny D64-fil och lägger till en PRG-fil i den.
    /// </summary>
    /// <param name="d64Path">Sökväg till den nya d64-filen som ska skapas.</param>
    /// <param name="prgFileName">Namnet filen ska ha INNE i C64-katalogen (max 16 tecken).</param>
    /// <param name="prgData">Rå prg-data (inklusive de 2 byte header).</param>
    /// <param name="diskName">Namnet på själva disketten (max 16 tecken).</param>
    public static void CreateD64WithPrg(string d64Path, string prgFileName, byte[] prgData, string diskName = "SPRDEF2 DISK")
    {
        var d64Buffer = new byte[D64ImageSize];

        // 1. Formatera disken i minnet (fyll BAM och katalogspår med standardvärden)
        FormatD64Structure(d64Buffer, diskName);

        // 2. Skriv PRG-filens data till lediga sektorer
        // På en tom disk börjar vi lämpligen på Spår 1, Sektor 0
        const int startTrack = 1;
        const int startSector = 0;
        var fileSectorCount = WriteFileData(d64Buffer, prgData, startTrack, startSector);

        // 3. Lägg till filen i katalogen (Spår 18, Sektor 1)
        AddFileToDirectory(d64Buffer, prgFileName, startTrack, startSector, fileSectorCount);

        // 4. Spara hela D64-avbildningen till disk
        File.WriteAllBytes(d64Path, d64Buffer);
    }

    private static void FormatD64Structure(byte[] buffer, string diskName)
    {
        // Spår 18, Sektor 0 är BAM (Block Availability Map)
        var bamOffset = GetSectorOffset(18, 0);

        // Standard BAM-huvud för 1541
        buffer[bamOffset + 0x00] = 18; // Internt länkspår till katalog (Spår 18)
        buffer[bamOffset + 0x01] = 1;  // Intern länksektor till katalog (Sektor 1)
        buffer[bamOffset + 0x02] = 0x41; // DOS-version 'A'

        // Sätt diskens namn (Offset 0x90 i BAM)
        var nameBytes = Encoding.ASCII.GetBytes(diskName.PadRight(16, (char)0xA0)); // 0xA0 är skiftat mellanslag i C64
        Array.Copy(nameBytes, 0, buffer, bamOffset + 0x90, 16);

        // DOS ID och bitar (fyller med standardvärden '2A')
        buffer[bamOffset + 0xA2] = 0x32;
        buffer[bamOffset + 0xA3] = 0x41;

        // Fyll i BAM för alla 35 spår (Sätt alla sektorer som lediga)
        // Detta är en förenkling, men tillräcklig för en ny/tom disk i emulatorer
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
                // Sista sektorn!
                buffer[sectorOffset + 0] = 0x00; // 0x00 i spårlänk betyder "sista blocket"
                buffer[sectorOffset + 1] = (byte)(remainingData + 1); // Hur många bytes som användes i sektorn (+1 för länkpekaren)
                Array.Copy(prgData, dataOffset, buffer, sectorOffset + 2, remainingData);
                break;
            }

            // Fler sektorer följer. Vi lägger nästa block på nästa sektor (enkel interleave +1)
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
        // Vi lägger filen i den första posten i Spår 18, Sektor 1 (Katalogen)
        var dirOffset = GetSectorOffset(18, 1);

        // Gör filnamnet exakt 16 bytes (fyll ut med C64-padding 0xA0)
        var nameBytes = Encoding.ASCII.GetBytes(fileName.ToUpper().PadRight(16, (char)0xA0));

        // File Type: 0x82 = PRG (Bit 7 satt betyder "Closed/Valid file", Bit 1-0: 2 = PRG)
        buffer[dirOffset + 0x02] = 0x82;

        // Startspår och startsektor för filens data
        buffer[dirOffset + 0x03] = (byte)fileStartTrack;
        buffer[dirOffset + 0x04] = (byte)fileStartSector;

        // Kopiera in filnamnet
        Array.Copy(nameBytes, 0, buffer, dirOffset + 0x05, 16);

        // Storlek i sektorer (Little-Endian)
        buffer[dirOffset + 0x1E] = (byte)(sectorCount & 0xFF);
        buffer[dirOffset + 0x1F] = (byte)((sectorCount >> 8) & 0xFF);
    }

    // Hjälpmetod för att räkna ut var på disk-imagen ett spår/sektor ligger fysiskt
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

    // 1541-disketter har olika antal sektorer beroende på hur nära mitten spåret är (Density zones)
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
}