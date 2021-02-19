#ifndef wregcls_h_
#define wregcls_h_

#define WCLS_main_style CS_HREDRAW | CS_VREDRAW
#define WCLS_main_proc MyBitmapWindowProc
#define WCLS_main_icon 0
#define WCLS_main_iconsm 0
#define WCLS_main_cursor IDC_ARROW
#define WCLS_main_bgcol (COLOR_WINDOW + 1)
#define WCLS_main_menu IDM_MAINMENU
#define WCLS_main_name 0

extern WNDCLASSEX WCLS_main;

bool win_regclass(HINSTANCE hInst, WNDCLASSEX *wc, TCHAR *wcname);

#endif

