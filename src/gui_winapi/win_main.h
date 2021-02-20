#ifndef win_main_h_
#define win_main_h_

HWND WinMain_init(HINSTANCE hInst, TCHAR* wcls, TCHAR *title);
LRESULT CALLBACK WinMain_proc(HWND hWnd, UINT message, WPARAM wParam,
                              LPARAM lParam);
#endif

