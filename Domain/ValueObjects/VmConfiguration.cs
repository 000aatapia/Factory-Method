using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class VmConfiguration
    {
        public int CpuCores { get; private set; }
        public int MemoryGb { get; private set; }
        public int DiskGb { get; private set; }
        public string RegionOrZone { get; private set; }
        public string Image { get; private set; }

        public VmConfiguration(int cpuCores, int memoryGb, int diskGb, string regionOrZone, string image)
        {
            if (cpuCores <= 0) throw new ArgumentException("CPU cores must be positive");
            if (memoryGb <= 0) throw new ArgumentException("Memory must be positive");
            if (diskGb <= 0) throw new ArgumentException("Disk must be positive");
            if (string.IsNullOrWhiteSpace(regionOrZone)) throw new ArgumentException("Region or zone required");

            CpuCores = cpuCores;
            MemoryGb = memoryGb;
            DiskGb = diskGb;
            RegionOrZone = regionOrZone;
            Image = image;
        }

        public override string ToString()
            => $"{CpuCores} vCPU, {MemoryGb}GB RAM, {DiskGb}GB Disk, {RegionOrZone}, Image: {Image}";
    }
}
