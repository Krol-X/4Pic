#ifndef include_h_
#define include_h_

// For MSVS
#define _CRT_SECURE_NO_WARNINGS
#define _CRT_NON_CONFORMING_SWPRINTFS


#define MAX_LOADSTRING 64

#pragma pack(4)

#include <tchar.h>
#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <stdint.h>

#ifdef USE_WINAPI
#  include <windows.h>
//#  include <commctrl.h>
#endif

#include "main.h"
#include "resource.h"
#include "tools.h"

#include "gui_winapi/wregcls.h"
#include "gui_winapi/win_main.h"

#endif

