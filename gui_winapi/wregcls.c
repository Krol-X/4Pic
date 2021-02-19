#include "../include.h"
#include "wregcls.h"

WNDCLASSEX WCLS_main = {
    .cbSize        = sizeof(WNDCLASSEX),
    .style         = WCLS_main_style,
    .lpfnWndProc   = (WNDPROC) WinMain_proc,
    .cbClsExtra    = 0,
    .cbWndExtra    = 0,
    .hInstance     = 0,
    .hIcon         = (HICON) WCLS_main_icon,
    .hCursor       = (HCURSOR) WCLS_main_cursor,
    .hbrBackground = (HBRUSH) WCLS_main_bgcol,
    .lpszMenuName  = (LPTSTR) WCLS_main_menu,
    .lpszClassName = (LPTSTR) WCLS_main_name,
    .hIconSm       = (HICON) WCLS_main_iconsm
};

extern TCHAR szError[MAX_LOADSTRING];

bool win_regclass(HINSTANCE hInst, WNDCLASSEX *wc, TCHAR *wcname) {
    wc->hInstance = hInst;

    if (wc->hIcon)
        wc->hIcon = LoadIcon(hInst, (LPTSTR)wc->hIcon);
    if (wc->hCursor)
        wc->hCursor = LoadCursor(NULL, (LPTSTR)wc->hCursor);
    if (wc->lpszMenuName)
        wc->lpszMenuName = MAKEINTRESOURCE(wc->lpszMenuName);
    LPTSTR name = TEXT("");
    if (!wc->lpszClassName)
        wc->lpszClassName = wcname;
    if (wc->hIconSm)
        wc->hIconSm = LoadImage(hInst, (LPTSTR)wc->hIconSm,
                                IMAGE_ICON, 16, 16, 0);
    return RegisterClassEx(wc);
}

