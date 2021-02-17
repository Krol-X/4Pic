#include "../include.h"
#include "win_main.h"

HWND WinMain_init(HINSTANCE hInst, TCHAR* wcls, TCHAR *title) {
    HWND hWnd = CreateWindow(wcls, title,
                             WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, 0,
                             640, 480, NULL, NULL, hInst, NULL);
    if (hWnd) {
//        if (!InitListView(hWnd))
//            return NULL;
//        if (!InitStatusBar(hWnd))
//            return NULL;
        ShowWindow(hWnd, SW_SHOW);
        UpdateWindow(hWnd);
//      RefreshWindow(hWnd);
        return hWnd;
    } else
        return NULL;
}

LRESULT CALLBACK WinMain_proc(HWND hWnd, UINT message, WPARAM wParam,
                              LPARAM lParam) {
	UINT wmId = LOWORD(wParam);
	
	switch (message) {
        case WM_CREATE:
            break;
        case WM_COMMAND:
            switch (wmId) {
                case IDM_QUIT:
                    DestroyWindow(hWnd);
                    break;
            }
            break;
        case WM_NOTIFY:
            break;
        case WM_SIZE:
            break;
        case WM_DESTROY:
            PostQuitMessage(0);
            break;
    }
    return DefWindowProc(hWnd, message, wParam, lParam);
}

