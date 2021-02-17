#ifndef wregcls_h_
#define wregcls_h_

#define WCLS_main_style CS_HREDRAW | CS_VREDRAW
#define WCLS_main_proc 0
#define WCLS_main_icon 0
#define WCLS_main_iconsm 0
#define WCLS_main_cursor IDC_ARROW
#define WCLS_main_bgcol (COLOR_WINDOW + 1)
#define WCLS_main_menu 0
#define WCLS_main_name 0

WNDCLASSEX WCLS_main = {
	.cbSize        = sizeof(WNDCLASSEX),
	.style         = WCLS_main_style,
	.lpfnWndProc   = WCLS_main_proc,
	.cbClsExtra    = 0,
	.cbWndExtra    = 0,
	.hInstance     = 0,
	.hIcon         = (HICON) WCLS_main_icon,
	.hCursor       = (HCURSOR) WCLS_main_cursor,
	.hbrBackground = (HBRUSH) WCLS_main_bgcol,
	.lpszMenuName  = (LPCSTR) WCLS_main_menu,
	.lpszClassName = (LPCSTR) WCLS_main_name,
	.hIconSm       = (HICON) WCLS_main_iconsm
};

bool win_regclass(HINSTANCE hInst, WNDCLASSEX *wc);

#endif

