

## Flow

File -[ Load ]-> Raw -[ Script ]-> Src -[ config ]-> WriteableBitmap

- config
  - bitshift / depth
  - offset
  - color / bayer
  - stagger
  -

  ##

  - IO
    - stingの16進 10進

	#

## int -> byteへの書き込み

```CS
public unsafe void M(int* dst) {
    *dst &= 5;
    *dst <<= 16;
    *dst |= 3;
    *dst <<= 8;
    *dst |= 13;
    dst++;
}
public unsafe void M1(byte* dst) {
    *dst++ = 5;
    *dst++ = 3;
    *dst++ = 13;
}
```
```asm
//JIT ASM
C.M(Int32*)
    L0000: and dword [edx], 0x5
    L0003: shl dword [edx], 0x10
    L0006: or dword [edx], 0x3
    L0009: shl dword [edx], 0x8
    L000c: or dword [edx], 0xd
    L000f: ret

C.M1(Byte*)
    L0000: push esi
    L0001: mov esi, edx
    L0003: inc edx
    L0004: mov byte [esi], 0x5
    L0007: mov esi, edx
    L0009: inc edx
    L000a: mov byte [esi], 0x3
    L000d: mov byte [edx], 0xd
    L0010: pop esi
    L0011: ret
```

## LUTの計算



## float -> byteでのオーバーフローチェック

## int -> floatのマトリックス計算

```cs
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static void _Matrix(ref (float B, float G, float R) bayer, ref Matrix matrix)
{
    var _B = matrix.M11 * bayer.B + matrix.M12 * bayer.G + matrix.M13 * bayer.R;
    var _G = matrix.M21 * bayer.B + matrix.M22 * bayer.G + matrix.M23 * bayer.R;
    var _R = matrix.M31 * bayer.B + matrix.M32 * bayer.G + matrix.M33 * bayer.R;
    bayer = (_B, _G, _R);
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static (float B, float G, float R) _Matrix((float B, float G, float R) bayer, Matrix matrix)
{
    var _B = matrix.M11 * bayer.B + matrix.M12 * bayer.G + matrix.M13 * bayer.R;
    var _G = matrix.M21 * bayer.B + matrix.M22 * bayer.G + matrix.M23 * bayer.R;
    var _R = matrix.M31 * bayer.B + matrix.M32 * bayer.G + matrix.M33 * bayer.R;
    return (_B, _G, _R);
}
```

## ループブロックの再構成
