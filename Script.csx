src.Map(dst, (x, y, s, d) => {
    d[x, y] = s[x, y] >> 8;
}, subPlane: "Full");