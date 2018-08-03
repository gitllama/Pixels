
## がちがちの最適化の話

### Base

Line -> Col順のループの際、エリア限定と.Net Coreでの最適化ロードマップを想定してSpanでの受け取りとしている。  
ここから順次オーバーヘッドを削減していく。

```cs
public static void OpenCV(int[] src, byte[] dst, int w,int h,Matrix3x3 matrix)
{
    var ary = Array.ConvertAll(src, (i) => (UInt16)i);
    var CV_16UC3 = new UInt16[h * w * 3];
    float[] mat = new float[] {
        matrix.M11, matrix.M12, matrix.M13,
        matrix.M21, matrix.M22, matrix.M23,
        matrix.M31, matrix.M32, matrix.M33
    };

    using (var mat16UC1 = new Mat(h, w, MatType.CV_16U, ary))
    using (var mat16UC3 = new Mat(h, w, MatType.CV_16UC3, CV_16UC3))
    using (var matMatrix = new Mat(3, 3, MatType.CV_32FC1, mat))
    using (var matDst = new Mat(h, w, MatType.CV_8UC3, dst))
    {
        Cv2.CvtColor(mat16UC1, mat16UC3, ColorConversionCodes.BayerBG2BGR);
        Cv2.Transform(mat16UC3, mat16UC3, matMatrix);
        for(var i = 0; i < CV_16UC3.Length; i++)
        {
            var val = CV_16UC3[i];
            dst[i] = val > Byte.MaxValue ? Byte.MaxValue : val < Byte.MinValue ? Byte.MinValue : (byte)val;
        }

    }

}
```

```cs
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static void ColDefault(Span<int> src, Span<byte> dst, Matrix matrix)
{
    for (var i = 1; i < src.Length-1; i++)
    {
        var buf = (float)src[i];
        var R = buf;
        var G = buf;
        var B = buf;
        var _B = matrix.M11 * B + matrix.M12 * G + matrix.M13 * R;
        var _G = matrix.M21 * B + matrix.M22 * G + matrix.M23 * R;
        var _R = matrix.M31 * B + matrix.M32 * G + matrix.M33 * R;
        dst[i * 3 + 0] = _B > Byte.MaxValue ? Byte.MaxValue : _B < Byte.MinValue ? Byte.MinValue : (byte)_B;
        dst[i * 3 + 1] = _G > Byte.MaxValue ? Byte.MaxValue : _G < Byte.MinValue ? Byte.MinValue : (byte)_G;
        dst[i * 3 + 2] = _R > Byte.MaxValue ? Byte.MaxValue : _R < Byte.MinValue ? Byte.MinValue : (byte)_R;
    }
}
```
### 最適化

####

```cs
dst[i * 3 + 0] = _B > Byte.MaxValue ? Byte.MaxValue : _B < Byte.MinValue ? Byte.MinValue : (byte)_B;
dst[i * 3 + 1] = _G > Byte.MaxValue ? Byte.MaxValue : _G < Byte.MinValue ? Byte.MinValue : (byte)_G;
dst[i * 3 + 2] = _R > Byte.MaxValue ? Byte.MaxValue : _R < Byte.MinValue ? Byte.MinValue : (byte)_R;

↓

dst[i * 3 + 0] = Clamp(_B);
dst[i * 3 + 1] = Clamp(_G);
dst[i * 3 + 2] = Clamp(_R);

public static byte Clamp(float val) => val > Byte.MaxValue ? Byte.MaxValue : val < Byte.MinValue ? Byte.MinValue : (byte)val;

```

|  Method |     Mean |     Error |    StdDev |
|-------- |---------:|----------:|----------:|
| Default | 32.05 ms | 0.0911 ms | 0.0760 ms |
|    Test | 29.69 ms | 0.1666 ms | 0.1559 ms |

####

```cs
var R = buf;
var G = buf;
var B = buf;
var _B = matrix.M11 * B + matrix.M12 * G + matrix.M13 * R;
var _G = matrix.M21 * B + matrix.M22 * G + matrix.M23 * R;
var _R = matrix.M31 * B + matrix.M32 * G + matrix.M33 * R;

↓

var bayer = (buf, buf, buf);
var outBayer = Matrix(bayer, matrix);

↓

var bayer = (buf, buf, buf);
var outBayer = Matrix(in bayer,in matrix);

```

|  Method |     Mean |     Error |    StdDev |
|-------- |---------:|----------:|----------:|
| Default | 31.99 ms | 0.1929 ms | 0.1804 ms |
|    Test | 99.53 ms | 0.4293 ms | 0.4015 ms |
|    Test | 56.58 ms | 0.2042 ms | 0.1910 ms |


####

```cs
dst[i * 3 + 0] = _B > Byte.MaxValue ? Byte.MaxValue : _B < Byte.MinValue ? Byte.MinValue : (byte)_B;
dst[i * 3 + 1] = _G > Byte.MaxValue ? Byte.MaxValue : _G < Byte.MinValue ? Byte.MinValue : (byte)_G;
dst[i * 3 + 2] = _R > Byte.MaxValue ? Byte.MaxValue : _R < Byte.MinValue ? Byte.MinValue : (byte)_R;

↓

dst[i * 3 + 0] = Clamp(_B);
dst[i * 3 + 1] = Clamp(_G);
dst[i * 3 + 2] = Clamp(_R);
```

|  Method |     Mean |     Error |    StdDev |
|-------- |---------:|----------:|----------:|
| Default | 28.66 ms | 0.0734 ms | 0.0687 ms |
|    Test | 29.19 ms | 0.0853 ms | 0.0798 ms |




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
