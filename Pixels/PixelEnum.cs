using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Pixels
{
    public enum FileType : uint
    {
        None = 0,

        Byte    = 0x_0000_0001,

        Int16   = 0x_0000_0002,  //for IGXL 0
        Int24   = 0x_0000_0003,
        Int32   = 0x_0000_0004,  //for IGXL 1
        Int64   = 0x_0000_0006,
        UInt16  = 0x_0000_0102,
        UInt24  = 0x_0000_0103,
        UInt32  = 0x_0000_0104,
        UInt64  = 0x_0000_0106,
        Single  = 0x_0000_0204, //for IGXL 2
        Double  = 0x_0000_0208,

        Int16E  = 0x_0000_1302,
        Int24E  = 0x_0000_1303,
        Int32E  = 0x_0000_1304,
        Int64E  = 0x_0000_1308,
        UInt16E = 0x_0000_1402,
        UInt24E = 0x_0000_1403,
        UInt32E = 0x_0000_1404,
        UInt64E = 0x_0000_1408,
        SingleE = 0x_0000_1504,
        DoubleE = 0x_0000_1508,
    }

    public enum Bayer
    {
        Mono,
        BG,
        GB,
        RG,
        GR
    }
}
