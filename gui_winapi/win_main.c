#include "../include.h"
#include "win_main.h"

HDC hdcScreenCompat; // temp

HWND WinMain_init(HINSTANCE hInst, TCHAR* wcls, TCHAR *title) {
    HWND hWnd = CreateWindow(wcls, title,
                             WS_OVERLAPPEDWINDOW,
                             CW_USEDEFAULT, 0,
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
    static HDC hdcWin;  // window DC

    static bool flag_scrool;
    static bool flag_resize;

    static int xMinScroll;       // minimum horizontal scroll value
    static int xCurrentScroll;   // current horizontal scroll value
    static int xMaxScroll;       // maximum horizontal scroll value

    static int yMinScroll;       // minimum vertical scroll value
    static int yCurrentScroll;   // current vertical scroll value
    static int yMaxScroll;       // maximum vertical scroll value

    HDC hdc;
    PAINTSTRUCT ps;
    SCROLLINFO si;

    switch (message) {
        case WM_CREATE: {
            // Initialize flags
            flag_redraw = false;
            flag_scrool = false;
            flag_resize = false;

            // Initialize the horizontal scrolling variables.
            xMinScroll = 0;
            xCurrentScroll = 0;
            xMaxScroll = 0;

            // Initialize the vertical scrolling variables.
            yMinScroll = 0;
            yCurrentScroll = 0;
            yMaxScroll = 0;

            // Working???
            si.cbSize = sizeof(si);
            si.fMask  = SIF_RANGE | SIF_PAGE | SIF_POS;
            break;
        }

        case WM_DESTROY: {
            PostQuitMessage(0);
            return 0;
        }

        case WM_SIZE: {
            int xNewSize, yNewSize;
            xNewSize = LOWORD(lParam);
            yNewSize = HIWORD(lParam);
            break;

            if (flag_redraw) flag_resize = TRUE;

            // The horizontal scrolling range is defined by
            // (bitmap_width) - (client_width). The current horizontal
            // scroll value remains within the horizontal scrolling range.
            // bmp.bmWidth?
            xMaxScroll = max(bmp.bmWidth-xNewSize, 0);
            xCurrentScroll = min(xCurrentScroll, xMaxScroll);
            si.nMin   = xMinScroll;
            si.nMax   = bmp.bmWidth;
            si.nPage  = xNewSize;
            si.nPos   = xCurrentScroll;
            SetScrollInfo(hWnd, SB_HORZ, &si, TRUE);

            // The vertical scrolling range is defined by
            // (bitmap_height) - (client_height). The current vertical
            // scroll value remains within the vertical scrolling range.
            yMaxScroll = max(bmp.bmHeight - yNewSize, 0);
            yCurrentScroll = min(yCurrentScroll, yMaxScroll);
            si.nMin   = yMinScroll;
            si.nMax   = bmp.bmHeight;
            si.nPage  = yNewSize;
            si.nPos   = yCurrentScroll;
            SetScrollInfo(hWnd, SB_VERT, &si, TRUE);
            break;
        }

        case WM_PAINT: {
            hdc = BeginPaint(hWnd, &ps);

            if (flag_resize) {
                BitBlt(ps.hdc, 0, 0,
                       bmp.bmWidth, bmp.bmHeight,
                       hdcScreenCompat,
                       xCurrentScroll, yCurrentScroll,
                       SRCCOPY);
                flag_resize = false;
            }

            if (flag_scrool) {
                PRECT prect = &ps.rcPaint;
                BitBlt(ps.hdc,
                       prect->left, prect->top,
                       (prect->right - prect->left),
                       (prect->bottom - prect->top),
                       hdcScreenCompat,
                       prect->left + xCurrentScroll,
                       prect->top + yCurrentScroll,
                       SRCCOPY);
                flag_scrool = false;
            }

            EndPaint(hWnd, &ps);
            break;
        }

        case WM_HSCROLL: {
            int xNewPos;     // new position
            int yDelta = 0;

            WORD sb = LOWORD(wParam);

            if (sb == SB_PAGEUP) {
                // User clicked the scroll bar shaft left of the scroll box
                xNewPos = xCurrentScroll - 50;
            } else if (sb == SB_PAGEDOWN) {
                // User clicked the scroll bar shaft right of the scroll box
                xNewPos = xCurrentScroll + 50;
            } else if (sb == SB_LINEUP) {
                // User clicked the left arrow
                xNewPos = xCurrentScroll - 5;
            } else if (sb == SB_LINEDOWN) {
                // User clicked the right arrow
                xNewPos = xCurrentScroll + 5;
            } else if (sb == SB_THUMBPOSITION) {
                // User dragged the scroll box
                xNewPos = HIWORD(wParam);
            } else {
                xNewPos = xCurrentScroll;
            }

            // New position must be between 0 and the screen width.
            xNewPos = max(0, xNewPos);
            xNewPos = min(xMaxScroll, xNewPos);

            // If the current position does not change, do not scroll.
            if (xNewPos == xCurrentScroll)
                break;
            else
                flag_scrool = true;

            // Determine the amount scrolled (in pixels).
            int xDelta = xNewPos - xCurrentScroll;
            // Reset the current scroll position.
            xCurrentScroll = xNewPos;

            ScrollWindowEx(hWnd, -xDelta, -yDelta, (CONST RECT *) NULL,
                           (CONST RECT *) NULL, (HRGN) NULL, (PRECT) NULL,
                           SW_INVALIDATE);
            UpdateWindow(hWnd);

            // Reset the scroll bar.
            si.cbSize = sizeof(si);
            si.fMask  = SIF_POS;
            si.nPos   = xCurrentScroll;
            SetScrollInfo(hWnd, SB_HORZ, &si, TRUE);
            break;
        }

        case WM_VSCROLL: {
            int yNewPos;     // new position
            int xDelta = 0;

            WORD sb = LOWORD(wParam);

            if (sb == SB_PAGEUP) {
                // User clicked the scroll bar shaft above the scroll box
                yNewPos = yCurrentScroll - 50;
            } else if (sb == SB_PAGEDOWN) {
                // User clicked the scroll bar shaft below the scroll box
                yNewPos = yCurrentScroll + 50;
            } else if (sb == SB_LINEUP) {
                // User clicked the top arrow
                yNewPos = yCurrentScroll - 5;
            } else if (sb == SB_LINEDOWN) {
                // User clicked the bottom arrow
                yNewPos = yCurrentScroll + 5;
            } else if (sb == SB_THUMBPOSITION) {
                // User dragged the scroll box
                yNewPos = HIWORD(wParam);
            } else {
                yNewPos = yCurrentScroll;
            }

            // New position must be between 0 and the screen width.
            yNewPos = max(0, yNewPos);
            yNewPos = min(yMaxScroll, yNewPos);

            // If the current position does not change, do not scroll.
            if (yNewPos == yCurrentScroll)
                break;
            else
                flag_scrool = true;

            // Determine the amount scrolled (in pixels).
            int yDelta = yNewPos - yCurrentScroll;
            // Reset the current scroll position.
            yCurrentScroll = yNewPos;

            ScrollWindowEx(hWnd, -xDelta, -yDelta, (CONST RECT *) NULL,
                           (CONST RECT *) NULL, (HRGN) NULL, (PRECT) NULL,
                           SW_INVALIDATE);
            UpdateWindow(hWnd);

            // Reset the scroll bar.
            si.cbSize = sizeof(si);
            si.fMask  = SIF_POS;
            si.nPos   = yCurrentScroll;
            SetScrollInfo(hWnd, SB_VERT, &si, TRUE);
            break;
        }

        case WM_COMMAND: {
            switch (LOWORD(wParam)) {
                case IDM_QUIT:
                    DestroyWindow(hWnd);
                    break;

                case IDM_OPEN:

                    break;

                case IDM_SAVEAS:

                    break;



                case IDM_RUN:

                    break;

                case IDM_RELOAD_DEFAULT:
                    break;
            }
            break;
        }
    }
}
