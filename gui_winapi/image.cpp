#include "../include.h"
#include <gdiplus.h>
#include "image.h"

#pragma comment(lib, "gdiplus.lib") // MSVS

#ifdef __cplusplus
extern "C" {
#endif

HBITMAP image_load(LPTSTR path) {
	Gdiplus::GdiplusStartupInput gdiplusStartupInput;
    ULONG_PTR gdiplusToken;
    Gdiplus::GdiplusStartup(&gdiplusToken, &gdiplusStartupInput, NULL);
    
    #ifdef _UNICODE
    WCHAR *_path = (WCHAR *) path;
    #else
    int len = strlen(path);
    WCHAR *_path = new WCHAR[len << 1];
    mbstowcs(_path, path, len);
    #endif
    
    Gdiplus::Image* image = Gdiplus::Image::FromFile(_path);
    
    
    delete image; image = 0;
    Gdiplus::GdiplusShutdown(gdiplusToken);
}

#ifdef __cplusplus
}
#endif

