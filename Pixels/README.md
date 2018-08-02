    // unsafe
    // {
    //     fixed (byte* pin = value.AsSpan().Slice(startindex, 3))
    //     {
    //        return *(Int24*)pin;
    //     }
    // }
    // だと1/4のパフォーマンス