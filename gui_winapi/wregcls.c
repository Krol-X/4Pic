#include "../include.h"
#include "wregcls.h"

bool win_regclass(HINSTANCE hInst, WNDCLASSEX *wc) {
	wc->hInstance = hInst;

	if (wc->hIcon) wc->hIcon = LoadIcon(hInst, (LPTSTR)wc->hIcon);
	if (wc->hCursor) wc->hCursor = LoadCursor(hInst, (LPTSTR)wc->hCursor);
	if (wc->lpszMenuName) wc->lpszMenuName = MAKEINTRESOURCE(wc->lpszMenuName);
	LPCSTR name = "";
	if (!wc->lpszClassName)	wc->lpszMenuName = name;
	if (wc->hIconSm) wc->hIconSm = LoadImage(hInst, (LPTSTR)wc->hIconSm,
		                               IMAGE_ICON, 16, 16, 0);

	return RegisterClassEx(wc) != 0;
}

