#include "include.h"

HBITMAP image;
BITMAP bmp;
bool flag_redraw;

#ifdef USE_WINAPI
#  include "main_winapi.h"
#else
#  error "Only USE_WINAPI now supported!"
#endif

